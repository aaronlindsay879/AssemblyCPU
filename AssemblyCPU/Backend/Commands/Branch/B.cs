namespace AssemblyCPU.Backend
{
    public partial class Command
    {
        private void B(Instance instance)
        {
            Constrainer.EnsureOperandCount(_operands, 1);

            //Sets the program counter to the value specified in operand
            instance.SpecialReg["PC"].Data = _operands[0].Value;
        }
    }
}
