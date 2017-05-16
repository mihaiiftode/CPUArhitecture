using System.Collections.Generic;

namespace ArhitecturaCPU.Assembler.Parser
{
    class Preprocessor
    {
        public void InstructionsToUpper(List<string> instructionList)
        {
            for (var i = 0; i < instructionList.Count; i++)
            {
                instructionList[i] = instructionList[i].ToUpper();
            }
        }
    }
}
