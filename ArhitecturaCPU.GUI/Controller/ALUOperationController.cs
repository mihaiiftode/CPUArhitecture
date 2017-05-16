using System;
using System.Windows.Media;

namespace ArhitecturaCPU.GUI.Controller
{
    public class ALUOperationController
    {
        private MainWindowController _controller;

        public ALUOperationController(MainWindowController mainWindowController)
        {
            _controller = mainWindowController;
        }

        public void NONE()
        {
            _controller.MainView.ALULabel.Text = "NONE";
        }

        public void SBUS()
        {
            _controller.MainView.ALULabel.Text = "SBUS";
            ActivateALU();

            _controller.RBUS = _controller.SBUS;
        }

      

        public void NSBUS()
        {
            _controller.MainView.ALULabel.Text = "NSBUS";
            ActivateALU();

            string SBUS = _controller.SBUS;

            int number = Convert.ToInt16(SBUS, 2);
            number = ~number;

            _controller.RBUS = _controller.SignExtension16((short)number);
        }

        public void DBUS()
        {
            _controller.MainView.ALULabel.Text = "DBUS";
            ActivateALU();

            _controller.RBUS = _controller.DBUS;
        }

        public void SUM()
        {
            _controller.MainView.ALULabel.Text = "SUM";
            ActivateALU();

            var sbus = Convert.ToInt16(_controller.SBUS, 2);
            var dbus = Convert.ToInt16(_controller.DBUS, 2);

            var result = sbus + dbus;
            var flag = _controller.ALUFLAG.ToCharArray();
            flag[15] = sbus + dbus < 0 && sbus > 0 && dbus > 0 ? '1' : '0';

            flag[13] = result == 0 ? '1' : '0';

            _controller.RBUS = _controller.SignExtension16((short) result);
            flag[14] = _controller.RBUS[0] == '1' ? '1' : '0';

            _controller.ALUFLAG = new string(flag);
        }

        public void AND()
        {
            _controller.MainView.ALULabel.Text = "AND";
            ActivateALU();

            var sbus = Convert.ToInt16(_controller.SBUS, 2);
            var dbus = Convert.ToInt16(_controller.DBUS, 2);

            var result = sbus & dbus;
            var flag = _controller.ALUFLAG.ToCharArray();
            
            flag[13] = result == 0 ? '1' : '0';

            _controller.RBUS = _controller.SignExtension16((short)result);
            flag[14] = _controller.RBUS[0] == '1' ? '1' : '0';

            _controller.ALUFLAG = new string(flag); 
        }

        public void OR()
        {
            _controller.MainView.ALULabel.Text = "OR";
            ActivateALU();

            var sbus = Convert.ToInt16(_controller.SBUS, 2);
            var dbus = Convert.ToInt16(_controller.DBUS, 2);

            var result = sbus | dbus;
            var flag = _controller.ALUFLAG.ToCharArray();

            flag[13] = result == 0 ? '1' : '0';

            _controller.RBUS = _controller.SignExtension16((short)result);
            flag[14] = _controller.RBUS[0] == '1' ? '1' : '0';

            _controller.ALUFLAG = new string(flag);
        }

        public void XOR()
        {
            _controller.MainView.ALULabel.Text = "XOR";
            ActivateALU();

            var sbus = Convert.ToInt16(_controller.SBUS, 2);
            var dbus = Convert.ToInt16(_controller.DBUS, 2);

            var result = sbus ^ dbus;
            var flag = _controller.ALUFLAG.ToCharArray();

            flag[13] = result == 0 ? '1' : '0';

            _controller.RBUS = _controller.SignExtension16((short)result);
            flag[14] = _controller.RBUS[0] == '1' ? '1' : '0';

            _controller.ALUFLAG = new string(flag);
        }

        public void ASL()
        {
            _controller.MainView.ALULabel.Text = "ASL";
            ActivateALU();

            string dbus = _controller.DBUS;
            var flag = _controller.FLAG.ToCharArray();

            flag[12] = dbus[0];
            dbus = dbus.Remove(0, 1);
            dbus += "0";

            _controller.RBUS = dbus;
        }

        public void ASR()
        {
            _controller.MainView.ALULabel.Text = "ASR";
            ActivateALU();

            string dbus = _controller.DBUS;
            var flag = _controller.FLAG.ToCharArray();

            flag[12] = dbus[15];
            dbus = dbus.Remove(15, 1);
            dbus = dbus.Insert(0, dbus[0].ToString());

            _controller.RBUS = dbus;
        }

        public void LSR()
        {
            _controller.MainView.ALULabel.Text = "LSR";
            ActivateALU();

            string dbus = _controller.DBUS;
            var flag = _controller.FLAG.ToCharArray();

            flag[12] = dbus[15];
            dbus = dbus.Remove(15, 1);
            dbus = dbus.Insert(0,"0");

            _controller.RBUS = dbus;
        }

        public void ROL()
        {
            _controller.MainView.ALULabel.Text = "ROL";
            ActivateALU();

            string dbus = _controller.DBUS;
            var flag = _controller.FLAG.ToCharArray();

            flag[12] = dbus[0];
            dbus = dbus.Remove(0, 1);
            dbus = dbus.Insert(14, dbus[0].ToString());

            _controller.RBUS = dbus;
        }

        public void ROR()
        {
            _controller.MainView.ALULabel.Text = "ROR";
            ActivateALU();

            string dbus = _controller.DBUS;
            var flag = _controller.FLAG.ToCharArray();

            flag[12] = dbus[15];
            dbus = dbus.Remove(15, 1);
            dbus = dbus.Insert(0, dbus[0].ToString());

            _controller.RBUS = dbus;
        }

        public void RLC()
        {
            _controller.MainView.ALULabel.Text = "RLC";
            ActivateALU();

            string dbus = _controller.DBUS;
            var flag = _controller.FLAG.ToCharArray();
            var flagBefore = flag[12];

            flag[12] = dbus[0];
            dbus = dbus.Remove(0, 1);
            dbus = dbus.Insert(14, flagBefore.ToString());

            _controller.RBUS = dbus;
        }

        public void RRC()
        {
            _controller.MainView.ALULabel.Text = "RRC";
            ActivateALU();

            string dbus = _controller.DBUS;
            var flag = _controller.FLAG.ToCharArray();
            var flagBefore = flag[12];

            flag[12] = dbus[15];
            dbus = dbus.Remove(15, 1);
            dbus = dbus.Insert(0,flagBefore.ToString());

            _controller.RBUS = dbus;
        }
        private void ActivateALU()
        {
           _controller.MainView.ALU.Fill =new SolidColorBrush(Colors.Red);
           _controller.MainView.ALU2.Fill =new SolidColorBrush(Colors.Red);
           _controller.MainView.ALU3.Fill =new SolidColorBrush(Colors.Red);
           _controller.MainView.ALU4.Fill =new SolidColorBrush(Colors.Red);
           _controller.MainView.ALUActivate.Fill =new SolidColorBrush(Colors.Red);
        }
    }
}
