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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.IO;

namespace DreamViewer
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        Window dbTestWindow = null;
        public MainWindow()
        {
            InitializeComponent();
            this.Closed += MainWindow_Closed;
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown(0);
        }

        private void BtOpenDbTest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dbTestWindow==null)
                {
                    dbTestWindow = new DBtest();
                    dbTestWindow.Show();
                    dbTestWindow.Owner = this;
                }
                else
                {
                    dbTestWindow.Show();
                    dbTestWindow.Activate();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
            
        }

        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window zipTest = new ZipViewTest();

            zipTest.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Window ExteernalAppTest = new LoadExternalApp();

            ExteernalAppTest.Show();
        }
    }
}