using System;
using System.Text.RegularExpressions;
using ArhitecturaCPU.Assembler.AssemblerData;
using ArhitecturaCPU.Assembler.AssemblerData.Implementations;
using ArhitecturaCPU.Assembler.Model;

namespace ArhitecturaCPU.Assembler.Parser
{
    public class OperandAnalyzer
    {
        readonly ParserAddressingData _addressingData = new ParserAddressingData();
        readonly ParserRegisterData _registerData = new ParserRegisterData();

        private string _registerString;
        private string _offsetForRegister;
        public Instruction Instruction { get; }

        public OperandAnalyzer(Instruction instruction)
        {
            Instruction = instruction;
        }

        public Instruction GetOperands()
        {
            switch (Instruction.InstructionType)
            {
                case InstructionType.TwoOperandInstruction:
                    GetSourceOperand();
                    GetDestinationOperand();
                    break;
                case InstructionType.OneOperandInstruction:
                    GetDestinationOperand();
                    break;
                case InstructionType.JumpInstruction:
                    GetJumpOperand();
                    break;
            }

            return Instruction;
        }

        #region operand conversion and parsing
        private void GetSourceOperand()
        {
            var sourceOperand = Instruction.SplitedInstruction[2];

            _registerString = Regex.Match(sourceOperand, @"(?<=\().+?(?=\))").Value;
            _offsetForRegister = Regex.Match(sourceOperand, @"^.*?(?=\()").Value;

            if (IsNumber(sourceOperand))
            {
                Instruction.BinaryString += _addressingData.AdressingModeDictionary["Imediat"] + "0000";
                Instruction.FirsOperandString = Convert.ToString(int.Parse(sourceOperand), 2);
            }

            CheckIndirect(sourceOperand);

            CheckIndexat(sourceOperand, "src");

            CheckDirect(sourceOperand);
        }

        private void GetDestinationOperand()
        {
            var destinationOperand = Instruction.SplitedInstruction[1];

            _registerString = Regex.Match(destinationOperand, @"(?<=\().+?(?=\))").Value;
            _offsetForRegister = Regex.Match(destinationOperand, @"^.*?(?=\()").Value;

            if (IsNumber(destinationOperand))
            {
                Instruction.BinaryString += _addressingData.AdressingModeDictionary["Imediat"] + "0000";
                Instruction.FirsOperandString = Convert.ToString(int.Parse(destinationOperand), 2);
            }

            CheckIndirect(destinationOperand);

            CheckIndexat(destinationOperand, "dst");

            CheckDirect(destinationOperand);
        }

        private static bool IsNumber(string operand)
        {
            return Regex.IsMatch(operand, @"^-*[0-9,\.]+$");
        }

        private void CheckIndirect(string operand)
        {
            if (_registerString.Equals("") || !_offsetForRegister.Equals("")) return;
            if (_registerData.GetRegister(operand).Equals("")) return;

            Instruction.BinaryString +=
                _addressingData.AdressingModeDictionary["Indirect"] +
                _registerData.RegisterDictionary[_registerString];
        }

        private void CheckIndexat(string operand, string source)
        {
            if (_registerString.Equals("") || _offsetForRegister.Equals("")) return;
            if (_registerData.GetRegister(operand).Equals("")) return;

            Instruction.BinaryString +=
                _addressingData.AdressingModeDictionary["Indexat"] +

                _registerData.RegisterDictionary[_registerString];

            if (source.Equals("src"))
                Instruction.FirsOperandString = Convert.ToString(int.Parse(_offsetForRegister), 2);
            else
                Instruction.SecondOperandString = Convert.ToString(int.Parse(_offsetForRegister), 2);
        }

        private void CheckDirect(string operand)
        {
            if (!_registerString.Equals("") || _registerData.GetRegister(operand).Equals("")) return;

            Instruction.BinaryString +=
                _addressingData.AdressingModeDictionary["Direct"] +
                _registerData.GetRegister(operand);
        }

        // TODO: fix here
        private void GetJumpOperand()
        {
            var offset = Instruction.SplitedInstruction[1];
            if (Regex.IsMatch(offset, @"^-*[0-9,\.]+$"))
            {
                Instruction.BinaryString += Convert.ToString(sbyte.Parse(offset),2).Remove(0,8);
            }
            else
            {
                throw new Exception("Invalid offset");
            }
        }
        #endregion 

        private AddressingType Analyze(string str)
        {
            if (_registerString.Equals(""))
            {
                if (Regex.IsMatch(str, @"^-*[0-9,\.]+$"))
                {
                    return AddressingType.Imediat;
                }
            }

            if (!_registerString.Equals("") && _offsetForRegister.Equals(""))
            {
                return AddressingType.Indirect;
            }

            if (!_registerString.Equals("") && !_offsetForRegister.Equals(""))
            {
                return AddressingType.Indexat;
            }

            return AddressingType.Direct;
        }

        private static string SignExtension16(short value)
        {
            return Convert.ToString(value, 2).PadLeft(16, value < 0 ? '1' : '0');
        }

        private static string SignExtension8(sbyte value)
        {
            return Convert.ToString(value, 2).PadLeft(8, value < 0 ? '1' : '0');
        }
    }
}