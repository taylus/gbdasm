﻿using System;
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
        /// The number of bytes skipped by the last instruction decode (if any).
        /// E.g. NOP is one byte, so zero bytes are skipped after it for arguments.
        /// However, JR n is two bytes, so one byte is skipped after it for its relative offset.
        /// </summary>
        public int AdditionalBytesToAdvance { get; private set; }

        public string Decode(byte[] data, int baseAddress = 0)
        {
            return Decode(new ArraySegment<byte>(data), baseAddress);
        }

        /// <summary>
        /// TODO: genericize to indexing into an array of mnemonics and then
        /// string-replacing with following bytes if applicable (like BizHawk)
        /// </summary>
        public string Decode(ArraySegment<byte> data, int baseAddress = 0)
        {
            if (baseAddress < 0) throw new ArgumentException("Base address cannot be negative.", nameof(baseAddress));

            var sb = new StringBuilder();
            byte opcode, arg1, arg2;

            int addr = data.Offset;
            opcode = data.Array[addr++];

            switch (opcode)
            {
                case 0x00:
                    sb.AppendLine("nop");
                    break;
                case 0x01:
                    arg1 = data.Array[addr++];
                    arg2 = data.Array[addr++];
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
                    arg1 = data.Array[addr++];
                    sb.AppendLine($"ld b, ${arg1:x2}");
                    break;
                case 0x07:
                    sb.AppendLine("rlc a");
                    break;
                case 0x08:
                    arg1 = data.Array[addr++];
                    arg2 = data.Array[addr++];
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
                    arg1 = data.Array[addr++];
                    sb.AppendLine($"ld c, ${arg1:x2}");
                    break;
                case 0x0F:
                    sb.AppendLine("rrc a");
                    break;
                case 0x10:
                    sb.AppendLine("stop");
                    break;
                case 0x11:
                    arg1 = data.Array[addr++];
                    arg2 = data.Array[addr++];
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
                    arg1 = data.Array[addr++];
                    sb.AppendLine($"ld d, ${arg1:x2}");
                    break;
                case 0x17:
                    sb.AppendLine("rl a");
                    break;
                case 0x18:
                    arg1 = data.Array[addr++];
                    sb.AppendLine($"jr ${CalculateAbsoluteJumpAddressFromRelativeOffset(baseAddress + addr, (sbyte)arg1):x4}");
                    break;
                case 0x19:
                    sb.AppendLine("add hl, de");
                    break;
                case 0x1A:
                    sb.AppendLine("ld a, [de]");
                    break;
                case 0x1B:
                    sb.AppendLine("dec de");
                    break;
                case 0x1C:
                    sb.AppendLine("inc e");
                    break;
                case 0x1D:
                    sb.AppendLine("dec e");
                    break;
                case 0x1E:
                    arg1 = data.Array[addr++];
                    sb.AppendLine($"ld e, ${arg1:x2}");
                    break;
                case 0x1F:
                    sb.AppendLine("rr a");
                    break;
                case 0x20:
                    arg1 = data.Array[addr++];
                    sb.AppendLine($"jr nz, ${CalculateAbsoluteJumpAddressFromRelativeOffset(baseAddress + addr, (sbyte)arg1):x4}");
                    break;
                case 0x21:
                    arg1 = data.Array[addr++];
                    arg2 = data.Array[addr++];
                    sb.AppendLine($"ld hl, ${ToLittleEndian(arg1, arg2):x4}");
                    break;
                case 0x22:
                    sb.AppendLine("ldi [hl], a");
                    break;
                case 0x23:
                    sb.AppendLine("inc hl");
                    break;
                case 0x24:
                    sb.AppendLine("inc h");
                    break;
                case 0x25:
                    sb.AppendLine("dec h");
                    break;
                case 0x26:
                    arg1 = data.Array[addr++];
                    sb.AppendLine($"ld h, ${arg1:x2}");
                    break;
                case 0x27:
                    sb.AppendLine("daa");
                    break;
                case 0x28:
                    arg1 = data.Array[addr++];
                    sb.AppendLine($"jr z, ${CalculateAbsoluteJumpAddressFromRelativeOffset(baseAddress + addr, (sbyte)arg1):x4}");
                    break;
                case 0x29:
                    sb.AppendLine("add hl, hl");
                    break;
                case 0x2A:
                    sb.AppendLine("ldi a, [hl]");
                    break;
                case 0x2B:
                    sb.AppendLine("dec hl");
                    break;
                case 0x2C:
                    sb.AppendLine("inc l");
                    break;
                case 0x2D:
                    sb.AppendLine("dec l");
                    break;
                case 0x2E:
                    arg1 = data.Array[addr++];
                    sb.AppendLine($"ld l, ${arg1:x2}");
                    break;
                case 0x2F:
                    sb.AppendLine("cpl");
                    break;
                case 0x30:
                    arg1 = data.Array[addr++];
                    sb.AppendLine($"jr nc, ${CalculateAbsoluteJumpAddressFromRelativeOffset(baseAddress + addr, (sbyte)arg1):x4}");
                    break;
                case 0x31:
                    arg1 = data.Array[addr++];
                    arg2 = data.Array[addr++];
                    sb.AppendLine($"ld sp, ${ToLittleEndian(arg1, arg2):x4}");
                    break;
                case 0x32:
                    sb.AppendLine("ldd [hl], a");
                    break;
                case 0x33:
                    sb.AppendLine("inc sp");
                    break;
                case 0x34:
                    sb.AppendLine("inc [hl]");
                    break;
                case 0x35:
                    sb.AppendLine("dec [hl]");
                    break;
                case 0x36:
                    arg1 = data.Array[addr++];
                    sb.AppendLine($"ld [hl], ${arg1:x2}");
                    break;
                case 0x37:
                    sb.AppendLine("scf");
                    break;
                case 0x38:
                    arg1 = data.Array[addr++];
                    sb.AppendLine($"jr c, ${CalculateAbsoluteJumpAddressFromRelativeOffset(baseAddress + addr, (sbyte)arg1):x4}");
                    break;
                case 0x39:
                    sb.AppendLine("add hl, sp");
                    break;
                case 0x3A:
                    sb.AppendLine("ldd a, [hl]");
                    break;
                case 0x3B:
                    sb.AppendLine("dec sp");
                    break;
                case 0x3C:
                    sb.AppendLine("inc a");
                    break;
                case 0x3D:
                    sb.AppendLine("dec a");
                    break;
                case 0x3E:
                    arg1 = data.Array[addr++];
                    sb.AppendLine($"ld a, ${arg1:x2}");
                    break;
                case 0x3F:
                    sb.AppendLine("ccf");
                    break;
                case 0x40:
                    sb.AppendLine("ld b, b");
                    break;
                case 0x41:
                    sb.AppendLine("ld b, c");
                    break;
                case 0x42:
                    sb.AppendLine("ld b, d");
                    break;
                case 0x43:
                    sb.AppendLine("ld b, e");
                    break;
                case 0x44:
                    sb.AppendLine("ld b, h");
                    break;
                case 0x45:
                    sb.AppendLine("ld b, l");
                    break;
                case 0x46:
                    sb.AppendLine("ld b, [hl]");
                    break;
                case 0x47:
                    sb.AppendLine("ld b, a");
                    break;
                case 0x48:
                    sb.AppendLine("ld c, b");
                    break;
                case 0x49:
                    sb.AppendLine("ld c, c");
                    break;
                case 0x4A:
                    sb.AppendLine("ld c, d");
                    break;
                case 0x4B:
                    sb.AppendLine("ld c, e");
                    break;
                case 0x4C:
                    sb.AppendLine("ld c, h");
                    break;
                case 0x4D:
                    sb.AppendLine("ld c, l");
                    break;
                case 0x4E:
                    sb.AppendLine("ld c, [hl]");
                    break;
                case 0x4F:
                    sb.AppendLine("ld c, a");
                    break;
                case 0x50:
                    sb.AppendLine("ld d, b");
                    break;
                case 0x51:
                    sb.AppendLine("ld d, c");
                    break;
                case 0x52:
                    sb.AppendLine("ld d, d");
                    break;
                case 0x53:
                    sb.AppendLine("ld d, e");
                    break;
                case 0x54:
                    sb.AppendLine("ld d, h");
                    break;
                case 0x55:
                    sb.AppendLine("ld d, l");
                    break;
                case 0x56:
                    sb.AppendLine("ld d, [hl]");
                    break;
                case 0x57:
                    sb.AppendLine("ld d, a");
                    break;
                case 0x58:
                    sb.AppendLine("ld e, b");
                    break;
                case 0x59:
                    sb.AppendLine("ld e, c");
                    break;
                case 0x5A:
                    sb.AppendLine("ld e, d");
                    break;
                case 0x5B:
                    sb.AppendLine("ld e, e");
                    break;
                case 0x5C:
                    sb.AppendLine("ld e, h");
                    break;
                case 0x5D:
                    sb.AppendLine("ld e, l");
                    break;
                case 0x5E:
                    sb.AppendLine("ld e, [hl]");
                    break;
                case 0x5F:
                    sb.AppendLine("ld e, a");
                    break;
                case 0x60:
                    sb.AppendLine("ld h, b");
                    break;
                case 0x61:
                    sb.AppendLine("ld h, c");
                    break;
                case 0x62:
                    sb.AppendLine("ld h, d");
                    break;
                case 0x63:
                    sb.AppendLine("ld h, e");
                    break;
                case 0x64:
                    sb.AppendLine("ld h, h");
                    break;
                case 0x65:
                    sb.AppendLine("ld h, l");
                    break;
                case 0x66:
                    sb.AppendLine("ld h, [hl]");
                    break;
                case 0x67:
                    sb.AppendLine("ld h, a");
                    break;
                case 0x68:
                    sb.AppendLine("ld l, b");
                    break;
                case 0x69:
                    sb.AppendLine("ld l, c");
                    break;
                case 0x6A:
                    sb.AppendLine("ld l, d");
                    break;
                case 0x6B:
                    sb.AppendLine("ld l, e");
                    break;
                case 0x6C:
                    sb.AppendLine("ld l, h");
                    break;
                case 0x6D:
                    sb.AppendLine("ld l, l");
                    break;
                case 0x6E:
                    sb.AppendLine("ld l, [hl]");
                    break;
                case 0x6F:
                    sb.AppendLine("ld l, a");
                    break;
                case 0x70:
                    sb.AppendLine("ld [hl], b");
                    break;
                case 0x71:
                    sb.AppendLine("ld [hl], c");
                    break;
                case 0x72:
                    sb.AppendLine("ld [hl], d");
                    break;
                case 0x73:
                    sb.AppendLine("ld [hl], e");
                    break;
                case 0x74:
                    sb.AppendLine("ld [hl], h");
                    break;
                case 0x75:
                    sb.AppendLine("ld [hl], l");
                    break;
                case 0x76:
                    sb.AppendLine("halt");
                    break;
                case 0x77:
                    sb.AppendLine("ld [hl], a");
                    break;
                case 0x78:
                    sb.AppendLine("ld a, b");
                    break;
                case 0x79:
                    sb.AppendLine("ld a, c");
                    break;
                case 0x7A:
                    sb.AppendLine("ld a, d");
                    break;
                case 0x7B:
                    sb.AppendLine("ld a, e");
                    break;
                case 0x7C:
                    sb.AppendLine("ld a, h");
                    break;
                case 0x7D:
                    sb.AppendLine("ld a, l");
                    break;
                case 0x7E:
                    sb.AppendLine("ld a, [hl]");
                    break;
                case 0x7F:
                    sb.AppendLine("ld a, a");
                    break;
                case 0x80:
                    sb.AppendLine("add a, b");
                    break;
                case 0x81:
                    sb.AppendLine("add a, c");
                    break;
                case 0x82:
                    sb.AppendLine("add a, d");
                    break;
                case 0x83:
                    sb.AppendLine("add a, e");
                    break;
                case 0x84:
                    sb.AppendLine("add a, h");
                    break;
                case 0x85:
                    sb.AppendLine("add a, l");
                    break;
                case 0x86:
                    sb.AppendLine("add a, [hl]");
                    break;
                case 0x87:
                    sb.AppendLine("add a, a");
                    break;
                case 0x88:
                    sb.AppendLine("adc a, b");
                    break;
                case 0x89:
                    sb.AppendLine("adc a, c");
                    break;
                case 0x8A:
                    sb.AppendLine("adc a, d");
                    break;
                case 0x8B:
                    sb.AppendLine("adc a, e");
                    break;
                case 0x8C:
                    sb.AppendLine("adc a, h");
                    break;
                case 0x8D:
                    sb.AppendLine("adc a, l");
                    break;
                case 0x8E:
                    sb.AppendLine("adc a, [hl]");
                    break;
                case 0x8F:
                    sb.AppendLine("adc a, a");
                    break;
                case 0x90:
                    sb.AppendLine("sub a, b");
                    break;
                case 0x91:
                    sb.AppendLine("sub a, c");
                    break;
                case 0x92:
                    sb.AppendLine("sub a, d");
                    break;
                case 0x93:
                    sb.AppendLine("sub a, e");
                    break;
                case 0x94:
                    sb.AppendLine("sub a, h");
                    break;
                case 0x95:
                    sb.AppendLine("sub a, l");
                    break;
                case 0x96:
                    sb.AppendLine("sub a, [hl]");
                    break;
                case 0x97:
                    sb.AppendLine("sub a, a");
                    break;
                case 0x98:
                    sb.AppendLine("sbc a, b");
                    break;
                case 0x99:
                    sb.AppendLine("sbc a, c");
                    break;
                case 0x9A:
                    sb.AppendLine("sbc a, d");
                    break;
                case 0x9B:
                    sb.AppendLine("sbc a, e");
                    break;
                case 0x9C:
                    sb.AppendLine("sbc a, h");
                    break;
                case 0x9D:
                    sb.AppendLine("sbc a, l");
                    break;
                case 0x9E:
                    sb.AppendLine("sbc a, [hl]");
                    break;
                case 0x9F:
                    sb.AppendLine("sbc a, a");
                    break;
                case 0xA0:
                    sb.AppendLine("and b");
                    break;
                case 0xA1:
                    sb.AppendLine("and c");
                    break;
                case 0xA2:
                    sb.AppendLine("and d");
                    break;
                case 0xA3:
                    sb.AppendLine("and e");
                    break;
                case 0xA4:
                    sb.AppendLine("and h");
                    break;
                case 0xA5:
                    sb.AppendLine("and l");
                    break;
                case 0xA6:
                    sb.AppendLine("and [hl]");
                    break;
                case 0xA7:
                    sb.AppendLine("and a");
                    break;
                case 0xA8:
                    sb.AppendLine("xor b");
                    break;
                case 0xA9:
                    sb.AppendLine("xor c");
                    break;
                case 0xAA:
                    sb.AppendLine("xor d");
                    break;
                case 0xAB:
                    sb.AppendLine("xor e");
                    break;
                case 0xAC:
                    sb.AppendLine("xor h");
                    break;
                case 0xAD:
                    sb.AppendLine("xor l");
                    break;
                case 0xAE:
                    sb.AppendLine("xor [hl]");
                    break;
                case 0xAF:
                    sb.AppendLine("xor a");
                    break;
                case 0xB0:
                    sb.AppendLine("or b");
                    break;
                case 0xB1:
                    sb.AppendLine("or c");
                    break;
                case 0xB2:
                    sb.AppendLine("or d");
                    break;
                case 0xB3:
                    sb.AppendLine("or e");
                    break;
                case 0xB4:
                    sb.AppendLine("or h");
                    break;
                case 0xB5:
                    sb.AppendLine("or l");
                    break;
                case 0xB6:
                    sb.AppendLine("or [hl]");
                    break;
                case 0xB7:
                    sb.AppendLine("or a");
                    break;
                case 0xB8:
                    sb.AppendLine("cp b");
                    break;
                case 0xB9:
                    sb.AppendLine("cp c");
                    break;
                case 0xBA:
                    sb.AppendLine("cp d");
                    break;
                case 0xBB:
                    sb.AppendLine("cp e");
                    break;
                case 0xBC:
                    sb.AppendLine("cp h");
                    break;
                case 0xBD:
                    sb.AppendLine("cp l");
                    break;
                case 0xBE:
                    sb.AppendLine("cp [hl]");
                    break;
                case 0xBF:
                    sb.AppendLine("cp a");
                    break;
                case 0xC0:
                    sb.AppendLine("ret nz");
                    break;
                case 0xC1:
                    sb.AppendLine("pop bc");
                    break;
                case 0xC2:
                    arg1 = data.Array[addr++];
                    arg2 = data.Array[addr++];
                    sb.AppendLine($"jp nz, ${ToLittleEndian(arg1, arg2):x4}");
                    break;
                case 0xC3:
                    arg1 = data.Array[addr++];
                    arg2 = data.Array[addr++];
                    sb.AppendLine($"jp ${ToLittleEndian(arg1, arg2):x4}");
                    break;
                case 0xC4:
                    arg1 = data.Array[addr++];
                    arg2 = data.Array[addr++];
                    sb.AppendLine($"call nz, ${ToLittleEndian(arg1, arg2):x4}");
                    break;
                case 0xC5:
                    sb.AppendLine("push bc");
                    break;
                case 0xC6:
                    arg1 = data.Array[addr++];
                    sb.AppendLine($"add a, ${arg1:x2}");
                    break;
                case 0xC7:
                    sb.AppendLine("rst $00");
                    break;
                case 0xC8:
                    sb.AppendLine("ret z");
                    break;
                case 0xC9:
                    sb.AppendLine("ret");
                    break;
                case 0xCA:
                    arg1 = data.Array[addr++];
                    arg2 = data.Array[addr++];
                    sb.AppendLine($"jp z, ${ToLittleEndian(arg1, arg2):x4}");
                    break;
                case 0xCB:
                    //TODO: handle two-byte instructions
                    arg1 = data.Array[addr++];
                    throw new UndefinedOpcodeException(opcode, baseAddress + addr - 2);
                case 0xCC:
                    arg1 = data.Array[addr++];
                    arg2 = data.Array[addr++];
                    sb.AppendLine($"call z, ${ToLittleEndian(arg1, arg2):x4}");
                    break;
                case 0xCD:
                    arg1 = data.Array[addr++];
                    arg2 = data.Array[addr++];
                    sb.AppendLine($"call ${ToLittleEndian(arg1, arg2):x4}");
                    break;
                case 0xCE:
                    arg1 = data.Array[addr++];
                    sb.AppendLine($"adc a, ${arg1:x2}");
                    break;
                case 0xCF:
                    sb.AppendLine("rst $08");
                    break;
                case 0xD0:
                    sb.AppendLine("ret nc");
                    break;
                case 0xD1:
                    sb.AppendLine("pop de");
                    break;
                case 0xD2:
                    arg1 = data.Array[addr++];
                    arg2 = data.Array[addr++];
                    sb.AppendLine($"jp nc, ${ToLittleEndian(arg1, arg2):x4}");
                    break;
                case 0xD3:
                    throw new UndefinedOpcodeException(opcode, baseAddress + addr - 1);
                case 0xD4:
                    arg1 = data.Array[addr++];
                    arg2 = data.Array[addr++];
                    sb.AppendLine($"call nc, ${ToLittleEndian(arg1, arg2):x4}");
                    break;
                case 0xD5:
                    sb.AppendLine("push de");
                    break;
                case 0xD6:
                    arg1 = data.Array[addr++];
                    sb.AppendLine($"sub a, ${arg1:x2}");
                    break;
                case 0xD7:
                    sb.AppendLine("rst $10");
                    break;
                case 0xD8:
                    sb.AppendLine("ret c");
                    break;
                case 0xD9:
                    sb.AppendLine("reti");
                    break;
                case 0xDA:
                    arg1 = data.Array[addr++];
                    arg2 = data.Array[addr++];
                    sb.AppendLine($"jp c, ${ToLittleEndian(arg1, arg2):x4}");
                    break;
                case 0xDB:
                    throw new UndefinedOpcodeException(opcode, baseAddress + addr - 1);
                case 0xDC:
                    arg1 = data.Array[addr++];
                    arg2 = data.Array[addr++];
                    sb.AppendLine($"call c, ${ToLittleEndian(arg1, arg2):x4}");
                    break;
                case 0xDD:
                    throw new UndefinedOpcodeException(opcode, baseAddress + addr - 1);
                case 0xDE:
                    arg1 = data.Array[addr++];
                    sb.AppendLine($"sbc a, ${arg1:x2}");
                    break;
                case 0xDF:
                    sb.AppendLine("rst $18");
                    break;
                case 0xE0:
                    arg1 = data.Array[addr++];
                    sb.AppendLine($"ldh [${(0xFF00 + arg1):x2}], a");
                    break;
                case 0xE1:
                    sb.AppendLine("pop hl");
                    break;
                case 0xE2:
                    sb.AppendLine("ldh [$ff00 + c], a");
                    break;
                case 0xE3:
                case 0xE4:
                    throw new UndefinedOpcodeException(opcode, baseAddress + addr - 1);
                case 0xE5:
                    sb.AppendLine("push hl");
                    break;
                case 0xE6:
                    arg1 = data.Array[addr++];
                    sb.AppendLine($"and a, ${arg1:x2}");
                    break;
                case 0xE7:
                    sb.AppendLine("rst $20");
                    break;
                case 0xE8:
                    arg1 = data.Array[addr++];
                    sb.AppendLine($"add sp, ${arg1:x2}");
                    break;
                case 0xE9:
                    sb.AppendLine("jp [hl]");
                    break;
                case 0xEA:
                    arg1 = data.Array[addr++];
                    arg2 = data.Array[addr++];
                    sb.AppendLine($"ld [${ToLittleEndian(arg1, arg2):x4}], a");
                    break;
                case 0xEB:
                case 0xEC:
                case 0xED:
                    throw new UndefinedOpcodeException(opcode, baseAddress + addr - 1);
                case 0xEE:
                    arg1 = data.Array[addr++];
                    sb.AppendLine($"xor ${arg1:x2}");
                    break;
                case 0xEF:
                    sb.AppendLine("rst $28");
                    break;
                case 0xF0:
                    arg1 = data.Array[addr++];
                    sb.AppendLine($"ldh a, [${(0xFF00 + arg1):x2}]");
                    break;
                case 0xF1:
                    sb.AppendLine("pop af");
                    break;
                case 0xF2:
                    throw new UndefinedOpcodeException(opcode, baseAddress + addr - 1);
                case 0xF3:
                    sb.AppendLine("di");
                    break;
                case 0xF4:
                    throw new UndefinedOpcodeException(opcode, baseAddress + addr - 1);
                case 0xF5:
                    sb.AppendLine("push af");
                    break;
                case 0xF6:
                    arg1 = data.Array[addr++];
                    sb.AppendLine($"or ${arg1:x2}");
                    break;
                case 0xF7:
                    sb.AppendLine("rst $30");
                    break;
                case 0xF8:
                    arg1 = data.Array[addr++];
                    sb.AppendLine($"ld hl, [sp + ${arg1:x2}]");
                    break;
                case 0xF9:
                    sb.AppendLine("ld sp, hl");
                    break;
                case 0xFA:
                    arg1 = data.Array[addr++];
                    arg2 = data.Array[addr++];
                    sb.AppendLine($"ld a, [${ToLittleEndian(arg1, arg2):x4}]");
                    break;
                case 0xFB:
                    sb.AppendLine("ei");
                    break;
                case 0xFC:
                case 0xFD:
                    throw new UndefinedOpcodeException(opcode, baseAddress + addr - 1);
                case 0xFE:
                    arg1 = data.Array[addr++];
                    sb.AppendLine($"cp ${arg1:x2}");
                    break;
                case 0xFF:
                    sb.AppendLine("rst $38");
                    break;
            }

            AdditionalBytesToAdvance = addr - data.Offset - 1;
            return sb.ToString().Trim();
        }

        /// <summary>
        /// Calculates the absolute memory address to jump to from a relative jump (JR) instruction
        /// with the given offset executed from the given base address + offset.
        /// </summary>
        /// <param name="baseAddress">The base address of the program ROM (usually $0000).</param>
        /// <param name="currentAddress">The relative address of this instruction in ROM.</param>
        /// <param name="offset">The signed offset specified immediately after the JR instruction.</param>
        private static int CalculateAbsoluteJumpAddressFromRelativeOffset(int addressOfInstruction, sbyte offsetFromInstruction)
        {
            int jumpAddress = addressOfInstruction + offsetFromInstruction;

            //lower bounds check: addresses don't go negative
            if (jumpAddress < 0) return 0;

            //upper bounds check: wrap around from $0000
            //this is bgb's behavior (is this what a real Game Boy does? a ROM would never do this anyway, right...?)
            if (jumpAddress > 0xffff) return jumpAddress - 0x10000;

            return jumpAddress;
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
