using GBDasm.Core;

namespace GBDasm.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var rom = new RomFile(@"roms\Link's Awakening.gb");
            rom.HexDump(bytesPerLine: 16, stopAfterBytes: 256);
        }
    }
}
