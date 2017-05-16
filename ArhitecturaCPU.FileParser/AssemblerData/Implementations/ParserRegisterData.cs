﻿using System.Collections.Generic;
using System.Linq;
using ArhitecturaCPU.Assembler.AssemblerData.Interfaces;

namespace ArhitecturaCPU.Assembler.AssemblerData.Implementations
{
    public class ParserRegisterData : IRegister
    {
        #region Register dictionary
        public Dictionary<string, string> RegisterDictionary { get; } =  new Dictionary<string, string>
        {
            {"R0", "0000" },
            {"R1", "0001" },
            {"R2", "0010" },
            {"R3", "0011" },
            {"R4", "0100" },
            {"R5", "0101" },
            {"R6", "0110" },
            {"R7", "0111" },
            {"R8", "1000" },
            {"R9", "1001" },
            {"R10", "1010" },
            {"R11", "1011" },
            {"R12", "1100" },
            {"R13", "1101" },
            {"R14", "1110" },
            {"R15", "1111" }
        };

        #endregion

        public string GetRegister(string instr)
        {
            foreach (var key in RegisterDictionary.Keys.Where(instr.Contains))
            {
                return RegisterDictionary[key];
            }

            return "";
        }
    }
}
