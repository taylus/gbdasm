using System;
using System.IO;
using System.Text;

namespace GBDasm.Core
{
    public class Disassembler
    {
        public RomFile RomFile { get; private set; }
        public Decoder Decoder { get; private set; }
        public int TargetLineWidth { get; private set; } = 40;

        public Disassembler(RomFile rom, Decoder decoder)
        {
            RomFile = rom;
            Decoder = decoder;
        }

        public Disassembler(RomFile rom, Decoder decoder, int targetLineWidth) : this(rom, decoder)
        {
            TargetLineWidth = targetLineWidth;
        }

        /// <summary>
        /// Produce RGBDS-compatible assembly of the ROM file.
        /// </summary>
        public string Disassemble()
        {
            var sb = new StringBuilder();
            for (int bankNumber = 0; bankNumber < RomFile.NumberOfBanks; bankNumber++)
            {
                sb.AppendLine($"SECTION \"rom{bankNumber}\", ROM{(bankNumber == 0 ? '0' : 'X')}");
                for (int addressWithinBank = 0; addressWithinBank < Math.Min(RomFile.BankSize, RomFile.Data.Length); addressWithinBank++)
                {
                    int absoluteAddress = (bankNumber * RomFile.BankSize) + addressWithinBank;
                    if (RomFile.IsInHeader(absoluteAddress))
                    {
                        //don't decode the ROM header as instructions
                        sb.AppendLine($"db ${RomFile.Data[absoluteAddress]:x2} ;{absoluteAddress:x4}");
                    }
                    else
                    {
                        var data = new ArraySegment<byte>(RomFile.Data, absoluteAddress, Math.Min(RomFile.BankSize, RomFile.Data.Length) - addressWithinBank);
                        string dasm = Decoder.Decode(data, out int instructionLength);
                        string comment = $";${absoluteAddress:x4}";
                        sb.AppendLine($"{dasm}{GetWhitespaceBetween(dasm, comment)}{comment}");
                        addressWithinBank += instructionLength - 1;
                    }
                }
            }
            return sb.ToString().Trim();
        }

        public void Disassemble(string path)
        {
            File.WriteAllText(path, Disassemble());
        }

        private string GetWhitespaceBetween(string instruction, string comment)
        {
            int spaces = TargetLineWidth - instruction.Length - comment.Length;
            if (spaces < 0) spaces = 0;
            return new string(' ', spaces);
        }
    }
}
