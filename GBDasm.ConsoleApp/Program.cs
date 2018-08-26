using System;
using System.IO;
using System.Diagnostics;
using GBDasm.Core;

namespace GBDasm.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ParseCommandLineArgs(args, out string outFile, out string inFile);
            if (inFile == null || outFile == null) return;

            var rom = new RomFile(inFile);
            var dasm = new Disassembler(rom, new Decoder());

            Console.WriteLine($"Disassembling \"{inFile}\"...");
            dasm.Disassemble(outFile);

            Console.WriteLine($"Wrote \"{outFile}\"");
            Process.Start(new ProcessStartInfo() { FileName = outFile, UseShellExecute = true });
        }

        private static void ParseCommandLineArgs(string[] args, out string outFile, out string inFile)
        {
            if (args.Length == 1)
            {
                inFile = args[0];
                outFile = Path.ChangeExtension(inFile, ".asm");
            }
            else if (args.Length == 3 && args[0] == "-o")
            {
                outFile = args[1];
                inFile = args[2];
            }
            else
            {
                outFile = inFile = null;
                Console.WriteLine("Usage: gbdasm [-o outfile] rom.gb");
            }
        }
    }
}
