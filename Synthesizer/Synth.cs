using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using Windows.Devices.Enumeration;
using Windows.Devices.Midi;
using System.Threading.Tasks;
using Melanchall.DryWetMidi.Devices;
using System.Text.RegularExpressions;

namespace Synthesizer
{
    public class SynthData
    {
        public readonly short OSCount = 4;
        public readonly short PitchDepth = 8;
        public short Amplitude;
        public SynthData ()
        {
            // init data
            Amplitude = 5192;
        }
    }
    public class Synth : Pitchable
    {
        private SynthData SynthData;
        private Oscillator[] Oscillators;
        private InputDevice _inputDevice;
        public InputDevice InputDevice
        {
            get
            {
                return _inputDevice;
            }
            set
            {
                _inputDevice = value;
                _inputDevice.EventReceived += OnMIDIEvent;
                _inputDevice.StartEventsListening();
            }
        }
        public Synth(SynthData synthData) : base (synthData.PitchDepth)
        {
            SynthData = synthData;
            Oscillators = new Oscillator[SynthData.OSCount];

            for (int i = 0; i < SynthData.OSCount; i++)
            {
                Oscillators[i] = new Oscillator(synthData.PitchDepth);
                Oscillators[i].Amplitude = SynthData.Amplitude;
            }
        }
        ~Synth()
        {
            (_inputDevice as IDisposable)?.Dispose();
        }
        public void Play()
        {
            for (int i = 0; i < SynthData.OSCount; i++)
            {
                Oscillators[i].Play();
            }
        }
        public void Pause()
        {
            for (int i = 0; i < SynthData.OSCount; i++)
            {
                Oscillators[i].Pause();
            }
        }
        new public void AddPitch(double pitch)
        {
            int i = base.AddPitch(pitch);
            if (i != -1)
            {
                for (int j = 0; j < SynthData.OSCount; j++)
                {
                    Oscillators[j].AddPitch(pitch);
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
                    Oscillators[j].RemovePitch(pitch);
                }
            }
        }
        private void OnMIDIEvent(object sender, MidiEventReceivedEventArgs e)
        {
            string eventstring = e.Event.ToString();
            GroupCollection midiInfo = new Regex(@"Note On \[0\] \((?<pitch>\d+), (?<amount>\d+)\)").Match(eventstring).Groups; // trust me

            short amount;
            if (!short.TryParse(midiInfo["amount"].Value, out amount))
            {
                amount = 0;
            }

            double pitch;
            if (!double.TryParse(midiInfo["pitch"].Value, out pitch))
            {
                pitch = 0.0d;
            }

            if (amount != 0) // later i might regulate the amount, for now i just check if it is 0
            {
                AddPitch(pitch);
            }
            else
            {
                RemovePitch(pitch);
            }
        }

    }
}
