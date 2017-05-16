using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace ArhitecturaCPU.GUI.Controller
{
    public class SBUSAndDBUSController
    {
        private MainWindowController _controller;

        public SBUSAndDBUSController(MainWindowController mainWindowController)
        {
            _controller = mainWindowController;
        }

        public string NONE(string bus)
        {
            return bus;
        }

        public string Pd_IR(string bus)
        {

            bus = _controller.IR;

            _controller.MainView.IRActivate.Fill = new SolidColorBrush(Colors.Red);
            if (ReferenceEquals(_controller.SBUS, bus))
            {
                _controller.MainView.PdIRs.Fill = new SolidColorBrush(Colors.Red);
            }
            else
            {
                _controller.MainView.PdIRd.Fill = new SolidColorBrush(Colors.Red);
            }

            return bus;
        }


        public string Pd_IR_OFFSET(string bus)
        {
            bus = _controller.IR.Substring(8, 8).PadLeft(16, _controller.IR[8] == '1' ? '1' : '0');

            _controller.MainView.IRActivate.Fill = new SolidColorBrush(Colors.Red);
            if (ReferenceEquals(_controller.SBUS, bus))
            {
                _controller.MainView.PdIRs.Fill = new SolidColorBrush(Colors.Red);
            }
            else
            {
                _controller.MainView.PdIRd.Fill = new SolidColorBrush(Colors.Red);
            }

            return bus;
        }

        public string Pd_MDR(string bus)
        {
            _controller.MainView.MDRActivate.Fill = new SolidColorBrush(Colors.Red);
            if (ReferenceEquals(_controller.SBUS, bus))
            {
                _controller.MainView.PdMDRs.Fill = new SolidColorBrush(Colors.Red);
            }
            else
            {
                _controller.MainView.PdMDRd.Fill = new SolidColorBrush(Colors.Red);
            }

            bus = _controller.MDR;
            return bus;
        }

        public string Pd_ADR(string bus)
        {
            _controller.MainView.ADRActivate.Fill = new SolidColorBrush(Colors.Red);
            if (ReferenceEquals(_controller.SBUS, bus))
            {
                _controller.MainView.PdADRs.Fill = new SolidColorBrush(Colors.Red);
            }
            else
            {
                _controller.MainView.PdADRd.Fill = new SolidColorBrush(Colors.Red);
            }
            bus = _controller.ADR;
            return bus;
        }

        public string Pd_IVR(string bus)
        {
            _controller.MainView.IVRActivate.Fill = new SolidColorBrush(Colors.Red);
            if (ReferenceEquals(_controller.SBUS, bus))
            {
                _controller.MainView.PdIVRs.Fill = new SolidColorBrush(Colors.Red);
            }
            else
            {
                _controller.MainView.PdIVRd.Fill = new SolidColorBrush(Colors.Red);
            }

            bus = _controller.IVR;
            return bus;
        }

        public string Pd_PC(string bus)
        {
            _controller.MainView.PCAtivate.Fill = new SolidColorBrush(Colors.Red);
            if (ReferenceEquals(_controller.SBUS, bus))
            {
                _controller.MainView.PdPCs.Fill = new SolidColorBrush(Colors.Red);
            }
            else
            {
                _controller.MainView.PdPCd.Fill = new SolidColorBrush(Colors.Red);
            }

            bus = _controller.PC;
            return bus;
        }

        public string Pd_T(string bus)
        {
            _controller.MainView.TActivate.Fill = new SolidColorBrush(Colors.Red);
            if (ReferenceEquals(_controller.SBUS, bus))
            {
                _controller.MainView.PdTs.Fill = new SolidColorBrush(Colors.Red);
            }
            else
            {
                _controller.MainView.PdTd.Fill = new SolidColorBrush(Colors.Red);
            }

            bus = _controller.T;
            return bus;
        }

        public string Pd_NT(string bus)
        {
            _controller.MainView.TActivate.Fill = new SolidColorBrush(Colors.Red);
            if (ReferenceEquals(_controller.SBUS, bus))
            {
                _controller.MainView.PdTs.Fill = new SolidColorBrush(Colors.Red);
            }
            else
            {
                _controller.MainView.PdTd.Fill = new SolidColorBrush(Colors.Red);
            }

            string t = _controller.T;

            int number = Convert.ToInt16(t, 2);
            number = ~number;

            bus = _controller.SignExtension16((short)number);
            return bus;
        }

        public string Pd_SP(string bus)
        {
            _controller.MainView.SPActivate.Fill = new SolidColorBrush(Colors.Red);
            if (ReferenceEquals(_controller.SBUS, bus))
            {
                _controller.MainView.PdSPs.Fill = new SolidColorBrush(Colors.Red);
            }
            else
            {
                _controller.MainView.PdSPd.Fill = new SolidColorBrush(Colors.Red);
            }

            bus = _controller.SP;
            return bus;
        }

        public string Pd_RG(String bus)
        {
            
            if (ReferenceEquals(bus, _controller.SBUS))
            {
                var index = Convert.ToInt32(_controller.IR.Substring(6, 4), 2);
                var label = _controller.MainView.FindName("R" + index) as TextBlock;

                if (label != null) label.Background = new SolidColorBrush(Colors.Red);
                bus = _controller.GeneralRegisters[index];
            }
            else
            {
                var index = Convert.ToInt32(_controller.IR.Substring(12, 4), 2);
                var label = _controller.MainView.FindName("R" + index) as TextBlock;

                if (label != null) label.Background = new SolidColorBrush(Colors.Red);
                bus = _controller.GeneralRegisters[index];
            }

            _controller.MainView.RGActivate.Fill = new SolidColorBrush(Colors.Red);
            if (ReferenceEquals(_controller.SBUS, bus))
            {
                _controller.MainView.PdRGs.Fill = new SolidColorBrush(Colors.Red);
            }
            else
            {
                _controller.MainView.PdRGd.Fill = new SolidColorBrush(Colors.Red);
            }

            return bus;
        }

        public string Pd_NRG(String bus)
        {
            Pd_RG(bus);

            int number = Convert.ToInt16(bus, 2);
            number = ~number;

            bus = _controller.SignExtension16((short)number);
            return bus;
        }

        public string Pd_FLAG(string bus)
        {
            _controller.MainView.FLAGActivate.Fill = new SolidColorBrush(Colors.Red);
            if (ReferenceEquals(_controller.SBUS, bus))
            {
                _controller.MainView.PdFLAGs.Fill = new SolidColorBrush(Colors.Red);
            }
            else
            {
                _controller.MainView.PdFLAGd.Fill = new SolidColorBrush(Colors.Red);
            }

            bus = _controller.FLAG;
            return bus;
        }

        public string Pd_0(string bus)
        {
            _controller.MainView.ZeroActivate.Fill = new SolidColorBrush(Colors.Red);
            if (ReferenceEquals(_controller.SBUS, bus))
            {
                _controller.MainView.Pd0s.Fill = new SolidColorBrush(Colors.Red);
            }
            else
            {
                _controller.MainView.Pd0d.Fill = new SolidColorBrush(Colors.Red);
            }

            bus = _controller.SignExtension16(0);
            return bus;
        }

        public string Pd_Minus1(string bus)
        {
            _controller.MainView.MinusOneActivate.Fill = new SolidColorBrush(Colors.Red);
            if (ReferenceEquals(_controller.SBUS, bus))
            {
                _controller.MainView.PdMinus1.Fill = new SolidColorBrush(Colors.Red);
            }

            bus = _controller.SignExtension16(-1);
            return bus;
        }

        public string Pd_1(string bus)
        {
            _controller.MainView.OneActivate.Fill = new SolidColorBrush(Colors.Red);
            if (ReferenceEquals(_controller.SBUS, bus))
            {
                _controller.MainView.Pd1.Fill = new SolidColorBrush(Colors.Red);
            }

            bus = _controller.SignExtension16(1);
            return bus;
        }
    }
}
