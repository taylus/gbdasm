using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GBDasm.Core.Test
{
    [TestClass]
    public class Disassembler_Should
    {
        private Decoder decoder = new Decoder();

        [TestMethod]
        public void Disassemble_Test_Program()
        {
            var program = new byte[] { 0x00, 0x01, 0xAD, 0xDE, 0x00, 0x03 };
            var dasm = new Disassembler(new RomFile(program) { HasHeader = false }, decoder, targetLineWidth: 20);

            string expectedDisassembly =
                $"SECTION \"rom0\", ROM0{Environment.NewLine}" +
                $"nop           ;$0000{Environment.NewLine}" +
                $"ld bc, $dead  ;$0001{Environment.NewLine}" +
                $"nop           ;$0004{Environment.NewLine}" +
                $"inc bc        ;$0005";

            Assert.AreEqual(expectedDisassembly, dasm.Disassemble());
        }
    }
}
