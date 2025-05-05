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
using System.Diagnostics;
using System.IO;
using System.Media;

namespace USB_Scope
{
    /// <summary>
    /// Spike Scope tool used to view time-aligned neural action potentials.
    /// </summary>
    public partial class MagnifyForm : Form
    {
        private BufferedGraphicsContext myContext;
        private BufferedGraphics myPanelBuffer;
        private MemoryStream memWave;
        private BinaryWriter binWaveWriter;
        private SpikeRecord mySpikeRecord;
        private float[] spikeSnippet = new float[62];

        // voltage axis scales and labels
        private const int NumYScales = 10;
        private float[] YScaleFactors = new float[NumYScales] { 30.0F, 15.0F, 6.0F, 3.0F, 1.50F, 0.60F, 0.30F, 0.15F, 0.06F, 0.03F };
        private string[] YScaleText = new string[NumYScales] { "5 " + '\u03bc' + "V", "10 " + '\u03bc' + "V", "25 " + '\u03bc' + "V", "50 " + '\u03bc' + "V", "100 " + '\u03bc' + "V", "250 " + '\u03bc' + "V", "500 " + '\u03bc' + "V", "1.0 mV", "2.5 mV", "5.0 mV" };
        private int yScaleIndex = 9;

        private Pen[,] scopePens = new Pen[3, 30] {  
            { Pens.DarkBlue, Pens.DarkBlue, Pens.DarkBlue, Pens.DarkBlue, Pens.Gray, Pens.Gray, Pens.DarkGray, Pens.DarkGray, Pens.LightGray, Pens.LightGray, Pens.LightGray, Pens.LightGray,  Pens.LightGray, Pens.LightGray, Pens.LightGray, Pens.LightGray, Pens.LightGray, Pens.LightGray, Pens.LightGray, Pens.LightGray, Pens.LightGray, Pens.LightGray, Pens.LightGray, Pens.LightGray, Pens.LightGray, Pens.LightGray, Pens.LightGray, Pens.LightGray, Pens.LightGray, Pens.LightGray,},
            { Pens.DarkBlue, Pens.DarkBlue, Pens.DarkBlue, Pens.DarkBlue, Pens.DarkBlue, Pens.DarkBlue, Pens.DarkBlue, Pens.DarkBlue, Pens.Gray, Pens.Gray, Pens.Gray, Pens.Gray,  Pens.DarkGray, Pens.DarkGray, Pens.DarkGray, Pens.DarkGray, Pens.LightGray, Pens.LightGray, Pens.LightGray, Pens.LightGray, Pens.LightGray, Pens.LightGray, Pens.LightGray, Pens.LightGray, Pens.LightGray, Pens.LightGray, Pens.LightGray, Pens.LightGray, Pens.LightGray, Pens.LightGray,},
            { Pens.DarkBlue, Pens.DarkBlue, Pens.DarkBlue, Pens.DarkBlue, Pens.DarkBlue, Pens.DarkBlue, Pens.DarkBlue, Pens.DarkBlue, Pens.DarkBlue, Pens.DarkBlue, Pens.DarkBlue, Pens.DarkBlue,  Pens.Gray, Pens.Gray, Pens.Gray, Pens.Gray, Pens.Gray, Pens.Gray, Pens.DarkGray, Pens.DarkGray, Pens.DarkGray, Pens.DarkGray, Pens.DarkGray, Pens.DarkGray, Pens.LightGray, Pens.LightGray, Pens.LightGray, Pens.LightGray, Pens.LightGray, Pens.LightGray,}
        };

        public MagnifyForm()
        {
            InitializeComponent();
        }

        private void MagnifyForm_Load(object sender, EventArgs e)
        {
            // write lowercase Greek letter mu plus 'V' for microvolts
            lblUV.Text = '\u03bc' + "V";
        }

        private void MagnifyForm_Paint(object sender, PaintEventArgs e)
        {
            myPanelBuffer.Render();
        }

        private void MagnifyForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            myPanelBuffer.Dispose();
        }

        /// <summary>
        /// Initialize graphics and audio wave file header for Spike Scope
        /// </summary>
        /// <param name="context">Graphics context for double buffering.</param>
        public void SetUpGraphicsAndSound(BufferedGraphicsContext context)
        {
            myContext = context;

            myPanelBuffer = myContext.Allocate(panel1.CreateGraphics(), panel1.DisplayRectangle);
            myPanelBuffer.Graphics.Clear(SystemColors.Control);

            // Generate wave file header in memory

            int m = 2;              // number of bytes per sample
            int nc = 1;             // number of channels
            int ns = 62;            // number of samples
            int f = 25000;          // sample rate (Hz)
            int waveFormatPCM = 1;

            memWave = new MemoryStream(44 + m * nc * ns);
            binWaveWriter = new BinaryWriter(memWave);

            binWaveWriter.Write('R');
            binWaveWriter.Write('I');
            binWaveWriter.Write('F');
            binWaveWriter.Write('F');
            binWaveWriter.Write((Int32)(4 + 24 + (8 + m * nc * ns)));
            binWaveWriter.Write('W');
            binWaveWriter.Write('A');
            binWaveWriter.Write('V');
            binWaveWriter.Write('E');
            binWaveWriter.Write('f');
            binWaveWriter.Write('m');
            binWaveWriter.Write('t');
            binWaveWriter.Write(' ');
            binWaveWriter.Write((Int32)16);
            binWaveWriter.Write((Int16)waveFormatPCM);
            binWaveWriter.Write((Int16)nc);
            binWaveWriter.Write((Int32)f);
            binWaveWriter.Write((Int32)(f * m * nc));
            binWaveWriter.Write((Int16)(m * nc));
            binWaveWriter.Write((Int16)(8 * m));
            binWaveWriter.Write('d');
            binWaveWriter.Write('a');
            binWaveWriter.Write('t');
            binWaveWriter.Write('a');
            binWaveWriter.Write((Int32)(m * nc * ns));

            for (int i = 0; i < ns; i++)
            {
                binWaveWriter.Write((Int16)0);
            }

            binWaveWriter.Seek(0, SeekOrigin.Begin);

            cmbChannel.SelectedIndex = mySpikeRecord.Channel;
            cmbNumShow.SelectedIndex = mySpikeRecord.NumSpikesToShow;
            chkAudioEnable.Checked = mySpikeRecord.AudioEnabled;
            trkVolume.Value = mySpikeRecord.AudioVolume;
        }

        public void SetSpikeRecord(SpikeRecord inSpikeRecord)
        {
            mySpikeRecord = inSpikeRecord;
        }
     
        /// <summary>
        /// Y axis has changed; update display.
        /// </summary>
        /// <param name="yIndex">New Y scale index.</param>
        public void UpdateYScale(int yIndex)
        {
            yScaleIndex = yIndex;
            lblYScalePlus.Text = "+" + YScaleText[yIndex];
            lblYScaleMinus.Text = "-" + YScaleText[yIndex];
            this.DrawSpikes();
        }

        /// <summary>
        /// Update spike waveform window in Spike Scope
        /// </summary>
        public void DrawSpikes()
        {
            int i, j;
            int numSpikes;

            // Clear drawing area and draw simple grid lines
            myPanelBuffer.Graphics.Clear(SystemColors.Control);
            myPanelBuffer.Graphics.DrawLine(Pens.LightGray, 48, 0, 48, 300);
            myPanelBuffer.Graphics.DrawLine(Pens.LightGray, 98, 0, 98, 300);
            myPanelBuffer.Graphics.DrawLine(Pens.LightGray, 148, 0, 148, 300);
            myPanelBuffer.Graphics.DrawLine(Pens.LightGray, 198, 0, 198, 300);
            myPanelBuffer.Graphics.DrawLine(Pens.LightGray, 0, 150, 248, 150);

            if (mySpikeRecord.NumSpikesToShow == 0)
                numSpikes = 10;
            else if (mySpikeRecord.NumSpikesToShow == 1)
                numSpikes = 20;
            else
                numSpikes = 30;

            if (mySpikeRecord.SpikeCount < numSpikes)
                numSpikes = mySpikeRecord.SpikeCount;

            for (i = numSpikes - 1; i >= 0; i--)
            {
                mySpikeRecord.CopySpikeToArray(spikeSnippet, i);
                for (j = 0; j < 61; j++)
                {
                    myPanelBuffer.Graphics.DrawLine(scopePens[mySpikeRecord.NumSpikesToShow, i], (float)(4 * j), 150.0F - YScaleFactors[yScaleIndex] * spikeSnippet[j], (float)(4 * j + 4), 150.0F - YScaleFactors[yScaleIndex] * spikeSnippet[j + 1]);
                }
            }

            // Draw spike detection threshold level
            myPanelBuffer.Graphics.DrawLine(Pens.Red, 0.0F, 150.0F - YScaleFactors[yScaleIndex] * mySpikeRecord.GetThreshold(), (float)(4 * 61 + 4), 150.0F - YScaleFactors[yScaleIndex] * mySpikeRecord.GetThreshold());

            myPanelBuffer.Render();

            if (mySpikeRecord.AudioEnabled)
            {
                // Generate quick audio wave file of spike in memory
                binWaveWriter.Seek(44, SeekOrigin.Begin);

                spikeSnippet[0] *= 0.125F;
                spikeSnippet[1] *= 0.25F;
                spikeSnippet[2] *= 0.5F;
                spikeSnippet[3] *= 0.75F;
                spikeSnippet[61] *= 0.125F;
                spikeSnippet[60] *= 0.25F;
                spikeSnippet[59] *= 0.5F;
                spikeSnippet[58] *= 0.75F;

                float f;
                float vol = (float)mySpikeRecord.AudioVolume;
                for (int k = 0; k < 62; k++)
                {
                    f = vol * vol * 10.0F * spikeSnippet[k];
                    if (f > 32767.0F)
                        f = 32767.0F;
                    else if (f < -32767.0F)
                        f = -32767.0F;
                    binWaveWriter.Write((Int16)f);
                }

                binWaveWriter.Seek(0, SeekOrigin.Begin);

                // Play wave file of spike from memory
                SoundPlayer smallSoundPlayer = new SoundPlayer();
                smallSoundPlayer.Stream = memWave;
                smallSoundPlayer.Load();
                smallSoundPlayer.Play();
                smallSoundPlayer.Dispose();
            }
        }

        // Reset to Zero button
        private void btnResetThreshold_Click(object sender, EventArgs e)
        {
            mySpikeRecord.SetThreshold(0.0F);
            numThreshold.Value = 0;
            this.DrawSpikes();
        }

        // Clear Scope button
        private void btnClearScope_Click(object sender, EventArgs e)
        {
            // Clear drawing area and draw simple grid lines; draw spike detection threshold level
            mySpikeRecord.ClearSpikes();
            myPanelBuffer.Graphics.Clear(SystemColors.Control);
            myPanelBuffer.Graphics.DrawLine(Pens.LightGray, 48, 0, 48, 300);
            myPanelBuffer.Graphics.DrawLine(Pens.LightGray, 98, 0, 98, 300);
            myPanelBuffer.Graphics.DrawLine(Pens.LightGray, 148, 0, 148, 300);
            myPanelBuffer.Graphics.DrawLine(Pens.LightGray, 198, 0, 198, 300);
            myPanelBuffer.Graphics.DrawLine(Pens.Red, 0.0F, 150.0F - YScaleFactors[yScaleIndex] * mySpikeRecord.GetThreshold(), (float)(4 * 61 + 3), 150.0F - YScaleFactors[yScaleIndex] * mySpikeRecord.GetThreshold());
            myPanelBuffer.Render();
        }

        // Spike detection threshold selector
        private void numThreshold_ValueChanged(object sender, EventArgs e)
        {
            mySpikeRecord.SetThreshold((float)numThreshold.Value);
            this.DrawSpikes();
            lblUV.Focus();  // remove focus from this control
        }

        // Channel selector
        private void cmbChannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            mySpikeRecord.Channel = cmbChannel.SelectedIndex;
            mySpikeRecord.ClearSpikes();
            numThreshold.Value = (decimal) mySpikeRecord.GetThreshold();
            this.DrawSpikes();
            lblUV.Focus();  // remove focus from this control
        }

        /// <summary>
        /// Change amplifier channel displayed in Spike Scope
        /// </summary>
        /// <param name="ch">Amplifier channel.</param>
        public void ChangeChannel(int ch)
        {
            cmbChannel.SelectedIndex = ch;
        }

        // Show Last X Spikes selector
        private void cmbNumShow_SelectedIndexChanged(object sender, EventArgs e)
        {
            mySpikeRecord.NumSpikesToShow = cmbNumShow.SelectedIndex;
            lblUV.Focus();  // remove focus from this control
        }

        // Audio Volume slider
        private void trkVolume_Scroll(object sender, EventArgs e)
        {
            mySpikeRecord.AudioVolume = trkVolume.Value;
        }

        // Audio Enable check box
        private void chkAudioEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAudioEnable.Checked)
                mySpikeRecord.AudioEnabled = true;
            else
                mySpikeRecord.AudioEnabled = false;
        }

        // Allow user to select spike detection threshold level with mouse click on waveform
        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            int y = 150 - e.Y;
            float thresh = (float)y / YScaleFactors[yScaleIndex];
            numThreshold.Value = (decimal)thresh;
        }
    }
}
