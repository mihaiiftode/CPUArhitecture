using System.Windows;
using ArhitecturaCPU.GUI.Controller;

namespace ArhitecturaCPU.GUI.View
{
    /// <summary>
    /// Interaction logic for MemoryView.xaml
    /// </summary>
    public partial class MemoryView : Window
    {
        public MainWindowController Controller { get; set; }
        public MemoryView(MainWindowController controller)
        {
            Controller = controller;
            InitializeComponent();
        }
    }
}
