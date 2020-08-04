using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssemblyCPU.Backend
{
    public partial class Command
    {
        private void BLT(Instance instance)
        {
            Constrainer.EnsureOperandCount(_operands, 1);

            //SR = overflow/equal/carry/zero/sign
            if ((instance.SpecialReg["SR"].Data & 0b00001) == 0)
                instance.SpecialReg["PC"].Data = _operands[0].Value;
        }
    }
}
