using System;
using System.Threading;
using Synthesizer;

namespace SynthConsole
{
    static class Program
    {
        enum ProgramState
        {
            startup,
            exit,
            unhandled
        }
        static ProgramState programState;
        static private string input;
        static SynthData SynthData;
        static void Main(string[] args)
        {
            Synth synth = new Synth(SynthData);
            SynthData = new SynthData(500);
            programState = ProgramState.startup;
            ThreadPool.QueueUserWorkItem(state => synth.PlayWaveform());
            do Run();
            while (ParseInput(Read()) != ProgramState.exit);
        }
        private static void Run()
        {
            Console.Write(">");
        }
        private static string Read()
        {
            input = Console.ReadLine();
            return input;
        }

        private static ProgramState ParseInput(string s)
        {
            switch (s)
            {
                case "x":
                case "exit":
                    return ProgramState.exit;
                default:
                    return ProgramState.unhandled;
            }
        }
    }
}
