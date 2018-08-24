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
            Assert.AreEqual(expected, decoder.Decode(0x00, 0x01, 0xad, 0xde, 0x00, 0x03));
        }

        [TestMethod]
        public void Decode_0x00_To_Nop()
        {
            Assert.AreEqual("nop", decoder.Decode(0x00));
        }

        [TestMethod]
        public void Decode_0x01_To_Load_BC_With_16_Bit_Immediate()
        {
            Assert.AreEqual("ld bc, $abcd", decoder.Decode(0x01, 0xcd, 0xab));
        }

        [TestMethod]
        public void Decode_0x02_To_Load_Address_Pointed_To_By_BC_With_A()
        {
            Assert.AreEqual("ld [bc], a", decoder.Decode(0x02));
        }

        [TestMethod]
        public void Decode_0x03_To_Increment_BC()
        {
            Assert.AreEqual("inc bc", decoder.Decode(0x03));
        }

        [TestMethod]
        public void Decode_0x04_To_Increment_B()
        {
            Assert.AreEqual("inc b", decoder.Decode(0x04));
        }

        [TestMethod]
        public void Decode_0x05_To_Decrement_B()
        {
            Assert.AreEqual("dec b", decoder.Decode(0x05));
        }

        [TestMethod]
        public void Decode_0x06_To_Load_B_With_8_Bit_Immediate()
        {
            Assert.AreEqual("ld b, $ff", decoder.Decode(0x06, 0xff));
        }

        [TestMethod]
        public void Decode_0x07_To_Rotate_A_With_Left_Carry()
        {
            Assert.AreEqual("rlc a", decoder.Decode(0x07));
        }

        [TestMethod]
        public void Decode_0x08_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x08));
        }

        [TestMethod]
        public void Decode_0x09_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x09));
        }

        [TestMethod]
        public void Decode_0x0A_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x0A));
        }

        [TestMethod]
        public void Decode_0x0B_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x0B));
        }

        [TestMethod]
        public void Decode_0x0C_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x0C));
        }

        [TestMethod]
        public void Decode_0x0D_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x0D));
        }

        [TestMethod]
        public void Decode_0x0E_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x0E));
        }

        [TestMethod]
        public void Decode_0x0F_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x0F));
        }

        [TestMethod]
        public void Decode_0x10_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x10));
        }

        [TestMethod]
        public void Decode_0x11_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x11));
        }

        [TestMethod]
        public void Decode_0x12_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x12));
        }

        [TestMethod]
        public void Decode_0x13_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x13));
        }

        [TestMethod]
        public void Decode_0x14_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x14));
        }

        [TestMethod]
        public void Decode_0x15_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x15));
        }

        [TestMethod]
        public void Decode_0x16_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x16));
        }

        [TestMethod]
        public void Decode_0x17_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x17));
        }

        [TestMethod]
        public void Decode_0x18_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x18));
        }

        [TestMethod]
        public void Decode_0x19_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x19));
        }

        [TestMethod]
        public void Decode_0x1A_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x1A));
        }

        [TestMethod]
        public void Decode_0x1B_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x1B));
        }

        [TestMethod]
        public void Decode_0x1C_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x1C));
        }

        [TestMethod]
        public void Decode_0x1D_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x1D));
        }

        [TestMethod]
        public void Decode_0x1E_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x1E));
        }

        [TestMethod]
        public void Decode_0x1F_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x1F));
        }

        [TestMethod]
        public void Decode_0x20_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x20));
        }

        [TestMethod]
        public void Decode_0x21_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x21));
        }

        [TestMethod]
        public void Decode_0x22_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x22));
        }

        [TestMethod]
        public void Decode_0x23_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x23));
        }

        [TestMethod]
        public void Decode_0x24_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x24));
        }

        [TestMethod]
        public void Decode_0x25_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x25));
        }

        [TestMethod]
        public void Decode_0x26_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x26));
        }

        [TestMethod]
        public void Decode_0x27_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x27));
        }

        [TestMethod]
        public void Decode_0x28_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x28));
        }

        [TestMethod]
        public void Decode_0x29_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x29));
        }

        [TestMethod]
        public void Decode_0x2A_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x2A));
        }

        [TestMethod]
        public void Decode_0x2B_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x2B));
        }

        [TestMethod]
        public void Decode_0x2C_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x2C));
        }

        [TestMethod]
        public void Decode_0x2D_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x2D));
        }

        [TestMethod]
        public void Decode_0x2E_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x2E));
        }

        [TestMethod]
        public void Decode_0x2F_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x2F));
        }

        [TestMethod]
        public void Decode_0x30_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x30));
        }

        [TestMethod]
        public void Decode_0x31_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x31));
        }

        [TestMethod]
        public void Decode_0x32_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x32));
        }

        [TestMethod]
        public void Decode_0x33_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x33));
        }

        [TestMethod]
        public void Decode_0x34_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x34));
        }

        [TestMethod]
        public void Decode_0x35_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x35));
        }

        [TestMethod]
        public void Decode_0x36_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x36));
        }

        [TestMethod]
        public void Decode_0x37_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x37));
        }

        [TestMethod]
        public void Decode_0x38_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x38));
        }

        [TestMethod]
        public void Decode_0x39_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x39));
        }

        [TestMethod]
        public void Decode_0x3A_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x3A));
        }

        [TestMethod]
        public void Decode_0x3B_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x3B));
        }

        [TestMethod]
        public void Decode_0x3C_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x3C));
        }

        [TestMethod]
        public void Decode_0x3D_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x3D));
        }

        [TestMethod]
        public void Decode_0x3E_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x3E));
        }

        [TestMethod]
        public void Decode_0x3F_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x3F));
        }

        [TestMethod]
        public void Decode_0x40_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x40));
        }

        [TestMethod]
        public void Decode_0x41_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x41));
        }

        [TestMethod]
        public void Decode_0x42_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x42));
        }

        [TestMethod]
        public void Decode_0x43_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x43));
        }

        [TestMethod]
        public void Decode_0x44_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x44));
        }

        [TestMethod]
        public void Decode_0x45_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x45));
        }

        [TestMethod]
        public void Decode_0x46_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x46));
        }

        [TestMethod]
        public void Decode_0x47_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x47));
        }

        [TestMethod]
        public void Decode_0x48_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x48));
        }

        [TestMethod]
        public void Decode_0x49_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x49));
        }

        [TestMethod]
        public void Decode_0x4A_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x4A));
        }

        [TestMethod]
        public void Decode_0x4B_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x4B));
        }

        [TestMethod]
        public void Decode_0x4C_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x4C));
        }

        [TestMethod]
        public void Decode_0x4D_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x4D));
        }

        [TestMethod]
        public void Decode_0x4E_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x4E));
        }

        [TestMethod]
        public void Decode_0x4F_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x4F));
        }

        [TestMethod]
        public void Decode_0x50_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x50));
        }

        [TestMethod]
        public void Decode_0x51_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x51));
        }

        [TestMethod]
        public void Decode_0x52_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x52));
        }

        [TestMethod]
        public void Decode_0x53_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x53));
        }

        [TestMethod]
        public void Decode_0x54_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x54));
        }

        [TestMethod]
        public void Decode_0x55_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x55));
        }

        [TestMethod]
        public void Decode_0x56_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x56));
        }

        [TestMethod]
        public void Decode_0x57_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x57));
        }

        [TestMethod]
        public void Decode_0x58_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x58));
        }

        [TestMethod]
        public void Decode_0x59_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x59));
        }

        [TestMethod]
        public void Decode_0x5A_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x5A));
        }

        [TestMethod]
        public void Decode_0x5B_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x5B));
        }

        [TestMethod]
        public void Decode_0x5C_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x5C));
        }

        [TestMethod]
        public void Decode_0x5D_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x5D));
        }

        [TestMethod]
        public void Decode_0x5E_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x5E));
        }

        [TestMethod]
        public void Decode_0x5F_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x5F));
        }

        [TestMethod]
        public void Decode_0x60_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x60));
        }

        [TestMethod]
        public void Decode_0x61_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x61));
        }

        [TestMethod]
        public void Decode_0x62_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x62));
        }

        [TestMethod]
        public void Decode_0x63_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x63));
        }

        [TestMethod]
        public void Decode_0x64_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x64));
        }

        [TestMethod]
        public void Decode_0x65_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x65));
        }

        [TestMethod]
        public void Decode_0x66_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x66));
        }

        [TestMethod]
        public void Decode_0x67_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x67));
        }

        [TestMethod]
        public void Decode_0x68_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x68));
        }

        [TestMethod]
        public void Decode_0x69_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x69));
        }

        [TestMethod]
        public void Decode_0x6A_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x6A));
        }

        [TestMethod]
        public void Decode_0x6B_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x6B));
        }

        [TestMethod]
        public void Decode_0x6C_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x6C));
        }

        [TestMethod]
        public void Decode_0x6D_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x6D));
        }

        [TestMethod]
        public void Decode_0x6E_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x6E));
        }

        [TestMethod]
        public void Decode_0x6F_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x6F));
        }

        [TestMethod]
        public void Decode_0x70_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x70));
        }

        [TestMethod]
        public void Decode_0x71_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x71));
        }

        [TestMethod]
        public void Decode_0x72_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x72));
        }

        [TestMethod]
        public void Decode_0x73_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x73));
        }

        [TestMethod]
        public void Decode_0x74_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x74));
        }

        [TestMethod]
        public void Decode_0x75_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x75));
        }

        [TestMethod]
        public void Decode_0x76_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x76));
        }

        [TestMethod]
        public void Decode_0x77_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x77));
        }

        [TestMethod]
        public void Decode_0x78_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x78));
        }

        [TestMethod]
        public void Decode_0x79_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x79));
        }

        [TestMethod]
        public void Decode_0x7A_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x7A));
        }

        [TestMethod]
        public void Decode_0x7B_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x7B));
        }

        [TestMethod]
        public void Decode_0x7C_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x7C));
        }

        [TestMethod]
        public void Decode_0x7D_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x7D));
        }

        [TestMethod]
        public void Decode_0x7E_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x7E));
        }

        [TestMethod]
        public void Decode_0x7F_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x7F));
        }

        [TestMethod]
        public void Decode_0x80_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x80));
        }

        [TestMethod]
        public void Decode_0x81_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x81));
        }

        [TestMethod]
        public void Decode_0x82_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x82));
        }

        [TestMethod]
        public void Decode_0x83_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x83));
        }

        [TestMethod]
        public void Decode_0x84_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x84));
        }

        [TestMethod]
        public void Decode_0x85_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x85));
        }

        [TestMethod]
        public void Decode_0x86_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x86));
        }

        [TestMethod]
        public void Decode_0x87_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x87));
        }

        [TestMethod]
        public void Decode_0x88_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x88));
        }

        [TestMethod]
        public void Decode_0x89_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x89));
        }

        [TestMethod]
        public void Decode_0x8A_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x8A));
        }

        [TestMethod]
        public void Decode_0x8B_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x8B));
        }

        [TestMethod]
        public void Decode_0x8C_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x8C));
        }

        [TestMethod]
        public void Decode_0x8D_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x8D));
        }

        [TestMethod]
        public void Decode_0x8E_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x8E));
        }

        [TestMethod]
        public void Decode_0x8F_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x8F));
        }

        [TestMethod]
        public void Decode_0x90_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x90));
        }

        [TestMethod]
        public void Decode_0x91_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x91));
        }

        [TestMethod]
        public void Decode_0x92_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x92));
        }

        [TestMethod]
        public void Decode_0x93_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x93));
        }

        [TestMethod]
        public void Decode_0x94_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x94));
        }

        [TestMethod]
        public void Decode_0x95_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x95));
        }

        [TestMethod]
        public void Decode_0x96_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x96));
        }

        [TestMethod]
        public void Decode_0x97_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x97));
        }

        [TestMethod]
        public void Decode_0x98_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x98));
        }

        [TestMethod]
        public void Decode_0x99_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x99));
        }

        [TestMethod]
        public void Decode_0x9A_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x9A));
        }

        [TestMethod]
        public void Decode_0x9B_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x9B));
        }

        [TestMethod]
        public void Decode_0x9C_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x9C));
        }

        [TestMethod]
        public void Decode_0x9D_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x9D));
        }

        [TestMethod]
        public void Decode_0x9E_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x9E));
        }

        [TestMethod]
        public void Decode_0x9F_To_()
        {
            Assert.AreEqual("", decoder.Decode(0x9F));
        }

        [TestMethod]
        public void Decode_0xA0_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xA0));
        }

        [TestMethod]
        public void Decode_0xA1_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xA1));
        }

        [TestMethod]
        public void Decode_0xA2_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xA2));
        }

        [TestMethod]
        public void Decode_0xA3_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xA3));
        }

        [TestMethod]
        public void Decode_0xA4_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xA4));
        }

        [TestMethod]
        public void Decode_0xA5_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xA5));
        }

        [TestMethod]
        public void Decode_0xA6_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xA6));
        }

        [TestMethod]
        public void Decode_0xA7_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xA7));
        }

        [TestMethod]
        public void Decode_0xA8_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xA8));
        }

        [TestMethod]
        public void Decode_0xA9_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xA9));
        }

        [TestMethod]
        public void Decode_0xAA_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xAA));
        }

        [TestMethod]
        public void Decode_0xAB_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xAB));
        }

        [TestMethod]
        public void Decode_0xAC_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xAC));
        }

        [TestMethod]
        public void Decode_0xAD_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xAD));
        }

        [TestMethod]
        public void Decode_0xAE_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xAE));
        }

        [TestMethod]
        public void Decode_0xAF_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xAF));
        }

        [TestMethod]
        public void Decode_0xB0_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xB0));
        }

        [TestMethod]
        public void Decode_0xB1_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xB1));
        }

        [TestMethod]
        public void Decode_0xB2_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xB2));
        }

        [TestMethod]
        public void Decode_0xB3_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xB3));
        }

        [TestMethod]
        public void Decode_0xB4_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xB4));
        }

        [TestMethod]
        public void Decode_0xB5_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xB5));
        }

        [TestMethod]
        public void Decode_0xB6_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xB6));
        }

        [TestMethod]
        public void Decode_0xB7_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xB7));
        }

        [TestMethod]
        public void Decode_0xB8_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xB8));
        }

        [TestMethod]
        public void Decode_0xB9_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xB9));
        }

        [TestMethod]
        public void Decode_0xBA_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xBA));
        }

        [TestMethod]
        public void Decode_0xBB_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xBB));
        }

        [TestMethod]
        public void Decode_0xBC_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xBC));
        }

        [TestMethod]
        public void Decode_0xBD_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xBD));
        }

        [TestMethod]
        public void Decode_0xBE_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xBE));
        }

        [TestMethod]
        public void Decode_0xBF_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xBF));
        }

        [TestMethod]
        public void Decode_0xC0_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xC0));
        }

        [TestMethod]
        public void Decode_0xC1_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xC1));
        }

        [TestMethod]
        public void Decode_0xC2_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xC2));
        }

        [TestMethod]
        public void Decode_0xC3_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xC3));
        }

        [TestMethod]
        public void Decode_0xC4_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xC4));
        }

        [TestMethod]
        public void Decode_0xC5_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xC5));
        }

        [TestMethod]
        public void Decode_0xC6_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xC6));
        }

        [TestMethod]
        public void Decode_0xC7_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xC7));
        }

        [TestMethod]
        public void Decode_0xC8_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xC8));
        }

        [TestMethod]
        public void Decode_0xC9_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xC9));
        }

        [TestMethod]
        public void Decode_0xCA_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xCA));
        }

        [TestMethod]
        public void Decode_0xCB_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xCB));
        }

        [TestMethod]
        public void Decode_0xCC_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xCC));
        }

        [TestMethod]
        public void Decode_0xCD_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xCD));
        }

        [TestMethod]
        public void Decode_0xCE_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xCE));
        }

        [TestMethod]
        public void Decode_0xCF_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xCF));
        }

        [TestMethod]
        public void Decode_0xD0_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xD0));
        }

        [TestMethod]
        public void Decode_0xD1_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xD1));
        }

        [TestMethod]
        public void Decode_0xD2_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xD2));
        }

        [TestMethod]
        public void Decode_0xD3_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xD3));
        }

        [TestMethod]
        public void Decode_0xD4_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xD4));
        }

        [TestMethod]
        public void Decode_0xD5_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xD5));
        }

        [TestMethod]
        public void Decode_0xD6_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xD6));
        }

        [TestMethod]
        public void Decode_0xD7_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xD7));
        }

        [TestMethod]
        public void Decode_0xD8_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xD8));
        }

        [TestMethod]
        public void Decode_0xD9_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xD9));
        }

        [TestMethod]
        public void Decode_0xDA_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xDA));
        }

        [TestMethod]
        public void Decode_0xDB_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xDB));
        }

        [TestMethod]
        public void Decode_0xDC_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xDC));
        }

        [TestMethod]
        public void Decode_0xDD_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xDD));
        }

        [TestMethod]
        public void Decode_0xDE_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xDE));
        }

        [TestMethod]
        public void Decode_0xDF_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xDF));
        }

        [TestMethod]
        public void Decode_0xE0_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xE0));
        }

        [TestMethod]
        public void Decode_0xE1_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xE1));
        }

        [TestMethod]
        public void Decode_0xE2_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xE2));
        }

        [TestMethod]
        public void Decode_0xE3_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xE3));
        }

        [TestMethod]
        public void Decode_0xE4_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xE4));
        }

        [TestMethod]
        public void Decode_0xE5_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xE5));
        }

        [TestMethod]
        public void Decode_0xE6_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xE6));
        }

        [TestMethod]
        public void Decode_0xE7_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xE7));
        }

        [TestMethod]
        public void Decode_0xE8_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xE8));
        }

        [TestMethod]
        public void Decode_0xE9_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xE9));
        }

        [TestMethod]
        public void Decode_0xEA_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xEA));
        }

        [TestMethod]
        public void Decode_0xEB_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xEB));
        }

        [TestMethod]
        public void Decode_0xEC_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xEC));
        }

        [TestMethod]
        public void Decode_0xED_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xED));
        }

        [TestMethod]
        public void Decode_0xEE_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xEE));
        }

        [TestMethod]
        public void Decode_0xEF_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xEF));
        }

        [TestMethod]
        public void Decode_0xF0_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xF0));
        }

        [TestMethod]
        public void Decode_0xF1_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xF1));
        }

        [TestMethod]
        public void Decode_0xF2_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xF2));
        }

        [TestMethod]
        public void Decode_0xF3_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xF3));
        }

        [TestMethod]
        public void Decode_0xF4_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xF4));
        }

        [TestMethod]
        public void Decode_0xF5_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xF5));
        }

        [TestMethod]
        public void Decode_0xF6_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xF6));
        }

        [TestMethod]
        public void Decode_0xF7_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xF7));
        }

        [TestMethod]
        public void Decode_0xF8_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xF8));
        }

        [TestMethod]
        public void Decode_0xF9_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xF9));
        }

        [TestMethod]
        public void Decode_0xFA_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xFA));
        }

        [TestMethod]
        public void Decode_0xFB_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xFB));
        }

        [TestMethod]
        public void Decode_0xFC_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xFC));
        }

        [TestMethod]
        public void Decode_0xFD_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xFD));
        }

        [TestMethod]
        public void Decode_0xFE_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xFE));
        }

        [TestMethod]
        public void Decode_0xFF_To_()
        {
            Assert.AreEqual("", decoder.Decode(0xFF));
        }
    }
}
