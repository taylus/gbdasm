using System;
using System.Diagnostics;
using GBDasm.Core;

namespace GBDasm.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //TODO: make command-line args
            const string inFile = @"roms\Link's Awakening.gb";
            const string outFile = @"Link's Awakening.asm";

            var rom = new RomFile(inFile);
            var dasm = new Disassembler(rom, new Decoder());

            Console.WriteLine($"Disassembling \"{inFile}\"...");
            dasm.Disassemble(outFile);

            Console.WriteLine($"Wrote \"{outFile}\"");
            Process.Start(new ProcessStartInfo() { FileName = outFile, UseShellExecute = true });
        }
    }
}
