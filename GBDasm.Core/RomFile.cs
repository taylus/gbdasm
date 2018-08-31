using System;
using System.IO;

namespace GBDasm.Core
{
    public class RomFile
    {
        /// <summary>
        /// Raw bytes of entire ROM file. Contains both instructions and data.
        /// </summary>
        public byte[] Data { get; private set; }

        public const int BankSize = 0x4000;
        public int NumberOfBanks => Math.Max(1, Data.Length / BankSize);
        public bool HasHeader { get; set; }

        /// <summary>
        /// Header section of the ROM containing metadata about the game.
        /// </summary>
        public ArraySegment<byte> Header => new ArraySegment<byte>(Data, 0x104, 0x4C);

        /// <summary>
        /// Returns true if the given address falls within the ROM header, false otherwise.
        /// </summary>
        public bool IsInHeader(int address) => (HasHeader && address >= Header.Offset && address < (Header.Offset + Header.Count));

        /// <summary>
        /// Loads a ROM from the given data.
        /// </summary>
        public RomFile(byte[] data)
        {
            Data = data;
        }

        /// <summary>
        /// Loads a ROM from the file at the given path.
        /// </summary>
        public RomFile(string path)
        {
            Data = File.ReadAllBytes(path);
        }

        /// <summary>
        /// Returns the ROM bank with the given number.
        /// Game Boy ROMs are split up into 16KB banks.
        /// The first bank is always addressable from $0000 - $3fff.
        /// The other banks are switched into range $4000 - $7fff using a memory management controller (MMC).
        /// </summary>
        /// <see cref="http://gameboy.mongenel.com/dmg/asmmemmap.html"/>
        public ArraySegment<byte> GetBank(int bankNumber)
        {
            if (bankNumber < NumberOfBanks) throw new ArgumentException("Bank number exceeds number of banks in ROM.", nameof(bankNumber));
            return new ArraySegment<byte>(Data, bankNumber * BankSize, BankSize);
        }
    }
}
