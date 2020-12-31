using System;
using DevExpress.Mvvm.DataAnnotations;
using Synthesizer;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System.Reactive.Linq;
//using System.Windows.Input;
using DevExpress.Mvvm;
using System.Linq;
using DevExpress.Mvvm.Native;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Windows.Devices.Enumeration;
using Windows.Devices.Midi;
using System.Threading.Tasks;
using System.Windows.Controls;
using Windows.UI.Core;
using Melanchall.DryWetMidi.Devices;

namespace SynthView.ViewModels
{
    [POCOViewModel]
    public class MainWindowVM
    {
        public SynthData SynthData;
        public Synth Synth;
        public virtual double Pitch { get; set; }
        public string[] MIDIDeviceNames;
        public MainWindowVM()
        {
            SynthData = new SynthData();
            Synth = new Synth(SynthData);

            Pitch = 60;

            Synth.Play();

            MIDIDeviceNames = InputDevice
                .GetAll()
                .Select(inputDevice => inputDevice.Name)
                .ToArray();

            Synth.InputDevice = InputDevice.GetByName("Axiom A.I.R. Mini32");

        }
        public void ToggleNoteA()
        {
            if (!Synth.HasPitch(40))
            {
                Synth.AddPitch(40);
            }
            else
            {
                Synth.RemovePitch(40);
            }
        }
        public void ToggleNoteB()
        {
            if (!Synth.HasPitch(45))
            {
                Synth.AddPitch(45);
            }
            else
            {
                Synth.RemovePitch(45);
            }
        }
    }
}
