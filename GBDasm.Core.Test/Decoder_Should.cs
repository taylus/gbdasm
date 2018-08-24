using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GBDasm.Core.Test
{
    [TestClass]
    public class Decoder_Should
    {
        private Decoder decoder = new Decoder();

        //TODO: move this! this is a test for the disassembler, not the decoder!
        //[TestMethod]
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
            Assert.AreEqual("jr $000a", decoder.Decode(new byte[] { 0x18, 0xFC /* -4 */ }, baseAddress: 0x000c), "Relative jump by a negative offset should subtract from the current address.");
            Assert.AreEqual("jr $0000", decoder.Decode(new byte[] { 0x18, 0xC0 /* -64 */ }), "Relative jump by a negative offset should floor at $0000.");
            Assert.AreEqual("jr $0001", decoder.Decode(new byte[] { 0x18, 0x0F }, baseAddress: 0xFFF0), "Relative jump by a positive offset from a high address should wrap around from $0000.");
        }

        [TestMethod]
        public void Decode_0x19_To_Add_DE_To_HL()
        {
            Assert.AreEqual("add hl, de", decoder.Decode(new byte[] { 0x19 }));
        }

        [TestMethod]
        public void Decode_0x1A_To_Load_A_From_Address_Pointed_To_By_DE()
        {
            Assert.AreEqual("ld a, [de]", decoder.Decode(new byte[] { 0x1A }));
        }

        [TestMethod]
        public void Decode_0x1B_To_Decrement_DE()
        {
            Assert.AreEqual("dec de", decoder.Decode(new byte[] { 0x1B }));
        }

        [TestMethod]
        public void Decode_0x1C_To_Increment_E()
        {
            Assert.AreEqual("inc e", decoder.Decode(new byte[] { 0x1C }));
        }

        [TestMethod]
        public void Decode_0x1D_To_Decrement_E()
        {
            Assert.AreEqual("dec e", decoder.Decode(new byte[] { 0x1D }));
        }

        [TestMethod]
        public void Decode_0x1E_To_Load_E_With_8_Bit_Immediate()
        {
            Assert.AreEqual("ld e, $5e", decoder.Decode(new byte[] { 0x1E, 0x5E }));
        }

        [TestMethod]
        public void Decode_0x1F_To_Rotate_A_Right()
        {
            Assert.AreEqual("rr a", decoder.Decode(new byte[] { 0x1F }));
        }

        [TestMethod]
        public void Decode_0x20_To_Relative_Jump_By_Signed_Immediate_If_Last_Result_Was_Not_Zero()
        {
            Assert.AreEqual("jr nz, $0003", decoder.Decode(new byte[] { 0x20, 0x01 }));
        }

        [TestMethod]
        public void Decode_0x21_To_Load_HL_With_16_Bit_Immediate()
        {
            Assert.AreEqual("ld hl, $cafe", decoder.Decode(new byte[] { 0x21, 0xFE, 0xCA }));
        }

        [TestMethod]
        public void Decode_0x22_To_Load_Address_Pointed_To_By_HL_With_A_And_Increment_HL()
        {
            Assert.AreEqual("ldi [hl], a", decoder.Decode(new byte[] { 0x22 }));
        }

        [TestMethod]
        public void Decode_0x23_To_Increment_HL()
        {
            Assert.AreEqual("inc hl", decoder.Decode(new byte[] { 0x23 }));
        }

        [TestMethod]
        public void Decode_0x24_To_Increment_H()
        {
            Assert.AreEqual("inc h", decoder.Decode(new byte[] { 0x24 }));
        }

        [TestMethod]
        public void Decode_0x25_To_Decrement_H()
        {
            Assert.AreEqual("dec h", decoder.Decode(new byte[] { 0x25 }));
        }

        [TestMethod]
        public void Decode_0x26_To_Load_H_With_8_Bit_Immediate()
        {
            Assert.AreEqual("ld h, $42", decoder.Decode(new byte[] { 0x26, 0x42 }));
        }

        [TestMethod]
        public void Decode_0x27_To_Decimal_Adjust_A_For_Correct_Result_After_Binary_Coded_Decimal_Arithmetic_Instruction()
        {
            Assert.AreEqual("daa", decoder.Decode(new byte[] { 0x27 }));
        }

        [TestMethod]
        public void Decode_0x28_To_Relative_Jump_By_Signed_Immediate_If_Last_Result_Was_Zero()
        {
            Assert.AreEqual("jr z, $0003", decoder.Decode(new byte[] { 0x28, 0x01 }));
        }

        [TestMethod]
        public void Decode_0x29_To_Add_HL_To_HL()
        {
            Assert.AreEqual("add hl, hl", decoder.Decode(new byte[] { 0x29 }));
        }

        [TestMethod]
        public void Decode_0x2A_To_Load_A_From_Address_Pointed_To_By_HL_And_Increment_HL()
        {
            Assert.AreEqual("ldi a, [hl]", decoder.Decode(new byte[] { 0x2A }));
        }

        [TestMethod]
        public void Decode_0x2B_To_Decrement_HL()
        {
            Assert.AreEqual("dec hl", decoder.Decode(new byte[] { 0x2B }));
        }

        [TestMethod]
        public void Decode_0x2C_To_Increment_L()
        {
            Assert.AreEqual("inc l", decoder.Decode(new byte[] { 0x2C }));
        }

        [TestMethod]
        public void Decode_0x2D_To_Decrement_L()
        {
            Assert.AreEqual("dec l", decoder.Decode(new byte[] { 0x2D }));
        }

        [TestMethod]
        public void Decode_0x2E_To_Load_L_With_8_Bit_Immediate()
        {
            Assert.AreEqual("ld l, $01", decoder.Decode(new byte[] { 0x2E, 0x01 }));
        }

        [TestMethod]
        public void Decode_0x2F_To_Bitwise_Complement_A()
        {
            Assert.AreEqual("cpl", decoder.Decode(new byte[] { 0x2F }));
        }

        [TestMethod]
        public void Decode_0x30_To_Relative_Jump_By_Signed_Immediate_If_Last_Result_Caused_No_Carry()
        {
            Assert.AreEqual("jr nc, $0005", decoder.Decode(new byte[] { 0x30, 0x03 }));
        }

        [TestMethod]
        public void Decode_0x31_To_Load_Stack_Pointer_With_16_Bit_Immediate()
        {
            Assert.AreEqual("ld sp, $00aa", decoder.Decode(new byte[] { 0x31, 0xAA, 0x00 }));
        }

        [TestMethod]
        public void Decode_0x32_To_Load_Address_Pointed_To_By_HL_From_A_And_Decrement_HL()
        {
            Assert.AreEqual("ldd [hl], a", decoder.Decode(new byte[] { 0x32 }));
        }

        [TestMethod]
        public void Decode_0x33_To_Increment_Stack_Pointer()
        {
            Assert.AreEqual("inc sp", decoder.Decode(new byte[] { 0x33 }));
        }

        [TestMethod]
        public void Decode_0x34_To_Increment_Value_Pointed_To_By_HL()
        {
            Assert.AreEqual("inc [hl]", decoder.Decode(new byte[] { 0x34 }));
        }

        [TestMethod]
        public void Decode_0x35_To_Decrement_Value_Pointed_To_By_HL()
        {
            Assert.AreEqual("dec [hl]", decoder.Decode(new byte[] { 0x35 }));
        }

        [TestMethod]
        public void Decode_0x36_To_Load_Address_Pointed_To_By_HL_With_8_Bit_Immediate()
        {
            Assert.AreEqual("ld [hl], $5c", decoder.Decode(new byte[] { 0x36, 0x5C }));
        }

        [TestMethod]
        public void Decode_0x37_To_Set_Carry_Flag()
        {
            Assert.AreEqual("scf", decoder.Decode(new byte[] { 0x37 }));
        }

        [TestMethod]
        public void Decode_0x38_To_Relative_Jump_By_Signed_Immediate_If_Last_Result_Caused_Carry()
        {
            Assert.AreEqual("jr c, $0003", decoder.Decode(new byte[] { 0x38, 0x01 }));
        }

        [TestMethod]
        public void Decode_0x39_To_Add_Stack_Pointer_To_HL()
        {
            Assert.AreEqual("add hl, sp", decoder.Decode(new byte[] { 0x39 }));
        }

        [TestMethod]
        public void Decode_0x3A_To_Load_A_From_Address_Pointed_To_By_HL_And_Decrement_HL()
        {
            Assert.AreEqual("ldd a, [hl]", decoder.Decode(new byte[] { 0x3A }));
        }

        [TestMethod]
        public void Decode_0x3B_To_Decrement_Stack_Pointer()
        {
            Assert.AreEqual("dec sp", decoder.Decode(new byte[] { 0x3B }));
        }

        [TestMethod]
        public void Decode_0x3C_To_Increment_A()
        {
            Assert.AreEqual("inc a", decoder.Decode(new byte[] { 0x3C }));
        }

        [TestMethod]
        public void Decode_0x3D_To_Decrement_A()
        {
            Assert.AreEqual("dec a", decoder.Decode(new byte[] { 0x3D }));
        }

        [TestMethod]
        public void Decode_0x3E_To_Load_A_With_8_Bit_Immediate()
        {
            Assert.AreEqual("ld a, $aa", decoder.Decode(new byte[] { 0x3E, 0xAA }));
        }

        [TestMethod]
        public void Decode_0x3F_To_Clear_Carry_Flag()
        {
            Assert.AreEqual("ccf", decoder.Decode(new byte[] { 0x3F }));
        }

        [TestMethod]
        public void Decode_0x40_To_Load_B_From_B()
        {
            Assert.AreEqual("ld b, b", decoder.Decode(new byte[] { 0x40 }));
        }

        [TestMethod]
        public void Decode_0x41_To_Load_B_From_C()
        {
            Assert.AreEqual("ld b, c", decoder.Decode(new byte[] { 0x41 }));
        }

        [TestMethod]
        public void Decode_0x42_To_Load_B_From_D()
        {
            Assert.AreEqual("ld b, d", decoder.Decode(new byte[] { 0x42 }));
        }

        [TestMethod]
        public void Decode_0x43_To_Load_B_From_E()
        {
            Assert.AreEqual("ld b, e", decoder.Decode(new byte[] { 0x43 }));
        }

        [TestMethod]
        public void Decode_0x44_To_Load_B_From_H()
        {
            Assert.AreEqual("ld b, h", decoder.Decode(new byte[] { 0x44 }));
        }

        [TestMethod]
        public void Decode_0x45_To_Load_B_From_L()
        {
            Assert.AreEqual("ld b, l", decoder.Decode(new byte[] { 0x45 }));
        }

        [TestMethod]
        public void Decode_0x46_To_Load_B_From_Address_Pointed_To_By_HL()
        {
            Assert.AreEqual("ld b, [hl]", decoder.Decode(new byte[] { 0x46 }));
        }

        [TestMethod]
        public void Decode_0x47_To_Load_B_From_A()
        {
            Assert.AreEqual("ld b, a", decoder.Decode(new byte[] { 0x47 }));
        }

        [TestMethod]
        public void Decode_0x48_To_Load_C_From_B()
        {
            Assert.AreEqual("ld c, b", decoder.Decode(new byte[] { 0x48 }));
        }

        [TestMethod]
        public void Decode_0x49_To_Load_C_From_C()
        {
            Assert.AreEqual("ld c, c", decoder.Decode(new byte[] { 0x49 }));
        }

        [TestMethod]
        public void Decode_0x4A_To_Load_C_From_D()
        {
            Assert.AreEqual("ld c, d", decoder.Decode(new byte[] { 0x4A }));
        }

        [TestMethod]
        public void Decode_0x4B_To_Load_C_From_E()
        {
            Assert.AreEqual("ld c, e", decoder.Decode(new byte[] { 0x4B }));
        }

        [TestMethod]
        public void Decode_0x4C_To_Load_C_From_H()
        {
            Assert.AreEqual("ld c, h", decoder.Decode(new byte[] { 0x4C }));
        }

        [TestMethod]
        public void Decode_0x4D_To_Load_C_From_L()
        {
            Assert.AreEqual("ld c, l", decoder.Decode(new byte[] { 0x4D }));
        }

        [TestMethod]
        public void Decode_0x4E_To_Load_C_From_Address_Pointed_To_By_HL()
        {
            Assert.AreEqual("ld c, [hl]", decoder.Decode(new byte[] { 0x4E }));
        }

        [TestMethod]
        public void Decode_0x4F_To_Load_C_From_A()
        {
            Assert.AreEqual("ld c, a", decoder.Decode(new byte[] { 0x4F }));
        }

        [TestMethod]
        public void Decode_0x50_To_Load_D_From_B()
        {
            Assert.AreEqual("ld d, b", decoder.Decode(new byte[] { 0x50 }));
        }

        [TestMethod]
        public void Decode_0x51_To_Load_D_From_C()
        {
            Assert.AreEqual("ld d, c", decoder.Decode(new byte[] { 0x51 }));
        }

        [TestMethod]
        public void Decode_0x52_To_Load_D_From_D()
        {
            Assert.AreEqual("ld d, d", decoder.Decode(new byte[] { 0x52 }));
        }

        [TestMethod]
        public void Decode_0x53_To_Load_D_From_E()
        {
            Assert.AreEqual("ld d, e", decoder.Decode(new byte[] { 0x53 }));
        }

        [TestMethod]
        public void Decode_0x54_To_Load_D_From_H()
        {
            Assert.AreEqual("ld d, h", decoder.Decode(new byte[] { 0x54 }));
        }

        [TestMethod]
        public void Decode_0x55_To_Load_D_From_L()
        {
            Assert.AreEqual("ld d, l", decoder.Decode(new byte[] { 0x55 }));
        }

        [TestMethod]
        public void Decode_0x56_To_Load_D_From_Address_Pointed_To_By_HL()
        {
            Assert.AreEqual("ld d, [hl]", decoder.Decode(new byte[] { 0x56 }));
        }

        [TestMethod]
        public void Decode_0x57_To_Load_D_From_A()
        {
            Assert.AreEqual("ld d, a", decoder.Decode(new byte[] { 0x57 }));
        }

        [TestMethod]
        public void Decode_0x58_To_Load_E_From_B()
        {
            Assert.AreEqual("ld e, b", decoder.Decode(new byte[] { 0x58 }));
        }

        [TestMethod]
        public void Decode_0x59_To_Load_E_From_C()
        {
            Assert.AreEqual("ld e, c", decoder.Decode(new byte[] { 0x59 }));
        }

        [TestMethod]
        public void Decode_0x5A_To_Load_E_From_D()
        {
            Assert.AreEqual("ld e, d", decoder.Decode(new byte[] { 0x5A }));
        }

        [TestMethod]
        public void Decode_0x5B_To_Load_E_From_E()
        {
            Assert.AreEqual("ld e, e", decoder.Decode(new byte[] { 0x5B }));
        }

        [TestMethod]
        public void Decode_0x5C_To_Load_E_From_H()
        {
            Assert.AreEqual("ld e, h", decoder.Decode(new byte[] { 0x5C }));
        }

        [TestMethod]
        public void Decode_0x5D_To_Load_E_From_L()
        {
            Assert.AreEqual("ld e, l", decoder.Decode(new byte[] { 0x5D }));
        }

        [TestMethod]
        public void Decode_0x5E_To_Load_E_From_Address_Pointed_To_By_HL()
        {
            Assert.AreEqual("ld e, [hl]", decoder.Decode(new byte[] { 0x5E }));
        }

        [TestMethod]
        public void Decode_0x5F_To_Load_E_From_A()
        {
            Assert.AreEqual("ld e, a", decoder.Decode(new byte[] { 0x5F }));
        }

        [TestMethod]
        public void Decode_0x60_To_Load_H_From_B()
        {
            Assert.AreEqual("ld h, b", decoder.Decode(new byte[] { 0x60 }));
        }

        [TestMethod]
        public void Decode_0x61_To_Load_H_From_C()
        {
            Assert.AreEqual("ld h, c", decoder.Decode(new byte[] { 0x61 }));
        }

        [TestMethod]
        public void Decode_0x62_To_Load_H_From_D()
        {
            Assert.AreEqual("ld h, d", decoder.Decode(new byte[] { 0x62 }));
        }

        [TestMethod]
        public void Decode_0x63_To_Load_H_From_E()
        {
            Assert.AreEqual("ld h, e", decoder.Decode(new byte[] { 0x63 }));
        }

        [TestMethod]
        public void Decode_0x64_To_Load_H_From_H()
        {
            Assert.AreEqual("ld h, h", decoder.Decode(new byte[] { 0x64 }));
        }

        [TestMethod]
        public void Decode_0x65_To_Load_H_From_L()
        {
            Assert.AreEqual("ld h, l", decoder.Decode(new byte[] { 0x65 }));
        }

        [TestMethod]
        public void Decode_0x66_To_Load_H_From_Address_Pointed_To_By_HL()
        {
            Assert.AreEqual("ld h, [hl]", decoder.Decode(new byte[] { 0x66 }));
        }

        [TestMethod]
        public void Decode_0x67_To_Load_H_From_A()
        {
            Assert.AreEqual("ld h, a", decoder.Decode(new byte[] { 0x67 }));
        }

        [TestMethod]
        public void Decode_0x68_To_Load_L_From_B()
        {
            Assert.AreEqual("ld l, b", decoder.Decode(new byte[] { 0x68 }));
        }

        [TestMethod]
        public void Decode_0x69_To_Load_L_From_C()
        {
            Assert.AreEqual("ld l, c", decoder.Decode(new byte[] { 0x69 }));
        }

        [TestMethod]
        public void Decode_0x6A_To_Load_L_From_D()
        {
            Assert.AreEqual("ld l, d", decoder.Decode(new byte[] { 0x6A }));
        }

        [TestMethod]
        public void Decode_0x6B_To_Load_L_From_E()
        {
            Assert.AreEqual("ld l, e", decoder.Decode(new byte[] { 0x6B }));
        }

        [TestMethod]
        public void Decode_0x6C_To_Load_L_From_H()
        {
            Assert.AreEqual("ld l, h", decoder.Decode(new byte[] { 0x6C }));
        }

        [TestMethod]
        public void Decode_0x6D_To_Load_L_From_L()
        {
            Assert.AreEqual("ld l, l", decoder.Decode(new byte[] { 0x6D }));
        }

        [TestMethod]
        public void Decode_0x6E_To_Load_L_From_Address_Pointed_To_By_HL()
        {
            Assert.AreEqual("ld l, [hl]", decoder.Decode(new byte[] { 0x6E }));
        }

        [TestMethod]
        public void Decode_0x6F_To_Load_L_From_A()
        {
            Assert.AreEqual("ld l, a", decoder.Decode(new byte[] { 0x6F }));
        }

        [TestMethod]
        public void Decode_0x70_To_Load_Address_Pointed_To_By_HL_With_B()
        {
            Assert.AreEqual("ld [hl], b", decoder.Decode(new byte[] { 0x70 }));
        }

        [TestMethod]
        public void Decode_0x71_To_Load_Address_Pointed_To_By_HL_With_C()
        {
            Assert.AreEqual("ld [hl], c", decoder.Decode(new byte[] { 0x71 }));
        }

        [TestMethod]
        public void Decode_0x72_To_Load_Address_Pointed_To_By_HL_With_D()
        {
            Assert.AreEqual("ld [hl], d", decoder.Decode(new byte[] { 0x72 }));
        }

        [TestMethod]
        public void Decode_0x73_To_Load_Address_Pointed_To_By_HL_With_E()
        {
            Assert.AreEqual("ld [hl], e", decoder.Decode(new byte[] { 0x73 }));
        }

        [TestMethod]
        public void Decode_0x74_To_Load_Address_Pointed_To_By_HL_With_H()
        {
            Assert.AreEqual("ld [hl], h", decoder.Decode(new byte[] { 0x74 }));
        }

        [TestMethod]
        public void Decode_0x75_To_Load_Address_Pointed_To_By_HL_With_L()
        {
            Assert.AreEqual("ld [hl], l", decoder.Decode(new byte[] { 0x75 }));
        }

        [TestMethod]
        public void Decode_0x76_To_Halt()
        {
            Assert.AreEqual("halt", decoder.Decode(new byte[] { 0x76 }));
        }

        [TestMethod]
        public void Decode_0x77_To_Load_Address_Pointed_To_By_HL_With_A()
        {
            Assert.AreEqual("ld [hl], a", decoder.Decode(new byte[] { 0x77 }));
        }

        [TestMethod]
        public void Decode_0x78_To_Load_A_From_B()
        {
            Assert.AreEqual("ld a, b", decoder.Decode(new byte[] { 0x78 }));
        }

        [TestMethod]
        public void Decode_0x79_To_Load_A_From_C()
        {
            Assert.AreEqual("ld a, c", decoder.Decode(new byte[] { 0x79 }));
        }

        [TestMethod]
        public void Decode_0x7A_To_Load_A_From_D()
        {
            Assert.AreEqual("ld a, d", decoder.Decode(new byte[] { 0x7A }));
        }

        [TestMethod]
        public void Decode_0x7B_To_Load_A_From_E()
        {
            Assert.AreEqual("ld a, e", decoder.Decode(new byte[] { 0x7B }));
        }

        [TestMethod]
        public void Decode_0x7C_To_Load_A_From_H()
        {
            Assert.AreEqual("ld a, h", decoder.Decode(new byte[] { 0x7C }));
        }

        [TestMethod]
        public void Decode_0x7D_To_Load_A_From_L()
        {
            Assert.AreEqual("ld a, l", decoder.Decode(new byte[] { 0x7D }));
        }

        [TestMethod]
        public void Decode_0x7E_To_Load_A_From_Address_Pointed_To_By_HL()
        {
            Assert.AreEqual("ld a, [hl]", decoder.Decode(new byte[] { 0x7E }));
        }

        [TestMethod]
        public void Decode_0x7F_To_Load_A_From_A()
        {
            Assert.AreEqual("ld a, a", decoder.Decode(new byte[] { 0x7F }));
        }

        [TestMethod]
        public void Decode_0x80_To_Add_B_To_A()
        {
            Assert.AreEqual("add a, b", decoder.Decode(new byte[] { 0x80 }));
        }

        [TestMethod]
        public void Decode_0x81_To_Add_C_To_A()
        {
            Assert.AreEqual("add a, c", decoder.Decode(new byte[] { 0x81 }));
        }

        [TestMethod]
        public void Decode_0x82_To_Add_D_To_A()
        {
            Assert.AreEqual("add a, d", decoder.Decode(new byte[] { 0x82 }));
        }

        [TestMethod]
        public void Decode_0x83_To_Add_E_To_A()
        {
            Assert.AreEqual("add a, e", decoder.Decode(new byte[] { 0x83 }));
        }

        [TestMethod]
        public void Decode_0x84_To_Add_H_To_A()
        {
            Assert.AreEqual("add a, h", decoder.Decode(new byte[] { 0x84 }));
        }

        [TestMethod]
        public void Decode_0x85_To_Add_L_To_A()
        {
            Assert.AreEqual("add a, l", decoder.Decode(new byte[] { 0x85 }));
        }

        [TestMethod]
        public void Decode_0x86_To_Add_Address_Pointed_To_By_HL_To_A()
        {
            Assert.AreEqual("add a, [hl]", decoder.Decode(new byte[] { 0x86 }));
        }

        [TestMethod]
        public void Decode_0x87_To_Add_A_To_A()
        {
            Assert.AreEqual("add a, a", decoder.Decode(new byte[] { 0x87 }));
        }

        [TestMethod]
        public void Decode_0x88_To_Add_B_And_Carry_Flag_To_A()
        {
            Assert.AreEqual("adc a, b", decoder.Decode(new byte[] { 0x88 }));
        }

        [TestMethod]
        public void Decode_0x89_To_Add_C_And_Carry_Flag_To_A()
        {
            Assert.AreEqual("adc a, c", decoder.Decode(new byte[] { 0x89 }));
        }

        [TestMethod]
        public void Decode_0x8A_To_Add_D_And_Carry_Flag_To_A()
        {
            Assert.AreEqual("adc a, d", decoder.Decode(new byte[] { 0x8A }));
        }

        [TestMethod]
        public void Decode_0x8B_To_Add_E_And_Carry_Flag_To_A()
        {
            Assert.AreEqual("adc a, e", decoder.Decode(new byte[] { 0x8B }));
        }

        [TestMethod]
        public void Decode_0x8C_To_Add_H_And_Carry_Flag_To_A()
        {
            Assert.AreEqual("adc a, h", decoder.Decode(new byte[] { 0x8C }));
        }

        [TestMethod]
        public void Decode_0x8D_To_Add_L_And_Carry_Flag_To_A()
        {
            Assert.AreEqual("adc a, l", decoder.Decode(new byte[] { 0x8D }));
        }

        [TestMethod]
        public void Decode_0x8E_To_Add_Address_Pointed_To_By_HL_And_Carry_Flag_To_A()
        {
            Assert.AreEqual("adc a, [hl]", decoder.Decode(new byte[] { 0x8E }));
        }

        [TestMethod]
        public void Decode_0x8F_To_Add_A_And_Carry_Flag_To_A()
        {
            Assert.AreEqual("adc a, a", decoder.Decode(new byte[] { 0x8F }));
        }

        [TestMethod]
        public void Decode_0x90_To_Subtract_B_From_A()
        {
            Assert.AreEqual("sub a, b", decoder.Decode(new byte[] { 0x90 }));
        }

        [TestMethod]
        public void Decode_0x91_To_Subtract_C_From_A()
        {
            Assert.AreEqual("sub a, c", decoder.Decode(new byte[] { 0x91 }));
        }

        [TestMethod]
        public void Decode_0x92_To_Subtract_D_From_A()
        {
            Assert.AreEqual("sub a, d", decoder.Decode(new byte[] { 0x92 }));
        }

        [TestMethod]
        public void Decode_0x93_To_Subtract_E_From_A()
        {
            Assert.AreEqual("sub a, e", decoder.Decode(new byte[] { 0x93 }));
        }

        [TestMethod]
        public void Decode_0x94_To_Subtract_H_From_A()
        {
            Assert.AreEqual("sub a, h", decoder.Decode(new byte[] { 0x94 }));
        }

        [TestMethod]
        public void Decode_0x95_To_Subtract_L_From_A()
        {
            Assert.AreEqual("sub a, l", decoder.Decode(new byte[] { 0x95 }));
        }

        [TestMethod]
        public void Decode_0x96_To_Subtract_Address_Pointed_To_By_HL_From_A()
        {
            Assert.AreEqual("sub a, [hl]", decoder.Decode(new byte[] { 0x96 }));
        }

        [TestMethod]
        public void Decode_0x97_To_Subtract_A_From_A()
        {
            Assert.AreEqual("sub a, a", decoder.Decode(new byte[] { 0x97 }));
        }

        [TestMethod]
        public void Decode_0x98_To_Subtract_B_And_Carry_Flag_From_A()
        {
            Assert.AreEqual("sbc a, b", decoder.Decode(new byte[] { 0x98 }));
        }

        [TestMethod]
        public void Decode_0x99_To_Subtract_C_And_Carry_Flag_From_A()
        {
            Assert.AreEqual("sbc a, c", decoder.Decode(new byte[] { 0x99 }));
        }

        [TestMethod]
        public void Decode_0x9A_To_Subtract_D_And_Carry_Flag_From_A()
        {
            Assert.AreEqual("sbc a, d", decoder.Decode(new byte[] { 0x9A }));
        }

        [TestMethod]
        public void Decode_0x9B_To_Subtract_E_And_Carry_Flag_From_A()
        {
            Assert.AreEqual("sbc a, e", decoder.Decode(new byte[] { 0x9B }));
        }

        [TestMethod]
        public void Decode_0x9C_To_Subtract_H_And_Carry_Flag_From_A()
        {
            Assert.AreEqual("sbc a, h", decoder.Decode(new byte[] { 0x9C }));
        }

        [TestMethod]
        public void Decode_0x9D_To_Subtract_L_And_Carry_Flag_From_A()
        {
            Assert.AreEqual("sbc a, l", decoder.Decode(new byte[] { 0x9D }));
        }

        [TestMethod]
        public void Decode_0x9E_To_Subtract_Address_Pointed_To_By_HL_And_Carry_Flag_From_A()
        {
            Assert.AreEqual("sbc a, [hl]", decoder.Decode(new byte[] { 0x9E }));
        }

        [TestMethod]
        public void Decode_0x9F_To_Subtract_A_And_Carry_Flag_From_A()
        {
            Assert.AreEqual("sbc a, a", decoder.Decode(new byte[] { 0x9F }));
        }

        [TestMethod]
        public void Decode_0xA0_To_Logical_And_B_With_A()
        {
            Assert.AreEqual("and b", decoder.Decode(new byte[] { 0xA0 }));
        }

        [TestMethod]
        public void Decode_0xA1_To_Logical_And_C_With_A()
        {
            Assert.AreEqual("and c", decoder.Decode(new byte[] { 0xA1 }));
        }

        [TestMethod]
        public void Decode_0xA2_To_Logical_And_D_With_A()
        {
            Assert.AreEqual("and d", decoder.Decode(new byte[] { 0xA2 }));
        }

        [TestMethod]
        public void Decode_0xA3_To_Logical_And_E_With_A()
        {
            Assert.AreEqual("and e", decoder.Decode(new byte[] { 0xA3 }));
        }

        [TestMethod]
        public void Decode_0xA4_To_Logical_And_H_With_A()
        {
            Assert.AreEqual("and h", decoder.Decode(new byte[] { 0xA4 }));
        }

        [TestMethod]
        public void Decode_0xA5_To_Logical_And_L_With_A()
        {
            Assert.AreEqual("and l", decoder.Decode(new byte[] { 0xA5 }));
        }

        [TestMethod]
        public void Decode_0xA6_To_Logical_And_Address_Pointed_To_By_HL_With_A()
        {
            Assert.AreEqual("and [hl]", decoder.Decode(new byte[] { 0xA6 }));
        }

        [TestMethod]
        public void Decode_0xA7_To_Logical_And_A_With_A()
        {
            Assert.AreEqual("and a", decoder.Decode(new byte[] { 0xA7 }));
        }

        [TestMethod]
        public void Decode_0xA8_To_Exclusive_Or_B_With_A()
        {
            Assert.AreEqual("xor b", decoder.Decode(new byte[] { 0xA8 }));
        }

        [TestMethod]
        public void Decode_0xA9_To_Exclusive_Or_C_With_A()
        {
            Assert.AreEqual("xor c", decoder.Decode(new byte[] { 0xA9 }));
        }

        [TestMethod]
        public void Decode_0xAA_To_Exclusive_Or_D_With_A()
        {
            Assert.AreEqual("xor d", decoder.Decode(new byte[] { 0xAA }));
        }

        [TestMethod]
        public void Decode_0xAB_To_Exclusive_Or_E_With_A()
        {
            Assert.AreEqual("xor e", decoder.Decode(new byte[] { 0xAB }));
        }

        [TestMethod]
        public void Decode_0xAC_To_Exclusive_Or_H_With_A()
        {
            Assert.AreEqual("xor h", decoder.Decode(new byte[] { 0xAC }));
        }

        [TestMethod]
        public void Decode_0xAD_To_Exclusive_Or_L_With_A()
        {
            Assert.AreEqual("xor l", decoder.Decode(new byte[] { 0xAD }));
        }

        [TestMethod]
        public void Decode_0xAE_To_Exclusive_Or_Address_Pointed_To_By_HL_With_A()
        {
            Assert.AreEqual("xor [hl]", decoder.Decode(new byte[] { 0xAE }));
        }

        [TestMethod]
        public void Decode_0xAF_To_Exclusive_Or_A_With_A()
        {
            Assert.AreEqual("xor a", decoder.Decode(new byte[] { 0xAF }));
        }

        [TestMethod]
        public void Decode_0xB0_To_Logical_Or_B_With_A()
        {
            Assert.AreEqual("or b", decoder.Decode(new byte[] { 0xB0 }));
        }

        [TestMethod]
        public void Decode_0xB1_To_Logical_Or_C_With_A()
        {
            Assert.AreEqual("or c", decoder.Decode(new byte[] { 0xB1 }));
        }

        [TestMethod]
        public void Decode_0xB2_To_Logical_Or_D_With_A()
        {
            Assert.AreEqual("or d", decoder.Decode(new byte[] { 0xB2 }));
        }

        [TestMethod]
        public void Decode_0xB3_To_Logical_Or_E_With_A()
        {
            Assert.AreEqual("or e", decoder.Decode(new byte[] { 0xB3 }));
        }

        [TestMethod]
        public void Decode_0xB4_To_Logical_Or_H_With_A()
        {
            Assert.AreEqual("or h", decoder.Decode(new byte[] { 0xB4 }));
        }

        [TestMethod]
        public void Decode_0xB5_To_Logical_Or_L_With_A()
        {
            Assert.AreEqual("or l", decoder.Decode(new byte[] { 0xB5 }));
        }

        [TestMethod]
        public void Decode_0xB6_To_Logical_Or_Address_Pointed_To_By_HL_With_A()
        {
            Assert.AreEqual("or [hl]", decoder.Decode(new byte[] { 0xB6 }));
        }

        [TestMethod]
        public void Decode_0xB7_To_Logical_Or_A_With_A()
        {
            Assert.AreEqual("or a", decoder.Decode(new byte[] { 0xB7 }));
        }

        [TestMethod]
        public void Decode_0xB8_To_Compare_B_To_A_And_Set_Flags_As_If_Subtraction_Occurred()
        {
            Assert.AreEqual("cp b", decoder.Decode(new byte[] { 0xB8 }));
        }

        [TestMethod]
        public void Decode_0xB9_To_Compare_C_To_A_And_Set_Flags_As_If_Subtraction_Occurred()
        {
            Assert.AreEqual("cp c", decoder.Decode(new byte[] { 0xB9 }));
        }

        [TestMethod]
        public void Decode_0xBA_To_Compare_D_To_A_And_Set_Flags_As_If_Subtraction_Occurred()
        {
            Assert.AreEqual("cp d", decoder.Decode(new byte[] { 0xBA }));
        }

        [TestMethod]
        public void Decode_0xBB_To_Compare_E_To_A_And_Set_Flags_As_If_Subtraction_Occurred()
        {
            Assert.AreEqual("cp e", decoder.Decode(new byte[] { 0xBB }));
        }

        [TestMethod]
        public void Decode_0xBC_To_Compare_H_To_A_And_Set_Flags_As_If_Subtraction_Occurred()
        {
            Assert.AreEqual("cp h", decoder.Decode(new byte[] { 0xBC }));
        }

        [TestMethod]
        public void Decode_0xBD_To_Compare_L_To_A_And_Set_Flags_As_If_Subtraction_Occurred()
        {
            Assert.AreEqual("cp l", decoder.Decode(new byte[] { 0xBD }));
        }

        [TestMethod]
        public void Decode_0xBE_To_Compare_Address_Pointed_To_By_HL_To_A_And_Set_Flags_As_If_Subtraction_Occurred()
        {
            Assert.AreEqual("cp [hl]", decoder.Decode(new byte[] { 0xBE }));
        }

        [TestMethod]
        public void Decode_0xBF_To_Compare_A_To_A_And_Set_Flags_As_If_Subtraction_Occurred()
        {
            Assert.AreEqual("cp a", decoder.Decode(new byte[] { 0xBF }));
        }

        [TestMethod]
        public void Decode_0xC0_To_Return_If_Last_Result_Was_Not_Zero()
        {
            Assert.AreEqual("ret nz", decoder.Decode(new byte[] { 0xC0 }));
        }

        [TestMethod]
        public void Decode_0xC1_To_Pop_16_Bit_Value_From_Stack_Into_BC()
        {
            Assert.AreEqual("pop bc", decoder.Decode(new byte[] { 0xC1 }));
        }

        [TestMethod]
        public void Decode_0xC2_To_Absolute_Jump_To_16_Bit_Address_If_Last_Result_Was_Not_Zero()
        {
            Assert.AreEqual("jp nz, $abcd", decoder.Decode(new byte[] { 0xC2, 0xCD, 0xAB }));
        }

        [TestMethod]
        public void Decode_0xC3_To_Absolute_Jump_To_16_Bit_Address_If_Last_Result_Was_Not_Zero()
        {
            Assert.AreEqual("jp $abcd", decoder.Decode(new byte[] { 0xC3, 0xCD, 0xAB }));
        }

        [TestMethod]
        public void Decode_0xC4_To_Call_Routine_At_16_Bit_Address_If_Last_Result_Was_Not_Zero()
        {
            Assert.AreEqual("call nz, $abcd", decoder.Decode(new byte[] { 0xC4, 0xCD, 0xAB }));
        }

        [TestMethod]
        public void Decode_0xC5_To_Push_BC_Onto_Stack()
        {
            Assert.AreEqual("push bc", decoder.Decode(new byte[] { 0xC5 }));
        }

        [TestMethod]
        public void Decode_0xC6_To_Add_8_Bit_Immediate_To_A()
        {
            Assert.AreEqual("add a, $bc", decoder.Decode(new byte[] { 0xC6, 0xBC }));
        }

        [TestMethod]
        public void Decode_0xC7_To_Call_Restart_Vector_Zero()
        {
            Assert.AreEqual("rst $00", decoder.Decode(new byte[] { 0xC7 }));
        }

        [TestMethod]
        public void Decode_0xC8_To_Return_From_Subroutine_If_Last_Result_Was_Zero()
        {
            Assert.AreEqual("ret z", decoder.Decode(new byte[] { 0xC8 }));
        }

        [TestMethod]
        public void Decode_0xC9_To_Return_From_Subroutine()
        {
            Assert.AreEqual("ret", decoder.Decode(new byte[] { 0xC9 }));
        }

        [TestMethod]
        public void Decode_0xCA_To_Absolute_Jump_To_16_Bit_Address_If_Last_Result_Was_Zero()
        {
            Assert.AreEqual("jp z, $cafe", decoder.Decode(new byte[] { 0xCA, 0xFE, 0xCA }));
        }

        //TODO: test two-bit instructions beginning with 0xCB

        [TestMethod]
        public void Decode_0xCC_To_Call_Routine_At_16_Bit_Address_If_Last_Result_Was_Zero()
        {
            Assert.AreEqual("call z, $cafe", decoder.Decode(new byte[] { 0xCC, 0xFE, 0xCA }));
        }

        [TestMethod]
        public void Decode_0xCD_To_Call_Routine_At_16_Bit_Address()
        {
            Assert.AreEqual("call $cafe", decoder.Decode(new byte[] { 0xCD, 0xFE, 0xCA }));
        }

        [TestMethod]
        public void Decode_0xCE_To_Add_8_Bit_Immediate_And_Carry_Flag_To_A()
        {
            Assert.AreEqual("adc a, $a0", decoder.Decode(new byte[] { 0xCE, 0xA0 }));
        }

        [TestMethod]
        public void Decode_0xCF_To_Call_Reset_Vector_Eight()
        {
            Assert.AreEqual("rst $08", decoder.Decode(new byte[] { 0xCF }));
        }

        [TestMethod]
        public void Decode_0xD0_To_Return_From_Subroutine_If_Last_Result_Caused_No_Carry()
        {
            Assert.AreEqual("ret nc", decoder.Decode(new byte[] { 0xD0 }));
        }

        [TestMethod]
        public void Decode_0xD1_To_Pop_16_Bit_Value_From_Stack_Into_DE()
        {
            Assert.AreEqual("pop de", decoder.Decode(new byte[] { 0xD1 }));
        }

        [TestMethod]
        public void Decode_0xD2_To_Absolute_Jump_To_16_Bit_Address_If_Last_Result_Caused_No_Carry()
        {
            Assert.AreEqual("jp nc, $beef", decoder.Decode(new byte[] { 0xD2, 0xEF, 0xBE }));
        }

        [TestMethod, ExpectedException(typeof(UndefinedOpcodeException))]
        public void Throw_Exception_For_Undefined_0xD3_Opcode()
        {
            decoder.Decode(new byte[] { 0xD3 });
        }

        [TestMethod]
        public void Decode_0xD4_To_Call_Routine_At_16_Bit_Address_If_Last_Result_Caused_No_Carry()
        {
            Assert.AreEqual("call nc, $babe", decoder.Decode(new byte[] { 0xD4, 0xBE, 0xBA }));
        }

        [TestMethod]
        public void Decode_0xD5_To_Push_DE_Onto_Stack()
        {
            Assert.AreEqual("push de", decoder.Decode(new byte[] { 0xD5 }));
        }

        [TestMethod]
        public void Decode_0xD6_To_Subtract_8_Bit_Immediate_From_A()
        {
            Assert.AreEqual("sub a, $aa", decoder.Decode(new byte[] { 0xD6, 0xAA }));
        }

        [TestMethod]
        public void Decode_0xD7_To_Call_Reset_Vector_Ten()
        {
            Assert.AreEqual("rst $10", decoder.Decode(new byte[] { 0xD7 }));
        }

        [TestMethod]
        public void Decode_0xD8_To_Return_From_Subroutine_If_Last_Result_Caused_Carry()
        {
            Assert.AreEqual("ret c", decoder.Decode(new byte[] { 0xD8 }));
        }

        [TestMethod]
        public void Decode_0xD9_To_Enable_Interrupts_And_Return()
        {
            Assert.AreEqual("reti", decoder.Decode(new byte[] { 0xD9 }));
        }

        [TestMethod]
        public void Decode_0xDA_To_Absolute_Jump_To_16_Bit_Address_If_Last_Result_Caused_Carry()
        {
            Assert.AreEqual("jp c, $00ff", decoder.Decode(new byte[] { 0xDA, 0xFF, 0x00 }));
        }

        [TestMethod, ExpectedException(typeof(UndefinedOpcodeException))]
        public void Throw_Exception_For_Undefined_0xDB_Opcode()
        {
            decoder.Decode(new byte[] { 0xDB });
        }

        [TestMethod]
        public void Decode_0xDC_To_Call_Routine_At_16_Bit_Address_If_Last_Result_Caused_Carry()
        {
            Assert.AreEqual("call c, $babe", decoder.Decode(new byte[] { 0xDC, 0xBE, 0xBA }));
        }

        [TestMethod, ExpectedException(typeof(UndefinedOpcodeException))]
        public void Throw_Exception_For_Undefined_0xDD_Opcode()
        {
            decoder.Decode(new byte[] { 0xDD });
        }

        [TestMethod]
        public void Decode_0xDE_To_Subtract_8_Bit_Immediate_And_Carry_Flag_From_A()
        {
            Assert.AreEqual("sbc a, $01", decoder.Decode(new byte[] { 0xDE, 0x01 }));
        }

        [TestMethod]
        public void Decode_0xDF_To_Call_Reset_Vector_Eighteen()
        {
            Assert.AreEqual("rst $18", decoder.Decode(new byte[] { 0xDF }));
        }

        [TestMethod]
        public void Decode_0xE0_To_Load_A_Into_High_Memory_Address_Offset_By_8_Bit_Immediate()
        {
            Assert.AreEqual("ldh [$ff05], a", decoder.Decode(new byte[] { 0xE0, 0x05 }));
        }

        [TestMethod]
        public void Decode_0xE1_To_Pop_16_Bit_Value_From_Stack_Into_HL()
        {
            Assert.AreEqual("pop hl", decoder.Decode(new byte[] { 0xE1 }));
        }

        [TestMethod]
        public void Decode_0xE2_To_Load_A_Into_High_Memory_Address_Offset_By_C()
        {
            Assert.AreEqual("ldh [$ff00 + c], a", decoder.Decode(new byte[] { 0xE2 }));
        }

        [TestMethod, ExpectedException(typeof(UndefinedOpcodeException))]
        public void Throw_Exception_For_Undefined_0xE3_Opcode()
        {
            decoder.Decode(new byte[] { 0xE3 });
        }

        [TestMethod, ExpectedException(typeof(UndefinedOpcodeException))]
        public void Throw_Exception_For_Undefined_0xE4_Opcode()
        {
            decoder.Decode(new byte[] { 0xE4 });
        }

        [TestMethod]
        public void Decode_0xE5_To_Push_HL_Onto_Stack()
        {
            Assert.AreEqual("push hl", decoder.Decode(new byte[] { 0xE5 }));
        }

        [TestMethod]
        public void Decode_0xE6_To_Logical_And_8_Bit_Immediate_With_A()
        {
            Assert.AreEqual("and a, $42", decoder.Decode(new byte[] { 0xE6, 0x42 }));
        }

        [TestMethod]
        public void Decode_0xE7_To_Call_Reset_Vector_Twenty()
        {
            Assert.AreEqual("rst $20", decoder.Decode(new byte[] { 0xE7 }));
        }

        [TestMethod]
        public void Decode_0xE8_To_Add_8_Bit_Signed_Immediate_To_Stack_Pointer()
        {
            Assert.AreEqual("add sp, $fe", decoder.Decode(new byte[] { 0xE8, 0xFE }));
        }

        [TestMethod]
        public void Decode_0xE9_To_Jump_To_16_Bit_Address_Pointed_To_By_HL()
        {
            Assert.AreEqual("jp [hl]", decoder.Decode(new byte[] { 0xE9 }));
        }

        [TestMethod]
        public void Decode_0xEA_To_Load_16_Bit_Address_From_A()
        {
            Assert.AreEqual("ld [$1234], a", decoder.Decode(new byte[] { 0xEA, 0x34, 0x12 }));
        }

        [TestMethod, ExpectedException(typeof(UndefinedOpcodeException))]
        public void Throw_Exception_For_Undefined_0xEB_Opcode()
        {
            decoder.Decode(new byte[] { 0xEB });
        }

        [TestMethod, ExpectedException(typeof(UndefinedOpcodeException))]
        public void Throw_Exception_For_Undefined_0xEC_Opcode()
        {
            decoder.Decode(new byte[] { 0xEC });
        }

        [TestMethod, ExpectedException(typeof(UndefinedOpcodeException))]
        public void Throw_Exception_For_Undefined_0xED_Opcode()
        {
            decoder.Decode(new byte[] { 0xED });
        }

        [TestMethod]
        public void Decode_0xEE_To_Exclusive_Or_8_Bit_Immediate_With_A()
        {
            Assert.AreEqual("xor $ee", decoder.Decode(new byte[] { 0xEE, 0xEE }));
        }

        [TestMethod]
        public void Decode_0xEF_To_Call_Reset_Vector_Twenty_Eight()
        {
            Assert.AreEqual("rst $28", decoder.Decode(new byte[] { 0xEF }));
        }

        [TestMethod]
        public void Decode_0xF0_To_Load_A_From_High_Memory_Address_Offset_By_8_Bit_Immediate()
        {
            Assert.AreEqual("ldh a, [$ffee]", decoder.Decode(new byte[] { 0xF0, 0xEE }));
        }

        [TestMethod]
        public void Decode_0xF1_To_Pop_16_Bit_Value_From_Stack_Into_AF()
        {
            Assert.AreEqual("pop af", decoder.Decode(new byte[] { 0xF1 }));
        }

        [TestMethod, ExpectedException(typeof(UndefinedOpcodeException))]
        public void Throw_Exception_For_Undefined_0xF2_Opcode()
        {
            decoder.Decode(new byte[] { 0xF2 });
        }

        [TestMethod]
        public void Decode_0xF3_To_Disable_Interrupts()
        {
            Assert.AreEqual("di", decoder.Decode(new byte[] { 0xF3 }));
        }

        [TestMethod, ExpectedException(typeof(UndefinedOpcodeException))]
        public void Throw_Exception_For_Undefined_0xF4_Opcode()
        {
            decoder.Decode(new byte[] { 0xF4 });
        }

        [TestMethod]
        public void Decode_0xF5_To_Push_AF_Onto_Stack()
        {
            Assert.AreEqual("push af", decoder.Decode(new byte[] { 0xF5 }));
        }

        [TestMethod]
        public void Decode_0xF6_To_Logical_Or_8_Bit_Immediate_With_A()
        {
            Assert.AreEqual("or $42", decoder.Decode(new byte[] { 0xF6, 0x42 }));
        }

        [TestMethod]
        public void Decode_0xF7_To_Call_Reset_Vector_30()
        {
            Assert.AreEqual("rst $30", decoder.Decode(new byte[] { 0xF7 }));
        }

        [TestMethod]
        public void Decode_0xF8_To_Add_8_Bit_Signed_Immediate_To_Stack_Pointer_And_Store_Result_In_HL()
        {
            Assert.AreEqual("ld hl, [sp + $da]", decoder.Decode(new byte[] { 0xF8, 0xDA }));
        }

        [TestMethod]
        public void Decode_0xF9_To_Load_Stack_Pointer_From_HL()
        {
            Assert.AreEqual("ld sp, hl", decoder.Decode(new byte[] { 0xF9 }));
        }

        [TestMethod]
        public void Decode_0xFA_To_Load_A_From_16_Bit_Address()
        {
            Assert.AreEqual("ld a, [$c01a]", decoder.Decode(new byte[] { 0xFA, 0x1A, 0xC0 }));
        }

        [TestMethod]
        public void Decode_0xFB_To_Enable_Interrupts()
        {
            Assert.AreEqual("ei", decoder.Decode(new byte[] { 0xFB }));
        }

        [TestMethod, ExpectedException(typeof(UndefinedOpcodeException))]
        public void Throw_Exception_For_Undefined_0xFC_Opcode()
        {
            decoder.Decode(new byte[] { 0xFC });
        }

        [TestMethod, ExpectedException(typeof(UndefinedOpcodeException))]
        public void Throw_Exception_For_Undefined_0xFD_Opcode()
        {
            decoder.Decode(new byte[] { 0xFD });
        }

        [TestMethod]
        public void Decode_0xFE_To_Compare_8_Bit_Immediate_To_A_And_Set_Flags_As_If_Subtraction_Occurred()
        {
            Assert.AreEqual("cp $05", decoder.Decode(new byte[] { 0xFE, 0x05 }));
        }

        [TestMethod]
        public void Decode_0xFF_To_Call_Reset_Vector_Thirty_Eight()
        {
            Assert.AreEqual("rst $38", decoder.Decode(new byte[] { 0xFF }));
        }
    }
}
