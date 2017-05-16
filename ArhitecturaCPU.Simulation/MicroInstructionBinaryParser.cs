using System;
using System.Collections.Generic;
using System.Reflection;
using ArhitecturaCPU.Simulation.Model;

namespace ArhitecturaCPU.Simulation
{
    public class MicroInstructionBinaryParser
    {

        private readonly List<Tuple<string, int, string>> _microInstructionTuples;

        private readonly MicroInstructioBinaryDictionary _microInstructionDictionaries;

        public List<MicroInstruction> MicroInstructions { get; set; }

        public List<SimpleMicroInstruction> MicroInstructionsBinaryList { get; set; }

        public MicroInstructionBinaryParser(List<Tuple<string, int, string>> microInstructionTuples)
        {
            _microInstructionDictionaries = new MicroInstructioBinaryDictionary(AppDomain.CurrentDomain.BaseDirectory + @"\MicroInstructionBinary.txt");
            _microInstructionTuples = microInstructionTuples;
            MicroInstructions = new List<MicroInstruction>();
            MicroInstructionsBinaryList = new List<SimpleMicroInstruction>();
        }
        public void Parse()
        {
            foreach (var microInstructionTuple in _microInstructionTuples)
            {
                var microInstruction = new MicroInstruction();
                microInstruction.MicroInstructionString = microInstructionTuple.Item3;

                string[] microCommands = microInstructionTuple.Item3.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                
                microInstruction.SBUSSourceBinary = _microInstructionDictionaries.SBUSDictionary[microCommands[0]];
                microInstruction.DBUSSourceBinary = _microInstructionDictionaries.DBUSDictionary[microCommands[1]];
                microInstruction.ALUOperationBinary = _microInstructionDictionaries.ALUOperationDictionary[microCommands[2]];
                microInstruction.RBUSDestinationBinary = _microInstructionDictionaries.RBUSDictionary[microCommands[3]];
                microInstruction.OtherOperationsBinary = _microInstructionDictionaries.OtherOperationDictionary[microCommands[4]];
                microInstruction.MemoryOperationsBinary = _microInstructionDictionaries.MemoryOperationDictionary[microCommands[5]];

                microInstruction.SBUSSourceString = microCommands[0];
                microInstruction.DBUSSourceString = microCommands[1];
                microInstruction.ALUOperationString = microCommands[2];
                microInstruction.RBUSDestinationString = microCommands[3];
                microInstruction.OtherOperationsString = microCommands[4];
                microInstruction.MemoryOperationsString = microCommands[5];
                microInstruction.RamificationConditionString = microCommands[6];

                ComputeJump(microInstruction);

                MicroInstructions.Add(microInstruction);
                //Console.WriteLine("Address: " + microInstructionTuple.Item2 + " " + microInstruction.GetMicroInstructionBinary() + " Length:" + microInstruction.GetMicroInstructionBinary().Length);
            }

            MicroInstructions.ForEach(mistr => MicroInstructionsBinaryList.Add(new SimpleMicroInstruction
            {
                MicroInstructionBinary = mistr.GetMicroInstructionBinary(),
                MicroInstructionString = mistr.MicroInstructionString
            }));
        }

        //KILL ME
        private void ComputeJump(MicroInstruction microInstruction)
        {
            string[] jumpStrings = microInstruction.RamificationConditionString.Split(new[] { ' ' },
                StringSplitOptions.RemoveEmptyEntries);

            if (jumpStrings[0].Contains("NONE"))
            {
                microInstruction.RamificationConditionBinary =
                    _microInstructionDictionaries.RamificationConditionDictionary[jumpStrings[0]];

                if (jumpStrings.Length >= 2)
                {
                    microInstruction.TrueOrFalseExecutionBinary = "1";
                    if (jumpStrings[1].Contains("STEP"))
                    {
                        microInstruction.IndexSelectionBinaryFirst  = "111";
                    }
                    else if(jumpStrings[1].Equals("JMP"))
                    {
                        byte microInstructionAddress = Convert.ToByte(
                            _microInstructionTuples.Find(mistr => mistr.Item1.Equals(jumpStrings[2])).Item2);

                        microInstruction.MicroAddressJumpBinaryFirst = Convert.ToString(microInstructionAddress, 2).PadLeft(8, '0');

                        microInstruction.MicroAddressJumpStringFirst = microInstructionAddress.ToString();

                        microInstruction.IndexSelectionBinaryFirst = "000"; // NONE
                    }
                    else if (jumpStrings[1].Equals("JMPI"))
                    {
                        string[] jumpTarget = jumpStrings[2].Split('+');

                        byte microInstructionAddress = Convert.ToByte(
                            _microInstructionTuples.Find(mistr => mistr.Item1.Equals(jumpTarget[0])).Item2);

                        microInstruction.MicroAddressJumpBinaryFirst = Convert.ToString(microInstructionAddress, 2).PadLeft(8, '0');

                        microInstruction.MicroAddressJumpStringFirst = microInstructionAddress.ToString();

                        microInstruction.IndexSelectionBinaryFirst = GetIndex(jumpTarget[1]);
                    }

                }
            }
            else if (jumpStrings[0].Contains("IF"))
            {
                microInstruction.RamificationConditionBinary =
                   _microInstructionDictionaries.RamificationConditionDictionary[jumpStrings[0] + " " + jumpStrings[1]];

                // 0 Specifica TRUE(saltul se face pe conditie adev, adica f = 1)
                // 1 Specifica FALSE(saltul se face pe conditie falsa, adica f(neg) = 1 / f = 0)

                microInstruction.TrueOrFalseExecutionBinary = jumpStrings[1].StartsWith("N") ? "1" : "0";

                microInstruction.TrueOrFalseExecutionString = microInstruction.TrueOrFalseExecutionBinary == "1" ? "false" : "true";

                GetFirstOperand(microInstruction, jumpStrings,2);

                GetSecondOperand(microInstruction, jumpStrings,5);
               
            }
        }

        private void GetFirstOperand(MicroInstruction microInstruction, string[] jumpStrings, int position)
        {
            if (jumpStrings[position].Equals("JMP"))
            {
                byte microInstructionAddress = Convert.ToByte(
                    _microInstructionTuples.Find(mistr => mistr.Item1.Equals(jumpStrings[position + 1])).Item2);

                microInstruction.MicroAddressJumpBinaryFirst = Convert.ToString(microInstructionAddress, 2).PadLeft(8, '0');

                microInstruction.MicroAddressJumpStringFirst = microInstructionAddress.ToString();
            }
            else if (jumpStrings[position].Equals("JMPI"))
            {
                string[] jumpTarget = jumpStrings[position + 1].Split('+');

                byte microInstructionAddress = Convert.ToByte(
                    _microInstructionTuples.Find(mistr => mistr.Item1.Equals(jumpTarget[0])).Item2);

                microInstruction.MicroAddressJumpBinaryFirst = Convert.ToString(microInstructionAddress, 2).PadLeft(8, '0');

                microInstruction.MicroAddressJumpStringFirst = microInstructionAddress.ToString();

                microInstruction.IndexSelectionBinaryFirst = GetIndex(jumpTarget[1]);
            }
            else if (jumpStrings[position].Equals("STEP"))
            {
                microInstruction.IndexSelectionBinaryFirst = "111"; // STEP
            }
        }

        private void GetSecondOperand(MicroInstruction microInstruction, string[] jumpStrings,int position )
        {
            if (jumpStrings[position].Equals("JMP"))
            {
                byte microInstructionAddress = Convert.ToByte(
                    _microInstructionTuples.Find(mistr => mistr.Item1.Equals(jumpStrings[position + 1])).Item2);

                microInstruction.MicroAddressJumpBinarySecond = Convert.ToString(microInstructionAddress, 2).PadLeft(8, '0');

                microInstruction.MicroAddressJumpStringSecond = microInstructionAddress.ToString();
            }
            else if (jumpStrings[position].Equals("JMPI"))
            {
                string[] jumpTarget = jumpStrings[position + 1].Split('+');

                byte microInstructionAddress = Convert.ToByte(
                    _microInstructionTuples.Find(mistr => mistr.Item1.Equals(jumpTarget[0])).Item2);

                microInstruction.MicroAddressJumpBinarySecond = Convert.ToString(microInstructionAddress, 2).PadLeft(8, '0');

                microInstruction.MicroAddressJumpStringSecond = microInstructionAddress.ToString();

                microInstruction.IndexSelectionBinarySecond = GetIndex(jumpTarget[1]);
            }
            else if (jumpStrings[position].Equals("STEP"))
            {
                microInstruction.IndexSelectionBinarySecond = "111"; // STEP
            }
        }

        private string GetIndex(string index)
        {
            switch (index)
            {
                case "(IR14;IR13;IR12;0)": //B1
                    return "001";

                case "(IR9;IR8;IR7;IR6;0)": //B2
                    return "010";

                case "(IR11;IR10;IR9;IR8;0)": //B3
                    return "011";

                case "(IR5;IR4;IR3;IR2;IR1;IR0;0)": // B4
                    return "100";

                case "(IR5;IR4;0)": // MAD
                    return "101";

                case "(IR11;IR10;0)": // MAS
                    return "110";
            }

            return "000";
        }
    }
}

/*   else if (jumpStrings.Length > 2)
                {
                    if (jumpStrings[1].Equals("JMP"))
                    {
                        byte microInstructionAddress = Convert.ToByte(
                            _microInstructionTuples.Find(mistr => mistr.Item1.Equals(jumpStrings[2])).Item2);

                        microInstruction.MicroAddressJumpBinaryFirst = Convert.ToString(microInstructionAddress, 2).PadLeft(8,'0');

                        microInstruction.MicroAddressJumpStringFirst = microInstructionAddress.ToString();

                        microInstruction.IndexSelectionBinaryFirst = "000"; // NONE

                    }
                    else if (jumpStrings[1].Equals("JMPI"))
                    {
                        string[] jumpTarget = jumpStrings[2].Split('+');

                        byte microInstructionAddress = Convert.ToByte(
                            _microInstructionTuples.Find(mistr => mistr.Item1.Equals(jumpTarget[0])).Item2);

                        microInstruction.MicroAddressJumpBinaryFirst = Convert.ToString(microInstructionAddress, 2).PadLeft(8, '0');

                        microInstruction.MicroAddressJumpStringFirst = microInstructionAddress.ToString();

                        microInstruction.IndexSelectionBinaryFirst = GetIndex(jumpTarget[1]);
                    }
                }     
     
     */
