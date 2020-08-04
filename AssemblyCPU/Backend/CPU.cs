using AssemblyCPU.Backend.Data;
using System;
using System.Collections.Generic;

namespace AssemblyCPU.Backend
{

    public class CPU
    {
        private Instance _instance;
        private int _stage;
        private Command _command;

        public Instance Instance { get => _instance; }
        public State State
        {
            get
            {
                //Returns a state depending on the current stage of the CPU
                if (_stage >= 1 && _stage <= 3)
                    return State.Fetch;

                if (_stage == 4)
                    return State.Decode;

                return State.Execute;
            }
        }

        public CPU(int numRegisters, int numRam)
        {
            _stage = 1;
            _instance = new Instance(numRegisters, numRam);
            _command = new Command();
        }

        public void RegenerateInstance()
        {
            //Find out number of registers and ram slots
            int numRegisters = _instance.GeneralReg["Registers"].GetLength();
            int numRam = _instance.GeneralReg["RAM"].GetLength();

            //Resets stage and instance
            _stage = 1;
            _instance = new Instance(numRegisters, numRam);
        }

        private void IncrementStage()
        {
            //Increments stage, resets to 1 if needed
            _stage++;

            if (_stage > 5) _stage = 1;
        }

        public void Pulse()
        {
            //Do nothing if CPU halted
            if (_instance.Halted)
                return;

            switch (_stage)
            {
                //Copy PC value to MAR
                case 1:
                    _instance.SpecialReg["MAR"].Data = _instance.SpecialReg["PC"].Data;
                    break;

                //Fetch instruction from MAR address and increment PC
                case 2:
                    long instructionAddress = _instance.SpecialReg["MAR"].Data;

                    _instance.SpecialReg["MBR"].Data = _instance.GeneralReg["RAM"].GetData(instructionAddress);
                    _instance.SpecialReg["PC"].Data++;
                    break;

                //Copy CIR to MBR
                case 3:
                    _instance.SpecialReg["CIR"].Data = _instance.SpecialReg["MBR"].Data;
                    break;

                //Decode instruction
                case 4:
                    Opcode opcode;
                    List<Operand> operands = new List<Operand>();

                    //Fetch instruction value from memory
                    long instruction = _instance.SpecialReg["CIR"].Data;

                    //Convert instruction to hex string
                    string str = Convert.ToString(instruction, 16);

                    //If command is blank, indicate that and stop
                    if (str == "0")
                    {
                        _command.IsEmpty = true;
                        break;
                    }

                    //Pad hex string to appropriate  length
                    int length = (int)(2 * Math.Ceiling(str.Length / (float)2));
                    str = str.PadLeft(length, '0');

                    //Parse opcode
                    opcode = new Opcode(Convert.ToInt32(str.Substring(0, 2), 16));

                    //For every 2 hex chars excluding opcode
                    for (int i = 2; i < str.Length; i += 2)
                        operands.Add(new Operand(Convert.ToInt32(str.Substring(i, 2), 16)));

                    _command = new Command(opcode, operands.ToArray());

                    break;

                //Execute instruction
                case 5:
                    if (!_command.IsEmpty)
                        _command.Execute(_instance);

                    break;
            }

            IncrementStage();
        }

        public void Cycle()
        {
            //Run until stage resets to start
            do
            {
                Pulse();
            } while (_stage != 1);
        }
    }
}
