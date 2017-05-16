using System.Collections.Generic;
using System.Linq;
using ArhitecturaCPU.FileOperation.Implementations;

namespace ArhitecturaCPU.Simulation.Model
{
    public class MicroInstructioBinaryDictionary
    {
        public Dictionary<string, string> SBUSDictionary { get; set; } = new Dictionary<string, string>();

        public Dictionary<string, string> DBUSDictionary { get; set; } = new Dictionary<string, string>();

        public Dictionary<string, string> ALUOperationDictionary { get; set; } = new Dictionary<string, string>();

        public Dictionary<string, string> RBUSDictionary { get; set; } = new Dictionary<string, string>();

        public Dictionary<string, string> OtherOperationDictionary { get; set; } = new Dictionary<string, string>();

        public Dictionary<string, string> MemoryOperationDictionary { get; set; } = new Dictionary<string, string>();

        public Dictionary<string, string> RamificationConditionDictionary { get; set; } = new Dictionary<string, string>();

        //public Dictionary<string, string> IndexDictionary { get; set; } = new Dictionary<string, string>();

        public string FilePath { get; private set; }

        public MicroInstructioBinaryDictionary(string filePath = "")
        {
            FilePath = !string.IsNullOrEmpty(filePath)
                ? filePath
                : @"D:\Dropbox\Projects\ArhitecturaCPU\MicroInstructionBinary.txt";
            LoadDictionaries();
        }

        private void LoadDictionaries()
        {
            var fileReader = new FileReader(FilePath);

            List<string> fileLineList = fileReader.GetAllContentList();
            string labelInstruction = "";

            fileLineList.Where(line => !string.IsNullOrEmpty(line)).ToList().ForEach(line =>
            {
                if (line.Contains("#"))
                {
                    labelInstruction = line.Replace("#", "");
                }
                else
                {
                    AddToDictionary(labelInstruction, line);
                }
            });
        }

        private void AddToDictionary(string labelInstruction, string line)
        {
            var itemsToAdd = line.Split(' ');
            switch (labelInstruction)
            {
                case "SBUS":
                    SBUSDictionary.Add(itemsToAdd[0], itemsToAdd[1]);
                    break;
                case "DBUS":
                    DBUSDictionary.Add(itemsToAdd[0], itemsToAdd[1]);
                    break;
                case "ALU":
                    ALUOperationDictionary.Add(itemsToAdd[0], itemsToAdd[1]);
                    break;
                case "RBUS":
                    RBUSDictionary.Add(itemsToAdd[0], itemsToAdd[1]);
                    break;
                case "OTHER":
                    OtherOperationDictionary.Add(itemsToAdd[0], itemsToAdd[1]);
                    break;
                case "MEMORY":
                    MemoryOperationDictionary.Add(itemsToAdd[0], itemsToAdd[1]);
                    break;
                case "CONDITION":
                    if (itemsToAdd.Length == 2)
                        RamificationConditionDictionary.Add(itemsToAdd[0], itemsToAdd[1]);
                    else
                        RamificationConditionDictionary.Add(itemsToAdd[0] + " " + itemsToAdd[1], itemsToAdd[2]);
                    break;
            }
        }
    }
}