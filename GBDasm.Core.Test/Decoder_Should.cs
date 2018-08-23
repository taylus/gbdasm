using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GBDasm.Core.Test
{
    [TestClass]
    public class Decoder_Should
    {
        private Decoder decoder = new Decoder();

        [TestMethod]
        public void Decode_0x00_To_Nop()
        {
            Assert.AreEqual("nop", decoder.Decode(0x00));
        }

        [TestMethod]
        public void Decode_0x01_X_Y_To_Load_Register_Pair_Immediate()
        {
            Assert.AreEqual("ld bc, $abcd", decoder.Decode(0x01, 0xcd, 0xab));
        }

        [TestMethod]
        public void Decode_0x02_To_()
        {
            //...
        }

        //TODO: write the decoder using TDD
        //figure out what each opcode/bit pattern is supposed to decode to, write the test, then implement it in the Decoder class
    }
}
