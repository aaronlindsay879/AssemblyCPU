namespace AssemblyCPU.Backend
{
    public partial class Command
    {
        private void MOV(Instance instance)
        {
            Constrainer.EnsureOperandCount(_operands, 2);

            //Fetch value
            long value = FetchValue(_operands[1], instance);

            //Set register to value
            instance.GeneralReg["Registers"].SetData(value, _operands[0].Value);
        }
    }
}
