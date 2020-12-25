using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace Synthesizer
{
    public class SynthData
    {
        public short OSCount;
        public short Amplitude;
        public short PitchDepth;

        public SynthData ()
        {
            // init data
            OSCount = 4;
            Amplitude = 5192;
            PitchDepth = 5;
        }
    }
    public class Synth : Pitchable
    {
        private SynthData SynthData;
        private Oscillator[] oscillators;
        public Synth(SynthData synthData) : base (synthData.PitchDepth)
        {
            SynthData = synthData;
            oscillators = new Oscillator[SynthData.OSCount];

            for (int i = 0; i < SynthData.OSCount; i++)
            {
                oscillators[i] = new Oscillator(synthData.PitchDepth);
                oscillators[i].Amplitude = SynthData.Amplitude;
            }
        }
        public void Play()
        {
            for (int i = 0; i < SynthData.OSCount; i++)
            {
                oscillators[i].Play();
            }
        }
        public void Pause()
        {
            for (int i = 0; i < SynthData.OSCount; i++)
            {
                oscillators[i].Pause();
            }
        }
        new public void AddPitch(double pitch)
        {
            int i = base.AddPitch(pitch);
            if (i != -1)
            {
                for (int j = 0; j < SynthData.OSCount; j++)
                {
                    oscillators[j].AddPitch(pitch);
                }
            }
            
        }
        new public void RemovePitch(double pitch)
        {
            int i = base.RemovePitch(pitch);
            if (i != -1)
            {
                for (int j = 0; j < SynthData.OSCount; j++)
                {
                    oscillators[j].RemovePitch(pitch);
                }
            }
        }
    }
}
