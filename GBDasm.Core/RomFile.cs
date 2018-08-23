using System;
using System.IO;

namespace GBDasm.Core
{
    public class RomFile
    {
        private readonly byte[] data;

        /// <summary>
        /// Loads a ROM from the file at the given path.
        /// </summary>
        public RomFile(string path)
        {
            data = File.ReadAllBytes(path);
        }

        /// <summary>
        /// Prints the first N bytes of the ROM to the console.
        /// </summary>
        public void HexDump(int bytesPerLine = 16, int? stopAfterBytes = null)
        {
            HexDump(data, bytesPerLine, stopAfterBytes);
        }

        /// <summary>
        /// Prints the first N bytes of the given buffer to the console.
        /// </summary>
        private static void HexDump(byte[] bytes, int bytesPerLine = 16, int? stopAfterBytes = null)
        {
            int length = stopAfterBytes ?? bytes.Length;
            for (int i = 0; i < length; i++)
            {
                Console.Write("{0:X2} ", bytes[i]);
                if (i % bytesPerLine == bytesPerLine - 1) Console.WriteLine();
            }
        }
    }
}
