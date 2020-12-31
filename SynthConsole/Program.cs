using System;
using System.Threading;
using Synthesizer;
using Melanchall.DryWetMidi.Devices;

namespace SynthConsole
{
    static class Program
    {
        enum ProgramState
        {
            Startup,
            Exit,
            SingleWaveTest,
            Unhandled
        }
        private static ProgramState state;
        private static string input;
        private static SingleWaveProvider swp;

        static void Main(string[] args)
        {
            state = ProgramState.Startup;
            swp = new SingleWaveProvider();
            swp.Frequency = 440;
            swp.Amplitude = 5000;

            Console.WriteLine("Welcome");

            do Run();
            while (state != ProgramState.Exit); ;
        }
        private static void Run()
        {
            Console.Write(">");
            ParseInput(Read());
            switch (state)
            {
                case ProgramState.SingleWaveTest:
                    SingleWaveTest();
                    break;
                case ProgramState.Exit:
                    Console.WriteLine("bye!");
                    break;
            }

        }

        private static void SingleWaveTest()
        {
            short[] buffer = new short[60];
            swp.Read(buffer, 0, 60);

            Console.WriteLine(String.Join(", ", buffer));
        }

        private static string Read()
        {
            input = Console.ReadLine();
            return input;
        }
        private static void OnEventReceived(object sender, MidiEventReceivedEventArgs e)
        {
            var midiDevice = (MidiDevice)sender;
            Console.WriteLine($"Event received from '{midiDevice.Name}' at {DateTime.Now}: {e.Event.ToString()}");
        }
        private static void ParseInput(string s)
        {
            switch (s)
            {
                case "x":
                case "exit":
                    state = ProgramState.Exit;
                    break;
                case "test":
                    state = ProgramState.SingleWaveTest;
                    break;
                default:
                    state = ProgramState.Unhandled;
                    break;
            }
        }
    }
}
