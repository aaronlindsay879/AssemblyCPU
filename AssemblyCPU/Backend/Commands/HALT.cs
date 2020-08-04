using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssemblyCPU.Backend
{
    public partial class Command
    {
        private void HALT(Instance instance)
        {
            instance.Halted = true;
        }
    }
}
