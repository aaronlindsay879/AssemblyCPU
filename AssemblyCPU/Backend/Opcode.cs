using AssemblyCPU.Backend.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AssemblyCPU.Backend
{
    public class Opcode
    {
        private Operation _operation;
        private Addressing _addressing;

        public Operation Operation { get => _operation; }
        public Addressing Addressing { get => _addressing; }

        public Opcode(Operation operation, Addressing addressing)
        {
            _operation = operation;
            _addressing = addressing;
        }

        public Opcode(int opcode)
        {
            //Use bit masks to find relevant bits
            _operation = (Operation)((opcode & 0b11111110) >> 1);
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
