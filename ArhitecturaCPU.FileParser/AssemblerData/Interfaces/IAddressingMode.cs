using System.Collections.Generic;
using ArhitecturaCPU.Assembler.Model;

namespace ArhitecturaCPU.Assembler.AssemblerData.Interfaces
{
    interface IAddressingMode
    {
        Dictionary<string, string> AdressingModeDictionary { get; }

        void GetAddresingMode(Instruction str);
    }
}
