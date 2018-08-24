using System;
using System.IO;
using System.Text;

namespace GBDasm.Core
{
    public class Disassembler
    {
        public RomFile RomFile { get; private set; }
        public Decoder Decoder { get; private set; }

        public Disassembler(RomFile rom, Decoder decoder)
        {
            RomFile = rom;
            Decoder = decoder;
        }

        public string Disassemble()
        {
            //return Decoder.Decode(RomFile.Data); // can't just do this! decoder treats data sections as instructions and may see invalid opcodes

            var sb = new StringBuilder();
            for(int i = 0; i < RomFile.Data.Length; i++)
            {
                if (RomFile.IsInHeader(i))
                {
                    //don't decode the ROM header as instructions
                    sb.AppendLine($"{i:x4}: db ${RomFile.Data[i]:x2}");
                }
                else
                { 
                    var seg = new ArraySegment<byte>(RomFile.Data, i, RomFile.Data.Length - i);
                    try
                    {
                        sb.AppendLine($"{i:x4}: {Decoder.Decode(seg)}");
                    }
                    catch (UndefinedOpcodeException ex)
                    {
                        sb.AppendLine($"{i:x4}: <invalid opcode ${ex.OpCode:x2}>");
                    }

                    i += Decoder.AdditionalBytesToAdvance;
                }
            }

            return sb.ToString();
        }

        public void Disassemble(string path)
        {
            File.WriteAllText(path, Disassemble());
        }
    }
}
