using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace _12_2_2024_hw
{
    public partial class MainWindow : Window
    {
        //public IList<Process> Processes { get; set; } = Process.GetProcesses().OrderBy(p =>p.ProcessName).ToArray();
        public ObservableCollection<Process> Processes { get; set; } = new ObservableCollection<Process>();
        DispatcherTimer timer;
        public MainWindow()
        {
            InitializeComponent();
            Process[] p = Process.GetProcesses();
            p = p.OrderBy(p => p.ProcessName).ToArray();
            foreach (Process process in p)
            {
                Processes.Add(process);
            }
            DataContext = Processes;
            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 5);
            timer.Start();

        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            for(int i = 0; i < Processes.Count; i++)
            {
                Processes[i].Refresh();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(listView.SelectedItem == null) return;

            Processes[listView.SelectedIndex].Kill();
            Processes.RemoveAt(listView.SelectedIndex);
        }
    }
}