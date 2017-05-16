using System;
using System.Windows.Media;

namespace ArhitecturaCPU.GUI.Controller
{
    public class OtherOperationController
    {
        private MainWindowController _controller;

        public OtherOperationController(MainWindowController mainWindowController)
        {
            _controller = mainWindowController;
        }

        public void NONE()
        {

        }

        public void Cin_Pd_Cond()
        {
            Cin();
            Pd_Cond();
        }

        public void INT_Minus2SP()
        {

        }

        public void Pd_Cond()
        {
            _controller.MainView.PdCond1.Fill = new SolidColorBrush(Colors.Red);
            _controller.MainView.PdCond2.Fill = new SolidColorBrush(Colors.Red);
            _controller.MainView.PdCond3.Fill = new SolidColorBrush(Colors.Red);

            _controller.FLAG = _controller.ALUFLAG;
        }

        public void Cin()
        {
            short value = Convert.ToInt16(_controller.MDR, 2);
            value += 1;

            _controller.MDR = _controller.SignExtension16(value);

            var flag = _controller.ALUFLAG.ToCharArray();

            flag[13] = value == 0 ? '1' : '0';

            _controller.RBUS = _controller.SignExtension16(value);

            _controller.ALUFLAG = new string(flag);
        }

        public void Plus2SP()
        {
            short value = Convert.ToInt16(_controller.SP, 2);
            value += 2;

            _controller.SP = _controller.SignExtension16(value);
        }

        public void Minus2SP()
        {
            short value = Convert.ToInt16(_controller.SP, 2);
            value -= 2;

            _controller.SP = _controller.SignExtension16(value);
        }

        public void Plus2PC()
        {
            short value = Convert.ToInt16(_controller.PC, 2);
            value += 2;

            _controller.PC = _controller.SignExtension16(value);
        }

        public void A1BVI()
        {
            //
        }

        public void A0BVI()
        {
            //
        }

        public void A0C()
        {
            var flag = _controller.FLAG.ToCharArray();

            flag[12] = '0';

            _controller.FLAG = flag.ToString();
        }

        public void A0V()
        {
            var flag = _controller.FLAG.ToCharArray();

            flag[15] = '0';

            _controller.FLAG = flag.ToString();
        }

        public void A0Z()
        {
            var flag = _controller.FLAG.ToCharArray();

            flag[13] = '0';

            _controller.FLAG = flag.ToString();
        }

        public void A0S()
        {
            var flag = _controller.FLAG.ToCharArray();

            flag[14] = '0';

            _controller.FLAG = flag.ToString();
        }

        public void A1C()
        {
            var flag = _controller.FLAG.ToCharArray();

            flag[12] = '1';

            _controller.FLAG = flag.ToString();
        }

        public void A1V()
        {
            var flag = _controller.FLAG.ToCharArray();

            flag[15] = '1';

            _controller.FLAG = flag.ToString();
        }

        public void A1Z()
        {
            var flag = _controller.FLAG.ToCharArray();

            flag[13] = '1';

            _controller.FLAG = flag.ToString();
        }

        public void A1S()
        {
            var flag = _controller.FLAG.ToCharArray();

            flag[14] = '1';

            _controller.FLAG = flag.ToString();
        }

        public void A0CVZS()
        {
            A0C();
            A0V();
            A0Z();
            A0S();
        }

        public void A1CVZS()
        {
            A1C();
            A1V();
            A1Z();
            A1S();
        }
    }
}


