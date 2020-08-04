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
