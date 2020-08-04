namespace AssemblyCPU.Backend
{
    public partial class Command
    {
        private void STR(Instance instance)
        {
            Constrainer.EnsureOperandCount(_operands, 2);

            //Fetch value
            long value = instance.GeneralReg["Registers"].GetData(_operands[0].Value);

            //Set location in RAM to value from register
            instance.GeneralReg["RAM"].SetData(value, _operands[1].Value);
        }
    }
}
