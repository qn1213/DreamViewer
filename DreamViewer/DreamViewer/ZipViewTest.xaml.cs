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

// 프로젝트 설정 -> 참조에다가
// 여러가지 추가해주고 해야함

// 압축
using System.IO;
using System.IO.Compression;
using Microsoft.Win32;
using System.Windows.Forms;

namespace DreamViewer
{
    /// <summary>
    /// ZipViewTest.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ZipViewTest : Window
    {
        private string path, name;

        public ZipViewTest()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog ofdlg = new Microsoft.Win32.OpenFileDialog();
            {
                // 다이얼로그의 좌측 즐겨찾기(?) 에 바로가기가 표시된다.
                ofdlg.CustomPlaces.Add(FileDialogCustomPlaces.Contacts);
                ofdlg.CustomPlaces.Add(FileDialogCustomPlaces.Cookies);
                ofdlg.CustomPlaces.Add(FileDialogCustomPlaces.Desktop);
                ofdlg.CustomPlaces.Add(FileDialogCustomPlaces.Documents);
                ofdlg.CustomPlaces.Add(FileDialogCustomPlaces.Favorites);
                ofdlg.CustomPlaces.Add(FileDialogCustomPlaces.LocalApplicationData);
                ofdlg.CustomPlaces.Add(FileDialogCustomPlaces.Music);
                ofdlg.CustomPlaces.Add(FileDialogCustomPlaces.Pictures);
                ofdlg.CustomPlaces.Add(FileDialogCustomPlaces.ProgramFiles);
                ofdlg.CustomPlaces.Add(FileDialogCustomPlaces.ProgramFilesCommon);
                ofdlg.CustomPlaces.Add(FileDialogCustomPlaces.Programs);
                ofdlg.CustomPlaces.Add(FileDialogCustomPlaces.RoamingApplicationData);
                ofdlg.CustomPlaces.Add(FileDialogCustomPlaces.SendTo);
                ofdlg.CustomPlaces.Add(FileDialogCustomPlaces.StartMenu);
                ofdlg.CustomPlaces.Add(FileDialogCustomPlaces.Startup);
                ofdlg.CustomPlaces.Add(FileDialogCustomPlaces.System);
                ofdlg.CustomPlaces.Add(FileDialogCustomPlaces.Templates);

                // 바로가기 위치 생성도 가능하다.
                Microsoft.Win32.FileDialogCustomPlace myPlace = new Microsoft.Win32.FileDialogCustomPlace(@"C:\driver");
                ofdlg.CustomPlaces.Add(myPlace);

                ofdlg.InitialDirectory = @"C:\driver";   // 기본 폴더 지정
                ofdlg.CheckFileExists = true;   // 파일 존재여부확인
                ofdlg.CheckPathExists = true;   // 폴더 존재여부확인
                ofdlg.Filter = // 필터설정 (주로 확장자 및 주요 파일을 필터로 설정함)
                    "Zip 파일 | *.zip; *.rar";

                //// 파일 선택 완료 시 이벤트
                //ofdlg.FileOk += (s, a) =>
                //{
                //    System.Windows.MessageBox.Show("Completed");
                //};

                // 파일 열기 (값의 유무 확인)
                if (ofdlg.ShowDialog().GetValueOrDefault())
                {

                    addr.Text = ofdlg.FileName;
                    path = addr.Text;
                    System.Windows.MessageBox.Show(path);
                }
            }
        }

        private void Extraction_Click(object sender, RoutedEventArgs e)
        {
            string info_text = "info.txt";
            try
            {
                string text = new string(
                (new System.IO.StreamReader(
                System.IO.Compression.ZipFile.OpenRead(path)
                .Entries.Where(x => x.Name.Equals(info_text,
                                             StringComparison.InvariantCulture))
                .FirstOrDefault()
                .Open(), Encoding.UTF8)
                .ReadToEnd())
                .ToArray());

                test_text.Text = text;
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("파일 선택부터 하세요");
            }
           
        }

        // 파일 트리 뽑기
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Open))
                using (ZipArchive archive = new ZipArchive(fs, ZipArchiveMode.Read))
                {
                    TreeViewItem item = new TreeViewItem();

                    foreach (ZipArchiveEntry entry in archive.Entries)
                    {
                        item.Header = path;
                        item.Items.Add(entry.Name);
                    }
                    treenode.Items.Add(item);
                }
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("파일 선택부터 하세요");
            }
            
        }
    }
}
