using System;
using System.Collections.Generic;
using System.Text;

namespace Synthesizer
{
    public abstract class Pitchable
    {
        private double[] _pitches;
        public double[] Pitches { get { return _pitches; } }

        public Pitchable (short i)
        {
            _pitches = new double[i];
        }
        public int AddPitch(double pitch)
        {
            for (int i = 0; i < Pitches.Length; i++)
            {
                if (Pitches[i] == 0.0d)
                {
                    Pitches[i] = pitch;
                    return i;
                }
            }
            return -1;
        }
        public int RemovePitch(double pitch)
        {
            for (int i = 0; i < Pitches.Length; i++)
            {
                if (Pitches[i] == pitch)
                {
                    Pitches[i] = 0.0d;
                    return i;
                }
            }
            return -1;
        }
        public bool HasPitch(double pitch)
        {
            for (int i = 0; i < Pitches.Length; i++)
            {
                if (Pitches[i] == pitch)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
