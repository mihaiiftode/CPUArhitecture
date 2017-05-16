using System;
using System.Collections.Generic;
using ArhitecturaCPU.Assembler.Model;
using ArhitecturaCPU.FileOperation.Implementations;

namespace ArhitecturaCPU.Assembler.Parser
{
    public class FileParser
    {
        private string _asmFilePath;
        private List<string> _instructionList;
        public List<Instruction> Instructions { get; set; }

        public FileParser(string filePath)
        {
            _asmFilePath = filePath;
            Instructions = new List<Instruction>();
        }

        public void Parse()
        {
            var fileReader = new FileReader(_asmFilePath);
            _instructionList = fileReader.GetAllContentList();

            //Preprocess 
            var preproccessor = new Preprocessor();
            preproccessor.InstructionsToUpper(_instructionList);

            //Parse
            _instructionList.ForEach(instructionString =>
            {
                var instruction = new Instruction(instructionString);
                Instructions.Add(instruction.GetInstruction());
            });
            
            //Write
            var binaryWriter = new BinaryWriterFileWriter(_asmFilePath.Replace(".asm", ".bin"));
            Instructions.ForEach(item =>
            {
                Console.WriteLine(item.InstructionString);
                binaryWriter.Write(item.BinaryString);
                if(!string.IsNullOrEmpty(item.FirsOperandString))
                    binaryWriter.Write(item.FirsOperandString);
                if (!string.IsNullOrEmpty(item.SecondOperandString))
                    binaryWriter.Write(item.SecondOperandString);
            });

            binaryWriter.Close();
        }
    }
}
