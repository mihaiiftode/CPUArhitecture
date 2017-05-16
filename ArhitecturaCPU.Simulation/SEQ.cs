using System;

namespace ArhitecturaCPU.Simulation
{
    class SEQ
    {
        public IndexSelection IndexSelection { get; set; } = new IndexSelection();

        public void LdMIR()
        {
            int address = Convert.ToInt32(Simulation.MAR, 2);

            Simulation.MIR = Simulation.MicroProgramMemory[address].MicroInstructionBinary;
        }

        public void LdMAR(string jumpAddress /* 8 biti*/)
        {
            byte index = IndexSelection.GetIndex();

            byte address = Convert.ToByte(jumpAddress, 2);

            address += index;

            Simulation.MAR = Convert.ToString(address, 2).PadLeft(8, '0');
        }

        public void PlusOneMAR()
        {
            byte address = Convert.ToByte(Simulation.MAR,2);

            address++;

            Simulation.MAR = Convert.ToString(address, 2).PadLeft(8,'0');
        }

        public void EnableMicroCommandInterpreter()
        {
            MicroInstructionInterpreterD.Enabled = true;
        }
    }
}
