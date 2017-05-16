using System.Windows;
using ArhitecturaCPU.GUI.Controller;

namespace ArhitecturaCPU.GUI.View
{
    /// <summary>
    /// Interaction logic for MicroCodeMemory.xaml
    /// </summary>
    public partial class MicroCodeMemoryView : Window
    {
        public MainWindowController Controller { get; set; }

        public MicroCodeMemoryView(MainWindowController controller)
        {
            Controller = controller;
            InitializeComponent();
        }
    }
}
