using System.Collections.Generic;
using ArhitecturaCPU.Assembler.Model;

namespace ArhitecturaCPU.Assembler.AssemblerData.Interfaces
{
    public interface IInstructionSet
    {
        Dictionary<string, string> TwoOperandInstructionDictionary { get; }

        Dictionary<string, string> OneOperandInstructionDictionary { get; }

        Dictionary<string, string> JumpInstructionDictionary { get; }

        Dictionary<string, string> OtherInstructionDictionary { get;  }

        Instruction GetOpcode(string instr);

    }
}