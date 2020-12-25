using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Text;

namespace Synthesizer
{
    class IEEWaveFormat : WaveFormat
    {
        public IEEWaveFormat () : base()
        {
            this.waveFormatTag = WaveFormatEncoding.IeeeFloat;
        }
    }
}
