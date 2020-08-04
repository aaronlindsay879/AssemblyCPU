using AssemblyCPU.Backend.Data;
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

            //Fetches values
            long valueOne = instance.GeneralReg["Registers"].GetData(_operands[0].Value);
            long valueTwo = FetchValue(_operands[1], instance);

            //Subtracts values to calculate result
            long result = valueOne - valueTwo;

            //Sets bits one at a time depending upon result
            long SR = (result >= 255) ? 1 : 0; //check for overflow
            SR = (SR << 1) + ((result == 0) ? 1 : 0); //check if equal
            SR = (SR << 1) + ((result & 0b100000000) >> 8); //check for carry
            SR = (SR << 1) + ((result == 0) ? 1 : 0); //check if zero
            SR = (SR << 1) + ((result > 0) ? 1 : 0); //check for sign

            //Sets SR register to computed bits
            //SR = overflow/equal/carry/zero/sign
            instance.SpecialReg["SR"].Data = SR;
        }
    }
}
