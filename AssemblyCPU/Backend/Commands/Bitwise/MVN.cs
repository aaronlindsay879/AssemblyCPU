namespace AssemblyCPU.Backend
{
    public partial class Command
    {
        private void MVN(Instance instance)
        {
            Constrainer.EnsureOperandCount(_operands, 2);

            //Fetches value
            long value = FetchValue(_operands[1], instance);

            //Computes logical NOT of value
            value = (byte)~value;

            //Sets register to computed value
            instance.GeneralReg["Registers"].SetData(value, _operands[0].Value);
        }
    }
}
