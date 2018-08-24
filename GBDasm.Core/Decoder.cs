using System;
using System.Text;

namespace GBDasm.Core
{
    /// <summary>
    /// The world's most naive and inefficient decoder.
    /// Transforms byte sequences into GBZ80 instructions.
    /// </summary>
    /// <see cref="http://www.pastraiser.com/cpu/gameboy/gameboy_opcodes.html"/>
    /// <see cref="http://goldencrystal.free.fr/GBZ80Opcodes.pdf"/>
    /// <see cref="https://rednex.github.io/rgbds/gbz80.7.html"/>
    public class Decoder
    {
        /// <summary>
        /// TODO: genericize to indexing into an array of mnemonics and then
        /// string-replacing with following bytes if applicable (like BizHawk)
        /// </summary>
        public string Decode(byte[] data, int startAddress = 0)
        {
            if (startAddress < 0) throw new ArgumentException("Start address cannot be negative.", nameof(startAddress));

            var sb = new StringBuilder();
            byte opcode, arg1, arg2;

            for (int addr = 0; addr < data.Length;)
            {
                opcode = data[addr++];
                switch (opcode)
                {
                    case 0x00:
                        sb.AppendLine("nop");
                        break;
                    case 0x01:
                        arg1 = data[addr++];
                        arg2 = data[addr++];
                        sb.AppendLine($"ld bc, ${ToLittleEndian(arg1, arg2):x4}");
                        break;
                    case 0x02:
                        sb.AppendLine("ld [bc], a");
                        break;
                    case 0x03:
                        sb.AppendLine("inc bc");
                        break;
                    case 0x04:
                        sb.AppendLine("inc b");
                        break;
                    case 0x05:
                        sb.AppendLine("dec b");
                        break;
                    case 0x06:
                        arg1 = data[addr++];
                        sb.AppendLine($"ld b, ${arg1:x2}");
                        break;
                    case 0x07:
                        sb.AppendLine("rlc a");
                        break;
                    case 0x08:
                        arg1 = data[addr++];
                        arg2 = data[addr++];
                        sb.AppendLine($"ld [${ToLittleEndian(arg1, arg2):x4}], sp");
                        break;
                    case 0x09:
                        sb.AppendLine("add hl, bc");
                        break;
                    case 0x0A:
                        sb.AppendLine("ld a, [bc]");
                        break;
                    case 0x0B:
                        sb.AppendLine("dec bc");
                        break;
                    case 0x0C:
                        sb.AppendLine("inc c");
                        break;
                    case 0x0D:
                        sb.AppendLine("dec c");
                        break;
                    case 0x0E:
                        arg1 = data[addr++];
                        sb.AppendLine($"ld c, ${arg1:x2}");
                        break;
                    case 0x0F:
                        sb.AppendLine("rrc a");
                        break;
                    case 0x10:
                        sb.AppendLine("stop");
                        break;
                    case 0x11:
                        arg1 = data[addr++];
                        arg2 = data[addr++];
                        sb.AppendLine($"ld de, ${ToLittleEndian(arg1, arg2):x4}");
                        break;
                    case 0x12:
                        sb.AppendLine("ld [de], a");
                        break;
                    case 0x13:
                        sb.AppendLine("inc de");
                        break;
                    case 0x14:
                        sb.AppendLine("inc d");
                        break;
                    case 0x15:
                        sb.AppendLine("dec d");
                        break;
                    case 0x16:
                        arg1 = data[addr++];
                        sb.AppendLine($"ld d, ${arg1:x2}");
                        break;
                    case 0x17:
                        sb.AppendLine("rl a");
                        break;
                    case 0x18:
                        arg1 = data[addr++];
                        int jumpAddress = startAddress + addr + (sbyte)arg1; //offset is signed (relative jump)
                        if (jumpAddress < 0)
                        {
                            sb.AppendLine($"jr $0000");
                        }
                        else if (jumpAddress > 0xffff)
                        {
                            //wrap around from $0000
                            //this is bgb's behavior (is this what a real Game Boy does? a ROM would never do this anyway, right...?)
                            sb.AppendLine($"jr ${(jumpAddress - 0x10000):x4}");
                        }
                        else
                        {
                            sb.AppendLine($"jr ${jumpAddress:x4}");
                        }
                        break;
                    case 0x19:
                        sb.AppendLine("");
                        break;
                    case 0x1A:
                        sb.AppendLine("");
                        break;
                    case 0x1B:
                        sb.AppendLine("");
                        break;
                    case 0x1C:
                        sb.AppendLine("");
                        break;
                }
            }

            var dasm = sb.ToString().Trim();
            return string.IsNullOrWhiteSpace(dasm) ? null : dasm;
        }

        /// <summary>
        /// Turns the given two bytes into a little-endian 16-bit number.
        /// (The second byte makes up the most significant bits of the new number)
        /// E.g. $cd, $ab => $abcd
        /// </summary>
        private static ushort ToLittleEndian(byte byte1, byte byte2)
        {
            return (ushort)((byte2 << 8) | byte1);
        }
    }
}
