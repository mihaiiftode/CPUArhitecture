using System;
using System.Collections.Generic;
using ArhitecturaCPU.GUI.Controller;

namespace ArhitecturaCPU.GUI.Simulation
{
    public class MicroInstructionInterpreter
    {
        private MainWindowController mainWindowController;

        public MicroInstructionInterpreter(MainWindowController mainWindowController)
        {
            this.mainWindowController = mainWindowController;
        }

        public static bool Enabled { get; set; }

        public Dictionary<string, Func<string, string>> SBUSSourceActions { get; set; } = new Dictionary<string, Func<string,string>>();

        public Dictionary<string, Func<string, string>> DBUSSourceActions { get; set; } = new Dictionary<string, Func<string,string>>();

        public Dictionary<string, Action> ALUOperationActions { get; set; } = new Dictionary<string, Action>();

        public Dictionary<string, Action> RBUSDestinationActions { get; set; } = new Dictionary<string, Action>();

        public Dictionary<string, Action> OtherOperationActions { get; set; } = new Dictionary<string, Action>();

        public Dictionary<string, Action> MemoryOperationActions { get; set; } = new Dictionary<string, Action>();


        public void BindActions(MainWindowController controller)
        {
            BindSBUS(controller);
            BindDBUS(controller);
            BindALU(controller);
            BindRBUSDestination(controller);
            BindOtherOperations(controller);
            BindMemoryOperations(controller);
        }

        private void BindSBUS(MainWindowController controller)
        {
            SBUSSourceActions.Add("0000", controller.SbusAndDbusController.NONE); // NONE
            SBUSSourceActions.Add("0001", controller.SbusAndDbusController.Pd_IR); // Pd_IR
            SBUSSourceActions.Add("0010", controller.SbusAndDbusController.Pd_IR_OFFSET); // Pd_IR[OFFSET]
            SBUSSourceActions.Add("0011", controller.SbusAndDbusController.Pd_MDR); // Pd_MDR
            SBUSSourceActions.Add("0100", controller.SbusAndDbusController.Pd_ADR); // Pd_ADR
            SBUSSourceActions.Add("0101", controller.SbusAndDbusController.Pd_IVR); // Pd_IVR
            SBUSSourceActions.Add("0110", controller.SbusAndDbusController.Pd_PC); // Pd_PC
            SBUSSourceActions.Add("0111", controller.SbusAndDbusController.Pd_T); // Pd_T
            SBUSSourceActions.Add("1000", controller.SbusAndDbusController.Pd_NT); // Pd_NT
            SBUSSourceActions.Add("1001", controller.SbusAndDbusController.Pd_SP); // Pd_SP
            SBUSSourceActions.Add("1010", controller.SbusAndDbusController.Pd_RG); // Pd_RG
            SBUSSourceActions.Add("1011", controller.SbusAndDbusController.Pd_FLAG); // Pd_FLAG
            SBUSSourceActions.Add("1100", controller.SbusAndDbusController.Pd_0); // Pd_0
            SBUSSourceActions.Add("1101", controller.SbusAndDbusController.Pd_Minus1); // Pd_-1
            SBUSSourceActions.Add("1110", controller.SbusAndDbusController.Pd_1); // Pd_1
            SBUSSourceActions.Add("1111", null); // NONE
        }

        private void BindDBUS(MainWindowController controller)
        {
            DBUSSourceActions.Add("0000", controller.SbusAndDbusController.NONE); // NONE
            DBUSSourceActions.Add("0001", controller.SbusAndDbusController.Pd_IR); // Pd_IR
            DBUSSourceActions.Add("0010", controller.SbusAndDbusController.Pd_MDR); // Pd_MDR
            DBUSSourceActions.Add("0011", controller.SbusAndDbusController.Pd_ADR); // Pd_ADR
            DBUSSourceActions.Add("0100", controller.SbusAndDbusController.Pd_IVR); // Pd_IVR
            DBUSSourceActions.Add("0101", controller.SbusAndDbusController.Pd_PC); // Pd_PC
            DBUSSourceActions.Add("0110", controller.SbusAndDbusController.Pd_T); // Pd_T
            DBUSSourceActions.Add("0111", controller.SbusAndDbusController.Pd_NT); // Pd_NT
            DBUSSourceActions.Add("1000", controller.SbusAndDbusController.Pd_SP); // Pd_SP
            DBUSSourceActions.Add("1001", controller.SbusAndDbusController.Pd_RG); // Pd_RG
            DBUSSourceActions.Add("1010", controller.SbusAndDbusController.Pd_NRG); // Pd_NRG
            DBUSSourceActions.Add("1011", controller.SbusAndDbusController.Pd_FLAG); // Pd_FLAG
            DBUSSourceActions.Add("1100", controller.SbusAndDbusController.Pd_0); // Pd_0s
            DBUSSourceActions.Add("1101", controller.SbusAndDbusController.Pd_Minus1); // Pd_-1s
            DBUSSourceActions.Add("1110", null); // null
            DBUSSourceActions.Add("1111", null); // null
        }

        private void BindALU(MainWindowController controller)
        {
            ALUOperationActions.Add("0000", controller.AluOperationController.NONE); // NONE
            ALUOperationActions.Add("0001", controller.AluOperationController.SBUS); // SBUS
            ALUOperationActions.Add("0010", controller.AluOperationController.NSBUS); // NSBUS
            ALUOperationActions.Add("0011", controller.AluOperationController.DBUS); // DBUS
            ALUOperationActions.Add("0100", controller.AluOperationController.SUM); // SUM
            ALUOperationActions.Add("0101", controller.AluOperationController.AND); // AND
            ALUOperationActions.Add("0110", controller.AluOperationController.OR); // OR
            ALUOperationActions.Add("0111", controller.AluOperationController.XOR); // XOR
            ALUOperationActions.Add("1000", controller.AluOperationController.ASL); // ASL
            ALUOperationActions.Add("1001", controller.AluOperationController.ASR); // ASR
            ALUOperationActions.Add("1010", controller.AluOperationController.LSR); // LSR
            ALUOperationActions.Add("1011", controller.AluOperationController.ROL); // ROL
            ALUOperationActions.Add("1100", controller.AluOperationController.ROR); // ROR
            ALUOperationActions.Add("1101", controller.AluOperationController.RLC); // RLC
            ALUOperationActions.Add("1110", controller.AluOperationController.RRC); // RRC
            ALUOperationActions.Add("1111", null); // NONE
        }

        private void BindRBUSDestination(MainWindowController controller)
        {
            RBUSDestinationActions.Add("0000", controller.RbusDestinationController.NONE); // NONE
            RBUSDestinationActions.Add("0001", controller.RbusDestinationController.Pm_FLAG); // Pm_FLAG
            RBUSDestinationActions.Add("0010", controller.RbusDestinationController.Pm_RG); // Pm_RG
            RBUSDestinationActions.Add("0011", controller.RbusDestinationController.Pm_SP); // Pm_SP
            RBUSDestinationActions.Add("0100", controller.RbusDestinationController.Pm_T); // Pm_T
            RBUSDestinationActions.Add("0101", controller.RbusDestinationController.Pm_PC); // Pm_PC
            RBUSDestinationActions.Add("0110", controller.RbusDestinationController.Pm_IVR); // Pm_IVR
            RBUSDestinationActions.Add("0111", controller.RbusDestinationController.Pm_ADR); // Pm_ADR
            RBUSDestinationActions.Add("1000", controller.RbusDestinationController.Pm_MDR); // Pm_MDR
        }

        private void BindOtherOperations(MainWindowController controller)
        {
            OtherOperationActions.Add("00000", controller.OtherOperationController.NONE); // NONE
            OtherOperationActions.Add("00001", controller.OtherOperationController.Cin_Pd_Cond); // (Cin,Pd_COND)
            OtherOperationActions.Add("00010", controller.OtherOperationController.INT_Minus2SP); // (INTA,-2SP)
            OtherOperationActions.Add("00011", controller.OtherOperationController.Pd_Cond); // Pd_COND
            OtherOperationActions.Add("00100", controller.OtherOperationController.Cin); // Cin
            OtherOperationActions.Add("00101", controller.OtherOperationController.Plus2SP); // +2SP
            OtherOperationActions.Add("00110", controller.OtherOperationController.Minus2SP); // -SP
            OtherOperationActions.Add("00111", controller.OtherOperationController.Plus2PC); // +2PC
            OtherOperationActions.Add("01000", controller.OtherOperationController.A1BVI); // A(1)BVI
            OtherOperationActions.Add("01001", controller.OtherOperationController.A0BVI); // A(0)BVI
            OtherOperationActions.Add("01010", controller.OtherOperationController.A0C); // A(0)C
            OtherOperationActions.Add("01011", controller.OtherOperationController.A0V); // A(0)V
            OtherOperationActions.Add("01100", controller.OtherOperationController.A0Z); // A(0)Z
            OtherOperationActions.Add("01101", controller.OtherOperationController.A0S); // A(0)S
            OtherOperationActions.Add("01110", controller.OtherOperationController.A1C); // A(1)C
            OtherOperationActions.Add("01111", controller.OtherOperationController.A1V); // A(1)V
            OtherOperationActions.Add("10000", controller.OtherOperationController.A1Z); // A(1)Z
            OtherOperationActions.Add("10001", controller.OtherOperationController.A1S); // A(1)S
            OtherOperationActions.Add("10010", controller.OtherOperationController.A0CVZS); // A(0)CVZS
            OtherOperationActions.Add("10011", controller.OtherOperationController.A1CVZS); // A(1)CVZS
        }

        private void BindMemoryOperations(MainWindowController controller)
        {
            MemoryOperationActions.Add("00", controller.MemoryOperationController.NONE); // NONE
            MemoryOperationActions.Add("01", controller.MemoryOperationController.IFCH); // IFCH
            MemoryOperationActions.Add("10", controller.MemoryOperationController.READ); // READ
            MemoryOperationActions.Add("11", controller.MemoryOperationController.WRITE); // WRITE
        }

         
    }
}