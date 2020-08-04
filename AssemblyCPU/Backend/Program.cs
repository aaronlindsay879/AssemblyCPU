using AssemblyCPU.Backend.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace AssemblyCPU.Backend
{
    public abstract class Program
    {
        private static OperandType FindType(string operand)
        {
            switch (operand)
            {
                case string register when register[0] == 'R':
                    return OperandType.Register;
                case string value when value[0] == '#':
                    return OperandType.Value;
                case string memory when char.IsNumber(memory[0]):
                    return OperandType.Memory;
            }

            return OperandType.Value;
        }

        public static Operand[] ParseOperands(string operands)
        {
            string[] parts = operands.Split(',');

            Operand[] output = new Operand[parts.Length];

            foreach(var (part, index) in parts.Select((x, i) => (x, i)))
            {
                int num;

                switch (FindType(part))
                {
                    case OperandType.Register:
                        int.TryParse(part.Remove(0, 1), out num);
                        output[index] = new Operand(num);
                        break;

                    case OperandType.Value:
                        if (part.Length < 3 || part[2] != 'b')
                            num = (int)new Int32Converter().ConvertFromString(part.Remove(0, 1));
                        else
                            num = Convert.ToInt32(part.Remove(0, 3), 2);

                        output[index] = new Operand(num);
                        break;

                    case OperandType.Memory:
                        int.TryParse(part, out num);
                        output[index] = new Operand(num);
                        break;
                }
            }

            return output;
        }

        public static void CompileProgram(Instance instance, string program)
        {
            string[] lines = program.Split('\n');
            string[] labels = new string[lines.Length];

            foreach (var (line, index) in lines.Select((x, i) => (x, i)))
            {
                string[] splitLine = line.Split(':');

                labels[index] = (splitLine.Length == 1) ? null : splitLine[0];
            }
                

            foreach (var (line, index) in lines.Select((x, i) => (x, i)))
            {
                if (line == string.Empty)
                    continue;

                string[] splitLine = line.Split(':');

                string[] parts;
                if (splitLine.Length == 1)
                    parts = line.Split(' ');
                else
                    parts = splitLine[1].Trim().Split(' ');

                if (!Enum.TryParse(typeof(Operation), parts[0], out var operation))
                    throw new Exception($"{index}, {parts[0]} wrong");

                Operand[] operands = new Operand[0];
                Addressing addressing = Addressing.Direct;
                
                switch (Operations.GetCategory((Operation)operation))
                {
                    case "Memory":
                        operands = ParseOperands(parts[1]);
                        addressing = Addressing.Immediate;

                        break;

                    case "Arithmetic":
                    case "Bitwise":
                        operands = ParseOperands(parts[1]);
                        string lastOperand = parts[1].Split(',').Last();
                        addressing = (FindType(lastOperand) == OperandType.Value) ? Addressing.Immediate : Addressing.Direct;

                        break;

                    case "Branch":
                        int labelLine = Array.IndexOf(labels, parts[1]);
                        operands = new Operand[] { new Operand(labelLine) };
                        addressing = Addressing.Immediate;

                        break;
                }

                Opcode opcode = new Opcode((Operation)operation, addressing);
                Command command = new Command(opcode, operands);

                instance.GeneralReg["RAM"].SetData(command.ToValue(), index);
            }
        }
    }
}
