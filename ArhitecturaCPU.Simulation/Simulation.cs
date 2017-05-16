using System.Collections.Generic;
using ArhitecturaCPU.Simulation.Model;

namespace ArhitecturaCPU.Simulation
{
    public class Simulation
    {
        private GlobalFunction _globalFunction;
        private IndexSelection _indexSelection;
        private SEQ _seq;

        public static string Instruction { get; set; } // 16 biti instructiunea

        public static string Flags { get; set; } // 16 biti registru flag

        public static string MAR { get; set; } // registru MAR(8 biti)

        public static string MIR { get; set; } // registru MIR(50 biti)

        public static string GlobalG { get; set; }

        public static string INT { get; set; }

        public static string ACLOW { get; set; }

        public static List<SimpleMicroInstruction> MicroProgramMemory { get; set; }

        public Simulation()
        {
            _globalFunction = new GlobalFunction();
            _indexSelection = new IndexSelection();
            _seq = new SEQ();
        }

        public void LoadMemory(List<SimpleMicroInstruction> memoryList)
        {
            MicroProgramMemory = new List<SimpleMicroInstruction>(memoryList);
        }

        public void Initialize()
        {
            INT = "0";
            MAR = "00000000";
            _seq.LdMIR();
        }

        public void LoadNextMicroInstruction()
        {
            _globalFunction.ComputeGlobalG();
            var index = _indexSelection.GetIndex();

            var address = GlobalG == "1" ? MIR.Substring(31, 8) : MIR.Substring(42, 8);
            if (index == byte.MaxValue)
            {
                _seq.PlusOneMAR();
            }
            else
            {
                _seq.LdMAR(address);
            }

            _seq.LdMIR();
        }

        public string GetMicroInstruction()
        {
            return MIR;
        }
    }
}
