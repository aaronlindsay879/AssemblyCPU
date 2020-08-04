namespace AssemblyCPU.Backend
{
    public partial class Command
    {
        private void BGT(Instance instance)
        {
            Constrainer.EnsureOperandCount(_operands, 1);

            //Sets the program counter to the value specified in operand if the sign bit is set in SR
            //This is the same as checking if a > b, as sign bit = Sign(a - b)
            //SR = overflow/equal/carry/zero/sign
            if ((instance.SpecialReg["SR"].Data & 0b00001) != 0)
                instance.SpecialReg["PC"].Data = _operands[0].Value;
        }
    }
}
