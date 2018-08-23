using System.Text;

namespace GBDasm.Core
{
    /// <summary>
    /// The world's most naive and inefficient decoder.
    /// Transforms byte sequences into GBZ80 instructions.
    /// </summary>
    /// <see cref="http://goldencrystal.free.fr/GBZ80Opcodes.pdf"/>
    /// <see cref="https://rednex.github.io/rgbds/gbz80.7.html"/>
    public class Decoder
    {
        public string Decode(params byte[] data)
        {
            var sb = new StringBuilder();
            byte opcode, arg1, arg2;

            for(int i = 0; i < data.Length;)
            {
                opcode = data[i++];
                switch(opcode)
                {
                    case 0x00:
                        sb.AppendLine("nop");
                        break;
                    case 0x01:
                        arg1 = data[i++];
                        arg2 = data[i++];
                        sb.AppendLine($"ld bc, ${ToLittleEndian(arg1, arg2):x4}");
                        break;
                    case 0x02:
                        //...
                        break;
                }
            }

            return sb.ToString().Trim();
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
