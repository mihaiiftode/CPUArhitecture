using System;
using System.Windows.Media;

namespace ArhitecturaCPU.GUI.Controller
{
    public class MemoryOperationController
    {
        private MainWindowController _controller;

        public MemoryOperationController(MainWindowController mainWindowController)
        {
            _controller = mainWindowController;
        }

        public void NONE()
        {

        }

        public void IFCH()
        {
            _controller.MainView.DataOUT1.Fill = new SolidColorBrush(Colors.Red);
            _controller.MainView.DataOUT2.Fill = new SolidColorBrush(Colors.Red);
            _controller.MainView.ADR.Fill = new SolidColorBrush(Colors.Red);

            _controller.IR =_controller.SignExtension16(_controller.MainMemory[Convert.ToUInt16(_controller.ADR,2)]);
        }

        public void READ()
        {
            _controller.MainView.ADR.Fill = new SolidColorBrush(Colors.Red);
            _controller.MainView.DataOUT1.Fill = new SolidColorBrush(Colors.Red);
            _controller.MainView.DataOUT2.Fill = new SolidColorBrush(Colors.Red);
            _controller.MainView.ADR.Fill = new SolidColorBrush(Colors.Red);

            ushort index = Convert.ToUInt16(_controller.ADR,2);
            _controller.MDR = _controller.SignExtension16(_controller.MainMemory[index]);
        }

        public void WRITE()
        {
            _controller.MainView.ADR.Fill = new SolidColorBrush(Colors.Red);
            _controller.MainView.DataIN.Fill = new SolidColorBrush(Colors.Red);

            _controller.MainMemory[Convert.ToUInt16(_controller.ADR,2)] = Convert.ToInt16(_controller.MDR,2);
        }
    }
}
