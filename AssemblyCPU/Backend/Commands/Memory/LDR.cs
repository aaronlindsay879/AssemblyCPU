using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssemblyCPU.Backend 
{ 
    public partial class Command
    {
        private void LDR(Instance instance)
        {
            Constrainer.EnsureOperandCount(_operands, 2);

            //Fetch value
            long value = instance.GeneralReg["RAM"].GetData(_operands[1].Value);

            //Set register to value from memory
            instance.GeneralReg["Registers"].SetData(value, _operands[0].Value);
        }
    }
}
