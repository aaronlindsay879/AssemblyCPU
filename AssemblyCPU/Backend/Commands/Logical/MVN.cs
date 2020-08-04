using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssemblyCPU.Backend
{
    public partial class Command
    {
        private void MVN(Instance instance)
        {
            Constrainer.EnsureOperandCount(_operands, 2);

            long value = _opcode.Addressing switch
            {
                Addressing.Immediate => _operands[1].Value,
                Addressing.Direct => instance.GeneralReg["Registers"].GetData(_operands[1].Value),
                _ => throw new NotImplementedException()
            };

            value = (byte)~value;

            instance.GeneralReg["Registers"].SetData(value, _operands[0].Value);
        }
    }
}
