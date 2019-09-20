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
using System.Data.SQLite;
using System.IO;

namespace DreamViewer
{
    /// <summary>
    /// Window1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DBtest : Window
    {
        //DirectoryInfo dbPath = new DirectoryInfo(System.IO.Directory.GetCurrentDirectory() + @"\data");
        DirectoryInfo dbPath = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\data");
        private SQLiteConnection conn = null;

        public DBtest()
        {
            InitializeComponent();
        }






        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }
        private void Window_Closed(object sender, EventArgs e)
        {

        }

        private void BTCreateDB_Click(object sender, RoutedEventArgs e)
        {
            if (!dbPath.Exists) dbPath.Create();

            if (!System.IO.File.Exists(dbPath + @"\t.sqlite"))
            {
                SQLiteConnection.CreateFile(dbPath + @"\t.sqlite");
                MessageBox.Show(@"\data\t.sqlite Created");
            }

            conn = new SQLiteConnection("Data Source=" + dbPath + @"\t.sqlite");
            conn.Open();

            //search
            string sql = "select * from artists";

            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
            SQLiteDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                //MessageBox.Show(rdr["pri"] + " " + rdr["uniq"]);
            }
        }


    }
}
