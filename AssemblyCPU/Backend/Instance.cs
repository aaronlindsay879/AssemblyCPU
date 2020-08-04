using System;
using System.Collections.Generic;

namespace AssemblyCPU.Backend
{
    public class Instance
    {
        public Dictionary<string, SpecialStorage> SpecialReg { get; set; }
        public Dictionary<string, GeneralStorage> GeneralReg { get; set; }

        public bool Halted;

        public Instance(int numRegisters, int numRam)
        {
            SpecialReg = new Dictionary<string, SpecialStorage>();
            GeneralReg = new Dictionary<string, GeneralStorage>();

            //Populate the registers
            string[] specialRegisterNames = new string[] { "MBR", "MAR", "CIR", "SR", "PC" };
            foreach (string regName in specialRegisterNames)
                SpecialReg.Add(regName, new SpecialStorage());

            GeneralReg.Add("Registers", new GeneralStorage(numRegisters));
            GeneralReg.Add("RAM", new GeneralStorage(numRam));
        }
    }
}
