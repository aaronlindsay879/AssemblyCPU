namespace AssemblyCPU.Backend
{
    public partial class Command
    {
        private void BEQ(Instance instance)
        {
            Constrainer.EnsureOperandCount(_operands, 1);

            //Sets the program counter to the value specified in operand if the equal bit is set in SR
            //SR = overflow/equal/carry/zero/sign
            if ((instance.SpecialReg["SR"].Data & 0b01000) != 0)
                instance.SpecialReg["PC"].Data = _operands[0].Value;
        }
    }
}
