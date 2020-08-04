using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssemblyCPU.Backend
{
    public abstract class Constrainer
    {
        public static void EnsureOperandCount(Operand[] operands, int count)
        {
            if (operands.Length != count)
                throw new Exception($"Only {operands.Length} operands, should have {count}");
        }
    }
}
