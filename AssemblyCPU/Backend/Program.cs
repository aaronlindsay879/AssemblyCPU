﻿using AssemblyCPU.Backend.Data;
using System;
using System.ComponentModel;
using System.Linq;

namespace AssemblyCPU.Backend
{
    public abstract class Program
    {
        private static OperandType FindType(string operand)
        {
            switch (operand[0])
            {
                case 'R':
                    //If first char is R, register
                    return OperandType.Register;
                case '#':
                    //If first char is #, memory
                    return OperandType.Value;
                case char memory when char.IsNumber(memory):
                    //If first char is number, memory
                    return OperandType.Memory;
            }

            //Return default value
            return OperandType.Value;
        }

        private static int ParseValue(string value)
        {
            int num;

            //If value starts with a non numeric char, remove it
            if (FindType(value) != OperandType.Memory)
                value = value.Remove(0, 1);

            //If value is either less than 2 chars long or isn't a binary value (such as 0b001)
            if (value.Length < 2 || value[1] != 'b')
                num = (int)new Int32Converter().ConvertFromString(value);
            else
                //Otherwise remove first 2 chars (0b) and parse to int
                num = Convert.ToInt32(value.Remove(0, 2), 2);

            return num;
        }

        public static Operand[] ParseOperands(string operands)
        {
            //Split operands into array
            string[] parts = operands.Split(',');

            //Creates blank operand array with correct length
            Operand[] output = new Operand[parts.Length];

            //For each operand part (with index)
            foreach (var (part, index) in parts.Select((x, i) => (x, i)))
                output[index] = new Operand(ParseValue(part));

            return output;
        }

        public static void CompileProgram(Instance instance, string program)
        {
            //Split program into lines
            string[] lines = program.Split('\n');

            //Strip all newlines after last command
            lines = lines.Reverse().SkipWhile(x => x == string.Empty).Reverse().ToArray();

            //Generate labels array with same length as program lines
            string[] labels = new string[lines.Length];
            //For each line of code (with index)
            foreach (var (line, index) in lines.Select((x, i) => (x, i)))
            {
                //If there is a label (in the format "label: code"), add it to labels index - otherwise set it to null
                string[] splitLine = line.Split(':');
                labels[index] = (splitLine.Length == 1) ? null : splitLine[0];
            }

            //For each line of code (with index)
            foreach (var (line, index) in lines.Select((x, i) => (x, i)))
            {
                //Skip empty lines
                if (line == string.Empty)
                    continue;

                //Split line on colon to find labels
                string[] splitLine = line.Split(':');

                //If there is no label
                string[] parts;
                if (splitLine.Length == 1)
                    //Split the code on spaces
                    parts = line.Split(' ');
                else
                    //If there is a label, select the code and split on spaces
                    parts = splitLine[1].Trim().Split(' ');

                //Try and parse operation string as enum, throw error if there is no enum for that string
                if (!Enum.TryParse(typeof(Operation), parts[0], out var operation))
                    throw new Exception($"{index}, {parts[0]} wrong");

                //Create default value for operands and addressing mode
                Operand[] operands = new Operand[0];
                Addressing addressing = Addressing.Direct;

                switch (((Operation)operation).GetCategory())
                {
                    case "Memory":
                        //If the operation is memory, parse the operands and set addressing mode to immediate
                        operands = ParseOperands(parts[1]);
                        addressing = Addressing.Immediate;

                        break;

                    case "Arithmetic":
                    case "Bitwise":
                        //If the operation is Arithmetic or Bitwise, parse the operands
                        operands = ParseOperands(parts[1]);

                        //Set the addressing mode depending on whether the last operand is a value or register 
                        string lastOperand = parts[1].Split(',').Last();
                        addressing = (FindType(lastOperand) == OperandType.Value) ? Addressing.Immediate : Addressing.Direct;

                        break;

                    case "Branch":
                        //If the operation is branch, find the index of the label (which is the line it's on) and generate an operand with that line
                        int labelLine = Array.IndexOf(labels, parts[1]);
                        operands = new Operand[] { new Operand(labelLine) };

                        //Set the addressing mode to immediate
                        addressing = Addressing.Immediate;

                        break;
                }

                //Generate an opcode and command
                Opcode opcode = new Opcode((Operation)operation, addressing);
                Command command = new Command(opcode, operands);

                //Set the memory to the command's value
                instance.GeneralReg["RAM"].SetData(command.ToValue(), index);

                //If this is the last command and is not halt, insert a halt instruction in next memory address
                Console.WriteLine($"{index}, {lines.Length}");
                if (index == (lines.Length - 1) && opcode.Operation != Operation.HALT)
                {
                    Opcode opcodeHalt = new Opcode(Operation.HALT, Addressing.Direct);
                    Command commandHalt = new Command(opcodeHalt, new Operand[0]);

                    instance.GeneralReg["RAM"].SetData(commandHalt.ToValue(), index + 1);
                }
            }
        }
    }
}
