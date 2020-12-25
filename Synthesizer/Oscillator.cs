using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Synthesizer
{
    public class Oscillator : Pitchable
    {
        private short _amplitude;
        public short Amplitude {
            get { return _amplitude; }
            set
            {
                _amplitude = value;
                for (int i = 0; i < waves.Length; i++)
                {
                    waves[i].Amplitude = value;
                }
            }
        }
        
        public double Transposition { get; set; }

        private SingleWaveProvider[] waves;
        private WaveOutEvent[] waveOuts;

        public Oscillator (short pitchDepth = 5) : base (pitchDepth)
        {
            waves = new SingleWaveProvider[pitchDepth];
            waveOuts = new WaveOutEvent[pitchDepth];
            for (int i = 0; i < Pitches.Length; i++)
            {
                waveOuts[i] = new WaveOutEvent();
                waves[i] = new SingleWaveProvider();
                waves[i].Amplitude = Amplitude;
                waveOuts[i].Init(waves[i]);
                //waveOuts[i].Play();
            }
        }
        new public void AddPitch(double pitch)
        {
            int i = base.AddPitch(pitch);
            if (i != -1)
            {
                UpdateFrequency(i);
            }
        }
        new public void RemovePitch(double pitch) {
            int i = base.RemovePitch(pitch);
            if (i != -1)
            {
                UpdateFrequency(i);
            }
        }
        public void UpdateFrequency(int i)
        {
            if (Pitches[i] != 0.0d)
            {
                waves[i].Frequency = 440 * Math.Pow(2, (Pitches[i] + Transposition - 69) / 12);
            }
            else
            {
                waves[i].Frequency = 0; // math above comes to 440 instead of 0 if pitch is 0, which is stupid but oh well.
            }
        }
        public void UpdateAllFrequencies()
        {
            for (int i = 0; i < Pitches.Length; i++)
            {
                UpdateFrequency(i);
            }
        }
        public void Play()
        {
            for (int i = 0; i < waveOuts.Length; i++)
            {
                waveOuts[i].Play();
            }
        }
        public void Pause()
        {
            for (int i = 0; i < waveOuts.Length; i++)
            {
                waveOuts[i].Pause();
            }
        }
    }
}
