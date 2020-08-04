namespace AssemblyCPU.Backend
{
    public class Operand
    {
        private int _value;
        public int Value { get => _value; }

        public Operand(int operand)
        {
            _value = operand;
        }

        public int ToInt()
        {
            return _value;
        }
    }
}
