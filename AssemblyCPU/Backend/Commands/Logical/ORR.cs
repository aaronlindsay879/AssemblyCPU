using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssemblyCPU.Backend
{
    public partial class Command
    {
        private void ORR(Instance instance)
        {
            Constrainer.EnsureOperandCount(_operands, 3);

            long valueOne = instance.GeneralReg["Registers"].GetData(_operands[1].Value);
            long valueTwo = _opcode.Addressing switch
            {
                Addressing.Immediate => _operands[2].Value,
                Addressing.Direct => instance.GeneralReg["Registers"].GetData(_operands[2].Value),
                _ => throw new NotImplementedException()
            };

            long value = valueOne | valueTwo;

            instance.GeneralReg["Registers"].SetData(value, _operands[0].Value);
        }
    }
}
