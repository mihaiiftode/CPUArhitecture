using System;

namespace ArhitecturaCPU.Simulation
{
    class GlobalFunction
    {
        public void ComputeGlobalG()
        {
            byte f = 0;
            byte conditionSelection = Convert.ToByte(Simulation.MIR.Substring(23, 4), 2);
            byte trueOfFalse = Convert.ToByte(Simulation.MIR[27].ToString(), 2);

            switch (conditionSelection)
            {
                case 0x0: // none
                    f = 0;
                    break;
                case 0x1: // if RD
                    if (Simulation.Instruction.Substring(10, 2).Equals("01"))
                    {
                        f = 1;
                    }
                    break;
                case 0x2: // if INT
                    if (Simulation.INT.Equals("1"))
                    {
                        f = 1;
                    }
                    break;
                case 0x3: // if Z
                    if (Simulation.Flags[13].Equals('1'))
                    {
                        f = 1; // T(neg)/F = 1
                    }
                    break;
                case 0x4: // if NZ
                    if (!Simulation.Flags[13].Equals('1'))
                    {
                        f = 1; // T(neg)/F = 0
                    }
                    break; 
                case 0x5: // IF S
                    if (Simulation.Flags[14].Equals('1'))
                    {
                        f = 1; // T(neg)/F = 1
                    }
                    break;
                case 0x6: // IF NS
                    if (!Simulation.Flags[14].Equals('1'))
                    {
                        f = 1; // T(neg)/F = 0
                    }
                    break;
                case 0x7: // IF C
                    if (Simulation.Flags[12].Equals('1'))
                    {
                        f = 1; // T(neg)/F = 1
                    }
                    break;
                case 0x8: // IF NC
                    if (!Simulation.Flags[12].Equals('1'))
                    {
                        f = 1; // T(neg)/F = 0
                    }
                    break;
                case 0x9: // IF V
                    if (Simulation.Flags[15].Equals('1'))
                    {
                        f = 1; // T(neg)/F = 1
                    }
                    break;
                case 0xA: // IF NV
                    if (!Simulation.Flags[15].Equals('1'))
                    {
                        f = 1; // T(neg)/F = 0
                    }
                    break;
                case 0xB: // IF B1
                    if (Simulation.Instruction[0].Equals('0'))
                    {
                        f = 1;
                    }
                    break;
                case 0xC: // IF B2
                    if (Simulation.Instruction[0].Equals('1') && Simulation.Instruction[1].Equals('0') && Simulation.Instruction[2].Equals('0'))
                    {
                        f = 1;
                    }
                    break;
                case 0xD: // IF B3
                    if (Simulation.Instruction[0].Equals('1') && Simulation.Instruction[1].Equals('0') && Simulation.Instruction[2].Equals('1'))
                    {
                        f = 1;  
                    }
                    break;
                case 0xE: // IF B4
                    if (Simulation.Instruction[0].Equals('1') && Simulation.Instruction[1].Equals('1') && Simulation.Instruction[2].Equals('0'))
                    {
                        f = 1;
                    }
                    break;
            }

            Simulation.GlobalG = Convert.ToString(f ^ trueOfFalse, 2);
        }
    }
}
