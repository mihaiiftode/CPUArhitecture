using System;
using System.Collections.Generic;
using System.Linq;
using ArhitecturaCPU.Assembler.AssemblerData;
using ArhitecturaCPU.Assembler.AssemblerData.Implementations;
using ArhitecturaCPU.Assembler.Parser;

namespace ArhitecturaCPU.Assembler.Model
{
    public class Instruction
    {
        public string InstructionString { get; set; }

        public List<string> SplitedInstruction { get; set; } 

        public string BinaryString { get; set; }

        public string FirsOperandString { get; set; }

        public string SecondOperandString { get; set; }

        public InstructionType InstructionType { get; set; }

        public Instruction() { }

        public Instruction(string instructionString)
        {
            InstructionString = instructionString;
        }

        public Instruction GetInstruction()
        {
            var instructionData = new ParserInstructionData();
            var returnInstruction = instructionData.GetOpcode(InstructionString);
            returnInstruction.InstructionString = InstructionString;

            SplitInstruction(returnInstruction);

            var analyzer = new OperandAnalyzer(returnInstruction);
            returnInstruction = analyzer.GetOperands();

            return returnInstruction;
        }
        
        private void SplitInstruction(Instruction instruction)
        {
            switch (instruction.InstructionType)
            {
                case InstructionType.TwoOperandInstruction:
                    GetSplitString(instruction, new[] { ' ', ',' });
                    break;
                case InstructionType.OneOperandInstruction:
                    GetSplitString(instruction, new[] { ' ' });
                    break;
                case InstructionType.JumpInstruction:
                    GetSplitString(instruction, new[] { ' ' });
                    break;
            }
        }

        private static void GetSplitString(Instruction instruction, char[] separator)
        {
            instruction.SplitedInstruction = instruction.InstructionString.Split(separator,StringSplitOptions.RemoveEmptyEntries).ToList();
        }
    }
}