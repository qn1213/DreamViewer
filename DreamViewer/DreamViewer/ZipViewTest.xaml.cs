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
        private string path;

        // 갤러리 번호
        private string string_Id;
        // 타이틀
        private string string_Title;
        // 작가(배열)
        private string[] string_Artist;
        // 그룹(배열)
        private string[] string_Group;
        // 타입(배열)
        private string[] string_Type;
        // 시리즈(배열)
        private string[] string_Series;
        // 캐릭터(배열)
        private string[] string_Character;
        // 태그(배열)
        private string[] string_Tag;
        // 언어(배열)
        private string[] string_Language;

        public ZipViewTest()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog ofdlg = new Microsoft.Win32.OpenFileDialog();
            {
                //// 다이얼로그의 좌측 즐겨찾기(?) 에 바로가기가 표시된다.
                //ofdlg.CustomPlaces.Add(FileDialogCustomPlaces.Contacts);
                //ofdlg.CustomPlaces.Add(FileDialogCustomPlaces.Cookies);
                //ofdlg.CustomPlaces.Add(FileDialogCustomPlaces.Desktop);
                //ofdlg.CustomPlaces.Add(FileDialogCustomPlaces.Documents);
                //ofdlg.CustomPlaces.Add(FileDialogCustomPlaces.Favorites);
                //ofdlg.CustomPlaces.Add(FileDialogCustomPlaces.LocalApplicationData);
                //ofdlg.CustomPlaces.Add(FileDialogCustomPlaces.Music);
                //ofdlg.CustomPlaces.Add(FileDialogCustomPlaces.Pictures);
                //ofdlg.CustomPlaces.Add(FileDialogCustomPlaces.ProgramFiles);
                //ofdlg.CustomPlaces.Add(FileDialogCustomPlaces.ProgramFilesCommon);
                //ofdlg.CustomPlaces.Add(FileDialogCustomPlaces.Programs);
                //ofdlg.CustomPlaces.Add(FileDialogCustomPlaces.RoamingApplicationData);
                //ofdlg.CustomPlaces.Add(FileDialogCustomPlaces.SendTo);
                //ofdlg.CustomPlaces.Add(FileDialogCustomPlaces.StartMenu);
                //ofdlg.CustomPlaces.Add(FileDialogCustomPlaces.Startup);
                //ofdlg.CustomPlaces.Add(FileDialogCustomPlaces.System);
                //ofdlg.CustomPlaces.Add(FileDialogCustomPlaces.Templates);

                // 바로가기 위치 생성도 가능하다.
                Microsoft.Win32.FileDialogCustomPlace myPlace = new Microsoft.Win32.FileDialogCustomPlace(@"C:\driver");
                ofdlg.CustomPlaces.Add(myPlace);

                ofdlg.InitialDirectory = @"C:\driver";   // 기본 폴더 지정
                ofdlg.CheckFileExists = true;   // 파일 존재여부확인
                ofdlg.CheckPathExists = true;   // 폴더 존재여부확인
                ofdlg.Filter = // 필터설정 (주로 확장자 및 주요 파일을 필터로 설정함)
                    "Zip 파일 | *.zip; *.rar";

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

                string[] dataSort = text.Split(Environment.NewLine.ToCharArray(),

                StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < dataSort.Length; i++)
                {
                    test_text.Text += dataSort[i];
                    test_text.Text += "\r\n";
                }

                // 여기서 각 카테고리를 분류시켜서 각 변수에 넣어주는데
                // 양식을 제대로 지켜줘야 가능한 부분임. 안그러면 에러남
                string_Id = Single_StringFilter(dataSort[0]);
                string_Title = Single_StringFilter(dataSort[1]);
                string_Artist = Array_StringFilter(dataSort[2]);
                string_Group = Array_StringFilter(dataSort[3]);
                string_Type = Array_StringFilter(dataSort[4]);
                string_Series = Array_StringFilter(dataSort[5]);
                string_Character = Array_StringFilter(dataSort[6]);
                string_Tag = Array_StringFilter(dataSort[7]);
                string_Language = Array_StringFilter(dataSort[8]);


                id.Text = string_Id;
                tittle.Text = string_Title;
                ShowTextBox(artist, string_Artist);
                ShowTextBox(group, string_Group);
                ShowTextBox(type, string_Type);
                ShowTextBox(series, string_Series);
                ShowTextBox(character, string_Character);
                ShowTextBox(tag, string_Tag);                
                ShowTextBox(language, string_Language);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }
           
        }

        // 배열이 아니고 단일 변수일때
        private string Single_StringFilter(string strings)
        {
            string[] data = strings.Split(new string[] { ": " }, StringSplitOptions.None);

            return data[1];
        }

        // 배열 string 변수일때
        private string[] Array_StringFilter(string strings)
        {
            string[] data1 = strings.Split(new string[] { ": " }, StringSplitOptions.None);
       
            string[] data2 = data1[1].Split(new string[] { ", " }, StringSplitOptions.None);

            return data2;
        }

        // 배열 변수 TextBox에 뿌리기
        private void ShowTextBox(System.Windows.Controls.TextBox t, string[] arr)
        {
            t.Text = "";
            for (int i = 0; i < arr.Length; i++)
            {
                t.Text += arr[i];
                if(i != arr.Length-1)
                {
                    t.Text += "\r\n";
                }                
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
            catch (Exception err)
            {
                System.Windows.MessageBox.Show(err.ToString());
            }
            
        }

        // 이건 그냥 내비두면됨
        private void Test_text_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
