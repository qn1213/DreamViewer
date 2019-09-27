using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Diagnostics;
using System.IO;

namespace DreamViewer
{
    public partial class LoadExternalApp : Window
    {
        public LoadExternalApp()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(RunProcess("sql -a init") == 1)
            {
                MessageBox.Show("The process is still alive.");
            }
        }

        private int RunProcess(String Args)
        {
            Process dreamCore = new Process();
            // 프로그램의 현 위치 + Core 프로그램 위치
            DirectoryInfo dbPath = new DirectoryInfo(System.IO.Directory.GetCurrentDirectory() + @"\Data\lib\DreamViewerCore.exe");
            try
            {
                dreamCore.StartInfo.FileName = dbPath.ToString();
                dreamCore.StartInfo.Arguments = Args;
                dreamCore.StartInfo.CreateNoWindow = true;
                dreamCore.StartInfo.UseShellExecute = false;
                dreamCore.StartInfo.RedirectStandardOutput = true;

                dreamCore.Start();

                ProcName.Text = dreamCore.ProcessName;
                // https://docs.microsoft.com/ko-kr/dotnet/api/system.diagnostics.process.standardoutput?view=netframework-4.8
                StreamReader reader = dreamCore.StandardOutput;
                string outPut = reader.ReadToEnd();

                tempText.Text = outPut;

                dreamCore.WaitForExit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }

            return dreamCore.ExitCode;
        }
    }
}
