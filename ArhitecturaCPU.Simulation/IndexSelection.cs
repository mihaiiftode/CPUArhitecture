using System;

namespace ArhitecturaCPU.Simulation
{
    public class IndexSelection
    {
        public byte GetIndex()
        {
            var indexBinary = Simulation.GlobalG == "1" ? Simulation.MIR.Substring(28, 3) : Simulation.MIR.Substring(39, 3);
            byte index = Convert.ToByte(indexBinary, 2);

            switch (index)
            {
                case 0x0:
                    return 0;
                case 0x1:
                    return Convert.ToByte(Simulation.Instruction.Substring(1, 3).PadRight(4, '0').PadLeft(8, '0'), 2);
                case 0x2:
                    return Convert.ToByte(Simulation.Instruction.Substring(6, 4).PadRight(5, '0').PadLeft(8, '0'), 2);
                case 0x3:
                    return Convert.ToByte(Simulation.Instruction.Substring(4, 4).PadRight(5, '0').PadLeft(8, '0'), 2);
                case 0x4:
                    return Convert.ToByte(Simulation.Instruction.Substring(10, 6).PadRight(7, '0').PadLeft(8, '0'), 2);
                case 0x5:
                    return Convert.ToByte(Simulation.Instruction.Substring(10, 2).PadRight(3, '0').PadLeft(8, '0'), 2);
                case 0x6:
                    return Convert.ToByte(Simulation.Instruction.Substring(4, 2).PadRight(3, '0').PadLeft(8, '0'), 2);
                case 0x7:
                    return byte.MaxValue;
                default:
                    return byte.MaxValue;

            }
        }
    }
}