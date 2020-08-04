using AssemblyCPU.Backend.Data;

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
            //Use bit masks to find relevant bits - addressing is first bit, operation is last 7
            _addressing = (Addressing)(opcode & 0b1);
            _operation = (Operation)((opcode & 0b11111110) >> 1);
        }

        public int ToInt()
        {
            //Convert enums to ints and add them, ensuring addressing is first bit and operation is last 7
            int opInt = (int)_operation;
            int adInt = (int)_addressing;

            return (opInt << 1) + adInt;
        }
    }
}
