using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Process_Manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public List<Process> Processes { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Processes = Process.GetProcesses().ToList();
            Load();
        }

        public void Load()
        {
            ProcessList.Items.Clear();
            foreach (var item in Processes)
            {
                ProcessList.Items.Add(item.Id + "-" + item.ProcessName + "-" + item.HandleCount + "-" + item.MachineName + "-" + item.Threads.Count);
            }
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process process=Process.Start(searchtextb.Text);
                Processes.Add(process);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Load();
            }
        }

        private void KillButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string id=ProcessList.SelectedItem.ToString().Split('-')[0].Trim();
                Process process = Process.GetProcessById(Convert.ToInt32(id));
                process.Kill();
                ProcessList.Items.Remove(ProcessList.SelectedItem);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
