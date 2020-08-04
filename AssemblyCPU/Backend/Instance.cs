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

            //Populate the registers with register name and tooltip
            var specialRegisters = new KeyValuePair<string, string>[] {
                new KeyValuePair<string, string>("MBR", "The Memory Buffer Register holds data moving between the CPU and RAM" ),
                new KeyValuePair<string, string>("MAR", "The Memory Address Register holds the address of data to be accessed" ),
                new KeyValuePair<string, string>("CIR", "The Current Instruction Register holds the current instruction" ),
                new KeyValuePair<string, string>("SR", "The Status Register holds information about the current state of operations" ),
                new KeyValuePair<string, string>("PC", "The Program Counter holds the memory address of the next instruction to be executed" )
            };
            foreach (var register in specialRegisters)
                SpecialReg.Add(register.Key, new SpecialStorage(register.Value));

            GeneralReg.Add("Registers", new GeneralStorage(numRegisters));
            GeneralReg.Add("RAM", new GeneralStorage(numRam));
        }
    }
}
