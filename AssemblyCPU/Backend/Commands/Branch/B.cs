using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssemblyCPU.Backend
{
    public partial class Command
    {
        private void B(Instance instance)
        {
            Constrainer.EnsureOperandCount(_operands, 1);

            instance.SpecialReg["PC"].Data = _operands[0].Value;
        }
    }
}
