using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssemblyCPU.Backend 
{ 
    public partial class Command
    {
        private void STR(Instance instance)
        {
            Constrainer.EnsureOperandCount(_operands, 2);

            long value = instance.GeneralReg["Registers"].GetData(_operands[0].Value);

            instance.GeneralReg["RAM"].SetData(value, _operands[1].Value);
        }
    }
}
