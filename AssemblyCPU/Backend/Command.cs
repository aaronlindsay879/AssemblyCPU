using System;
using System.Collections.Generic;
using System.Linq;

namespace AssemblyCPU.Backend
{
    delegate void OperationAction<T1>(T1 instance);

    public partial class Command
    {
        private Opcode _opcode;
        private Operand[] _operands;
        private Dictionary<Operations, OperationAction<Instance>> _map;

        public bool IsEmpty;

        public Command()
        {
            _map = GenerateMap();
        }

        public Command(Opcode opcode, params Operand[] operands)
        {
            _opcode = opcode;
            _operands = operands;

            _map = GenerateMap();
        }

        private Dictionary<Operations, OperationAction<Instance>> GenerateMap()
        {
            var map = new Dictionary<Operations, OperationAction<Instance>>();

            map[Operations.LDR] = LDR;
            map[Operations.STR] = STR;
            map[Operations.ADD] = ADD;
            map[Operations.SUB] = SUB;
            map[Operations.MOV] = MOV;
            map[Operations.CMP] = CMP;
            map[Operations.B] = B;
            map[Operations.BEQ] = BEQ;
            map[Operations.BNE] = BNE;
            map[Operations.BGT] = BGT;
            map[Operations.BLT] = BLT;
            map[Operations.AND] = AND;
            map[Operations.ORR] = ORR;
            map[Operations.EOR] = EOR;
            map[Operations.MVN] = MVN;
            map[Operations.LSL] = LSL;
            map[Operations.LSR] = LSR;
            map[Operations.HALT] = HALT;

            return map;
        }

        private int IntLengthInBinary(int value)
        {
            return Convert.ToString(value, 2).Length;
        }

        public long ToValue()
        {
            string output = "";
            output += Convert.ToString(_opcode.ToInt(), 16).PadLeft(2, '0');

            foreach (Operand operand in _operands)
            {
                string partial = Convert.ToString(operand.ToInt(), 16);
                if (partial.Length > 2)
                    partial = partial.Substring(0, 2);

                output += partial.PadLeft(2, '0');
            }

            return Convert.ToInt64(output, 16);
        }

        public void Execute(Instance instance)
        {

            Console.WriteLine(_opcode.Operation);
            _map[_opcode.Operation].Invoke(instance);
            return;
        }
    }
}
