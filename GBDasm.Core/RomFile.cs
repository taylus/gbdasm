using System;
using System.IO;

namespace GBDasm.Core
{
    public class RomFile
    {
        /// <summary>
        /// Raw bytes of ROM file. Contains both instructions and data.
        /// </summary>
        public byte[] Data { get; private set; }

        /// <summary>
        /// Header section of the ROM containing metadata about the game.
        /// </summary>
        public ArraySegment<byte> Header => new ArraySegment<byte>(Data, 0x104, 0x4C);

        /// <summary>
        /// Returns true if the given address falls within the ROM header, false otherwise.
        /// </summary>
        public bool IsInHeader(int address) => (address >= Header.Offset && address < (Header.Offset + Header.Count));

        /// <summary>
        /// Loads a ROM from the file at the given path.
        /// </summary>
        public RomFile(string path)
        {
            Data = File.ReadAllBytes(path);
        }

        /// <summary>
        /// Prints the first N bytes of the ROM to the console.
        /// </summary>
        public void HexDump(int bytesPerLine = 16, int? stopAfterBytes = null)
        {
            HexDump(Data, bytesPerLine, stopAfterBytes);
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
