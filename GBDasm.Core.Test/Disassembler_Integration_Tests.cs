using System.IO;
using System.Text;
using System.Linq;
using System.Diagnostics;
using System.ComponentModel;
using System.Security.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GBDasm.Core.Test
{
    [TestClass]
    public class Disassembler_Integration_Tests
    {
        private const string Assembler = "rgbasm";
        private const string AssemblerFlags = "-h -L"; //disable optimizations -- we want rgbasm to assemble exactly what we give it
        private const string Linker = "rgblink";

        private const string TestRomPath = "roms";
        private const int WaitMilliseconds = 10 * 1000;

        /// <summary>
        /// Loop over every ROM in TestRomPath and for each: disassemble it, reassemble it,
        /// and assert that the reassembled ROM is bitwise identical to the original.
        /// </summary>
        /// <remarks>
        /// This test assumes RGBDS is installed and available in the system's PATH.
        /// Also, depending on how many ROMs are in TestRomPath, this test could take a LONG time.
        /// Adjust WaitMilliseconds or skip running this test altogether using its categories if it takes too long.
        /// </remarks>
        [TestMethod, TestCategory("Integration"), TestCategory("Requires_RGBDS")]
        public void Disassembly_And_Reassembly_Matches_Original_Rom()
        {
            var testRoms = Directory.GetFiles(TestRomPath, "*.*").Where(filename => filename.EndsWith(".gb") || filename.EndsWith(".gbc"));
            foreach (var testRom in testRoms)
            {
                TestRom(testRom);
            }
        }

        /// <summary>
        /// Disassemble and reassemble the given ROM and assert the reassembled copy
        /// is bitwise identical to the original.
        /// </summary>
        private static void TestRom(string romToTest)
        {
            //arrange: files and disassembler
            var rom = new RomFile(romToTest);
            var asmFile = Path.ChangeExtension(romToTest, ".asm");
            var objectFile = Path.ChangeExtension(romToTest, ".o");
            var reassembledRom = Path.GetFileNameWithoutExtension(romToTest) + "_Reassembled" + Path.GetExtension(romToTest);
            var dasm = new Disassembler(rom, new Decoder());

            //act: disassemble, reassemble, hash and compare
            try
            {
                dasm.Disassemble(asmFile);
                if (!Process.Start(new ProcessStartInfo() { FileName = Assembler, Arguments = $"{AssemblerFlags} -o \"{objectFile}\" \"{asmFile}\"" }).WaitForExit(WaitMilliseconds))
                    Assert.Fail($"Assembling \"{asmFile}\" failed or took longer than the allowed {WaitMilliseconds} milliseconds.");

                if (!Process.Start(new ProcessStartInfo() { FileName = Linker, Arguments = $"-o \"{reassembledRom}\" \"{objectFile}\"" }).WaitForExit(WaitMilliseconds))
                    Assert.Fail($"Linking \"{reassembledRom}\" failed or took longer than the allowed {WaitMilliseconds} milliseconds.");

                //assert: re-assembled ROMs are identical to originals
                Assert.AreEqual(ComputeHash(romToTest), ComputeHash(reassembledRom), $"Reassembled ROM differs from original ({romToTest})!");

                //Process.Start(new ProcessStartInfo() { FileName = "bgb", Arguments = $"\"{reassembledRom}\"" });
            }
            catch (Win32Exception ex)
            {
                //I dunno what kind of exception this throws on other platforms but this project is pretty Windows-centric anyway
                if (ex.Message.Contains("cannot find the file specified"))
                    Assert.Fail("Unable to find RGBDS (is it installed and in your PATH?)");
                else
                    throw;
            }
        }

        /// <summary>
        /// Compute and return a SHA256 hash for the given file.
        /// </summary>
        private static string ComputeHash(string filename)
        {
            using (var sha = SHA256.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    return Encoding.UTF8.GetString(sha.ComputeHash(stream));
                }
            }
        }
    }
}
