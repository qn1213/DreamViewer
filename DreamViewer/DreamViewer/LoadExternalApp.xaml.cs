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
    /// <summary>
    /// LoadExternalApp.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class LoadExternalApp : Window
    {
        private string string_test1;
        private bool bool_test1;
        private bool bool_test2;

        public LoadExternalApp()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            RunProcess("-r test");
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
                dreamCore.WaitForExit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }

            // https://docs.microsoft.com/ko-kr/dotnet/api/system.diagnostics.process.standardoutput?view=netframework-4.8
            StreamReader reader = dreamCore.StandardOutput;
            string outPut = reader.ReadToEnd();
            MessageBox.Show(outPut);


            // Core.exe에서 아웃풋된 인자를 읽어들여 분리 후 각 타입에 맞게 초기화
            string[] split = outPut.Split('\n');

            string_test1 = split[0] as string;
            if (!(bool_test1 = bool.TryParse(split[1], out bool_test1)))
            {
                MessageBox.Show("2번째 인자 변환 실패");
            }

            if (!(bool_test2 = bool.TryParse(split[2], out bool_test2)))
            {
                MessageBox.Show("3번째 인자 변환 실패");
            }
            ////////////////////////////////////////////////////////////////////////////

            // 예제용 출력 변수들
            t_Text.Text = split[0];
            t_bool1.Text = split[1];
            t_bool2.Text = split[2];

            return dreamCore.ExitCode;
        }
    }
}
