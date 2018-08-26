using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GBDasm.Core.Test
{
    /// <summary>
    /// Test decoding each GBZ80 instruction using <see cref="Decoder"/>.
    /// </summary>
    [TestClass]
    public class Decoder_Should
    {
        private Decoder decoder = new Decoder();

        [TestMethod]
        public void Decode_0x00_To_Nop()
        {
            Assert.AreEqual("nop", decoder.Decode(new byte[] { 0x00 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x01_To_Load_BC_With_16_Bit_Immediate()
        {
            Assert.AreEqual("ld bc, $abcd", decoder.Decode(new byte[] { 0x01, 0xCD, 0xAB }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x02_To_Load_Address_Pointed_To_By_BC_With_A()
        {
            Assert.AreEqual("ld [bc], a", decoder.Decode(new byte[] { 0x02 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x03_To_Increment_BC()
        {
            Assert.AreEqual("inc bc", decoder.Decode(new byte[] { 0x03 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x04_To_Increment_B()
        {
            Assert.AreEqual("inc b", decoder.Decode(new byte[] { 0x04 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x05_To_Decrement_B()
        {
            Assert.AreEqual("dec b", decoder.Decode(new byte[] { 0x05 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x06_To_Load_B_With_8_Bit_Immediate()
        {
            Assert.AreEqual("ld b, $ff", decoder.Decode(new byte[] { 0x06, 0xFF }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x07_To_Rotate_A_With_Left_Carry()
        {
            Assert.AreEqual("rlca", decoder.Decode(new byte[] { 0x07 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x08_To_Load_Address_With_Stack_Pointer()
        {
            Assert.AreEqual("ld [$babe], sp", decoder.Decode(new byte[] { 0x08, 0xBE, 0xBA }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x09_To_Add_BC_To_HL()
        {
            Assert.AreEqual("add hl, bc", decoder.Decode(new byte[] { 0x09 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x0A_To_Load_A_From_Address_Pointed_To_By_BC()
        {
            Assert.AreEqual("ld a, [bc]", decoder.Decode(new byte[] { 0x0A }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x0B_To_Decrement_BC()
        {
            Assert.AreEqual("dec bc", decoder.Decode(new byte[] { 0x0B }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x0C_To_Increment_C()
        {
            Assert.AreEqual("inc c", decoder.Decode(new byte[] { 0x0C }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x0D_To_Decrement_C()
        {
            Assert.AreEqual("dec c", decoder.Decode(new byte[] { 0x0D }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x0E_To_Load_C_With_8_Bit_Immediate()
        {
            Assert.AreEqual("ld c, $aa", decoder.Decode(new byte[] { 0x0E, 0xAA }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x0F_To_Rotate_A_Right_With_Carry()
        {
            Assert.AreEqual("rrca", decoder.Decode(new byte[] { 0x0F }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x10_0x00_To_Stop()
        {
            Assert.AreEqual("stop", decoder.Decode(new byte[] { 0x10, 0x00 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x10_To_Corrupted_Stop()
        {
            var dasm = decoder.Decode(new byte[] { 0x10 }, out int instructionLength);
            Assert.IsTrue(dasm.StartsWith("db $10"));
        }

        [TestMethod]
        public void Decode_0x11_To_Load_DE_With_16_Bit_Immediate()
        {
            Assert.AreEqual("ld de, $60fe", decoder.Decode(new byte[] { 0x11, 0xFE, 0x60 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x12_To_Load_Address_Pointed_To_By_DE_With_A()
        {
            Assert.AreEqual("ld [de], a", decoder.Decode(new byte[] { 0x12 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x13_To_Increment_DE()
        {
            Assert.AreEqual("inc de", decoder.Decode(new byte[] { 0x13 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x14_To_Increment_D()
        {
            Assert.AreEqual("inc d", decoder.Decode(new byte[] { 0x14 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x15_To_Decrement_D()
        {
            Assert.AreEqual("dec d", decoder.Decode(new byte[] { 0x15 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x16_To_Load_D_With_8_Bit_Immediate()
        {
            Assert.AreEqual("ld d, $0d", decoder.Decode(new byte[] { 0x16, 0x0D }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x17_To_Rotate_A_Left()
        {
            Assert.AreEqual("rla", decoder.Decode(new byte[] { 0x17 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x18_To_Relative_Jump_By_Signed_Immediate()
        {
            //negative offset (-10 dec); banks > 1 should wrap around to stay within $4000 - $7FFF
            Assert.AreEqual("jr $0ed6", decoder.Decode(new byte[] { 0x18, 0xF6 }, out int instructionLength, baseAddress: 0x0EDE)); //bank 0
            Assert.AreEqual("jr $4ed6", decoder.Decode(new byte[] { 0x18, 0xF6 }, out instructionLength, baseAddress: 0x4EDE)); //bank 1
            Assert.AreEqual("jr $4ed6", decoder.Decode(new byte[] { 0x18, 0xF6 }, out instructionLength, baseAddress: 0x8EDE)); //bank 2

            //positive offset (+10 dec); banks > 1 should wrap around to stay within $4000 - $7FFF
            Assert.AreEqual("jr $0eea", decoder.Decode(new byte[] { 0x18, 0x0A }, out instructionLength, baseAddress: 0x0EDE)); //bank 0
            Assert.AreEqual("jr $4eea", decoder.Decode(new byte[] { 0x18, 0x0A }, out instructionLength, baseAddress: 0x4EDE)); //bank 1
            Assert.AreEqual("jr $4eea", decoder.Decode(new byte[] { 0x18, 0x0A }, out instructionLength, baseAddress: 0x8EDE)); //bank 2
        }

        [TestMethod]
        public void Decode_0x19_To_Add_DE_To_HL()
        {
            Assert.AreEqual("add hl, de", decoder.Decode(new byte[] { 0x19 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x1A_To_Load_A_From_Address_Pointed_To_By_DE()
        {
            Assert.AreEqual("ld a, [de]", decoder.Decode(new byte[] { 0x1A }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x1B_To_Decrement_DE()
        {
            Assert.AreEqual("dec de", decoder.Decode(new byte[] { 0x1B }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x1C_To_Increment_E()
        {
            Assert.AreEqual("inc e", decoder.Decode(new byte[] { 0x1C }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x1D_To_Decrement_E()
        {
            Assert.AreEqual("dec e", decoder.Decode(new byte[] { 0x1D }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x1E_To_Load_E_With_8_Bit_Immediate()
        {
            Assert.AreEqual("ld e, $5e", decoder.Decode(new byte[] { 0x1E, 0x5E }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x1F_To_Rotate_A_Right()
        {
            Assert.AreEqual("rra", decoder.Decode(new byte[] { 0x1F }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x20_To_Relative_Jump_By_Signed_Immediate_If_Last_Result_Was_Not_Zero()
        {
            Assert.AreEqual("jr nz, $0ed6", decoder.Decode(new byte[] { 0x20, 0xF6 }, out int instructionLength, baseAddress: 0x0EDE));
        }

        [TestMethod]
        public void Decode_0x21_To_Load_HL_With_16_Bit_Immediate()
        {
            Assert.AreEqual("ld hl, $cafe", decoder.Decode(new byte[] { 0x21, 0xFE, 0xCA }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x22_To_Load_Address_Pointed_To_By_HL_With_A_And_Increment_HL()
        {
            Assert.AreEqual("ldi [hl], a", decoder.Decode(new byte[] { 0x22 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x23_To_Increment_HL()
        {
            Assert.AreEqual("inc hl", decoder.Decode(new byte[] { 0x23 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x24_To_Increment_H()
        {
            Assert.AreEqual("inc h", decoder.Decode(new byte[] { 0x24 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x25_To_Decrement_H()
        {
            Assert.AreEqual("dec h", decoder.Decode(new byte[] { 0x25 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x26_To_Load_H_With_8_Bit_Immediate()
        {
            Assert.AreEqual("ld h, $42", decoder.Decode(new byte[] { 0x26, 0x42 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x27_To_Decimal_Adjust_A_For_Correct_Result_After_Binary_Coded_Decimal_Arithmetic_Instruction()
        {
            Assert.AreEqual("daa", decoder.Decode(new byte[] { 0x27 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x28_To_Relative_Jump_By_Signed_Immediate_If_Last_Result_Was_Zero()
        {
            Assert.AreEqual("jr z, $0ed6", decoder.Decode(new byte[] { 0x28, 0xF6 }, out int instructionLength, baseAddress: 0x0EDE));
        }

        [TestMethod]
        public void Decode_0x29_To_Add_HL_To_HL()
        {
            Assert.AreEqual("add hl, hl", decoder.Decode(new byte[] { 0x29 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x2A_To_Load_A_From_Address_Pointed_To_By_HL_And_Increment_HL()
        {
            Assert.AreEqual("ldi a, [hl]", decoder.Decode(new byte[] { 0x2A }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x2B_To_Decrement_HL()
        {
            Assert.AreEqual("dec hl", decoder.Decode(new byte[] { 0x2B }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x2C_To_Increment_L()
        {
            Assert.AreEqual("inc l", decoder.Decode(new byte[] { 0x2C }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x2D_To_Decrement_L()
        {
            Assert.AreEqual("dec l", decoder.Decode(new byte[] { 0x2D }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x2E_To_Load_L_With_8_Bit_Immediate()
        {
            Assert.AreEqual("ld l, $01", decoder.Decode(new byte[] { 0x2E, 0x01 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x2F_To_Bitwise_Complement_A()
        {
            Assert.AreEqual("cpl", decoder.Decode(new byte[] { 0x2F }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x30_To_Relative_Jump_By_Signed_Immediate_If_Last_Result_Caused_No_Carry()
        {
            Assert.AreEqual("jr nc, $0ed6", decoder.Decode(new byte[] { 0x30, 0xF6 }, out int instructionLength, baseAddress: 0x0EDE));
        }

        [TestMethod]
        public void Decode_0x31_To_Load_Stack_Pointer_With_16_Bit_Immediate()
        {
            Assert.AreEqual("ld sp, $00aa", decoder.Decode(new byte[] { 0x31, 0xAA, 0x00 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x32_To_Load_Address_Pointed_To_By_HL_From_A_And_Decrement_HL()
        {
            Assert.AreEqual("ldd [hl], a", decoder.Decode(new byte[] { 0x32 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x33_To_Increment_Stack_Pointer()
        {
            Assert.AreEqual("inc sp", decoder.Decode(new byte[] { 0x33 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x34_To_Increment_Value_Pointed_To_By_HL()
        {
            Assert.AreEqual("inc [hl]", decoder.Decode(new byte[] { 0x34 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x35_To_Decrement_Value_Pointed_To_By_HL()
        {
            Assert.AreEqual("dec [hl]", decoder.Decode(new byte[] { 0x35 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x36_To_Load_Address_Pointed_To_By_HL_With_8_Bit_Immediate()
        {
            Assert.AreEqual("ld [hl], $5c", decoder.Decode(new byte[] { 0x36, 0x5C }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x37_To_Set_Carry_Flag()
        {
            Assert.AreEqual("scf", decoder.Decode(new byte[] { 0x37 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x38_To_Relative_Jump_By_Signed_Immediate_If_Last_Result_Caused_Carry()
        {
            Assert.AreEqual("jr c, $0ed6", decoder.Decode(new byte[] { 0x38, 0xF6 }, out int instructionLength, baseAddress: 0x0EDE));
        }

        [TestMethod]
        public void Decode_0x39_To_Add_Stack_Pointer_To_HL()
        {
            Assert.AreEqual("add hl, sp", decoder.Decode(new byte[] { 0x39 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x3A_To_Load_A_From_Address_Pointed_To_By_HL_And_Decrement_HL()
        {
            Assert.AreEqual("ldd a, [hl]", decoder.Decode(new byte[] { 0x3A }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x3B_To_Decrement_Stack_Pointer()
        {
            Assert.AreEqual("dec sp", decoder.Decode(new byte[] { 0x3B }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x3C_To_Increment_A()
        {
            Assert.AreEqual("inc a", decoder.Decode(new byte[] { 0x3C }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x3D_To_Decrement_A()
        {
            Assert.AreEqual("dec a", decoder.Decode(new byte[] { 0x3D }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x3E_To_Load_A_With_8_Bit_Immediate()
        {
            Assert.AreEqual("ld a, $aa", decoder.Decode(new byte[] { 0x3E, 0xAA }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x3F_To_Clear_Carry_Flag()
        {
            Assert.AreEqual("ccf", decoder.Decode(new byte[] { 0x3F }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x40_To_Load_B_From_B()
        {
            Assert.AreEqual("ld b, b", decoder.Decode(new byte[] { 0x40 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x41_To_Load_B_From_C()
        {
            Assert.AreEqual("ld b, c", decoder.Decode(new byte[] { 0x41 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x42_To_Load_B_From_D()
        {
            Assert.AreEqual("ld b, d", decoder.Decode(new byte[] { 0x42 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x43_To_Load_B_From_E()
        {
            Assert.AreEqual("ld b, e", decoder.Decode(new byte[] { 0x43 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x44_To_Load_B_From_H()
        {
            Assert.AreEqual("ld b, h", decoder.Decode(new byte[] { 0x44 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x45_To_Load_B_From_L()
        {
            Assert.AreEqual("ld b, l", decoder.Decode(new byte[] { 0x45 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x46_To_Load_B_From_Address_Pointed_To_By_HL()
        {
            Assert.AreEqual("ld b, [hl]", decoder.Decode(new byte[] { 0x46 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x47_To_Load_B_From_A()
        {
            Assert.AreEqual("ld b, a", decoder.Decode(new byte[] { 0x47 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x48_To_Load_C_From_B()
        {
            Assert.AreEqual("ld c, b", decoder.Decode(new byte[] { 0x48 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x49_To_Load_C_From_C()
        {
            Assert.AreEqual("ld c, c", decoder.Decode(new byte[] { 0x49 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x4A_To_Load_C_From_D()
        {
            Assert.AreEqual("ld c, d", decoder.Decode(new byte[] { 0x4A }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x4B_To_Load_C_From_E()
        {
            Assert.AreEqual("ld c, e", decoder.Decode(new byte[] { 0x4B }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x4C_To_Load_C_From_H()
        {
            Assert.AreEqual("ld c, h", decoder.Decode(new byte[] { 0x4C }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x4D_To_Load_C_From_L()
        {
            Assert.AreEqual("ld c, l", decoder.Decode(new byte[] { 0x4D }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x4E_To_Load_C_From_Address_Pointed_To_By_HL()
        {
            Assert.AreEqual("ld c, [hl]", decoder.Decode(new byte[] { 0x4E }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x4F_To_Load_C_From_A()
        {
            Assert.AreEqual("ld c, a", decoder.Decode(new byte[] { 0x4F }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x50_To_Load_D_From_B()
        {
            Assert.AreEqual("ld d, b", decoder.Decode(new byte[] { 0x50 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x51_To_Load_D_From_C()
        {
            Assert.AreEqual("ld d, c", decoder.Decode(new byte[] { 0x51 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x52_To_Load_D_From_D()
        {
            Assert.AreEqual("ld d, d", decoder.Decode(new byte[] { 0x52 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x53_To_Load_D_From_E()
        {
            Assert.AreEqual("ld d, e", decoder.Decode(new byte[] { 0x53 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x54_To_Load_D_From_H()
        {
            Assert.AreEqual("ld d, h", decoder.Decode(new byte[] { 0x54 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x55_To_Load_D_From_L()
        {
            Assert.AreEqual("ld d, l", decoder.Decode(new byte[] { 0x55 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x56_To_Load_D_From_Address_Pointed_To_By_HL()
        {
            Assert.AreEqual("ld d, [hl]", decoder.Decode(new byte[] { 0x56 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x57_To_Load_D_From_A()
        {
            Assert.AreEqual("ld d, a", decoder.Decode(new byte[] { 0x57 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x58_To_Load_E_From_B()
        {
            Assert.AreEqual("ld e, b", decoder.Decode(new byte[] { 0x58 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x59_To_Load_E_From_C()
        {
            Assert.AreEqual("ld e, c", decoder.Decode(new byte[] { 0x59 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x5A_To_Load_E_From_D()
        {
            Assert.AreEqual("ld e, d", decoder.Decode(new byte[] { 0x5A }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x5B_To_Load_E_From_E()
        {
            Assert.AreEqual("ld e, e", decoder.Decode(new byte[] { 0x5B }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x5C_To_Load_E_From_H()
        {
            Assert.AreEqual("ld e, h", decoder.Decode(new byte[] { 0x5C }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x5D_To_Load_E_From_L()
        {
            Assert.AreEqual("ld e, l", decoder.Decode(new byte[] { 0x5D }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x5E_To_Load_E_From_Address_Pointed_To_By_HL()
        {
            Assert.AreEqual("ld e, [hl]", decoder.Decode(new byte[] { 0x5E }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x5F_To_Load_E_From_A()
        {
            Assert.AreEqual("ld e, a", decoder.Decode(new byte[] { 0x5F }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x60_To_Load_H_From_B()
        {
            Assert.AreEqual("ld h, b", decoder.Decode(new byte[] { 0x60 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x61_To_Load_H_From_C()
        {
            Assert.AreEqual("ld h, c", decoder.Decode(new byte[] { 0x61 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x62_To_Load_H_From_D()
        {
            Assert.AreEqual("ld h, d", decoder.Decode(new byte[] { 0x62 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x63_To_Load_H_From_E()
        {
            Assert.AreEqual("ld h, e", decoder.Decode(new byte[] { 0x63 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x64_To_Load_H_From_H()
        {
            Assert.AreEqual("ld h, h", decoder.Decode(new byte[] { 0x64 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x65_To_Load_H_From_L()
        {
            Assert.AreEqual("ld h, l", decoder.Decode(new byte[] { 0x65 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x66_To_Load_H_From_Address_Pointed_To_By_HL()
        {
            Assert.AreEqual("ld h, [hl]", decoder.Decode(new byte[] { 0x66 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x67_To_Load_H_From_A()
        {
            Assert.AreEqual("ld h, a", decoder.Decode(new byte[] { 0x67 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x68_To_Load_L_From_B()
        {
            Assert.AreEqual("ld l, b", decoder.Decode(new byte[] { 0x68 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x69_To_Load_L_From_C()
        {
            Assert.AreEqual("ld l, c", decoder.Decode(new byte[] { 0x69 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x6A_To_Load_L_From_D()
        {
            Assert.AreEqual("ld l, d", decoder.Decode(new byte[] { 0x6A }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x6B_To_Load_L_From_E()
        {
            Assert.AreEqual("ld l, e", decoder.Decode(new byte[] { 0x6B }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x6C_To_Load_L_From_H()
        {
            Assert.AreEqual("ld l, h", decoder.Decode(new byte[] { 0x6C }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x6D_To_Load_L_From_L()
        {
            Assert.AreEqual("ld l, l", decoder.Decode(new byte[] { 0x6D }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x6E_To_Load_L_From_Address_Pointed_To_By_HL()
        {
            Assert.AreEqual("ld l, [hl]", decoder.Decode(new byte[] { 0x6E }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x6F_To_Load_L_From_A()
        {
            Assert.AreEqual("ld l, a", decoder.Decode(new byte[] { 0x6F }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x70_To_Load_Address_Pointed_To_By_HL_With_B()
        {
            Assert.AreEqual("ld [hl], b", decoder.Decode(new byte[] { 0x70 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x71_To_Load_Address_Pointed_To_By_HL_With_C()
        {
            Assert.AreEqual("ld [hl], c", decoder.Decode(new byte[] { 0x71 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x72_To_Load_Address_Pointed_To_By_HL_With_D()
        {
            Assert.AreEqual("ld [hl], d", decoder.Decode(new byte[] { 0x72 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x73_To_Load_Address_Pointed_To_By_HL_With_E()
        {
            Assert.AreEqual("ld [hl], e", decoder.Decode(new byte[] { 0x73 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x74_To_Load_Address_Pointed_To_By_HL_With_H()
        {
            Assert.AreEqual("ld [hl], h", decoder.Decode(new byte[] { 0x74 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x75_To_Load_Address_Pointed_To_By_HL_With_L()
        {
            Assert.AreEqual("ld [hl], l", decoder.Decode(new byte[] { 0x75 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x76_To_Halt()
        {
            Assert.AreEqual("halt", decoder.Decode(new byte[] { 0x76 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x77_To_Load_Address_Pointed_To_By_HL_With_A()
        {
            Assert.AreEqual("ld [hl], a", decoder.Decode(new byte[] { 0x77 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x78_To_Load_A_From_B()
        {
            Assert.AreEqual("ld a, b", decoder.Decode(new byte[] { 0x78 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x79_To_Load_A_From_C()
        {
            Assert.AreEqual("ld a, c", decoder.Decode(new byte[] { 0x79 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x7A_To_Load_A_From_D()
        {
            Assert.AreEqual("ld a, d", decoder.Decode(new byte[] { 0x7A }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x7B_To_Load_A_From_E()
        {
            Assert.AreEqual("ld a, e", decoder.Decode(new byte[] { 0x7B }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x7C_To_Load_A_From_H()
        {
            Assert.AreEqual("ld a, h", decoder.Decode(new byte[] { 0x7C }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x7D_To_Load_A_From_L()
        {
            Assert.AreEqual("ld a, l", decoder.Decode(new byte[] { 0x7D }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x7E_To_Load_A_From_Address_Pointed_To_By_HL()
        {
            Assert.AreEqual("ld a, [hl]", decoder.Decode(new byte[] { 0x7E }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x7F_To_Load_A_From_A()
        {
            Assert.AreEqual("ld a, a", decoder.Decode(new byte[] { 0x7F }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x80_To_Add_B_To_A()
        {
            Assert.AreEqual("add b", decoder.Decode(new byte[] { 0x80 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x81_To_Add_C_To_A()
        {
            Assert.AreEqual("add c", decoder.Decode(new byte[] { 0x81 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x82_To_Add_D_To_A()
        {
            Assert.AreEqual("add d", decoder.Decode(new byte[] { 0x82 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x83_To_Add_E_To_A()
        {
            Assert.AreEqual("add e", decoder.Decode(new byte[] { 0x83 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x84_To_Add_H_To_A()
        {
            Assert.AreEqual("add h", decoder.Decode(new byte[] { 0x84 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x85_To_Add_L_To_A()
        {
            Assert.AreEqual("add l", decoder.Decode(new byte[] { 0x85 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x86_To_Add_Address_Pointed_To_By_HL_To_A()
        {
            Assert.AreEqual("add [hl]", decoder.Decode(new byte[] { 0x86 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x87_To_Add_A_To_A()
        {
            Assert.AreEqual("add a", decoder.Decode(new byte[] { 0x87 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x88_To_Add_B_And_Carry_Flag_To_A()
        {
            Assert.AreEqual("adc b", decoder.Decode(new byte[] { 0x88 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x89_To_Add_C_And_Carry_Flag_To_A()
        {
            Assert.AreEqual("adc c", decoder.Decode(new byte[] { 0x89 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x8A_To_Add_D_And_Carry_Flag_To_A()
        {
            Assert.AreEqual("adc d", decoder.Decode(new byte[] { 0x8A }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x8B_To_Add_E_And_Carry_Flag_To_A()
        {
            Assert.AreEqual("adc e", decoder.Decode(new byte[] { 0x8B }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x8C_To_Add_H_And_Carry_Flag_To_A()
        {
            Assert.AreEqual("adc h", decoder.Decode(new byte[] { 0x8C }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x8D_To_Add_L_And_Carry_Flag_To_A()
        {
            Assert.AreEqual("adc l", decoder.Decode(new byte[] { 0x8D }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x8E_To_Add_Address_Pointed_To_By_HL_And_Carry_Flag_To_A()
        {
            Assert.AreEqual("adc [hl]", decoder.Decode(new byte[] { 0x8E }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x8F_To_Add_A_And_Carry_Flag_To_A()
        {
            Assert.AreEqual("adc a", decoder.Decode(new byte[] { 0x8F }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x90_To_Subtract_B_From_A()
        {
            Assert.AreEqual("sub b", decoder.Decode(new byte[] { 0x90 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x91_To_Subtract_C_From_A()
        {
            Assert.AreEqual("sub c", decoder.Decode(new byte[] { 0x91 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x92_To_Subtract_D_From_A()
        {
            Assert.AreEqual("sub d", decoder.Decode(new byte[] { 0x92 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x93_To_Subtract_E_From_A()
        {
            Assert.AreEqual("sub e", decoder.Decode(new byte[] { 0x93 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x94_To_Subtract_H_From_A()
        {
            Assert.AreEqual("sub h", decoder.Decode(new byte[] { 0x94 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x95_To_Subtract_L_From_A()
        {
            Assert.AreEqual("sub l", decoder.Decode(new byte[] { 0x95 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x96_To_Subtract_Address_Pointed_To_By_HL_From_A()
        {
            Assert.AreEqual("sub [hl]", decoder.Decode(new byte[] { 0x96 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x97_To_Subtract_A_From_A()
        {
            Assert.AreEqual("sub a", decoder.Decode(new byte[] { 0x97 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x98_To_Subtract_B_And_Carry_Flag_From_A()
        {
            Assert.AreEqual("sbc b", decoder.Decode(new byte[] { 0x98 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x99_To_Subtract_C_And_Carry_Flag_From_A()
        {
            Assert.AreEqual("sbc c", decoder.Decode(new byte[] { 0x99 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x9A_To_Subtract_D_And_Carry_Flag_From_A()
        {
            Assert.AreEqual("sbc d", decoder.Decode(new byte[] { 0x9A }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x9B_To_Subtract_E_And_Carry_Flag_From_A()
        {
            Assert.AreEqual("sbc e", decoder.Decode(new byte[] { 0x9B }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x9C_To_Subtract_H_And_Carry_Flag_From_A()
        {
            Assert.AreEqual("sbc h", decoder.Decode(new byte[] { 0x9C }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x9D_To_Subtract_L_And_Carry_Flag_From_A()
        {
            Assert.AreEqual("sbc l", decoder.Decode(new byte[] { 0x9D }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x9E_To_Subtract_Address_Pointed_To_By_HL_And_Carry_Flag_From_A()
        {
            Assert.AreEqual("sbc [hl]", decoder.Decode(new byte[] { 0x9E }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0x9F_To_Subtract_A_And_Carry_Flag_From_A()
        {
            Assert.AreEqual("sbc a", decoder.Decode(new byte[] { 0x9F }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xA0_To_Logical_And_B_With_A()
        {
            Assert.AreEqual("and b", decoder.Decode(new byte[] { 0xA0 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xA1_To_Logical_And_C_With_A()
        {
            Assert.AreEqual("and c", decoder.Decode(new byte[] { 0xA1 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xA2_To_Logical_And_D_With_A()
        {
            Assert.AreEqual("and d", decoder.Decode(new byte[] { 0xA2 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xA3_To_Logical_And_E_With_A()
        {
            Assert.AreEqual("and e", decoder.Decode(new byte[] { 0xA3 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xA4_To_Logical_And_H_With_A()
        {
            Assert.AreEqual("and h", decoder.Decode(new byte[] { 0xA4 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xA5_To_Logical_And_L_With_A()
        {
            Assert.AreEqual("and l", decoder.Decode(new byte[] { 0xA5 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xA6_To_Logical_And_Address_Pointed_To_By_HL_With_A()
        {
            Assert.AreEqual("and [hl]", decoder.Decode(new byte[] { 0xA6 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xA7_To_Logical_And_A_With_A()
        {
            Assert.AreEqual("and a", decoder.Decode(new byte[] { 0xA7 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xA8_To_Exclusive_Or_B_With_A()
        {
            Assert.AreEqual("xor b", decoder.Decode(new byte[] { 0xA8 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xA9_To_Exclusive_Or_C_With_A()
        {
            Assert.AreEqual("xor c", decoder.Decode(new byte[] { 0xA9 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xAA_To_Exclusive_Or_D_With_A()
        {
            Assert.AreEqual("xor d", decoder.Decode(new byte[] { 0xAA }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xAB_To_Exclusive_Or_E_With_A()
        {
            Assert.AreEqual("xor e", decoder.Decode(new byte[] { 0xAB }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xAC_To_Exclusive_Or_H_With_A()
        {
            Assert.AreEqual("xor h", decoder.Decode(new byte[] { 0xAC }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xAD_To_Exclusive_Or_L_With_A()
        {
            Assert.AreEqual("xor l", decoder.Decode(new byte[] { 0xAD }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xAE_To_Exclusive_Or_Address_Pointed_To_By_HL_With_A()
        {
            Assert.AreEqual("xor [hl]", decoder.Decode(new byte[] { 0xAE }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xAF_To_Exclusive_Or_A_With_A()
        {
            Assert.AreEqual("xor a", decoder.Decode(new byte[] { 0xAF }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xB0_To_Logical_Or_B_With_A()
        {
            Assert.AreEqual("or b", decoder.Decode(new byte[] { 0xB0 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xB1_To_Logical_Or_C_With_A()
        {
            Assert.AreEqual("or c", decoder.Decode(new byte[] { 0xB1 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xB2_To_Logical_Or_D_With_A()
        {
            Assert.AreEqual("or d", decoder.Decode(new byte[] { 0xB2 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xB3_To_Logical_Or_E_With_A()
        {
            Assert.AreEqual("or e", decoder.Decode(new byte[] { 0xB3 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xB4_To_Logical_Or_H_With_A()
        {
            Assert.AreEqual("or h", decoder.Decode(new byte[] { 0xB4 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xB5_To_Logical_Or_L_With_A()
        {
            Assert.AreEqual("or l", decoder.Decode(new byte[] { 0xB5 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xB6_To_Logical_Or_Address_Pointed_To_By_HL_With_A()
        {
            Assert.AreEqual("or [hl]", decoder.Decode(new byte[] { 0xB6 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xB7_To_Logical_Or_A_With_A()
        {
            Assert.AreEqual("or a", decoder.Decode(new byte[] { 0xB7 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xB8_To_Compare_B_To_A_And_Set_Flags_As_If_Subtraction_Occurred()
        {
            Assert.AreEqual("cp b", decoder.Decode(new byte[] { 0xB8 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xB9_To_Compare_C_To_A_And_Set_Flags_As_If_Subtraction_Occurred()
        {
            Assert.AreEqual("cp c", decoder.Decode(new byte[] { 0xB9 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xBA_To_Compare_D_To_A_And_Set_Flags_As_If_Subtraction_Occurred()
        {
            Assert.AreEqual("cp d", decoder.Decode(new byte[] { 0xBA }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xBB_To_Compare_E_To_A_And_Set_Flags_As_If_Subtraction_Occurred()
        {
            Assert.AreEqual("cp e", decoder.Decode(new byte[] { 0xBB }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xBC_To_Compare_H_To_A_And_Set_Flags_As_If_Subtraction_Occurred()
        {
            Assert.AreEqual("cp h", decoder.Decode(new byte[] { 0xBC }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xBD_To_Compare_L_To_A_And_Set_Flags_As_If_Subtraction_Occurred()
        {
            Assert.AreEqual("cp l", decoder.Decode(new byte[] { 0xBD }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xBE_To_Compare_Address_Pointed_To_By_HL_To_A_And_Set_Flags_As_If_Subtraction_Occurred()
        {
            Assert.AreEqual("cp [hl]", decoder.Decode(new byte[] { 0xBE }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xBF_To_Compare_A_To_A_And_Set_Flags_As_If_Subtraction_Occurred()
        {
            Assert.AreEqual("cp a", decoder.Decode(new byte[] { 0xBF }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xC0_To_Return_If_Last_Result_Was_Not_Zero()
        {
            Assert.AreEqual("ret nz", decoder.Decode(new byte[] { 0xC0 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xC1_To_Pop_16_Bit_Value_From_Stack_Into_BC()
        {
            Assert.AreEqual("pop bc", decoder.Decode(new byte[] { 0xC1 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xC2_To_Absolute_Jump_To_16_Bit_Address_If_Last_Result_Was_Not_Zero()
        {
            Assert.AreEqual("jp nz, $abcd", decoder.Decode(new byte[] { 0xC2, 0xCD, 0xAB }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xC3_To_Absolute_Jump_To_16_Bit_Address_If_Last_Result_Was_Not_Zero()
        {
            Assert.AreEqual("jp $abcd", decoder.Decode(new byte[] { 0xC3, 0xCD, 0xAB }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xC4_To_Call_Routine_At_16_Bit_Address_If_Last_Result_Was_Not_Zero()
        {
            Assert.AreEqual("call nz, $abcd", decoder.Decode(new byte[] { 0xC4, 0xCD, 0xAB }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xC5_To_Push_BC_Onto_Stack()
        {
            Assert.AreEqual("push bc", decoder.Decode(new byte[] { 0xC5 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xC6_To_Add_8_Bit_Immediate_To_A()
        {
            Assert.AreEqual("add a, $bc", decoder.Decode(new byte[] { 0xC6, 0xBC }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xC7_To_Call_Restart_Vector_Zero()
        {
            Assert.AreEqual("rst $00", decoder.Decode(new byte[] { 0xC7 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xC8_To_Return_From_Subroutine_If_Last_Result_Was_Zero()
        {
            Assert.AreEqual("ret z", decoder.Decode(new byte[] { 0xC8 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xC9_To_Return_From_Subroutine()
        {
            Assert.AreEqual("ret", decoder.Decode(new byte[] { 0xC9 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xCA_To_Absolute_Jump_To_16_Bit_Address_If_Last_Result_Was_Zero()
        {
            Assert.AreEqual("jp z, $cafe", decoder.Decode(new byte[] { 0xCA, 0xFE, 0xCA }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xCB_0x00_To_Rotate_B_Left_With_Carry()
        {
            Assert.AreEqual("rlc b", decoder.Decode(new byte[] { 0xCB, 0x00 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xCB_0x01_To_Rotate_C_Left_With_Carry()
        {
            Assert.AreEqual("rlc c", decoder.Decode(new byte[] { 0xCB, 0x01 }, out int instructionLength));
        }

        //TODO: remaining CB instruction tests

        [TestMethod]
        public void Decode_0xCC_To_Call_Routine_At_16_Bit_Address_If_Last_Result_Was_Zero()
        {
            Assert.AreEqual("call z, $cafe", decoder.Decode(new byte[] { 0xCC, 0xFE, 0xCA }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xCD_To_Call_Routine_At_16_Bit_Address()
        {
            Assert.AreEqual("call $cafe", decoder.Decode(new byte[] { 0xCD, 0xFE, 0xCA }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xCE_To_Add_8_Bit_Immediate_And_Carry_Flag_To_A()
        {
            Assert.AreEqual("adc a, $a0", decoder.Decode(new byte[] { 0xCE, 0xA0 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xCF_To_Call_Reset_Vector_Eight()
        {
            Assert.AreEqual("rst $08", decoder.Decode(new byte[] { 0xCF }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xD0_To_Return_From_Subroutine_If_Last_Result_Caused_No_Carry()
        {
            Assert.AreEqual("ret nc", decoder.Decode(new byte[] { 0xD0 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xD1_To_Pop_16_Bit_Value_From_Stack_Into_DE()
        {
            Assert.AreEqual("pop de", decoder.Decode(new byte[] { 0xD1 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xD2_To_Absolute_Jump_To_16_Bit_Address_If_Last_Result_Caused_No_Carry()
        {
            Assert.AreEqual("jp nc, $beef", decoder.Decode(new byte[] { 0xD2, 0xEF, 0xBE }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xD3_As_Undefined_Opcode()
        {
            var dasm = decoder.Decode(new byte[] { 0xD3 }, out int instructionLength);
            Assert.IsTrue(dasm.StartsWith("db $d3"));
            Assert.IsTrue(dasm.Contains("unknown instruction"));
        }

        [TestMethod]
        public void Decode_0xD4_To_Call_Routine_At_16_Bit_Address_If_Last_Result_Caused_No_Carry()
        {
            Assert.AreEqual("call nc, $babe", decoder.Decode(new byte[] { 0xD4, 0xBE, 0xBA }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xD5_To_Push_DE_Onto_Stack()
        {
            Assert.AreEqual("push de", decoder.Decode(new byte[] { 0xD5 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xD6_To_Subtract_8_Bit_Immediate_From_A()
        {
            Assert.AreEqual("sub a, $aa", decoder.Decode(new byte[] { 0xD6, 0xAA }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xD7_To_Call_Reset_Vector_Ten()
        {
            Assert.AreEqual("rst $10", decoder.Decode(new byte[] { 0xD7 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xD8_To_Return_From_Subroutine_If_Last_Result_Caused_Carry()
        {
            Assert.AreEqual("ret c", decoder.Decode(new byte[] { 0xD8 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xD9_To_Enable_Interrupts_And_Return()
        {
            Assert.AreEqual("reti", decoder.Decode(new byte[] { 0xD9 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xDA_To_Absolute_Jump_To_16_Bit_Address_If_Last_Result_Caused_Carry()
        {
            Assert.AreEqual("jp c, $00ff", decoder.Decode(new byte[] { 0xDA, 0xFF, 0x00 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xDB_As_Undefined_Opcode()
        {
            var dasm = decoder.Decode(new byte[] { 0xDB }, out int instructionLength);
            Assert.IsTrue(dasm.StartsWith("db $db"));
            Assert.IsTrue(dasm.Contains("unknown instruction"));
        }

        [TestMethod]
        public void Decode_0xDC_To_Call_Routine_At_16_Bit_Address_If_Last_Result_Caused_Carry()
        {
            Assert.AreEqual("call c, $babe", decoder.Decode(new byte[] { 0xDC, 0xBE, 0xBA }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xDD_As_Undefined_Opcode()
        {
            var dasm = decoder.Decode(new byte[] { 0xDD }, out int instructionLength);
            Assert.IsTrue(dasm.StartsWith("db $dd"));
            Assert.IsTrue(dasm.Contains("unknown instruction"));
        }

        [TestMethod]
        public void Decode_0xDE_To_Subtract_8_Bit_Immediate_And_Carry_Flag_From_A()
        {
            Assert.AreEqual("sbc a, $01", decoder.Decode(new byte[] { 0xDE, 0x01 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xDF_To_Call_Reset_Vector_Eighteen()
        {
            Assert.AreEqual("rst $18", decoder.Decode(new byte[] { 0xDF }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xE0_To_Load_A_Into_High_Memory_Address_Offset_By_8_Bit_Immediate()
        {
            Assert.AreEqual("ldh [$ff05], a", decoder.Decode(new byte[] { 0xE0, 0x05 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xE1_To_Pop_16_Bit_Value_From_Stack_Into_HL()
        {
            Assert.AreEqual("pop hl", decoder.Decode(new byte[] { 0xE1 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xE2_To_Load_A_Into_High_Memory_Address_Offset_By_C()
        {
            Assert.AreEqual("ld [$ff00+c], a", decoder.Decode(new byte[] { 0xE2 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xE3_As_Undefined_Opcode()
        {
            var dasm = decoder.Decode(new byte[] { 0xE3 }, out int instructionLength);
            Assert.IsTrue(dasm.StartsWith("db $e3"));
            Assert.IsTrue(dasm.Contains("unknown instruction"));
        }

        [TestMethod]
        public void Decode_0xE4_As_Undefined_Opcode()
        {
            var dasm = decoder.Decode(new byte[] { 0xE4 }, out int instructionLength);
            Assert.IsTrue(dasm.StartsWith("db $e4"));
            Assert.IsTrue(dasm.Contains("unknown instruction"));
        }

        [TestMethod]
        public void Decode_0xE5_To_Push_HL_Onto_Stack()
        {
            Assert.AreEqual("push hl", decoder.Decode(new byte[] { 0xE5 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xE6_To_Logical_And_8_Bit_Immediate_With_A()
        {
            Assert.AreEqual("and a, $42", decoder.Decode(new byte[] { 0xE6, 0x42 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xE7_To_Call_Reset_Vector_Twenty()
        {
            Assert.AreEqual("rst $20", decoder.Decode(new byte[] { 0xE7 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xE8_To_Add_8_Bit_Signed_Immediate_To_Stack_Pointer()
        {
            Assert.AreEqual("add sp, $fe", decoder.Decode(new byte[] { 0xE8, 0xFE }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xE9_To_Jump_To_16_Bit_Address_Pointed_To_By_HL()
        {
            Assert.AreEqual("jp hl", decoder.Decode(new byte[] { 0xE9 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xEA_To_Load_16_Bit_Address_From_A()
        {
            Assert.AreEqual("ld [$1234], a", decoder.Decode(new byte[] { 0xEA, 0x34, 0x12 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xEB_As_Undefined_Opcode()
        {
            var dasm = decoder.Decode(new byte[] { 0xEB }, out int instructionLength);
            Assert.IsTrue(dasm.StartsWith("db $eb"));
            Assert.IsTrue(dasm.Contains("unknown instruction"));
        }

        [TestMethod]
        public void Decode_0xEC_As_Undefined_Opcode()
        {
            var dasm = decoder.Decode(new byte[] { 0xEC }, out int instructionLength);
            Assert.IsTrue(dasm.StartsWith("db $ec"));
            Assert.IsTrue(dasm.Contains("unknown instruction"));
        }

        [TestMethod]
        public void Decode_0xED_As_Undefined_Opcode()
        {
            var dasm = decoder.Decode(new byte[] { 0xED }, out int instructionLength);
            Assert.IsTrue(dasm.StartsWith("db $ed"));
            Assert.IsTrue(dasm.Contains("unknown instruction"));
        }

        [TestMethod]
        public void Decode_0xEE_To_Exclusive_Or_8_Bit_Immediate_With_A()
        {
            Assert.AreEqual("xor $ee", decoder.Decode(new byte[] { 0xEE, 0xEE }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xEF_To_Call_Reset_Vector_Twenty_Eight()
        {
            Assert.AreEqual("rst $28", decoder.Decode(new byte[] { 0xEF }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xF0_To_Load_A_From_High_Memory_Address_Offset_By_8_Bit_Immediate()
        {
            Assert.AreEqual("ldh a, [$ffee]", decoder.Decode(new byte[] { 0xF0, 0xEE }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xF1_To_Pop_16_Bit_Value_From_Stack_Into_AF()
        {
            Assert.AreEqual("pop af", decoder.Decode(new byte[] { 0xF1 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xF2_As_Undefined_Opcode()
        {
            var dasm = decoder.Decode(new byte[] { 0xF2 }, out int instructionLength);
            Assert.IsTrue(dasm.StartsWith("db $f2"));
            Assert.IsTrue(dasm.Contains("unknown instruction"));
        }

        [TestMethod]
        public void Decode_0xF3_To_Disable_Interrupts()
        {
            Assert.AreEqual("di", decoder.Decode(new byte[] { 0xF3 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xF4_As_Undefined_Opcode()
        {
            var dasm = decoder.Decode(new byte[] { 0xF4 }, out int instructionLength);
            Assert.IsTrue(dasm.StartsWith("db $f4"));
            Assert.IsTrue(dasm.Contains("unknown instruction"));
        }

        [TestMethod]
        public void Decode_0xF5_To_Push_AF_Onto_Stack()
        {
            Assert.AreEqual("push af", decoder.Decode(new byte[] { 0xF5 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xF6_To_Logical_Or_8_Bit_Immediate_With_A()
        {
            Assert.AreEqual("or $42", decoder.Decode(new byte[] { 0xF6, 0x42 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xF7_To_Call_Reset_Vector_30()
        {
            Assert.AreEqual("rst $30", decoder.Decode(new byte[] { 0xF7 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xF8_To_Add_8_Bit_Signed_Immediate_To_Stack_Pointer_And_Store_Result_In_HL()
        {
            Assert.AreEqual("ld hl, sp+$da", decoder.Decode(new byte[] { 0xF8, 0xDA }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xF9_To_Load_Stack_Pointer_From_HL()
        {
            Assert.AreEqual("ld sp, hl", decoder.Decode(new byte[] { 0xF9 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xFA_To_Load_A_From_16_Bit_Address()
        {
            Assert.AreEqual("ld a, [$c01a]", decoder.Decode(new byte[] { 0xFA, 0x1A, 0xC0 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xFB_To_Enable_Interrupts()
        {
            Assert.AreEqual("ei", decoder.Decode(new byte[] { 0xFB }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xFC_As_Undefined_Opcode()
        {
            var dasm = decoder.Decode(new byte[] { 0xFC }, out int instructionLength);
            Assert.IsTrue(dasm.StartsWith("db $fc"));
            Assert.IsTrue(dasm.Contains("unknown instruction"));
        }

        [TestMethod]
        public void Decode_0xFD_As_Undefined_Opcode()
        {
            var dasm = decoder.Decode(new byte[] { 0xFD }, out int instructionLength);
            Assert.IsTrue(dasm.StartsWith("db $fd"));
            Assert.IsTrue(dasm.Contains("unknown instruction"));
        }

        [TestMethod]
        public void Decode_0xFE_To_Compare_8_Bit_Immediate_To_A_And_Set_Flags_As_If_Subtraction_Occurred()
        {
            Assert.AreEqual("cp $05", decoder.Decode(new byte[] { 0xFE, 0x05 }, out int instructionLength));
        }

        [TestMethod]
        public void Decode_0xFF_To_Call_Reset_Vector_Thirty_Eight()
        {
            Assert.AreEqual("rst $38", decoder.Decode(new byte[] { 0xFF }, out int instructionLength));
        }
    }
}
