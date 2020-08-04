using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssemblyCPU.Backend
{
    public partial class Command
    {
        private void BNE(Instance instance)
        {
            Constrainer.EnsureOperandCount(_operands, 1);

            //Sets the program counter to the value specified in operand if the equal bit isn't set in SR
            //SR = overflow/equal/carry/zero/sign
            if ((instance.SpecialReg["SR"].Data & 0b01000) == 0)
                instance.SpecialReg["PC"].Data = _operands[0].Value;
        }
    }
}
