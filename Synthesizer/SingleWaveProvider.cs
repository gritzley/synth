using NAudio.Wave;
using System;
using System.Collections.Generic;

namespace Synthesizer
{
    public class SingleWaveProvider : IWaveProvider
    {
        public short Amplitude { set; get; }
        private double _frequency;
        public double Frequency { set; get; }

        public WaveFormat WaveFormat { get; set; }
        private double phaseAngle;
        private bool previousBufferIdentation; // true => + ; false => -; starts at negative because the wave starts at 0 and goes up so logically it must have been negative at position -1;
        public SingleWaveProvider()
        {
            Amplitude = 0;
            Frequency = 0;
            this.WaveFormat = new WaveFormat();
            previousBufferIdentation = false;
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
            for (int i = 0; i < sampleCount; i++)
            {
                buffer[offset + i] = (short)(Amplitude * Math.Sin(phaseAngle));

                bool currentBufferIdentation = buffer[offset + i] >= 0;
                if (previousBufferIdentation == currentBufferIdentation)
                {
                    UpdateFrequency();
                }
                previousBufferIdentation = currentBufferIdentation;


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
