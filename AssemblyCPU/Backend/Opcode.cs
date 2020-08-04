using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssemblyCPU.Backend
{
    public enum Operations
    {
        LDR = 1, 
        STR,
        ADD,
        SUB,
        MOV,
        CMP,
        B,
        BEQ,
        BNE,
        BGT,
        BLT,
        AND,
        ORR,
        EOR,
        MVN,
        LSL,
        LSR,
        HALT
    }

    public enum Addressing
    {
        Immediate,
        Direct
    }

    public class Opcode
    {
        private Operations _operation;
        private Addressing _addressing;

        public Operations Operation { get => _operation; }
        public Addressing Addressing { get => _addressing; }

        public Opcode(Operations operation, Addressing addressing)
        {
            _operation = operation;
            _addressing = addressing;
        }

        public Opcode(int opcode)
        {
            //Use bit masks to find relevant bits
            _operation = (Operations)((opcode & 0b11111110) >> 1);
            Console.WriteLine($"operation: {opcode & 0b11111110}");
            _addressing = (Addressing)(opcode & 0b1);
        }

        public int ToInt()
        {
            int opInt = (int)_operation;
            int adInt = (int)_addressing;

            return (opInt << 1) + adInt;
        }
    }
}
