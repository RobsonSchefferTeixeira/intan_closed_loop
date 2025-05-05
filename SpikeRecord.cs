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


namespace USB_Scope
{
    /// <summary>
    /// This class assists with threshold-based spike detection for the
    /// Spike Scope tool.
    /// </summary>
    public class SpikeRecord
    {
        // private variables
        private const int maxDepth = 30;
        private float[,] spikeData = new float[maxDepth, 62];
        private float[] findSpikeBuffer = new float[62 + 750];
        private int spikeDataIndex;
        private int spikeCount;
        private float[] spikeThreshold = new float[16];
        private bool changedChannel = true;
        private bool audioEnabled = false;
        private int audioVolume = 5;

        private int channel;
        private int numSpikesToShow = 1;

        // properties

        /// <summary>
        /// Amplifier channel
        /// </summary>
        public int Channel
        {
            get
            {
                return channel;
            }
            set
            {
                if (value < 0)
                    channel = 0;
                else if (value > 15)
                    channel = 15;
                else
                {
                    channel = value;
                    changedChannel = true;
                }
            }
        }

        /// <summary>
        /// Number of spikes found
        /// </summary>
        public int SpikeCount
        {
            get
            {
                return spikeCount;
            }
        }

        /// <summary>
        /// Number of spikes to display in Spike Scope
        /// </summary>
        public int NumSpikesToShow
        {
            get
            {
                return numSpikesToShow;
            }
            set
            {
                numSpikesToShow = value;
            }
        }

        /// <summary>
        /// Enable audio output of spikes?
        /// </summary>
        public bool AudioEnabled
        {
            get
            {
                return audioEnabled;
            }
            set
            {
                audioEnabled = value;
            }
        }

        /// <summary>
        /// Audio output volume
        /// </summary>
        public int AudioVolume
        {
            get
            {
                return audioVolume;
            }
            set
            {
                audioVolume = value;
            }
        }


        // public methods

        /// <summary>
        /// Set spike detection threshold.
        /// </summary>
        /// <param name="threshold">Amplifier channel.</param>
        public void SetThreshold(float threshold)
        {
            spikeThreshold[channel] = threshold;
        }

        /// <summary>
        /// Get spike detection threshold of current channel.
        /// </summary>
        /// <returns>Spike detection threshold.</returns>
        public float GetThreshold()
        {
            return spikeThreshold[channel];
        }

        /// <summary>
        /// Search for spikes that exceed threshold.
        /// </summary>
        /// <param name="data">Amplifier waveform.</param>
        /// <returns>Number of spikes found in waveform.</returns>
        public int FindSpikes(float[,] data)
        {
            int i, j;
            int numSpikesFound = 0;
            float threshold = spikeThreshold[channel];

            for (i = 62; i < 62 + 750; i++)
            {
                findSpikeBuffer[i] = data[channel, i - 62];
            }

            if (changedChannel)
                i = 62 + 12;
            else
                i = 12;

            while (i < 750 + 62 - 50)
            {
                if (CheckThreshold(findSpikeBuffer[i - 1], findSpikeBuffer[i], threshold))
                {
                    numSpikesFound++;

                    spikeDataIndex++;
                    if (spikeDataIndex == maxDepth)
                        spikeDataIndex = 0;

                    spikeCount++;
                    if (spikeCount > maxDepth)
                        spikeCount = maxDepth;

                    int count = 0;
                    for (j = i - 12; j < i + 50; j++)
                    {
                        spikeData[spikeDataIndex, count++] = findSpikeBuffer[j];
                    }

                    i += 62;
                }
                else
                    i++;
            }

            for (i = 0; i < 62; i++)
            {
                findSpikeBuffer[i] = data[channel, 750 - 62 + i];
            }

            changedChannel = false;
            return numSpikesFound;
        }

        /// <summary>
        /// Look for positive or negative crossings of spike detection threshold.
        /// </summary>
        /// <param name="d1">Waveform sample at time t-1.</param>
        /// <param name="d2">Waveform sample at time t.</param>
        /// <param name="threshold">Spike detection threshold.</param>
        /// <returns>Did we find a spike?</returns>
        private bool CheckThreshold(float d1, float d2, float threshold)
        {
            bool foundSpike = false;

            if (threshold < 0)
            {
                if (d1 > threshold & d2 <= threshold)
                    foundSpike = true;
            }
            else
            {
                if (d1 < threshold & d2 >= threshold)
                    foundSpike = true;
            }

            return foundSpike;
        }

        /// <summary>
        /// Clear all detected spikes from memory.
        /// </summary>
        public void ClearSpikes()
        {
            int i, j;

            for (i = 0; i < maxDepth; i++)
            {
                for (j = 0; j < 62; j++)
                    spikeData[i, j] = 0.0F;
            }
            spikeDataIndex = 0;
            spikeCount = 0;
        }

        /// <summary>
        /// Copy spike waveform to array.
        /// </summary>
        /// <param name="dataOut">Array to receive waveform data.</param>
        /// <param name="index">Spike index: 0 = returned newest value; (maxDepth - 1) returns oldest spike.</param>
        public void CopySpikeToArray(float[] dataOut, int index)
        {
            int adjIndex;

            adjIndex = spikeDataIndex - index;
            if (adjIndex < 0)
                adjIndex += maxDepth;

            for (int i = 0; i < 62; i++)
            {
                dataOut[i] = spikeData[adjIndex, i];
            }
        }

        /// <summary>
        /// SpikeRecord constructor.
        /// </summary>
        public SpikeRecord()
        {
            channel = 0;
            for (int i = 0; i < 16; i++)
                spikeThreshold[i] = 0.0F;
            spikeDataIndex = 0;
            spikeCount = 0;
        }
    }

}
