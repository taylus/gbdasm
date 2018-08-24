using System;

namespace GBDasm.Core
{
    /// <summary>
    /// Thrown when the decoder encounters an undefined opcode at the specified address.
    /// </summary>
    [Serializable]
    public class UndefinedOpcodeException : Exception
    {
        public byte OpCode { get; private set; }
        public int Address { get; private set; }

        public UndefinedOpcodeException(byte opcode, int addr) : this($"Undefined opcode {opcode:x2} at address ${addr:x4}.", opcode, addr)
        {

        }

        public UndefinedOpcodeException(string message, byte opcode, int addr) : this(message, null, opcode, addr)
        {

        }

        public UndefinedOpcodeException(string message, Exception innerException, byte opcode, int addr) : base(message, innerException)
        {
            OpCode = opcode;
            Address = addr;
        }
    }
}
