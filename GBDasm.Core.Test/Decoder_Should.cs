using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GBDasm.Core.Test
{
    [TestClass]
    public class Decoder_Should
    {
        private Decoder decoder = new Decoder();

        [TestMethod]
        public void Decode_Multiple_Instructions()
        {
            string expected =
                $"nop{Environment.NewLine}" +
                $"ld bc, $dead{Environment.NewLine}" +
                $"nop{Environment.NewLine}" +
                $"inc bc";
            Assert.AreEqual(expected, decoder.Decode(new byte[] { 0x00, 0x01, 0xAD, 0xDE, 0x00, 0x03 }));
        }

        [TestMethod]
        public void Decode_0x00_To_Nop()
        {
            Assert.AreEqual("nop", decoder.Decode(new byte[] { 0x00 }));
        }

        [TestMethod]
        public void Decode_0x01_To_Load_BC_With_16_Bit_Immediate()
        {
            Assert.AreEqual("ld bc, $abcd", decoder.Decode(new byte[] { 0x01, 0xCD, 0xAB }));
        }

        [TestMethod]
        public void Decode_0x02_To_Load_Address_Pointed_To_By_BC_With_A()
        {
            Assert.AreEqual("ld [bc], a", decoder.Decode(new byte[] { 0x02 }));
        }

        [TestMethod]
        public void Decode_0x03_To_Increment_BC()
        {
            Assert.AreEqual("inc bc", decoder.Decode(new byte[] { 0x03 }));
        }

        [TestMethod]
        public void Decode_0x04_To_Increment_B()
        {
            Assert.AreEqual("inc b", decoder.Decode(new byte[] { 0x04 }));
        }

        [TestMethod]
        public void Decode_0x05_To_Decrement_B()
        {
            Assert.AreEqual("dec b", decoder.Decode(new byte[] { 0x05 }));
        }

        [TestMethod]
        public void Decode_0x06_To_Load_B_With_8_Bit_Immediate()
        {
            Assert.AreEqual("ld b, $ff", decoder.Decode(new byte[] { 0x06, 0xFF }));
        }

        [TestMethod]
        public void Decode_0x07_To_Rotate_A_With_Left_Carry()
        {
            Assert.AreEqual("rlc a", decoder.Decode(new byte[] { 0x07 }));
        }

        [TestMethod]
        public void Decode_0x08_To_Load_Address_With_Stack_Pointer()
        {
            Assert.AreEqual("ld [$babe], sp", decoder.Decode(new byte[] { 0x08, 0xBE, 0xBA }));
        }

        [TestMethod]
        public void Decode_0x09_To_Add_BC_To_HL()
        {
            Assert.AreEqual("add hl, bc", decoder.Decode(new byte[] { 0x09 }));
        }

        [TestMethod]
        public void Decode_0x0A_To_Load_A_From_Address_Pointed_To_By_BC()
        {
            Assert.AreEqual("ld a, [bc]", decoder.Decode(new byte[] { 0x0A }));
        }

        [TestMethod]
        public void Decode_0x0B_To_Decrement_BC()
        {
            Assert.AreEqual("dec bc", decoder.Decode(new byte[] { 0x0B }));
        }

        [TestMethod]
        public void Decode_0x0C_To_Increment_C()
        {
            Assert.AreEqual("inc c", decoder.Decode(new byte[] { 0x0C }));
        }

        [TestMethod]
        public void Decode_0x0D_To_Decrement_C()
        {
            Assert.AreEqual("dec c", decoder.Decode(new byte[] { 0x0D }));
        }

        [TestMethod]
        public void Decode_0x0E_To_Load_C_With_8_Bit_Immediate()
        {
            Assert.AreEqual("ld c, $aa", decoder.Decode(new byte[] { 0x0E, 0xAA }));
        }

        [TestMethod]
        public void Decode_0x0F_To_Rotate_A_Right_With_Carry()
        {
            Assert.AreEqual("rrc a", decoder.Decode(new byte[] { 0x0F }));
        }

        [TestMethod]
        public void Decode_0x10_To_Stop()
        {
            Assert.AreEqual("stop", decoder.Decode(new byte[] { 0x10 }));
        }

        [TestMethod]
        public void Decode_0x11_To_Load_DE_With_16_Bit_Immediate()
        {
            Assert.AreEqual("ld de, $60fe", decoder.Decode(new byte[] { 0x11, 0xFE, 0x60 }));
        }

        [TestMethod]
        public void Decode_0x12_To_Load_Address_Pointed_To_By_DE_With_A()
        {
            Assert.AreEqual("ld [de], a", decoder.Decode(new byte[] { 0x12 }));
        }

        [TestMethod]
        public void Decode_0x13_To_Increment_DE()
        {
            Assert.AreEqual("inc de", decoder.Decode(new byte[] { 0x13 }));
        }

        [TestMethod]
        public void Decode_0x14_To_Increment_D()
        {
            Assert.AreEqual("inc d", decoder.Decode(new byte[] { 0x14 }));
        }

        [TestMethod]
        public void Decode_0x15_To_Decrement_D()
        {
            Assert.AreEqual("dec d", decoder.Decode(new byte[] { 0x15 }));
        }

        [TestMethod]
        public void Decode_0x16_To_Load_D_With_8_Bit_Immediate()
        {
            Assert.AreEqual("ld d, $0d", decoder.Decode(new byte[] { 0x16, 0x0D }));
        }

        [TestMethod]
        public void Decode_0x17_To_Rotate_A_Left()
        {
            Assert.AreEqual("rl a", decoder.Decode(new byte[] { 0x17 }));
        }

        [TestMethod]
        public void Decode_0x18_To_Relative_Jump_By_Signed_Immediate()
        {
            Assert.AreEqual("jr $0003", decoder.Decode(new byte[] { 0x18, 0x01 }), "Relative jump by a positive offset should add to the current address.");
            Assert.AreEqual("jr $0000", decoder.Decode(new byte[] { 0x18, 0xFE /* -2 */ }), "Relative jump by a negative offset should subtract from the current address.");
            Assert.AreEqual("jr $000a", decoder.Decode(new byte[] { 0x18, 0xFC /* -4 */ }, startAddress:0x000c), "Relative jump by a negative offset should subtract from the current address.");
            Assert.AreEqual("jr $0000", decoder.Decode(new byte[] { 0x18, 0xC0 /* -64 */ }), "Relative jump by a negative offset should floor at $0000.");
            Assert.AreEqual("jr $0001", decoder.Decode(new byte[] { 0x18, 0x0F }, startAddress: 0xFFF0), "Relative jump by a positive offset from a high address should wrap around from $0000.");
        }
    }
}
