using AssemblyCPU.Backend.Data;
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

            //Fetch values
            long valueOne = instance.GeneralReg["Registers"].GetData(_operands[1].Value);
            long valueTwo = FetchValue(_operands[2], instance);

            //Computes logical OR of values
            long value = valueOne | valueTwo;

            //Sets register to computed value
            instance.GeneralReg["Registers"].SetData(value, _operands[0].Value);
        }
    }
}
