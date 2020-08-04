namespace AssemblyCPU.Backend
{
    public partial class Command
    {
        private void EOR(Instance instance)
        {
            Constrainer.EnsureOperandCount(_operands, 3);

            //Fetches values
            long valueOne = instance.GeneralReg["Registers"].GetData(_operands[1].Value);
            long valueTwo = FetchValue(_operands[2], instance);

            //Computes bitwise XOR of values
            long value = valueOne ^ valueTwo;

            //Sets register to computed value
            instance.GeneralReg["Registers"].SetData(value, _operands[0].Value);
        }
    }
}
