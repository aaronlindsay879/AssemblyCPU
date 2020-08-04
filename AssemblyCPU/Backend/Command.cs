using AssemblyCPU.Backend.Data;
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
        private Dictionary<Operation, OperationAction<Instance>> _map;

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

        private long FetchValue(Operand operand, Instance instance)
        {
            return _opcode.Addressing switch
            {
                Addressing.Immediate => operand.Value,
                Addressing.Direct => instance.GeneralReg["Registers"].GetData(operand.Value),
                _ => throw new NotImplementedException()
            };
        }

        private Dictionary<Operation, OperationAction<Instance>> GenerateMap()
        {
            var map = new Dictionary<Operation, OperationAction<Instance>>();

            map[Operation.LDR] = LDR;
            map[Operation.STR] = STR;
            map[Operation.ADD] = ADD;
            map[Operation.SUB] = SUB;
            map[Operation.MOV] = MOV;
            map[Operation.CMP] = CMP;
            map[Operation.B] = B;
            map[Operation.BEQ] = BEQ;
            map[Operation.BNE] = BNE;
            map[Operation.BGT] = BGT;
            map[Operation.BLT] = BLT;
            map[Operation.AND] = AND;
            map[Operation.ORR] = ORR;
            map[Operation.EOR] = EOR;
            map[Operation.MVN] = MVN;
            map[Operation.LSL] = LSL;
            map[Operation.LSR] = LSR;
            map[Operation.HALT] = HALT;

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
