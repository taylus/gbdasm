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
            //TODO: make command-line args
            const string fileName = "Super Mario Land.gb";
            string inFile = $"roms\\{fileName}";
            string outFile = Path.ChangeExtension(fileName, ".asm");

            var rom = new RomFile(inFile);
            var dasm = new Disassembler(rom, new Decoder());

            Console.WriteLine($"Disassembling \"{inFile}\"...");
            dasm.Disassemble(outFile);

            Console.WriteLine($"Wrote \"{outFile}\"");
            Process.Start(new ProcessStartInfo() { FileName = outFile, UseShellExecute = true });
        }
    }
}
