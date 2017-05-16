using System.Windows;
using ArhitecturaCPU.GUI.Controller;

namespace ArhitecturaCPU.GUI.View
{
    /// <summary>
    /// Interaction logic for MicroInstructionView.xaml
    /// </summary>
    public partial class MicroInstructionView : Window
    {
        public MainWindowController Controller { get; set; }

        public MicroInstructionView(MainWindowController controller)
        {
            Controller = controller;
            InitializeComponent();
        }
    }
}
