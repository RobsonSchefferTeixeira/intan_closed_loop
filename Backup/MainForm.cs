/*
 * Intan Amplifier Demo for use with RHA2000-EVAL Board and RHA2000 Series Amplifier Chips
 * Copyright (c) 2010-2011 Intan Technologies, LLC  http://www.intantech.com
 * 
 * This software is provided 'as-is', without any express or implied 
 * warranty.  In no event will the authors be held liable for any damages 
 * arising from the use of this software. 
 * 
 * Permission is granted to anyone to use this software for any applications that use
 * Intan Technologies integrated circuits, and to alter it and redistribute it freely,
 * subject to the following restrictions: 
 * 
 * 1. The application must require the use of Intan Technologies integrated circuits.
 *
 * 2. The origin of this software must not be misrepresented; you must not 
 *    claim that you wrote the original software. If you use this software 
 *    in a product, an acknowledgment in the product documentation is required.
 * 
 * 3. Altered source versions must be plainly marked as such, and must not be 
 *    misrepresented as being the original software.
 * 
 * 4. This notice may not be removed or altered from any source distribution.
 * 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Media;

using FTD2XX_NET;


namespace USB_Scope
{
    /// <summary>
    /// Main window for Intan Amplifier Demo.
    /// </summary>
    public partial class MainForm : Form
    {
        private BufferedGraphicsContext myContext;
        private BufferedGraphics myBuffer;

        private bool readMode = false;

        private float[,] dataFrame = new float[16, 750];
        private UInt16[] auxFrame = new UInt16[750];
        private USBSource myUSBSource = new USBSource();

        private Queue<USBData> plotQueue = new Queue<USBData>();
        private Queue<USBData> saveQueue = new Queue<USBData>();

        private bool[] displayChannel = new bool[16];
        private int xSlowPos = 0;
        private const int XPlotOffset = 54;

        // voltage axis scales and labels
        private const int NumYScales = 10;
        private float[] YScaleFactors = new float[NumYScales] {5.24F, 2.62F, 1.048F, 0.524F, 0.262F, 0.1048F, 0.0524F, 0.0262F, 0.01048F, 0.00524F};
        private string[] YScaleText = new string[NumYScales] { "10 " + '\u03bc' + "V", "20 " + '\u03bc' + "V", "50 " + '\u03bc' + "V", "100 " + '\u03bc' + "V", "200 " + '\u03bc' + "V", "500 " + '\u03bc' + "V", "1.0 mV", "2.0 mV", "5.0 mV", "10 mV" };
        private int yScaleIndex = 9;

        // time axis scales and labels
        private const int NumXScales = 7;
        private string[] XScaleText = new string[NumXScales] { "30 msec", "90 msec", "150 msec", "300 msec", "750 msec", "1.5 sec", "4.5 sec" };
        private int xScaleIndex = 5;

        // pen colors for Port J3 auxiliary TTL inputs
        private Pen[] auxPens = new Pen[6] { Pens.Red, Pens.Orange, Pens.Yellow, Pens.Green, Pens.Blue, Pens.Purple };

        private bool saveMode = false;

        private BinaryWriter binWriter;
        private FileStream fs;
        private string saveFileName;
        private double fileSize;
        private double fileSaveTime;

        // Spike Scope window
        private MagnifyForm frmMagnifyForm;
        private SpikeRecord mySpikeRecord = new SpikeRecord();
        private bool spikeWindowVisible = false;
        private Point spikeWindowOffset = new Point(980, 150);


        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            myContext = new BufferedGraphicsContext();
            myBuffer = myContext.Allocate(this.CreateGraphics(), this.DisplayRectangle);
            myBuffer.Graphics.Clear(SystemColors.Control);

            myBuffer.Graphics.DrawLine(Pens.Black, 820, 120, 820, 169);
            myBuffer.Graphics.DrawLine(Pens.Black, 821, 120, 821, 169);
            lblYScale.Text = YScaleText[yScaleIndex];
            lblXScale.Text = XScaleText[xScaleIndex];
            myBuffer.Render();

            int firmwareID1 = 0;
            int firmwareID2 = 0;
            int firmwareID3 = 0;

            try
            {
                // Try to open Intan RHA2000-EVAL board on USB port
                myUSBSource.Open(ref firmwareID1, ref firmwareID2, ref firmwareID3);
            }
            catch
            {
                // If no board is found (or drivers are not installed), run application with synthetic neural data for demonstration purposes

                if (MessageBox.Show("Intan Technologies USB device not found.  Click OK to run application with synthesized neural data for demonstration purposes.\n\nTo use the RHA2000-EVAL board click Cancel, load correct drivers and/or connect device to USB port, then restart application.\n\nVisit http://www.intantech.com for drivers and more information.",
                    "Intan USB Device Not Found", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel)
                    this.Close();

                this.Text = "Intan Technologies Amplifier Demo (SIMULATED DATA: Connect board and restart program to record live data)";
                myUSBSource.SynthDataMode = true;
                tmrSynthData.Enabled = true;
                yScaleIndex = 5;
                lblYScale.Text = YScaleText[yScaleIndex];

                // Disable hardware options if no board is connected
                chkSettle.Enabled = false;
                btnZCheck.Enabled = false;
            }

            if (myUSBSource.SynthDataMode)
                lblStatus.Text = "No USB board connected.  Ready to run with synthesized neural data.";
            else
            {
                lblStatus.Text = String.Concat("Connected to Intan Interface Board with firmware type ",
                    firmwareID1.ToString(), ", version ",
                    firmwareID2.ToString(), ".",
                    firmwareID3.ToString());
            }
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            myBuffer.Render();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            myBuffer.Dispose();
            myContext.Dispose();
        }

        // tmrDraw is a timer that 'ticks' once every 5 milliseconds.  Upon a 'tick', we check to see
        // if there is enough new data from the USB port to update our waveform plots.
        private void tmrDraw_Tick(object sender, EventArgs e)
        {

            if (readMode == true)
            {
                // Must call CheckForUsbData periodically during time-consuming operations (like updating graphics)
                // to make sure the USB read buffer doesn't overflow.
                myUSBSource.CheckForUsbData(plotQueue, saveQueue);

                // DateTime d1 = DateTime.Now;

                if (plotQueue.Count > 0)
                {
                    // lblStatus.Text = "plotQueue.Count = " + plotQueue.Count.ToString();
                    
                    USBData plotData;

                    plotData = plotQueue.Dequeue();
                    plotQueue.Clear();
                    plotData.CopyToArray(dataFrame, auxFrame);

                    if (spikeWindowVisible)
                    {
                        if (mySpikeRecord.FindSpikes(dataFrame) > 0)
                        {
                            frmMagnifyForm.DrawSpikes();
                        }
                    }

                    // DateTime dt = DateTime.Now;

                    if (saveMode)
                    {
                        lblStatus.Text = String.Concat("Saving data to file ", saveFileName, ".  Estimated file size = ", fileSize.ToString("F01"), " MB.");
                    }

                    // Must call CheckForUsbData periodically during time-consuming operations (like updating graphics)
                    // to make sure the USB read buffer doesn't overflow.
                    myUSBSource.CheckForUsbData(plotQueue, saveQueue);

                    float y_offset;
                    Rectangle rectBounds;

                    // Which channels should we plot on the screen?
                    displayChannel[0] = chkChannel1.Checked;
                    displayChannel[1] = chkChannel2.Checked;
                    displayChannel[2] = chkChannel3.Checked;
                    displayChannel[3] = chkChannel4.Checked;
                    displayChannel[4] = chkChannel5.Checked;
                    displayChannel[5] = chkChannel6.Checked;
                    displayChannel[6] = chkChannel7.Checked;
                    displayChannel[7] = chkChannel8.Checked;
                    displayChannel[8] = chkChannel9.Checked;
                    displayChannel[9] = chkChannel10.Checked;
                    displayChannel[10] = chkChannel11.Checked;
                    displayChannel[11] = chkChannel12.Checked;
                    displayChannel[12] = chkChannel13.Checked;
                    displayChannel[13] = chkChannel14.Checked;
                    displayChannel[14] = chkChannel15.Checked;
                    displayChannel[15] = chkChannel16.Checked;

                    float yPlotScale = YScaleFactors[yScaleIndex];
                    
                    if (xScaleIndex == 0)
                    {
                        rectBounds = new Rectangle(XPlotOffset, 0, 751, 800);
                        myBuffer.Graphics.FillRectangle(SystemBrushes.Control, rectBounds);

                        // Plot amplifier waveforms on screen
                        for (int channel = 0; channel < 16; channel++)
                        {
                            if (displayChannel[channel] == true)
                            {
                                y_offset = (float)channel * -40.0F + 582.0F;
                                for (int x = 1; x < 750; x++)
                                {
                                    if (spikeWindowVisible == true && channel == mySpikeRecord.Channel)
                                    {
                                        myBuffer.Graphics.DrawLine(Pens.Blue, (float)x + (float)XPlotOffset,
                                            640.0F - (y_offset + yPlotScale * dataFrame[channel, x - 1]), (float)x + (float)XPlotOffset + 1.0F,
                                            640.0F - (y_offset + yPlotScale * dataFrame[channel, x]));
                                    }
                                    else
                                    {
                                        myBuffer.Graphics.DrawLine(Pens.Black, (float)x + (float)XPlotOffset,
                                            640.0F - (y_offset + yPlotScale * dataFrame[channel, x - 1]), (float)x + (float)XPlotOffset + 1.0F,
                                            640.0F - (y_offset + yPlotScale * dataFrame[channel, x]));
                                    }
                                }

                                // Must call CheckForUsbData periodically during time-consuming operations (like updating graphics)
                                // to make sure the USB read buffer doesn't overflow.
                                myUSBSource.CheckForUsbData(plotQueue, saveQueue);
                            }
                        }
                        // Plot Port J3 auxiliary TTL input rasters on screen
                        for (int aux = 0; aux < 6; aux++)
                        {
                            y_offset = 698.0F + 3.0F * (float)aux;
                            for (int x = 1; x < 750; x++)
                            {
                                if ((auxFrame[x - 1] & (UInt16)(1 << aux)) > 0)
                                {
                                    myBuffer.Graphics.DrawLine(auxPens[aux], (float)x + (float)XPlotOffset,
                                        y_offset, (float)x + (float)XPlotOffset + 1.0F, y_offset);
                                }
                            }

                            // Must call CheckForUsbData periodically during time-consuming operations (like updating graphics)
                            // to make sure the USB read buffer doesn't overflow.
                            myUSBSource.CheckForUsbData(plotQueue, saveQueue);
                        }
                    }
                    else
                    {
                        int drawWidth, dataChunk;

                        switch (xScaleIndex)
                        {
                            case 1:
                                drawWidth = 250;
                                break;
                            case 2:
                                drawWidth = 150;
                                break;
                            case 3:
                                drawWidth = 75;
                                break;
                            case 4:
                                drawWidth = 30;
                                break;
                            case 5:
                                drawWidth = 15;
                                break;
                            case 6:
                                drawWidth = 5;
                                break;
                            default:
                                drawWidth = 5;
                                break;
                        }
                        dataChunk = 750 / drawWidth;

                        float maxValue, minValue;
                        bool auxHigh;

                        rectBounds = new Rectangle(xSlowPos + XPlotOffset, 0, drawWidth, 800);
                        myBuffer.Graphics.FillRectangle(SystemBrushes.Control, rectBounds);

                        if (xSlowPos == 0)
                        {
                            rectBounds = new Rectangle(750 + XPlotOffset, 0, 1, 800);
                            myBuffer.Graphics.FillRectangle(SystemBrushes.Control, rectBounds);
                        }

                        // Must call CheckForUsbData periodically during time-consuming operations (like updating graphics)
                        // to make sure the USB read buffer doesn't overflow.
                        myUSBSource.CheckForUsbData(plotQueue, saveQueue);

                        for (int x1 = 0; x1 < drawWidth; x1++)
                        {
                            // Plot amplifier waveforms on screen
                            for (int channel = 0; channel < 16; channel++)
                            {
                                if (displayChannel[channel] == true)
                                {
                                    maxValue = -999999.0F;
                                    minValue = 999999.0F;
                                    for (int x2 = 0; x2 < dataChunk; x2++)
                                    {
                                        if (dataFrame[channel, dataChunk * x1 + x2] > maxValue)
                                        {
                                            maxValue = dataFrame[channel, dataChunk * x1 + x2];
                                        }
                                        if (dataFrame[channel, dataChunk * x1 + x2] < minValue)
                                        {
                                            minValue = dataFrame[channel, dataChunk * x1 + x2];
                                        }
                                    }
                                    
                                    y_offset = (float)channel * -40.0F + 582.0F;

                                    // DrawLine will not draw anything if the specified line is too short, so we must set a
                                    // lower bound on its length.
                                    if (yPlotScale * (maxValue - minValue) < 0.10F)
                                    {
                                        maxValue += 0.1F / yPlotScale;
                                    }

                                    if (spikeWindowVisible == true && channel == mySpikeRecord.Channel)
                                    {
                                        myBuffer.Graphics.DrawLine(Pens.Blue, (float)xSlowPos + (float)XPlotOffset,
                                             640.0F - (y_offset + yPlotScale * minValue), (float)xSlowPos + (float)XPlotOffset,
                                             640.0F - (y_offset + yPlotScale * maxValue));
                                    }
                                    else
                                    {
                                        myBuffer.Graphics.DrawLine(Pens.Black, (float)xSlowPos + (float)XPlotOffset,
                                             640.0F - (y_offset + yPlotScale * minValue), (float)xSlowPos + (float)XPlotOffset,
                                             640.0F - (y_offset + yPlotScale * maxValue));
                                    }
                                    
                                }

                                // Must call CheckForUsbData periodically during time-consuming operations (like updating graphics)
                                // to make sure the USB read buffer doesn't overflow.
                                myUSBSource.CheckForUsbData(plotQueue, saveQueue);
                            }
                            // Plot Port J3 auxiliary TTL input rasters on screen
                            for (int aux = 0; aux < 6; aux++)
                            {
                                y_offset = 698.0F + 3.0F * (float)aux;
                                auxHigh = false;
                                for (int x2 = 0; x2 < dataChunk; x2++)
                                {
                                    if ((auxFrame[dataChunk * x1 + x2] & (UInt16)(1 << aux)) > 0)
                                    {
                                        auxHigh = true;
                                        x2 = dataChunk;
                                    }
                                }
                                if (auxHigh)
                                {
                                    myBuffer.Graphics.DrawLine(auxPens[aux], (float)xSlowPos + (float)XPlotOffset,
                                        y_offset, (float)xSlowPos + (float)XPlotOffset, y_offset + 0.1F);
                                }

                                // Must call CheckForUsbData periodically during time-consuming operations (like updating graphics)
                                // to make sure the USB read buffer doesn't overflow.
                                myUSBSource.CheckForUsbData(plotQueue, saveQueue);
                            }

                            xSlowPos++;
                        }

                        if (xScaleIndex > 2)
                        {
                            myBuffer.Graphics.DrawLine(Pens.Red, xSlowPos + XPlotOffset, 0, xSlowPos + XPlotOffset, 800);

                            // Must call CheckForUsbData periodically during time-consuming operations (like updating graphics)
                            // to make sure the USB read buffer doesn't overflow.
                            myUSBSource.CheckForUsbData(plotQueue, saveQueue);
                        }

                        if (xSlowPos >= 750)
                            xSlowPos = 0;

                    }

                    // Must call CheckForUsbData periodically during time-consuming operations (like updating graphics)
                    // to make sure the USB read buffer doesn't overflow.
                    myUSBSource.CheckForUsbData(plotQueue, saveQueue);

                    myBuffer.Render();

                    // Must call CheckForUsbData periodically during time-consuming operations (like updating graphics)
                    // to make sure the USB read buffer doesn't overflow.
                    myUSBSource.CheckForUsbData(plotQueue, saveQueue);

                }

                // DateTime d2 = DateTime.Now;

                // double elapsedTimeMSec = ((TimeSpan)(d2 - d1)).TotalMilliseconds;
                // lblStatus.Text = "frame rate (in msec): " + elapsedTimeMSec.ToString();

                // Must call CheckForUsbData periodically during time-consuming operations (like updating graphics)
                // to make sure the USB read buffer doesn't overflow.
                myUSBSource.CheckForUsbData(plotQueue, saveQueue);

                // If we are in Record mode, save selected data to disk
                if (saveMode == true)
                {
                    USBData saveData;

                    while (saveQueue.Count > 0)
                    {
                        saveData = saveQueue.Dequeue();
                        saveData.CopyToArray(dataFrame, auxFrame);
                        for (int i = 0; i < 750; i++)
                        {
                            if (chkChannel1.Checked)
                                binWriter.Write(dataFrame[0, i]);
                            if (chkChannel2.Checked)
                                binWriter.Write(dataFrame[1, i]);
                            if (chkChannel3.Checked)
                                binWriter.Write(dataFrame[2, i]);
                            if (chkChannel4.Checked)
                                binWriter.Write(dataFrame[3, i]);
                            if (chkChannel5.Checked)
                                binWriter.Write(dataFrame[4, i]);
                            if (chkChannel6.Checked)
                                binWriter.Write(dataFrame[5, i]);
                            if (chkChannel7.Checked)
                                binWriter.Write(dataFrame[6, i]);
                            if (chkChannel8.Checked)
                                binWriter.Write(dataFrame[7, i]);
                            if (chkChannel9.Checked)
                                binWriter.Write(dataFrame[8, i]);
                            if (chkChannel10.Checked)
                                binWriter.Write(dataFrame[9, i]);
                            if (chkChannel11.Checked)
                                binWriter.Write(dataFrame[10, i]);
                            if (chkChannel12.Checked)
                                binWriter.Write(dataFrame[11, i]);
                            if (chkChannel13.Checked)
                                binWriter.Write(dataFrame[12, i]);
                            if (chkChannel14.Checked)
                                binWriter.Write(dataFrame[13, i]);
                            if (chkChannel15.Checked)
                                binWriter.Write(dataFrame[14, i]);
                            if (chkChannel16.Checked)
                                binWriter.Write(dataFrame[15, i]);

                            binWriter.Write((byte)auxFrame[i]);
                        }
                        fileSize += (750.0 * 1.0) / 1000000.0;  // aux inputs
                        for (int channel = 0; channel < 16; channel++)
                        {
                            if (displayChannel[channel])
                                fileSize += (750.0 * 4.0) / 1000000.0;
                        }
 
                        fileSaveTime += 750.0 / 25000.0;

                        if (fileSaveTime >= (60.0 * (double)numMaxMinutes.Value))    // Every X minutes, close existing file and start a new one
                        {
                            binWriter.Flush();
                            binWriter.Close();
                            fs.Close();

                            DateTime dt = DateTime.Now;
                            saveFileName = String.Concat(Path.GetDirectoryName(saveFileDialog1.FileName), Path.DirectorySeparatorChar, Path.GetFileNameWithoutExtension(saveFileDialog1.FileName), dt.ToString("_yyMMdd_HHmmss"), ".int");
                            fs = new FileStream(saveFileName, FileMode.Create);
                            binWriter = new BinaryWriter(fs);
                            fileSize = 0.0;
                            fileSaveTime = 0.0;

                            binWriter.Write((byte)128);
                            binWriter.Write((byte)1);
                            binWriter.Write((byte)1);

                            for (int channel = 0; channel < 16; channel++)
                            {
                                if (displayChannel[channel] == true)
                                    binWriter.Write((byte)1);
                                else
                                    binWriter.Write((byte)0);
                            }
                            for (int i = 0; i < 48; i++)
                                binWriter.Write((byte)0);
                        }
                    }
                }
                else
                {
                    saveQueue.Clear();
                }

                // Must call CheckForUsbData periodically during time-consuming operations (like updating graphics)
                // to make sure the USB read buffer doesn't overflow.
                myUSBSource.CheckForUsbData(plotQueue, saveQueue);

            }
        }

        // Open Intan web page if user clicks on Intan logo
        private void picIntanLogo_Click(object sender, EventArgs e)
        {
            //Call the Process.Start method to open the default browser with a URL:
            System.Diagnostics.Process.Start("http://www.intantech.com");
        }

        // 'Hide All' button
        private void btnAllChannelsOff_Click(object sender, EventArgs e)
        {
            chkChannel1.Checked = false;
            chkChannel2.Checked = false;
            chkChannel3.Checked = false;
            chkChannel4.Checked = false;
            chkChannel5.Checked = false;
            chkChannel6.Checked = false;
            chkChannel7.Checked = false;
            chkChannel8.Checked = false;
            chkChannel9.Checked = false;
            chkChannel10.Checked = false;
            chkChannel11.Checked = false;
            chkChannel12.Checked = false;
            chkChannel13.Checked = false;
            chkChannel14.Checked = false;
            chkChannel15.Checked = false;
            chkChannel16.Checked = false;
        }

        // 'Show All' button
        private void btnAllChannelsOn_Click(object sender, EventArgs e)
        {
            chkChannel1.Checked = true;
            chkChannel2.Checked = true;
            chkChannel3.Checked = true;
            chkChannel4.Checked = true;
            chkChannel5.Checked = true;
            chkChannel6.Checked = true;
            chkChannel7.Checked = true;
            chkChannel8.Checked = true;
            chkChannel9.Checked = true;
            chkChannel10.Checked = true;
            chkChannel11.Checked = true;
            chkChannel12.Checked = true;
            chkChannel13.Checked = true;
            chkChannel14.Checked = true;
            chkChannel15.Checked = true;
            chkChannel16.Checked = true;
        }

        // Voltage axis 'Zoom In' button
        private void btnZoomIn_Click(object sender, EventArgs e)
        {
            if (yScaleIndex > 0)
            {
                yScaleIndex--;
                lblYScale.Text = YScaleText[yScaleIndex];
            }
            if (spikeWindowVisible)
                frmMagnifyForm.UpdateYScale(yScaleIndex);
        }

        // Voltage axis 'Zoom Out' button
        private void btnZoomOut_Click(object sender, EventArgs e)
        {
            if (yScaleIndex < (NumYScales - 1))
            {
                yScaleIndex++;
                lblYScale.Text = YScaleText[yScaleIndex];
            }
            if (spikeWindowVisible)
                frmMagnifyForm.UpdateYScale(yScaleIndex);
        }

        // Time axis 'Zoom In' button
        private void btnXZoomIn_Click(object sender, EventArgs e)
        {
            if (xScaleIndex > 0)
            {
                xScaleIndex--;
                lblXScale.Text = XScaleText[xScaleIndex];
                xSlowPos = 0;
            }
        }

        // Time axis 'Zoom Out' button
        private void btnXZoomOut_Click(object sender, EventArgs e)
        {
            if (xScaleIndex < (NumXScales - 1))
            {
                xScaleIndex++;
                lblXScale.Text = XScaleText[xScaleIndex];
                xSlowPos = 0;
            }
        }

        // Amplifier Settle check box
        private void chkSettle_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSettle.Checked == true)
            {
                myUSBSource.SettleOn();
            }
            else
            {
                myUSBSource.SettleOff();
            }
        }

        // Electrode Impedance Test button
        private void btnZCheck_Click(object sender, EventArgs e)
        {
            if (readMode == false)  // We can only check electrode impedance when the board is not streaming amplifier data
            {
                int i, channel;
                double[] zFrame = new double[1250];
                double[] zMeasured = new double[16];
                double meanI, meanQ, amplitude;

                plotQueue.Clear();
                saveQueue.Clear();

                // Make sure amplifier settle is off
                if (chkSettle.Checked)
                {
                    chkSettle.Checked = false;
                }

                // Enable electrode impedance test mode
                myUSBSource.ZCheckOn();

                Thread.Sleep(10);

                // Set analog MUX to channel 0
                myUSBSource.ChannelReset();

                // Wait 10 msec
                Thread.Sleep(10);

                // Start streaming data (all samples will be from channel 0)
                myUSBSource.Start();

                // Clear plots
                Rectangle rectBounds = new Rectangle(XPlotOffset, 0, 751, 800);
                myBuffer.Graphics.FillRectangle(SystemBrushes.Control, rectBounds);

                for (channel = 0; channel < 16; channel++)
                {
                    // Wait for enough data from this channel
                    while (plotQueue.Count < 4)
                    {
                        // Call CheckForUsbData once per millisecond to make sure the USB read buffer doesn't overflow.
                        myUSBSource.CheckForUsbData(plotQueue, saveQueue);
                        Thread.Sleep(1);
                    }

                    USBData plotData;

                    for (i = 0; i < 2; i++)  // read two "dummy" frames to ignore any transients
                    {
                        plotData = plotQueue.Dequeue();
                    }

                    plotData = plotQueue.Dequeue();             // now read two real frames to get a total of 1250 data points:
                    plotData.CopyToArray(dataFrame, auxFrame);  // 50 msec = 3 complete cycles of 60 Hz, 50 complete cycles of 1 kHz
                    for (i = 0; i < 750; i++)
                    {
                        zFrame[i] = (double)dataFrame[0, i];
                    }
                    plotData = plotQueue.Dequeue();
                    plotData.CopyToArray(dataFrame, auxFrame);
                    for (i = 0; i < 250; i++)
                    {
                        zFrame[i + 750] = (double)dataFrame[0, i];
                    }

                    myUSBSource.ChannelStep();  // Go ahead and move on to next channel
                    plotQueue.Clear();          // Make sure to clear any residual data from previous channel

                    // Calculate amplitude of 1 kHz component in signal
                    meanI = 0.0;
                    meanQ = 0.0;
                    for (i = 0; i < 1250; i++)
                    {
                        meanI += zFrame[i] * Math.Cos(2.0 * Math.PI * 1000.0 * (double)i * 0.00004);    // 0.00004 = 1/25000 = ADC sample rate
                        meanQ += zFrame[i] * Math.Sin(2.0 * Math.PI * 1000.0 * (double)i * 0.00004);
                    }
                    meanI = meanI / 1250.0;
                    meanQ = meanQ / 1250.0;

                    amplitude = 2.0 * Math.Sqrt(meanI * meanI + meanQ * meanQ);

                    zMeasured[channel] = (amplitude / 0.001) / 1000.0;   // Test current is +/-1 nA; voltage is expressed in uV.
                    // Divide by 1000 to put impedance in units of kOhm.

                    zMeasured[channel] *= 1.06;     // empirical fudge factor
                }

                // Turn off impedance check mode, and stop data transfer
                myUSBSource.ZCheckOff();
                myUSBSource.Stop();

                plotQueue.Clear();
                saveQueue.Clear();

                // Display results on screen
                string zText;
                Font objFont = new System.Drawing.Font("Arial", 12, FontStyle.Bold);
                for (channel = 0; channel < 16; channel++)
                {
                    if (zMeasured[channel] < 4.0)
                    {
                        zText = "< 4 k" + '\u03A9';
                        myBuffer.Graphics.DrawString(zText, objFont, System.Drawing.Brushes.Black, 70, 50 + (channel * 40));
                    }
                    else if (zMeasured[channel] > 4000.0)
                    {
                        zText = "> 4 M" + '\u03A9';
                        myBuffer.Graphics.DrawString(zText, objFont, System.Drawing.Brushes.Red, 70, 50 + (channel * 40));
                    }
                    else if (zMeasured[channel] < 1000.0)
                    {
                        zText = zMeasured[channel].ToString("F00") + " k" + '\u03A9';
                        myBuffer.Graphics.DrawString(zText, objFont, System.Drawing.Brushes.RoyalBlue, 70, 50 + (channel * 40));
                    }
                    else
                    {
                        double zMOhm = zMeasured[channel] / 1000.0;
                        zText = zMOhm.ToString("F02") + " M" + '\u03A9';
                        myBuffer.Graphics.DrawString(zText, objFont, System.Drawing.Brushes.RoyalBlue, 70, 50 + (channel * 40));
                    }
                }
            }
            myBuffer.Render();
        }

        // Software high-pass filter cutoff frequency text box
        private void txtHPF_TextChanged(object sender, EventArgs e)
        {
            double new_fHPF;

            new_fHPF = myUSBSource.FHPF;

            try
            {
                new_fHPF = Convert.ToDouble(txtHPF.Text);
            }
            catch
            {
                txtHPF.Text = myUSBSource.FHPF.ToString();
            }

            if (new_fHPF < 0.0)
            {
                myUSBSource.FHPF = 0.0;
                txtHPF.Text = "0";
            }
            else if (new_fHPF > 10000.0)
            {
                myUSBSource.FHPF = 10000.0;
                txtHPF.Text = "10000";
            }
            else
            {
                myUSBSource.FHPF = new_fHPF;
            }
        }

        // Select Base Filename button
        private void btnSelectFilename_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Title = "Specify Base Filename for Saved Data";
            saveFileDialog1.Filter = "Intan Data Files (*.int)|*.int";
            saveFileDialog1.FilterIndex = 1;

            saveFileDialog1.OverwritePrompt = true;

            if (saveFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                txtSaveFilename.Text = Path.GetFileNameWithoutExtension(saveFileDialog1.FileName);
                btnRecord.ForeColor = SystemColors.ControlText;
            }
        }

        // Start New File Every X Minutes selector
        private void numMaxMinutes_ValueChanged(object sender, EventArgs e)
        {
            lblAmpChannel.Focus();  // remove focus from this control
        }

        // Start button
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (saveMode == false)
            {
                xSlowPos = 0;
                myUSBSource.Start();

                btnZCheck.ForeColor = SystemColors.ControlDark;

                if (myUSBSource.SynthDataMode)
                    lblStatus.Text = "Viewing synthesized neural data.";
                else
                    lblStatus.Text = "Viewing live data.";

                readMode = true;
            }
        }

        // Stop button
        private void btnStop_Click(object sender, EventArgs e)
        {
            readMode = false;

            btnZCheck.ForeColor = SystemColors.ControlText;

            myUSBSource.Stop();

            if (saveMode == true)
            {
                binWriter.Flush();
                binWriter.Close();
                fs.Close();
                saveMode = false;

                chkChannel1.Enabled = true;
                chkChannel2.Enabled = true;
                chkChannel3.Enabled = true;
                chkChannel4.Enabled = true;
                chkChannel5.Enabled = true;
                chkChannel6.Enabled = true;
                chkChannel7.Enabled = true;
                chkChannel8.Enabled = true;
                chkChannel9.Enabled = true;
                chkChannel10.Enabled = true;
                chkChannel11.Enabled = true;
                chkChannel12.Enabled = true;
                chkChannel13.Enabled = true;
                chkChannel14.Enabled = true;
                chkChannel15.Enabled = true;
                chkChannel16.Enabled = true;
            }

            lblStatus.Text = "Ready to start.";
        }

        // Record button
        private void btnRecord_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.FileName != null)
            {
                DateTime dt = DateTime.Now;
                saveFileName = String.Concat(Path.GetDirectoryName(saveFileDialog1.FileName), Path.DirectorySeparatorChar, Path.GetFileNameWithoutExtension(saveFileDialog1.FileName), dt.ToString("_yyMMdd_HHmmss"), ".int");
                fs = new FileStream(saveFileName, FileMode.Create);
                binWriter = new BinaryWriter(fs);
                fileSize = 0.0;
                fileSaveTime = 0.0;

                saveMode = true;
                xSlowPos = 0;
                myUSBSource.Start();
                btnZCheck.ForeColor = SystemColors.ControlDark;

                readMode = true;

                chkChannel1.Enabled = false;
                chkChannel2.Enabled = false;
                chkChannel3.Enabled = false;
                chkChannel4.Enabled = false;
                chkChannel5.Enabled = false;
                chkChannel6.Enabled = false;
                chkChannel7.Enabled = false;
                chkChannel8.Enabled = false;
                chkChannel9.Enabled = false;
                chkChannel10.Enabled = false;
                chkChannel11.Enabled = false;
                chkChannel12.Enabled = false;
                chkChannel13.Enabled = false;
                chkChannel14.Enabled = false;
                chkChannel15.Enabled = false;
                chkChannel16.Enabled = false;

                binWriter.Write((byte)128);
                binWriter.Write((byte)1);
                binWriter.Write((byte)1);

                displayChannel[0] = chkChannel1.Checked;
                displayChannel[1] = chkChannel2.Checked;
                displayChannel[2] = chkChannel3.Checked;
                displayChannel[3] = chkChannel4.Checked;
                displayChannel[4] = chkChannel5.Checked;
                displayChannel[5] = chkChannel6.Checked;
                displayChannel[6] = chkChannel7.Checked;
                displayChannel[7] = chkChannel8.Checked;
                displayChannel[8] = chkChannel9.Checked;
                displayChannel[9] = chkChannel10.Checked;
                displayChannel[10] = chkChannel11.Checked;
                displayChannel[11] = chkChannel12.Checked;
                displayChannel[12] = chkChannel13.Checked;
                displayChannel[13] = chkChannel14.Checked;
                displayChannel[14] = chkChannel15.Checked;
                displayChannel[15] = chkChannel16.Checked;

                for (int channel = 0; channel < 16; channel++)
                {
                    if (displayChannel[channel] == true)
                        binWriter.Write((byte)1);
                    else
                        binWriter.Write((byte)0);
                }
                for (int i = 0; i < 48; i++)
                    binWriter.Write((byte)0);
            }
        }

        // About Intan Demo menu option
        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AboutForm frmAbout = new AboutForm();
            frmAbout.ShowDialog();
        }

        // Quit and exit application (menu option)
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            myUSBSource.Stop();
            myUSBSource.Close();

            this.Close();
        }

        // Quit and exit application (quit button)
        private void btnQuit_Click(object sender, EventArgs e)
        {
            myUSBSource.Stop();
            myUSBSource.Close();

            this.Close();
        }

        // Enable/disable software high-pass filter
        private void chkEnableHPF_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEnableHPF.Checked == true)
            {
                myUSBSource.EnableHPF = true;
            }
            else
            {
                myUSBSource.EnableHPF = false;
            }
        }

        // Disable software notch filter
        private void btnNotchFilterDisable_CheckedChanged(object sender, EventArgs e)
        {
            myUSBSource.EnableNotch = false;
        }

        // Enable software 50-Hz notch filter
        private void btnNotchFilter50Hz_CheckedChanged(object sender, EventArgs e)
        {
            myUSBSource.FNotch = 50.0;
            myUSBSource.EnableNotch = true;
        }

        // Enable software 60-Hz notch filter
        private void btnNotchFilter60Hz_CheckedChanged(object sender, EventArgs e)
        {
            myUSBSource.FNotch = 60.0;
            myUSBSource.EnableNotch = true;
        }

        // Open or close 'Spike Scope' window
        private void btnSpikeWindow_Click(object sender, EventArgs e)
        {
            if (spikeWindowVisible)
            {
                spikeWindowOffset.X = frmMagnifyForm.Location.X - this.Location.X;
                spikeWindowOffset.Y = frmMagnifyForm.Location.Y - this.Location.Y;
                frmMagnifyForm.Close();
                frmMagnifyForm.Dispose();
                spikeWindowVisible = false;
                btnSpikeWindow.Text = "Open Spike Scope";
            }
            else
            {
                frmMagnifyForm = new MagnifyForm();
                frmMagnifyForm.Location = new Point(this.Location.X + spikeWindowOffset.X, this.Location.Y + spikeWindowOffset.Y);
                frmMagnifyForm.Show();
                frmMagnifyForm.SetSpikeRecord(mySpikeRecord);
                frmMagnifyForm.SetUpGraphicsAndSound(myContext);
                frmMagnifyForm.UpdateYScale(yScaleIndex);
                spikeWindowVisible = true;
                btnSpikeWindow.Text = "Close Spike Scope";
            }
        }

        // Allow user to select amplifier channel (for Spike Scope) with mouse click
        private void MainForm_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.X > 66 && e.X < 790)
            {
                if (spikeWindowVisible)
                {
                    double y = (double)e.Y;
                    y = (58.0 - y) / -40.0;
                    y = Math.Round(y);
                    int ch = (int)y;
                    if (ch < 0)
                        ch = 0;
                    else if (ch > 15)
                        ch = 15;
                    mySpikeRecord.Channel = ch;
                    frmMagnifyForm.ChangeChannel(ch);
                }
            }
        }

        // tmrSynthData is a timer that 'ticks' once every 30 milliseconds to emulate the
        // data rate from the USB port when an Intan board is connected.
        private void tmrSynthData_Tick(object sender, EventArgs e)
        {
            myUSBSource.NewSynthDataReady = true;
        }

    }

}
