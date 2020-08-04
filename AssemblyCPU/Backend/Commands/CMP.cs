using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssemblyCPU.Backend 
{ 
    public partial class Command
    {
        private void CMP(Instance instance)
        {
            Constrainer.EnsureOperandCount(_operands, 2);

            long valueOne = instance.GeneralReg["Registers"].GetData(_operands[0].Value);
            long valueTwo = _opcode.Addressing switch
            {
                Addressing.Immediate => _operands[1].Value,
                Addressing.Direct => instance.GeneralReg["Registers"].GetData(_operands[1].Value),
                _ => throw new NotImplementedException()
            };

            long result = valueOne - valueTwo;
            long SR = (result >= 255) ? 1 : 0; //check for overflow
            SR = (SR << 1) + ((result == 0) ? 1 : 0); //check if equal
            SR = (SR << 1) + ((result & 0b100000000) >> 8); //check for carry
            SR = (SR << 1) + ((result == 0) ? 1 : 0); //check if zero
            SR = (SR << 1) + ((result > 0) ? 1 : 0); //check for sign

            //SR = overflow/equal/carry/zero/sign
            instance.SpecialReg["SR"].Data = SR;
        }
    }
}
