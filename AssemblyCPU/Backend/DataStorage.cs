﻿namespace AssemblyCPU.Backend
{
    public class GeneralStorage
    {
        private long[] data;

        public GeneralStorage(int length)
        {
            data = new long[length];
        }

        public long GetData(long pos)
        {
            return data[pos];
        }

        public bool SetData(long value, int pos)
        {
            //Only allow data to be set if position is in bounds
            if (pos < 0 || pos > data.Length)
                return false;

            data[pos] = value;
            return true;
        }

        public int GetLength()
        {
            return data.Length;
        }
    }

    public class SpecialStorage
    {
        private long _data;
        private string _tooltip;

        public long Data { get => _data; set => _data = value; }
        public string Tooltip { get => _tooltip; }

        public SpecialStorage(string tooltip)
        {
            _tooltip = tooltip;
        }
    }
}
