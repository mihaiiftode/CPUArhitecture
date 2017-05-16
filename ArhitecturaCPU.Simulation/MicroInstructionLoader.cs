using System;
using System.Collections.Generic;
using ArhitecturaCPU.FileOperation.Implementations;

namespace ArhitecturaCPU.Simulation
{
    public class MicroInstructionLoader
    {
        public string FilePath { get; private set; }

        public List<Tuple<string, int, string>> MicroInstructionTuples { get; set; }

        public MicroInstructionLoader(string filePath)
        {
            FilePath = filePath;

            MicroInstructionTuples = new List<Tuple<string, int, string>>();
        }
        
        public void Load()
        {
            FileReader fileReader = new FileReader(FilePath);

            List<string> microInstructionList = fileReader.GetAllContentList();

            var instructionLabel = "";
            var counter = 0;

            foreach (var line in microInstructionList)
            {
                string lineToProcess = line.Trim();
                if (!string.IsNullOrEmpty(lineToProcess) && !lineToProcess.Contains("###"))
                {
                    if (lineToProcess.Contains(":"))
                    {
                        instructionLabel = lineToProcess.Substring(0, lineToProcess.IndexOf(":"));
                    }
                    else
                    {
                        MicroInstructionTuples.Add(new Tuple<string, int, string>(instructionLabel, counter++, lineToProcess));
                        instructionLabel = "";
                    }
                }
            }
        }
    }
}
