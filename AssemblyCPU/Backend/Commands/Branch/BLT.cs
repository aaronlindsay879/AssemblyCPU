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

            //Sets the program counter to the value specified in operand if the sign bit isn't set in SR
            //This is the same as checking if a < b, as sign bit = Sign(a - b)
            //SR = overflow/equal/carry/zero/sign
            if ((instance.SpecialReg["SR"].Data & 0b00001) == 0)
                instance.SpecialReg["PC"].Data = _operands[0].Value;
        }
    }
}
