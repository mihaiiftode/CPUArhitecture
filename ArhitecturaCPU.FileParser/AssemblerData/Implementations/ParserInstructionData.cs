using System.Collections.Generic;
using System.Linq;
using ArhitecturaCPU.Assembler.AssemblerData.Interfaces;
using ArhitecturaCPU.Assembler.Model;

namespace ArhitecturaCPU.Assembler.AssemblerData.Implementations
{
    public class ParserInstructionData : IInstructionSet
    {
        #region Opcode related dictionaries
        public Dictionary<string, string> TwoOperandInstructionDictionary { get; } = new Dictionary<string, string>
        {
            {"MOV", "0000"},
            {"ADD", "0001"},
            {"SUB", "0010"},
            {"CMP", "0011"},
            {"AND", "0100"},
            {"OR", "0101"},
            {"XOR", "0110"}
        };

        public Dictionary<string, string> OneOperandInstructionDictionary { get; } = new Dictionary<string, string>
        {
            {"CLR", "1000000000"},
            {"NEG", "1000000001"},
            {"INC", "1000000010"},
            {"DEC", "1000000011"},
            {"ASL", "1000000100"},
            {"ASR", "1000000101"},
            {"LSR", "1000000110"},
            {"ROL", "1000000111"},
            {"ROR", "1000001000"},
            {"RLC", "1000001001"},
            {"RRC", "1000001010"},
            {"JMP", "1000001011"},
            {"CALL", "1000001100"},
            {"PUSH", "1000001101"},
            {"POP", "1000001110"}
        };

        public Dictionary<string, string> JumpInstructionDictionary { get; } = new Dictionary<string, string>
        {
            {"BR", "10100000"},
            {"BNE", "10100001"},
            {"BEQ", "10100010"},
            {"BPL", "10100011"},
            {"BMI", "10100100"},
            {"BCS", "10100101"},
            {"BCC", "10100110"},
            {"BVS", "10100111"},
            {"BVC", "10101000"}
        };

        public Dictionary<string, string> OtherInstructionDictionary { get; } = new Dictionary<string, string>
        {
            {"CLC", "1100000000000000"},
            {"CLV", "1100000000000001"},
            {"CLZ", "1100000000000010"},
            {"CLS", "1100000000000011"},
            {"CCC", "1100000000000100"},
            {"SEC", "1100000000000101"},
            {"SEV", "1100000000000110"},
            {"SEZ", "1100000000000111"},
            {"SES", "1100000000001000"},
            {"SCC", "1100000000001001"},
            {"NOP", "1100000000001010"},
            {"RET", "1100000000001011"},
            {"RETI", "1100000000001100"},
            {"HALT", "1100000000001101"},
            {"WAIT", "1100000000001110"},
            {"PUSH PC", "1100000000010000"},
            {"POP PC", "1100000000010001"},
            {"PUSH FLAG", "1100000000010010"},
            {"POP FLAG", "1100000000010011"}
        };

        #endregion

        public Instruction GetOpcode(string instr)
        {
            var instruction = new Instruction();

            foreach (var key in OtherInstructionDictionary.Keys.Where(instr.Contains))
            {
                instruction.InstructionType = InstructionType.OtherInstruction;
                instruction.BinaryString = OtherInstructionDictionary[key];
                return instruction;
            }

            foreach (var key in TwoOperandInstructionDictionary.Keys.Where(instr.Contains))
            {
                instruction.InstructionType = InstructionType.TwoOperandInstruction;
                instruction.BinaryString = TwoOperandInstructionDictionary[key];
                return instruction;
            }

            foreach (var key in OneOperandInstructionDictionary.Keys.Where(instr.Contains))
            {
                instruction.InstructionType = InstructionType.OneOperandInstruction;
                instruction.BinaryString = OneOperandInstructionDictionary[key];
                return instruction;
            }

            foreach (var key in JumpInstructionDictionary.Keys.Where(instr.Contains))
            {
                instruction.InstructionType = InstructionType.JumpInstruction;
                instruction.BinaryString = JumpInstructionDictionary[key];
                return instruction;
            }
            
            instruction.InstructionType = InstructionType.InstructionNotFound;
            return instruction;
        }
    }
}
