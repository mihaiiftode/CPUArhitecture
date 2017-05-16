using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;
using ArhitecturaCPU.GUI.Controller;
using Microsoft.Expression.Shapes;

namespace ArhitecturaCPU.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Tuple<string, int, string>> MicroInstructionTuples { get; set; }

        public MainWindowController Controller { get; set; }

        private Grid _gridBackup;

        public MainWindow()
        {
            Controller = new MainWindowController();
            Controller.RegisterView(this);
            _gridBackup = new Grid();

            InitializeComponent();
            _gridBackup = (Grid)XamlReader.Parse(XamlWriter.Save(DrawingGrid));

        }

        private void AsmLoadMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                DefaultExt = ".asm",
                Filter = "ASM Files (*.asm)|*.asm"
            };

            bool? result = openFileDialog.ShowDialog();

            if (result == true)
            {
                string filename = openFileDialog.FileName;
                Controller.LoadAsm(filename);
            }
        }

        private void MicroCodeLoadMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                DefaultExt = ".txt",
                Filter = "TXT Files (*.txt)|*.txt"
            };

            bool? result = openFileDialog.ShowDialog();

            if (result == true)
            {
                string filename = openFileDialog.FileName;
                Controller.LoadMicroInstructions(filename);
            }
        }

        private void ExistMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ShowFilesMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            Controller.ShowFiles();
        }

        private void ShowMicroCodeMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            Controller.ShowMicroInstructions();
        }

        private void ShowMemeoryMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            Controller.ShowMainMemory();
        }

        private void ShowMicroCodeMemoryMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            Controller.ShowMicroInstructionMemory();
        }

        private void StepButton_Click(object sender, RoutedEventArgs e)
        {
            Controller.Step();
        }

        private void RunBUtton_Click(object sender, RoutedEventArgs e)
        {
            Controller.Run();
        }

        public void ResetView()
        {
            ALULabel.Text = "NONE";
            for (int i = 0; i < DrawingGrid.Children.Count; i++)
            {
                if (DrawingGrid.Children[i] is TextBlock)
                {
                    ((TextBlock)DrawingGrid.Children[i]).Background = ((TextBlock)_gridBackup.Children[i]).Background;
                }
                else if (DrawingGrid.Children[i] is Shape)
                {
                    ((Shape)DrawingGrid.Children[i]).Fill = ((Shape)_gridBackup.Children[i]).Fill;
                }
            }

        }
    }
}
