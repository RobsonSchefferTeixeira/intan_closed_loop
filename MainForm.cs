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
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.IO;
using System.Media;
using System.IO.Ports;
using System.Runtime.InteropServices;
using FFTWSharp;
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
        private BufferedGraphics myBuffer_PSD;
        private BufferedGraphics myBuffer_PSD_noise;
        private BufferedGraphics myBuffer_lfp_thr;
        private BufferedGraphics myBuffer_speed_thr;


        private bool readMode = false;

        private float[,] dataFrame = new float[16, 750];
        private UInt16[] auxFrame = new UInt16[750];
        private USBSource myUSBSource = new USBSource();
        private double[] dataFrame_psd = new double[25000];

        private Queue<USBData> plotQueue = new Queue<USBData>();
        private Queue<USBData> saveQueue = new Queue<USBData>();

        private DateTime d1 = DateTime.Now;

        private bool[] displayChannel = new bool[16];
        private int xSlowPos = 0;
        private const int XPlotOffset = 54;
        private int channel = 0;
        private int channel_speed = 0;
        private float psd_amp = 1;
        private double previous_value = 100;
        private int psd_noise_amp = 1;
        private double current_value = 0;
        private bool isSpeedStimActive = false;
        private bool isAmpStimActive = false;
        private float maxValueSpeed;
        private float minValueSpeed;
        private double[] fft;
        private double[] amplitude;
        private bool finalTriggerState = false;
        private bool isAllStimActive = false;
        private bool auxHigh = false;
        public ChannelSelectionForm frmChannelSelectionForm = new ChannelSelectionForm();

        // voltage axis scales and labels
        private const int NumYScales = 10;
        private float[] YScaleFactors = new float[NumYScales] { 5.24F, 2.62F, 1.048F, 0.524F, 0.262F, 0.1048F, 0.0524F, 0.0262F, 0.01048F, 0.00524F };
        private string[] YScaleText = new string[NumYScales] { "10 " + '\u03bc' + "V", "20 " + '\u03bc' + "V", "50 " + '\u03bc' + "V", "100 " + '\u03bc' + "V", "200 " + '\u03bc' + "V", "500 " + '\u03bc' + "V", "1.0 mV", "2.0 mV", "5.0 mV", "10 mV" };
        private int yScaleIndex = 9;
        private int yAmpSpeed = 1;
        private int yAmpPSDAmp = 1;
        private int xSlowPos_previous = 0;

        // time axis scales and labels
        private const int NumXScales = 6;
        private string[] XScaleText = new string[NumXScales] { "90 msec", "150 msec", "300 msec", "750 msec", "1.5 sec", "4.5 sec" };
        private int xScaleIndex = 5;

        // pen colors for Port J3 auxiliary TTL inputs
        private Pen[] auxPens = new Pen[6] { Pens.Red, Pens.Orange, Pens.Yellow, Pens.Green, Pens.Blue, Pens.Purple };

        private bool saveMode = false;

        private BinaryWriter binWriter;
        private FileStream fs;
        private string saveFileName;
        private double fileSize;
        private double fileSaveTime;
        private float ThresholdValue_lfp = 155;
        private float ThresholdValue_speed = 155;


        // Spike Scope window
        private MagnifyForm frmMagnifyForm;
        private SpikeRecord mySpikeRecord = new SpikeRecord();
        private bool spikeWindowVisible = false;
        private Point spikeWindowOffset = new Point(980, 150);


        public MainForm()
        {

            /*
             * Thread t = new Thread(new ThreadStart(StartSplashScreen));
            t.Start();
            Thread.Sleep(1);
            InitializeComponent();
            frmChannelSelectionForm.SomethingHappened += FrmChannelSelectionForm_formclosed;

            */
            InitializeComponent();
            foreach (string str in SerialPort.GetPortNames())
            {
                if (serialPort.IsOpen == false)
                {

                    serialPort.PortName = str;
                    System.Diagnostics.Debug.WriteLine(serialPort.PortName);

                    try
                    {
                        //open serial port
                        serialPort.Open();
                        serialPort.BaudRate = 9600;
                        serialPort.WriteTimeout = 100000;
                        serialPort.ReadTimeout = 100000;
                        serialPort.Write("<mccon>");
                        string s = serialPort.ReadTo("\n");
                        System.Diagnostics.Debug.WriteLine(s);
                        if (s == "<connected>")
                        {

                            break;
                        }
                        else
                        {
                            serialPort.Close();
                        }
                    }
                    catch (TimeoutException)
                    {
                        serialPort.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            //t.Abort();
        }

        /*
        public void StartSplashScreen()
        {
            Application.Run(new frmSplashScreen());
        }
        */

        private void FrmChannelSelectionForm_formclosed()
        {
            frmChannelSelectionForm.channelSelectionFormVisible = false;
            btnChanSelectionWindow.Enabled = true;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // open port, send a message if port was opened and test LEDs with a 3 sec pulse


            serialPort.Open();

            // MessageBox.Show("Arduino opened at the port " + serialPort.PortName);

            serialPort.Write("1");
            System.Threading.Thread.Sleep(1000);
            serialPort.Write("0");
            System.Threading.Thread.Sleep(1000);
            serialPort.Write("1");
            System.Threading.Thread.Sleep(1000);
            serialPort.Write("0");
            System.Threading.Thread.Sleep(1000);
            serialPort.Write("1");
            System.Threading.Thread.Sleep(1000);
            serialPort.Write("0");



            myContext = new BufferedGraphicsContext();
            myBuffer = myContext.Allocate(panel_lfp.CreateGraphics(), panel_lfp.DisplayRectangle);
            myBuffer.Graphics.Clear(SystemColors.Control);

            myBuffer_PSD = myContext.Allocate(panel_psd.CreateGraphics(), panel_psd.DisplayRectangle);
            myBuffer_PSD.Graphics.Clear(SystemColors.Control);

            myBuffer_PSD_noise = myContext.Allocate(panel_psd_noise.CreateGraphics(), panel_psd_noise.DisplayRectangle);
            myBuffer_PSD_noise.Graphics.Clear(SystemColors.Control);

            myBuffer_lfp_thr = myContext.Allocate(panel_thr_lfp.CreateGraphics(), panel_thr_lfp.DisplayRectangle);
            myBuffer_lfp_thr.Graphics.Clear(SystemColors.Control);

            myBuffer_speed_thr = myContext.Allocate(panel_thr_speed.CreateGraphics(), panel_thr_speed.DisplayRectangle);
            myBuffer_speed_thr.Graphics.Clear(SystemColors.Control);

            lblYScale.Text = YScaleText[yScaleIndex];
            lblXScale.Text = XScaleText[xScaleIndex];

            myBuffer.Render();
            myBuffer_PSD.Render();
            myBuffer_PSD_noise.Render();
            myBuffer_lfp_thr.Render();
            myBuffer_speed_thr.Render();


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
                /*
                if (MessageBox.Show("Intan Technologies USB device not found.  Click OK to run application with synthesized neural data for demonstration purposes.\n\nTo use the RHA2000-EVAL board click Cancel, load correct drivers and/or connect device to USB port, then restart application.\n\nVisit http://www.intantech.com for drivers and more information.",
                    "Intan USB Device Not Found", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel)
                    this.Close();
                    */

                this.Text = "Intan Technologies Amplifier Demo (SIMULATED DATA: Connect board and restart program to record live data)";

                myUSBSource.SynthDataMode = true;
                tmrSynthData.Enabled = true;
                yScaleIndex = 5;
                lblYScale.Text = YScaleText[yScaleIndex];

                // Disable hardware options if no board is connected
                chkSettle.Enabled = false;
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
            myBuffer_PSD.Render();
            myBuffer_PSD_noise.Render();
            myBuffer_lfp_thr.Render();
            myBuffer_speed_thr.Render();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (serialPort.IsOpen == true)
                serialPort.Write("0");

            serialPort.Close();
            myBuffer.Dispose();
            myBuffer_PSD.Dispose();
            myBuffer_PSD_noise.Dispose();
            myBuffer_lfp_thr.Dispose();
            myBuffer_speed_thr.Render();
            myContext.Dispose();
        }

        // tmrDraw is a timer that 'ticks' once every 5 milliseconds.  Upon a 'tick', we check to see
        // if there is enough new data from the USB port to update our waveform plots.
        private void tmrDraw_Tick(object sender, EventArgs e)
        {




            if (xSlowPos == 0)
            {
                d1 = DateTime.Now;
            }

            if (readMode == true)
            {
                // Must call CheckForUsbData periodically during time-consuming operations (like updating graphics)
                // to make sure the USB read buffer doesn't overflow.
                myUSBSource.CheckForUsbData(plotQueue, saveQueue);



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

                    //DateTime dt = DateTime.Now;

                    if (saveMode)
                    {
                        lblStatus.Text = String.Concat("Saving data to file ", saveFileName, ".  Estimated file size = ", fileSize.ToString("F01"), " MB.");
                    }

                    // Must call CheckForUsbData periodically during time-consuming operations (like updating graphics)
                    // to make sure the USB read buffer doesn't overflow.
                    myUSBSource.CheckForUsbData(plotQueue, saveQueue);

                    float y_offset;
                    Rectangle rectBounds;
                    Rectangle rectBounds_speed;
                    Rectangle rectBoundsAmp;
                    // Which channels should we plot on the screen?


                    float yPlotScale = YScaleFactors[yScaleIndex];


                    int drawWidth, dataChunk;

                    switch (xScaleIndex)
                    {

                        case 0:
                            drawWidth = 250;
                            break;
                        case 1:
                            drawWidth = 150;
                            break;
                        case 2:
                            drawWidth = 75;
                            break;
                        case 3:
                            drawWidth = 30;
                            break;
                        case 4:
                            drawWidth = 15;
                            break;
                        case 5:
                            drawWidth = 5;
                            break;
                        default:
                            drawWidth = 5;
                            break;
                    }
                    dataChunk = 750 / drawWidth;

                    float maxValue, minValue;
                    // bool auxHigh;

                    rectBounds = new Rectangle(xSlowPos, 0, drawWidth, 100);
                    myBuffer.Graphics.FillRectangle(SystemBrushes.Control, rectBounds);

                    rectBounds_speed = new Rectangle(xSlowPos, 0, drawWidth, 150);
                    myBuffer_speed_thr.Graphics.FillRectangle(SystemBrushes.Control, rectBounds_speed);

                    rectBoundsAmp = new Rectangle(xSlowPos, 0, drawWidth, 150);
                    myBuffer_lfp_thr.Graphics.FillRectangle(SystemBrushes.Control, rectBoundsAmp);

                    if (xSlowPos == 0)
                    {
                        rectBounds = new Rectangle(750, 0, 1, 100);
                        myBuffer.Graphics.FillRectangle(SystemBrushes.Control, rectBounds);

                        rectBounds_speed = new Rectangle(750, 0, 1, 150);
                        myBuffer_speed_thr.Graphics.FillRectangle(SystemBrushes.Control, rectBounds_speed);

                        rectBoundsAmp = new Rectangle(750, 0, 1, 150);
                        myBuffer_lfp_thr.Graphics.FillRectangle(SystemBrushes.Control, rectBoundsAmp);


                    }

                    // Must call CheckForUsbData periodically during time-consuming operations (like updating graphics)
                    // to make sure the USB read buffer doesn't overflow.
                    myUSBSource.CheckForUsbData(plotQueue, saveQueue);

                    for (int x1 = 0; x1 < drawWidth; x1++)
                    {
                        if (useAmp.Checked == true)
                        {
                            comboBox1.Enabled = true;

                            if (comboBox1.SelectedItem != null)
                            {

                                // Plot amplifier waveforms on screen

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

                                //y_offset = 3*-40.0F + 582.0F;
                                y_offset = 50.0F;
                                // DrawLine will not draw anything if the specified line is too short, so we must set a
                                // lower bound on its length.
                                if (yPlotScale * (maxValue - minValue) < 0.10F)
                                {
                                    maxValue += 0.1F / yPlotScale;
                                }


                                myBuffer.Graphics.DrawLine(Pens.Black, (float)xSlowPos,
                                     (y_offset - yPlotScale * minValue), (float)xSlowPos,
                                    (y_offset - yPlotScale * maxValue));

                                //myBuffer.Graphics.DrawLine(Pens.Red, 0,ThresholdValue,750,ThresholdValue);

                                // if ((y_offset - yPlotScale * minValue) >= ThresholdValue)
                                //if ((y_offset - yPlotScale * maxValue) <= ThresholdValue)
                                // {
                                // serialPort.Write("1");
                                // serialPort.Write("0");
                                // }


                            }

                        }
                        else
                        {
                            comboBox1.Enabled = false;

                        }


                        // Must call CheckForUsbData periodically during time-consuming operations (like updating graphics)
                        // to make sure the USB read buffer doesn't overflow.
                        myUSBSource.CheckForUsbData(plotQueue, saveQueue);

                        if (useSpeed.Checked == true)
                        {
                            comboBox2.Enabled = true;

                            if (comboBox2.SelectedItem != null)
                            {

                                maxValue = -999999.0F;
                                minValue = 999999.0F;
                                for (int x2 = 0; x2 < dataChunk; x2++)
                                {
                                    if (dataFrame[channel_speed, dataChunk * x1 + x2] > maxValue)
                                    {
                                        maxValue = dataFrame[channel_speed, dataChunk * x1 + x2];
                                    }
                                    if (dataFrame[channel_speed, dataChunk * x1 + x2] < minValue)
                                    {
                                        minValue = dataFrame[channel_speed, dataChunk * x1 + x2];
                                    }
                                }

                                //y_offset = 3*-40.0F + 582.0F;
                                y_offset = 150.0F / 2.0F;
                                // DrawLine will not draw anything if the specified line is too short, so we must set a
                                // lower bound on its length.
                                if (yPlotScale * (maxValue - minValue) < 0.10F)
                                {
                                    maxValue += 0.1F / yPlotScale;
                                }


                                myBuffer_speed_thr.Graphics.DrawLine(Pens.Black, (float)xSlowPos,
                                     (y_offset - yAmpSpeed * minValue), (float)xSlowPos,
                                    (y_offset - yAmpSpeed * maxValue));

                                maxValueSpeed = y_offset - yAmpSpeed * maxValue;
                                minValueSpeed = y_offset - yAmpSpeed * minValue;




                            }
                        }
                        else
                        {
                            comboBox2.Enabled = false;
                        }


                        myUSBSource.CheckForUsbData(plotQueue, saveQueue);



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

                        myUSBSource.CheckForUsbData(plotQueue, saveQueue);
                        xSlowPos++;
                    }




                    if (xScaleIndex > 2)
                    {
                        myBuffer.Graphics.DrawLine(Pens.Red, xSlowPos, 0, xSlowPos, 100);
                        myBuffer_speed_thr.Graphics.DrawLine(Pens.Red, xSlowPos, 0, xSlowPos, 150);
                        myBuffer_lfp_thr.Graphics.DrawLine(Pens.Red, (float)xSlowPos, 0, (float)xSlowPos, 150);

                        // Must call CheckForUsbData periodically during time-consuming operations (like updating graphics)
                        // to make sure the USB read buffer doesn't overflow.
                        myUSBSource.CheckForUsbData(plotQueue, saveQueue);
                    }



                    if (useAmp.Checked == true)
                    {
                        comboBox1.Enabled = true;

                        if (comboBox1.SelectedItem != null)
                        {

                            int ii;
                            double[] dataFrame_aux = new double[750];
                            for (ii = 0; ii < 749; ii++)
                            {
                                dataFrame_aux[ii] = System.Convert.ToDouble(dataFrame[channel, ii]);
                            }


                            int iii;
                            for (iii = 750; iii < dataFrame_psd.Length; iii++)
                            {
                                dataFrame_psd[iii - 750] = dataFrame_psd[iii];
                            }


                            dataFrame_aux.CopyTo(dataFrame_psd, 24249);

                            myUSBSource.CheckForUsbData(plotQueue, saveQueue);


                            fft = FFT(dataFrame_psd, true);

                            amplitude = fft_amplitude(fft);


                            drawLFPRatios(amplitude);
                            drawPSD(amplitude);

                            myUSBSource.CheckForUsbData(plotQueue, saveQueue);
                        }
                    }




                    if (xSlowPos >= 750)
                    {
                        DateTime d2 = DateTime.Now;
                        double elapsedTimeMSec = ((TimeSpan)(d2 - d1)).TotalMilliseconds;
                        //lblCounter.Text = "Timer (in msec): " + String.Format("{0:0}", elapsedTimeMSec); ;
                        lblCounter.Text = "Timer (in msec): " + elapsedTimeMSec.ToString("0");

                    }

                    if (xSlowPos >= 750)
                    {
                        xSlowPos = 0;
                        xSlowPos_previous = 0;
                    }


                    // Must call CheckForUsbData periodically during time-consuming operations (like updating graphics)
                    // to make sure the USB read buffer doesn't overflow.
                    myUSBSource.CheckForUsbData(plotQueue, saveQueue);

                    myBuffer.Render();
                    myUSBSource.CheckForUsbData(plotQueue, saveQueue);

                    myBuffer_lfp_thr.Render();
                    myUSBSource.CheckForUsbData(plotQueue, saveQueue);

                    myBuffer_speed_thr.Render();

                    // Must call CheckForUsbData periodically during time-consuming operations (like updating graphics)
                    // to make sure the USB read buffer doesn't overflow.
                    myUSBSource.CheckForUsbData(plotQueue, saveQueue);




                    if (useAmp.Checked == true & comboBox1.SelectedItem != null & radioButtonEnableLFPStim.Checked == true)
                    {
                        checkBoxSpeedThreshold.Checked = false;
                        checkBoxSpeedThreshold.Enabled = false;
                        radioButton_Higher_SpeedThr.Enabled = false;
                        radioButton_Lwr_SpeedThr.Enabled = false;
                        radioButtonStimDesignAND.Enabled = false;
                        radioButtonStimDesignOR.Enabled = false;
                        radioButtonStimDesignNotSPEED.Enabled = false;
                        radioButtonStimDesignNOTLFP.Enabled = false;

                        checkBoxAmpThreshold.Enabled = true;
                        plotThresholdAmp();
                        takeDecisionAmp();
                        if (isAmpStimActive == true)
                        {
                            finalTriggerState = true;
                        }
                        else
                        {
                            finalTriggerState = false;
                        }
                    }
                    else if (useSpeed.Checked == true & comboBox2.SelectedItem != null & radioButtonEnableSpeedStim.Checked == true)
                    {
                        checkBoxAmpThreshold.Checked = false;
                        checkBoxAmpThreshold.Enabled = false;
                        radioButton_Higher_AmpThr.Enabled = false;
                        radioButton_Lwr_AmpThr.Enabled = false;
                        radioButtonStimDesignAND.Enabled = false;
                        radioButtonStimDesignOR.Enabled = false;
                        radioButtonStimDesignNotSPEED.Enabled = false;
                        radioButtonStimDesignNOTLFP.Enabled = false;

                        checkBoxSpeedThreshold.Enabled = true;
                        plotThresholdSpeed();
                        takeDecisionSpeed();
                        if (isSpeedStimActive == true)
                        {
                            finalTriggerState = true;
                        }
                        else
                        {
                            finalTriggerState = false;
                        }
                    }
                    else if (useSpeed.Checked == true & comboBox2.SelectedItem != null & useAmp.Checked == true & comboBox1.SelectedItem != null & radioButtonEnableAllStim.Checked == true)
                    {
                        checkBoxSpeedThreshold.Enabled = true;
                        checkBoxAmpThreshold.Enabled = true;
                        radioButtonStimDesignAND.Enabled = true;
                        radioButtonStimDesignOR.Enabled = true;
                        radioButtonStimDesignNotSPEED.Enabled = true;
                        radioButtonStimDesignNOTLFP.Enabled = true;
                        plotThresholdSpeed();
                        takeDecisionSpeed();
                        plotThresholdAmp();
                        takeDecisionAmp();

                        takeDecisionAll();

                        if (isAllStimActive == true)
                        {
                            finalTriggerState = true;
                        }
                        else
                        {
                            finalTriggerState = false;
                        }
                    }
                    else
                    {
                        checkBoxSpeedThreshold.Checked = false;
                        checkBoxSpeedThreshold.Enabled = false;
                        checkBoxAmpThreshold.Checked = false;
                        checkBoxAmpThreshold.Enabled = false;
                        radioButton_Higher_SpeedThr.Enabled = false;
                        radioButton_Lwr_SpeedThr.Enabled = false;
                        radioButton_Higher_AmpThr.Enabled = false;
                        radioButton_Lwr_AmpThr.Enabled = false;
                        radioButtonStimDesignAND.Enabled = false;
                        radioButtonStimDesignOR.Enabled = false;
                        radioButtonStimDesignNotSPEED.Enabled = false;
                        radioButtonStimDesignNOTLFP.Enabled = false;
                        finalTriggerState = false;
                    }


                    finalTrigger();

                    myUSBSource.CheckForUsbData(plotQueue, saveQueue);

                }



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




                            if (frmChannelSelectionForm.chkChannel1.Checked)
                                binWriter.Write(dataFrame[0, i]);
                            if (frmChannelSelectionForm.chkChannel2.Checked)
                                binWriter.Write(dataFrame[1, i]);
                            if (frmChannelSelectionForm.chkChannel3.Checked)
                                binWriter.Write(dataFrame[2, i]);
                            if (frmChannelSelectionForm.chkChannel4.Checked)
                                binWriter.Write(dataFrame[3, i]);
                            if (frmChannelSelectionForm.chkChannel5.Checked)
                                binWriter.Write(dataFrame[4, i]);
                            if (frmChannelSelectionForm.chkChannel6.Checked)
                                binWriter.Write(dataFrame[5, i]);
                            if (frmChannelSelectionForm.chkChannel7.Checked)
                                binWriter.Write(dataFrame[6, i]);
                            if (frmChannelSelectionForm.chkChannel8.Checked)
                                binWriter.Write(dataFrame[7, i]);
                            if (frmChannelSelectionForm.chkChannel9.Checked)
                                binWriter.Write(dataFrame[8, i]);
                            if (frmChannelSelectionForm.chkChannel10.Checked)
                                binWriter.Write(dataFrame[9, i]);
                            if (frmChannelSelectionForm.chkChannel11.Checked)
                                binWriter.Write(dataFrame[10, i]);
                            if (frmChannelSelectionForm.chkChannel12.Checked)
                                binWriter.Write(dataFrame[11, i]);
                            if (frmChannelSelectionForm.chkChannel13.Checked)
                                binWriter.Write(dataFrame[12, i]);
                            if (frmChannelSelectionForm.chkChannel14.Checked)
                                binWriter.Write(dataFrame[13, i]);
                            if (frmChannelSelectionForm.chkChannel15.Checked)
                                binWriter.Write(dataFrame[14, i]);
                            if (frmChannelSelectionForm.chkChannel16.Checked)
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



        // 'Hide All' button
        /*
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
            //chkChannel1.Checked = true;
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
        */

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
                Rectangle rectBounds = new Rectangle(XPlotOffset, 0, 751, 100);
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
                serialPort.Write("0");
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

            if (saveFileDialog1.ShowDialog() != DialogResult.Cancel & (((saveFileDialog1.FileName).Length) > 0))
            {
                txtSaveFilename.Text = Path.GetFileNameWithoutExtension(saveFileDialog1.FileName);
                //btnRecord.ForeColor = SystemColors.ControlText;
                btnRecord.Enabled = true;
            }
        }

        // Start New File Every X Minutes selector
        private void numMaxMinutes_ValueChanged(object sender, EventArgs e)
        {

        }

        // Start button
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (saveMode == false)
            {
                xSlowPos = 0;
                myUSBSource.Start();

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
            btnSelectFilename.Enabled = true;
            if (((saveFileDialog1.FileName).Length) >0 )
            {
                //lblStatus.Text = (saveFileDialog1.FileName != null).ToString();
                //lblStatus.Text = ((saveFileDialog1.FileName).Length).ToString();
                btnRecord.Enabled = true;
            }
            else
            {
                btnRecord.Enabled = false;
            }

            readMode = false;
            btnStart.Enabled = true;

            serialPort.Write("0");
            myUSBSource.Stop();

            if (saveMode == true)
            {
                binWriter.Flush();
                binWriter.Close();
                fs.Close();
                saveMode = false;


                frmChannelSelectionForm.chkChannel1.Enabled = true;
                frmChannelSelectionForm.chkChannel2.Enabled = true;
                frmChannelSelectionForm.chkChannel3.Enabled = true;
                frmChannelSelectionForm.chkChannel4.Enabled = true;
                frmChannelSelectionForm.chkChannel5.Enabled = true;
                frmChannelSelectionForm.chkChannel6.Enabled = true;
                frmChannelSelectionForm.chkChannel7.Enabled = true;
                frmChannelSelectionForm.chkChannel8.Enabled = true;
                frmChannelSelectionForm.chkChannel9.Enabled = true;
                frmChannelSelectionForm.chkChannel10.Enabled = true;
                frmChannelSelectionForm.chkChannel11.Enabled = true;
                frmChannelSelectionForm.chkChannel12.Enabled = true;
                frmChannelSelectionForm.chkChannel13.Enabled = true;
                frmChannelSelectionForm.chkChannel14.Enabled = true;
                frmChannelSelectionForm.chkChannel15.Enabled = true;
                frmChannelSelectionForm.chkChannel16.Enabled = true;


            }

            //lblStatus.Text = "Ready to start.";
        }

        // Record button
        private void btnRecord_Click(object sender, EventArgs e)
        {

            btnSelectFilename.Enabled = false;
            btnRecord.Enabled = false;
            btnStart.Enabled = false;
            DateTime dt = DateTime.Now;
            saveFileName = String.Concat(Path.GetDirectoryName(saveFileDialog1.FileName), Path.DirectorySeparatorChar, Path.GetFileNameWithoutExtension(saveFileDialog1.FileName), dt.ToString("_yyMMdd_HHmmss"), ".int");
            fs = new FileStream(saveFileName, FileMode.Create);
            binWriter = new BinaryWriter(fs);
            fileSize = 0.0;
            fileSaveTime = 0.0;

            saveMode = true;
            xSlowPos = 0;
            myUSBSource.Start();

            readMode = true;

            frmChannelSelectionForm.chkChannel1.Enabled = false;
            frmChannelSelectionForm.chkChannel2.Enabled = false;
            frmChannelSelectionForm.chkChannel3.Enabled = false;
            frmChannelSelectionForm.chkChannel4.Enabled = false;
            frmChannelSelectionForm.chkChannel5.Enabled = false;
            frmChannelSelectionForm.chkChannel6.Enabled = false;
            frmChannelSelectionForm.chkChannel7.Enabled = false;
            frmChannelSelectionForm.chkChannel8.Enabled = false;
            frmChannelSelectionForm.chkChannel9.Enabled = false;
            frmChannelSelectionForm.chkChannel10.Enabled = false;
            frmChannelSelectionForm.chkChannel11.Enabled = false;
            frmChannelSelectionForm.chkChannel12.Enabled = false;
            frmChannelSelectionForm.chkChannel13.Enabled = false;
            frmChannelSelectionForm.chkChannel14.Enabled = false;
            frmChannelSelectionForm.chkChannel15.Enabled = false;
            frmChannelSelectionForm.chkChannel16.Enabled = false;


            binWriter.Write((byte)128);
            binWriter.Write((byte)1);
            binWriter.Write((byte)1);


            displayChannel[0] = frmChannelSelectionForm.chkChannel1.Checked;
            displayChannel[1] = frmChannelSelectionForm.chkChannel2.Checked;
            displayChannel[2] = frmChannelSelectionForm.chkChannel3.Checked;
            displayChannel[3] = frmChannelSelectionForm.chkChannel4.Checked;
            displayChannel[4] = frmChannelSelectionForm.chkChannel5.Checked;
            displayChannel[5] = frmChannelSelectionForm.chkChannel6.Checked;
            displayChannel[6] = frmChannelSelectionForm.chkChannel7.Checked;
            displayChannel[7] = frmChannelSelectionForm.chkChannel8.Checked;
            displayChannel[8] = frmChannelSelectionForm.chkChannel9.Checked;
            displayChannel[9] = frmChannelSelectionForm.chkChannel10.Checked;
            displayChannel[10] = frmChannelSelectionForm.chkChannel11.Checked;
            displayChannel[11] = frmChannelSelectionForm.chkChannel12.Checked;
            displayChannel[12] = frmChannelSelectionForm.chkChannel13.Checked;
            displayChannel[13] = frmChannelSelectionForm.chkChannel14.Checked;
            displayChannel[14] = frmChannelSelectionForm.chkChannel15.Checked;
            displayChannel[15] = frmChannelSelectionForm.chkChannel16.Checked;


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

        // About Intan Demo menu option
        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AboutForm frmAbout = new AboutForm();
            frmAbout.ShowDialog();
        }

        // Quit and exit application (menu option)
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (serialPort.IsOpen == true)
                serialPort.Write("0");

            serialPort.Close();
            myUSBSource.Stop();
            myUSBSource.Close();
            this.Close();
        }

        // Quit and exit application (quit button)
        private void btnQuit_Click(object sender, EventArgs e)
        {
            if (serialPort.IsOpen == true)
                serialPort.Write("0");

            serialPort.Close();
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

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            channel = comboBox1.SelectedIndex;
            // System.Diagnostics.Debug.WriteLine("Channel is " + channel);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            channel_speed = comboBox2.SelectedIndex;
            // System.Diagnostics.Debug.WriteLine("Channel is " + channel);
        }

        private void panel_lfp_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            //float y = 100 - e.Y;
            //float thresh = y / YScaleFactors[yScaleIndex];
            //ThresholdValue = thresh;

            // ThresholdValue = e.Y;
        }

        private void panel_psd_Paint(object sender, PaintEventArgs e)
        {

        }


        public double[] fft_amplitude(double[] data)
        {
            double[] amplitude = new double[data.Length];

            for (int i = 0; i < data.Length; i++)
            {
                amplitude[i] = 2 * Math.Abs(data[i] / data.Length);
            }
            return amplitude;
        }


        public void drawLFPRatios(double[] amplitude)
        {
            double thetapow = 0;
            double deltapow = 0;
            int low_filt_theta = (int)(lowerFilterNumUpDown.Value * 2);
            int up_filt_theta = (int)(higherFilterNumUpDown.Value * 2);
            int low_filt_delta = (int)(lowerFilterDenUpDown.Value * 2);
            int up_filt_delta = (int)(higherFilterDenUpDown.Value * 2);

            int counter = 0;
            for (int j = low_filt_theta; j <= up_filt_theta; j++)
            {
                thetapow = thetapow + amplitude[j];
                counter++;
            }
            thetapow = thetapow / counter;

            int counter2 = 0;
            for (int j = low_filt_delta; j <= up_filt_delta; j++)
            {
                deltapow = deltapow + amplitude[j];
                counter2++;
            }
            deltapow = deltapow / counter2;

            current_value = thetapow / deltapow;
            myBuffer_lfp_thr.Graphics.DrawLine(Pens.Black, (float)xSlowPos_previous, 150 - (float)previous_value * yAmpPSDAmp, (float)xSlowPos, 150 - (float)current_value * yAmpPSDAmp);
            myUSBSource.CheckForUsbData(plotQueue, saveQueue);
            myBuffer_lfp_thr.Render();
            previous_value = current_value;
            xSlowPos_previous = xSlowPos;
            myUSBSource.CheckForUsbData(plotQueue, saveQueue);

        }

        public void drawPSD(double[] amplitude)
        {
            int j, fff;
            float[] freqs = new float[61];
            for (fff = 0; fff <= 60; fff++)
            {
                freqs[fff] = (float)fff * 5;
            }

            float[] freqsvec = new float[201];
            for (int ffff = 0; ffff <= 200; ffff++)
            {
                freqsvec[ffff] = (float)ffff * 25000 / amplitude.Length;
            }

            myBuffer_PSD.Graphics.Clear(SystemColors.Control);
            for (j = 0; j < freqs.Length - 1; j++)
            {
                myBuffer_PSD.Graphics.DrawLine(Pens.Black, (float)freqs[j], 170 - (float)amplitude[j] * psd_amp, (float)freqs[j + 1], 170 - (float)amplitude[j + 1] * psd_amp);

                if ((freqsvec[j] % 5) == 0)
                    myBuffer_PSD.Graphics.DrawString(freqsvec[j].ToString(), new Font("Arial", 12), new SolidBrush(Color.Black), (float)freqs[j], (float)180);
            }
            myBuffer_PSD.Graphics.DrawString("Frequency", new Font("Arial", 12), new SolidBrush(Color.Black), (float)113, (float)200);

            myBuffer_PSD.Render();
            myUSBSource.CheckForUsbData(plotQueue, saveQueue);



            myBuffer_PSD_noise.Graphics.Clear(SystemColors.Control);
            for (j = 60; j <= 180; j++)
            {
                myBuffer_PSD_noise.Graphics.DrawLine(Pens.Black, (float)j - 59, 140 - (float)amplitude[j] * psd_noise_amp, (float)j - 59 + 1, 140 - (float)amplitude[j + 1] * psd_noise_amp);

                if ((freqsvec[j] % 10) == 0)
                    myBuffer_PSD_noise.Graphics.DrawString(freqsvec[j].ToString(), new Font("Arial", 10), new SolidBrush(Color.Black), (float)j - 59, (float)150);
            }
            myBuffer_PSD_noise.Render();
            myUSBSource.CheckForUsbData(plotQueue, saveQueue);
        }


        static double[] FFT(double[] data, bool real)
        {
            // If the input is real, make it complex
            if (real)
                data = ToComplex(data);
            // Get the length of the array
            int n = data.Length;
            /* Allocate an unmanaged memory block for the input and output data.
             * (The input and output are of the same length in this case, so we can use just one memory block.) */
            IntPtr ptr = fftw.malloc(n * 8);    // or: n * sizeof(double)
            // Pass the managed input data to the unmanaged memory block
            Marshal.Copy(data, 0, ptr, n);
            // Plan the FFT and execute it (n/2 because complex numbers are stored as pairs of doubles)
            IntPtr plan = fftw.dft_1d(n / 2, ptr, ptr, fftw_direction.Forward, fftw_flags.Estimate);
            fftw.execute(plan);
            // Create an array to store the output values
            var fft = new double[n];
            // Pass the unmanaged output data to the managed array
            Marshal.Copy(ptr, fft, 0, n);
            // Do some cleaning
            fftw.destroy_plan(plan);
            fftw.free(ptr);
            fftw.cleanup();
            // Return the FFT output
            return fft;
        }


        static double[] ToComplex(double[] real)
        {
            int n = real.Length;
            var comp = new double[n * 2];
            for (int i = 0; i < n; i++)
                comp[2 * i] = real[i];

            return comp;
        }

        private void panel_thr_lfp_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_psd_amp_down_Click(object sender, EventArgs e)
        {
            psd_amp--;
            if (psd_amp == 0)
                psd_amp = 1;
        }

        private void btn_psd_amp_up_Click(object sender, EventArgs e)
        {
            psd_amp++;
        }

        private void panel_thr_speed_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel_thr_speed_MouseClick(object sender, MouseEventArgs e)
        {
            ThresholdValue_speed = e.Y;
        }


        private void panel_thr_lfp_MouseClick(object sender, MouseEventArgs e)
        {
            ThresholdValue_lfp = e.Y;
        }

        private void btnZoomInSpeed_Click(object sender, EventArgs e)
        {
            yAmpSpeed++;
        }

        private void btnZoomOutSpeed_Click(object sender, EventArgs e)
        {
            yAmpSpeed--;
            if (yAmpSpeed == 0)
                yAmpSpeed = 1;


        }

        private void btnZoomInAmp_Click(object sender, EventArgs e)
        {
            yAmpPSDAmp++;

        }

        private void btnZoomOutAmp_Click(object sender, EventArgs e)
        {
            yAmpPSDAmp--;
            if (yAmpPSDAmp == 0)
                yAmpPSDAmp = 1;
        }

        private void sbrMyStatusStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void picTortLabLogo_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.neuro.ufrn.br/research/groups/3");

        }

        private void btn_psdhigh_amp_up_Click(object sender, EventArgs e)
        {
            psd_noise_amp++;
        }

        private void btn_psdhigh_amp_down_Click(object sender, EventArgs e)
        {
            psd_noise_amp--;
            if (psd_noise_amp == 0)
                psd_noise_amp = 1;
        }

        public void plotThresholdSpeed()
        {
            if (checkBoxSpeedThreshold.Checked == true)
            {
                radioButton_Higher_SpeedThr.Enabled = true;
                radioButton_Lwr_SpeedThr.Enabled = true;
                myBuffer_speed_thr.Graphics.DrawLine(Pens.Red, 0, ThresholdValue_speed, 750, ThresholdValue_speed);

            }
            else
            {
                ThresholdValue_speed = 0;
                radioButton_Higher_SpeedThr.Enabled = false;
                radioButton_Lwr_SpeedThr.Enabled = false;
            }

        }


        public void plotThresholdAmp()
        {

            if (checkBoxAmpThreshold.Checked == true)
            {
                radioButton_Higher_AmpThr.Enabled = true;
                radioButton_Lwr_AmpThr.Enabled = true;
                myBuffer_lfp_thr.Graphics.DrawLine(Pens.Red, 0, ThresholdValue_lfp, 750, ThresholdValue_lfp);

            }
            else
            {
                ThresholdValue_lfp = 0;
                radioButton_Higher_AmpThr.Enabled = false;
                radioButton_Lwr_AmpThr.Enabled = false;

            }

        }

        public void takeDecisionAll()
        {

            var buttons = groupBoxStimDesign.Controls.OfType<RadioButton>()
                    .FirstOrDefault(n => n.Checked);
            if (buttons != null)
            {
                switch ((int)buttons.TabIndex)
                {
                    case 0:
                        // lblStatus.Text = "AND";

                        if (isSpeedStimActive == true & isAmpStimActive == true)
                        {
                            isAllStimActive = true;
                        }
                        else
                        {
                            isAllStimActive = false;
                        }


                        break;

                    case 1:
                        // lblStatus.Text = "OR";

                        if (isSpeedStimActive == true | isAmpStimActive == true)
                        {
                            isAllStimActive = true;
                        }
                        else
                        {
                            isAllStimActive = false;
                        }

                        break;

                    case 2:
                        //lblStatus.Text = "Not LFP";

                        if (isSpeedStimActive == true & isAmpStimActive == false)
                        {
                            isAllStimActive = true;
                        }
                        else
                        {
                            isAllStimActive = false;
                        }

                        break;
                    case 3:
                        //lblStatus.Text = "Not Speed";
                        if (isSpeedStimActive == false & isAmpStimActive == true)
                        {
                            isAllStimActive = true;
                        }
                        else
                        {
                            isAllStimActive = false;
                        }
                        break;

                }
            }
            else
            {
                isAllStimActive = false;
            }



        }

        public void takeDecisionSpeed()
        {

            if (checkBoxSpeedThreshold.Checked == true)
            {

                if (radioButton_Higher_SpeedThr.Checked == true)
                {

                    if (ThresholdValue_speed >= maxValueSpeed & isSpeedStimActive == false)
                    {
                        isSpeedStimActive = true;
                        //lblStatus.Text = "Start Stim";
                        // serialPort.Write("1");

                    }
                    else if (ThresholdValue_speed < maxValueSpeed & isSpeedStimActive == true)
                    {
                        isSpeedStimActive = false;
                        //lblStatus.Text = "Stop Stim";
                        //serialPort.Write("0");
                    }

                }

                if (radioButton_Lwr_SpeedThr.Checked == true)
                {
                    if (ThresholdValue_speed <= minValueSpeed & isSpeedStimActive == false)
                    {
                        isSpeedStimActive = true;
                        //lblStatus.Text = "Start Stim";
                        //serialPort.Write("1");

                    }
                    else if (ThresholdValue_speed > minValueSpeed & isSpeedStimActive == true)
                    {
                        isSpeedStimActive = false;
                        //lblStatus.Text = "Stop Stim";
                        // serialPort.Write("0");
                    }
                }


            }


        }


        public void takeDecisionAmp()
        {

            if (checkBoxAmpThreshold.Checked == true)
            {
                float testvalue_amp = 150 - (float)current_value * yAmpPSDAmp;

                if (radioButton_Higher_AmpThr.Checked == true)
                {

                    if (ThresholdValue_lfp >= testvalue_amp & isAmpStimActive == false)
                    {
                        isAmpStimActive = true;
                        //lblStatus.Text = "Start Stim";
                        //serialPort.Write("1");

                    }
                    else if (ThresholdValue_lfp < testvalue_amp & isAmpStimActive == true)
                    {
                        isAmpStimActive = false;
                        //lblStatus.Text = "Stop Stim";
                        //serialPort.Write("0");
                    }

                }

                if (radioButton_Lwr_AmpThr.Checked == true)
                {
                    if (ThresholdValue_lfp <= testvalue_amp & isAmpStimActive == false)
                    {
                        isAmpStimActive = true;
                        // lblStatus.Text = "Start Stim";
                        // serialPort.Write("1");
                        // serialPort.Write("0");
                    }
                    else if (ThresholdValue_lfp > testvalue_amp & isAmpStimActive == true)
                    {
                        isAmpStimActive = false;
                        //lblStatus.Text = "Stop Stim";
                        //serialPort.Write("0");

                    }
                }


            }

        }

        private void checkBoxAmpThreshold_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBoxAmpThreshold.Checked == false)
            {
                isAmpStimActive = false;
                serialPort.Write("0");
            }
        }

        private void checkBoxSpeedThreshold_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBoxSpeedThreshold.Checked == false)
            {
                isSpeedStimActive = false;
                serialPort.Write("0");
            }
        }

        private void finalTrigger()
        {
            if (finalTriggerState == true)
            {
                serialPort.Write("1");
            }
            else
            {
                serialPort.Write("0");
            }


        }

        private void btnChanSelectionWindow_Click(object sender, EventArgs e)
        {

            frmChannelSelectionForm.Show();
            frmChannelSelectionForm.channelSelectionFormVisible = true;
            btnChanSelectionWindow.Enabled = false;

        }

        private void lowerFilterNumUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (lowerFilterNumUpDown.Value >= higherFilterNumUpDown.Value)
            {
                lowerFilterNumUpDown.Value = higherFilterNumUpDown.Value;
            }
        }

        private void higherFilterNumUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (higherFilterNumUpDown.Value <= lowerFilterNumUpDown.Value)
            {
                higherFilterNumUpDown.Value = lowerFilterNumUpDown.Value;
            }
        }

        private void lowerFilterDenUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (lowerFilterDenUpDown.Value >= higherFilterDenUpDown.Value)
            {
                lowerFilterDenUpDown.Value = higherFilterDenUpDown.Value;
            }
        }

        private void higherFilterDenUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (higherFilterDenUpDown.Value <= lowerFilterDenUpDown.Value)
            {
                higherFilterDenUpDown.Value = lowerFilterDenUpDown.Value;
            }
        }
    }
}