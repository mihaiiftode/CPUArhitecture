using System;

namespace ArhitecturaCPU.Simulation.Model
{
    public class MicroInstruction
    {
        public string MicroInstructionString { get; set; }


        public string SBUSSourceBinary { get; set; } = "0000"; // 4 biti

        public string DBUSSourceBinary { get; set; } = "0000"; // 4 biti

        public string ALUOperationBinary { get; set; } = "0000"; // 4 biti

        public string RBUSDestinationBinary { get; set; } = "0000"; // 4 biti

        public string OtherOperationsBinary { get; set; } = "00000"; // 4 biti

        public string MemoryOperationsBinary { get; set; } = "00"; // 2 biti

        public string RamificationConditionBinary { get; set; } = "0000"; // 4 biti

        public string TrueOrFalseExecutionBinary { get; set; } = "0";// 1 bit

        public string IndexSelectionBinaryFirst { get; set; } = "000"; // 3 biti

        public string MicroAddressJumpBinaryFirst { get; set; } = "00000000"; // 8 biti

        public string IndexSelectionBinarySecond { get; set; } = "000";  // 3 biti

        public string MicroAddressJumpBinarySecond { get; set; } = "00000000"; // 8 biti


        public string SBUSSourceString { get; set; } // 4 biti

        public string DBUSSourceString { get; set; } // 4 biti

        public string ALUOperationString { get; set; } // 4 biti

        public string RBUSDestinationString { get; set; } // 4 biti

        public string OtherOperationsString { get; set; } // 4 biti

        public string MemoryOperationsString { get; set; } // 2 biti

        public string RamificationConditionString { get; set; } // 4 biti

        public string TrueOrFalseExecutionString { get; set; } // 1 bit

        public string IndexSelectionStringFirst { get; set; } // 3 biti

        public string IndexSelectionStringSecond { get; set; } // 3 biti

        public string MicroAddressJumpStringFirst { get; set; } // 8 biti

        public string MicroAddressJumpStringSecond { get; set; } // 8 biti


        public string GetMicroInstructionBinary()
        {
            return SBUSSourceBinary + DBUSSourceBinary + ALUOperationBinary + RBUSDestinationBinary +
                   OtherOperationsBinary + MemoryOperationsBinary + RamificationConditionBinary +
                   TrueOrFalseExecutionBinary + IndexSelectionBinaryFirst + MicroAddressJumpBinaryFirst +
                   IndexSelectionBinarySecond + MicroAddressJumpBinarySecond;
        }

        public string GetMicroInstructionHex()
        {
            return Convert.ToInt32( GetMicroInstructionBinary()).ToString("x8");
        }
    }
}