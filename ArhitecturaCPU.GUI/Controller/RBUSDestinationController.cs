using System;
using System.Windows.Media;

namespace ArhitecturaCPU.GUI.Controller
{
    public class RBUSDestinationController
    {
        private MainWindowController _controller;

        public RBUSDestinationController(MainWindowController mainWindowController)
        {
            _controller = mainWindowController;
        }

        public void NONE()
        {

        }

        public void Pm_FLAG()
        {
            _controller.MainView.PmFLAG.Fill = new SolidColorBrush(Colors.Red);

            _controller.FLAG = _controller.RBUS;
        }

        public void Pm_RG()
        {
            _controller.MainView.PmRG.Fill = new SolidColorBrush(Colors.Red);

            _controller.GeneralRegisters[Convert.ToInt32(_controller.IR.Substring(12, 4), 2)] = _controller.RBUS;
        }

        public void Pm_SP()
        {
            _controller.MainView.PmSP.Fill = new SolidColorBrush(Colors.Red);

            _controller.SP = _controller.RBUS;
        }

        public void Pm_T()
        {
            _controller.MainView.PmT.Fill = new SolidColorBrush(Colors.Red);


            _controller.T = _controller.RBUS;
        }

        public void Pm_PC()
        {
            _controller.MainView.PmPC.Fill = new SolidColorBrush(Colors.Red);


            _controller.PC = _controller.RBUS;
        }

        public void Pm_IVR()
        {
            _controller.MainView.PmIVR.Fill = new SolidColorBrush(Colors.Red);


            _controller.FLAG = _controller.RBUS;
        }

        public void Pm_ADR()
        {
            _controller.MainView.PmADR.Fill = new SolidColorBrush(Colors.Red);
            
            _controller.ADR = _controller.RBUS;
        }

        public void Pm_MDR()
        {
            _controller.MainView.PmMDR1.Fill = new SolidColorBrush(Colors.Red);
            _controller.MainView.PmMDR2.Fill = new SolidColorBrush(Colors.Red);
            _controller.MainView.PmMDR3.Fill = new SolidColorBrush(Colors.Red);
            _controller.MainView.PmMDRActivate.Fill = new SolidColorBrush(Colors.Red);

            _controller.MDR = _controller.RBUS;
        }
    }
}
