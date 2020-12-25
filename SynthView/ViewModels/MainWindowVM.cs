using System;
using DevExpress.Mvvm.DataAnnotations;
using Synthesizer;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using Melanchall.DryWetMidi.Devices;
using Melanchall.DryWetMidi.Core;
using InputDevice = Melanchall.DryWetMidi.Devices.InputDevice;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System.Reactive.Linq;
using System.Windows.Input;
using DevExpress.Mvvm;
using System.Linq;
using DevExpress.Mvvm.Native;
using System.Collections.Generic;

namespace SynthView.ViewModels
{
    [POCOViewModel]
    public class MainWindowVM
    {
        public SynthData SynthData;
        public Synth synth;
        public virtual double Pitch { get; set; }
        public MainWindowVM()
        {
            SynthData = new SynthData();
            SynthData.OSCount = 1;
            synth = new Synth(SynthData);

            Pitch = 60;

            synth.Play();

            synth.AddPitch(30);

            InputDevice
                .GetAll()
                .ForEach(inputDevice =>
                {
                    inputDevice.EventReceived += OnEventReceived;
                    inputDevice.StartEventsListening();
                });
        }
        private void OnEventReceived(object sender, MidiEventReceivedEventArgs e)
        {
            var midiDevice = (MidiDevice)sender;
            long night = e.Event.DeltaTime;
        }

        public void ToggleNoteA()
        {
            if (!synth.HasPitch(40))
            {
                synth.AddPitch(40);
            }
            else
            {
                synth.RemovePitch(40);
            }
        }
        public void ToggleNoteB()
        {
            if (!synth.HasPitch(45))
            {
                synth.AddPitch(45);
            }
            else
            {
                synth.RemovePitch(45);
            }
        }
    }
}
