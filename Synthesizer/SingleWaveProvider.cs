using NAudio.Wave;
using System;
using System.Collections.Generic;

namespace Synthesizer
{
    class SingleWaveProvider : IWaveProvider
    {
        public short Amplitude { set; get; }
        private double _frequency;
        public double Frequency { set; get; }

        public WaveFormat WaveFormat { get; set; }
        private double phaseAngle;
        public SingleWaveProvider()
        {
            Amplitude = 0;
            Frequency = 0;
            this.WaveFormat = new WaveFormat();
        }
        public int Read(byte[] buffer, int offset, int count)
        {
            int samplesRequired = count / 2;
            int samplesRead = Read(new WaveBuffer(buffer).ShortBuffer, offset / 2, samplesRequired);
            return samplesRead * 2;
        }
        public int Read(short[] buffer, int offset,
          int sampleCount)
        {
            UpdateFrequency();
            for (int i = 0; i < sampleCount; i++)
            {
                buffer[offset + i] = (short)(Amplitude * Math.Sin(phaseAngle));
                
                phaseAngle += 2 * Math.PI * _frequency / WaveFormat.SampleRate;

                if (phaseAngle > 2 * Math.PI)
                {
                    phaseAngle -= 2 * Math.PI;
                }
            }
            return sampleCount;
        }
        private void UpdateFrequency()
        {
            _frequency = Frequency;
        }
    }

}
