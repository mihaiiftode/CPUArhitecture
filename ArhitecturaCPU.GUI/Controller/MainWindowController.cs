// THIS IS A F**KING JOKE
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using ArhitecturaCPU.Assembler.Model;
using ArhitecturaCPU.GUI.Simulation;
using ArhitecturaCPU.Assembler.Parser;
using ArhitecturaCPU.FileOperation.Implementations;
using ArhitecturaCPU.GUI.View;
using ArhitecturaCPU.Simulation;
using ArhitecturaCPU.Simulation.Model;

namespace ArhitecturaCPU.GUI.Controller
{
    public class MainWindowController : INotifyPropertyChanged
    {
        private int _indexStep = 1;
        private bool _asmLoaded = false;
        private bool _microInstructionsLoaded = false;
        private bool _initialized;
        private string _pc = "0000000000000000";
        private String _rbus = "0000000000000000";
        private String _dbus = "0000000000000000";
        private string _sbus = "0000000000000000";
        private string _ivr = "0000000000000000";
        private string _t = "0000000000000000";
        private string _sp = "0000000010000000";
        private string _flag = "0000000000000000";
        private string _mdr = "0000000000000000";
        private string _adr = "0000000000000000";
        private string _ir = "0000000000000000";
        private string _aluflag = "0000000000000000";
        private ObservableCollection<string> _asmFileContentList;
        private ObservableCollection<string> _generalRegisters;
        private Dictionary<ushort, short> _mainMemory;
        private ObservableCollection<string> _binaryFileContentList;
        private MicroInstruction _currentMicroInstruction;
        private int _currentIndex = 0;
        private int _selectedBinaryIndex;
        private string _selectedAsmValue;

        public MainWindow MainView { get; private set; }

        public SBUSAndDBUSController SbusAndDbusController { get; set; }
        public ALUOperationController AluOperationController { get; set; }
        public RBUSDestinationController RbusDestinationController { get; set; }
        public OtherOperationController OtherOperationController { get; set; }
        public MemoryOperationController MemoryOperationController { get; set; }
        public MicroInstructionInterpreter MicroInstructionInterpreter { get; private set; }

        public ArhitecturaCPU.Simulation.Simulation Simulation { get; private set; }

        public List<Instruction> Instructions { get; set; }
        public ObservableCollection<Tuple<string, int, string>> MicroInstructionTuples { get; set; }
        public ObservableCollection<MicroInstruction> MicroInstructions { get; set; }
        public ObservableCollection<SimpleMicroInstruction> SimpleMicroInstructions { get; set; }

        public ObservableCollection<string> AsmFileContentList
        {
            get { return _asmFileContentList; }
            set
            {
                _asmFileContentList = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<string> BinaryFileContentList
        {
            get { return _binaryFileContentList; }
            set { _binaryFileContentList = value; OnPropertyChanged(); }
        }
        public ObservableCollection<string> GeneralRegisters
        {
            get { return _generalRegisters; }
            set
            {
                _generalRegisters = value;
                OnPropertyChanged();
            }
        }
        public Dictionary<ushort, short> MainMemory
        {
            get { return _mainMemory; }
            set { _mainMemory = value; OnPropertyChanged(); }
        }

        public String SBUS
        {
            get { return _sbus; }
            set
            {
                _sbus = value;
                OnPropertyChanged();
            }
        }
        public String DBUS
        {
            get { return _dbus; }
            set
            {
                _dbus = value;
                OnPropertyChanged();
            }
        }
        public string RBUS
        {
            get { return _rbus; }
            set
            {
                _rbus = value;
                OnPropertyChanged();
            }
        }
        public string PC
        {
            get { return _pc; }
            set
            {
                _pc = value;
                SelectedBinaryIndex = Convert.ToInt32(_pc, 2) / 2;
                OnPropertyChanged();
            }
        }

        public string IVR
        {
            get { return _ivr; }
            set
            {
                _ivr = value;
                OnPropertyChanged();
            }
        }
        public string T
        {
            get { return _t; }
            set
            {
                _t = value;
                OnPropertyChanged();
            }
        }
        public string SP
        {
            get { return _sp; }
            set
            {
                _sp = value;
                OnPropertyChanged();
            }
        }
        public string FLAG
        {
            get { return _flag; }
            set
            {
                _flag = value;
                ArhitecturaCPU.Simulation.Simulation.Flags = _flag;
                OnPropertyChanged();
            }
        }
        public string MDR
        {
            get { return _mdr; }
            set
            {
                _mdr = value;
                OnPropertyChanged();
            }
        }
        public string ADR
        {
            get { return _adr; }
            set
            {
                _adr = value;
                OnPropertyChanged();
            }
        }
        public string IR
        {
            get
            {
                return _ir;
            }
            set
            {
                _ir = value;

                ArhitecturaCPU.Simulation.Simulation.Instruction = _ir;
                Instruction instruction = Instructions.Find(instr => instr.BinaryString == _ir);
                SelectedAsmValue = instruction != null ? instruction.InstructionString : "";

                OnPropertyChanged();
            }
        }
        public string MIR { get; set; }
        public string ALUFLAG
        {
            get { return _aluflag; }
            set { _aluflag = value; }
        }
        public int CurrentIndex
        {
            get { return _currentIndex; }
            set
            {
                _currentIndex = value;
                OnPropertyChanged();
            }
        }

        public MicroInstruction CurrentMicroInstruction
        {
            get { return _currentMicroInstruction; }
            set
            {
                _currentMicroInstruction = value;
                OnPropertyChanged();
            }
        }

        public int SelectedBinaryIndex
        {
            get { return _selectedBinaryIndex; }
            set
            {
                _selectedBinaryIndex = value;
                OnPropertyChanged();
            }
        }

        public string SelectedAsmValue
        {
            get { return _selectedAsmValue; }
            set
            {
                _selectedAsmValue = value;
                OnPropertyChanged();
            }
        }

        public MainWindowController()
        {
            BinaryFileContentList = new ObservableCollection<string>();
            MicroInstructions = new ObservableCollection<MicroInstruction>();
            SimpleMicroInstructions = new ObservableCollection<SimpleMicroInstruction>();
            AsmFileContentList = new ObservableCollection<string>();
            LoadRegisters();
            InitializeMemory();
        }

        private void InitializeMemory()
        {
            MainMemory = new Dictionary<ushort, short>();

            for (ushort i = 0; i < 1000; i += 2)
            {
                MainMemory.Add(i, 0);
            }
        }

        private void LoadRegisters()
        {
            GeneralRegisters = new ObservableCollection<string>();
            for (ushort i = 0; i <= 15; i++)
            {
                GeneralRegisters.Add("".PadRight(16, '0'));
            }
        }

        public void RegisterView(MainWindow mainWindow)
        {
            MainView = mainWindow;

            Simulation = new ArhitecturaCPU.Simulation.Simulation();
            SbusAndDbusController = new SBUSAndDBUSController(this);
            AluOperationController = new ALUOperationController(this);
            RbusDestinationController = new RBUSDestinationController(this);
            OtherOperationController = new OtherOperationController(this);
            MemoryOperationController = new MemoryOperationController(this);
            MicroInstructionInterpreter = new MicroInstructionInterpreter(this);

            MicroInstructionInterpreter.BindActions(this);
        }

        public void GetNextMicroInstruction()
        {
            Simulation.LoadNextMicroInstruction();
            MIR = Simulation.GetMicroInstruction();
        }

        public void Step()
        {
            if (!_asmLoaded && !_microInstructionsLoaded)
            {
                MessageBox.Show("Files not loaded!");
                return;
            }

            if (!_initialized)
            {
                Simulation.Initialize();
                _initialized = true;
                MIR = Simulation.GetMicroInstruction();
                CurrentMicroInstruction = MicroInstructions[Convert.ToInt32(ArhitecturaCPU.Simulation.Simulation.MAR, 2)];
            }

            //GetNextMicroInstruction();
            string address;
            switch (_indexStep)
            {
                case 1:
                    address = MIR.Substring(0, 4);
                    SBUS = MicroInstructionInterpreter.SBUSSourceActions[address](SBUS);
                    MainView.SBUS.Fill = new SolidColorBrush(Colors.Red);
                    MainView.SBUSTextBox.Background = Brushes.Pink;
                    break;

                case 2:
                    address = MIR.Substring(4, 4);
                    DBUS = MicroInstructionInterpreter.DBUSSourceActions[address](DBUS);
                    MainView.SBUS.Fill = new SolidColorBrush(Colors.Red);
                    MainView.SBUSTextBox.Background = Brushes.White;
                    MainView.DBUSSTextBox.Background = Brushes.Pink;
                    break;

                case 3:
                    address = MIR.Substring(8, 4);
                    MicroInstructionInterpreter.ALUOperationActions[address]();
                    MainView.DBUSSTextBox.Background = Brushes.White;
                    MainView.ALUTextBox.Background = Brushes.Pink;
                    break;

                case 4:
                    address = MIR.Substring(12, 4);
                    MicroInstructionInterpreter.RBUSDestinationActions[address]();

                    MainView.RBUS1.Fill = new SolidColorBrush(Colors.Red);
                    MainView.RBUS2.Fill = new SolidColorBrush(Colors.Red);
                    MainView.RBUS3.Fill = new SolidColorBrush(Colors.Red);

                    MainView.ALUTextBox.Background = Brushes.White;
                    MainView.RBUSTextBox.Background = Brushes.Pink;
                    break;

                case 5:
                    address = MIR.Substring(16, 5);
                    MicroInstructionInterpreter.OtherOperationActions[address]();
                    MainView.RBUSTextBox.Background = Brushes.White;
                    MainView.OtherOperationTextBox.Background = Brushes.Pink;
                    break;

                case 6:
                    address = MIR.Substring(21, 2);
                    MicroInstructionInterpreter.MemoryOperationActions[address]();
                    MainView.OtherOperationTextBox.Background = Brushes.White;
                    MainView.MemoryTextBox.Background = Brushes.Pink;
                    break;

                case 7:
                    GetNextMicroInstruction();
                    MainView.MemoryTextBox.Background = Brushes.White;
                    MainView.JumpTextBox.Background = Brushes.Pink;

                    break;
                default:
                    MainView.JumpTextBox.Background = Brushes.White;
                    /*
                    var indexInstr = SimpleMicroInstructions.First(instr => instr.MicroInstructionBinary == MIR);
                    CurrentIndex = SimpleMicroInstructions.IndexOf(indexInstr);*/
                    MainView.ResetView();
                    CurrentIndex = Convert.ToInt32(ArhitecturaCPU.Simulation.Simulation.MAR, 2);
                    CurrentMicroInstruction = MicroInstructions[CurrentIndex];
                    _indexStep = 0;
                    break;

            }
            _indexStep++;
        }

        public void Run()
        {
            if (_indexStep != 1) return;
            for (var i = 0; i < 8; i++)
            {
                Step();
            }
        }

        public void LoadAsm(string filename)
        {
            var asmParser = new FileParser(filename);

            asmParser.Parse();
            var binaryReader = new BinaryFileReader(filename.Replace(".asm", ".bin"));

            AsmFileContentList.Clear();
            asmParser.Instructions.ForEach(instr => AsmFileContentList.Add(instr.InstructionString));

            Instructions = asmParser.Instructions;


            binaryReader.ReadAll();
            BinaryFileContentList.Clear();
            binaryReader.Instructions.ForEach(instr => BinaryFileContentList.Add(SignExtension16(instr)));

            for (int i = 0, y = 0; i < binaryReader.Instructions.Count; i++, y += 2)
            {
                MainMemory[(ushort)y] = binaryReader.Instructions[i];
            }
            _asmLoaded = true;
        }



        public void LoadMicroInstructions(string filename)
        {
            var microInstructionLoader = new MicroInstructionLoader(filename);
            microInstructionLoader.Load();
            MicroInstructionTuples = new ObservableCollection<Tuple<string, int, string>>(microInstructionLoader.MicroInstructionTuples);

            var microInstructionBinaryParser = new MicroInstructionBinaryParser(MicroInstructionTuples.ToList());
            microInstructionBinaryParser.Parse();

            MicroInstructions = new ObservableCollection<MicroInstruction>(microInstructionBinaryParser.MicroInstructions);
            SimpleMicroInstructions = new ObservableCollection<SimpleMicroInstruction>(microInstructionBinaryParser.MicroInstructionsBinaryList);

            Simulation.LoadMemory(SimpleMicroInstructions.ToList());

            _microInstructionsLoaded = true;
        }

        public void ShowFiles()
        {
            if (_microInstructionsLoaded)
            {
                FileContentView view = new FileContentView(this) { Visibility = Visibility.Visible };
            }
            else
            {
                MessageBox.Show("Files not loaded");
            }
        }

        public void ShowMicroInstructions()
        {
            if (_microInstructionsLoaded)
            {
                MicroInstructionView view = new MicroInstructionView(this) { Visibility = Visibility.Visible };
            }
            else
            {
                MessageBox.Show("Files not loaded");
            }
        }

        public void ShowMainMemory()
        {
            if (_asmLoaded)
            {
                MemoryView view = new MemoryView(this) { Visibility = Visibility.Visible };
            }
            else
            {
                MessageBox.Show("Files not loaded");
            }
        }

        public void ShowMicroInstructionMemory()
        {
            if (_microInstructionsLoaded)
            {
                MicroCodeMemoryView view = new MicroCodeMemoryView(this) { Visibility = Visibility.Visible };
            }
            else
            {
                MessageBox.Show("Files not loaded");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public string SignExtension16(short value)
        {
            return Convert.ToString(value, 2).PadLeft(16, value < 0 ? '1' : '0');
        }

        public string SignExtension8(short value)
        {
            return Convert.ToString(value, 2).PadLeft(8, value < 0 ? '1' : '0');
        }
    }
}
