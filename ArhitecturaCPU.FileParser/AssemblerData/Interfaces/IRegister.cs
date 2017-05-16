using System.Collections.Generic;

namespace ArhitecturaCPU.Assembler.AssemblerData.Interfaces
{
    interface IRegister
    {
        Dictionary<string,string> RegisterDictionary { get; }

        string GetRegister(string str);
    }
}
