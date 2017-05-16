using System.Windows;
using ArhitecturaCPU.GUI.Controller;

namespace ArhitecturaCPU.GUI.View
{
    /// <summary>
    /// Interaction logic for FileContentView.xaml
    /// </summary>
    public partial class FileContentView : Window
    {
        public MainWindowController Controller { get; set; }

        public FileContentView(MainWindowController controller)
        {
            Controller = controller;
            InitializeComponent();
        }
    }
}
