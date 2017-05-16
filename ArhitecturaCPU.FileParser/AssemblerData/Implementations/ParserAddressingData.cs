using System.Collections.Generic;
using ArhitecturaCPU.Assembler.AssemblerData.Interfaces;
using ArhitecturaCPU.Assembler.Model;

namespace ArhitecturaCPU.Assembler.AssemblerData.Implementations
{
    public class ParserAddressingData : IAddressingMode
    {
        #region Addressing Mode Dictionary
        public Dictionary<string, string> AdressingModeDictionary { get; } = new Dictionary<string, string>
        {
            {"Imediat","00" },
            {"Direct","01" },
            {"Indirect","10" },
            {"Indexat","11" }
        };

        #endregion
        public void GetAddresingMode(Instruction str)
        {
            throw new System.NotImplementedException();
        }
    }
}
