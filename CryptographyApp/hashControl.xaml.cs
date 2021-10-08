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

namespace CryptographyApp
{
    /// <summary>
    /// Логика взаимодействия для hashControl.xaml
    /// </summary>
    public partial class hashControl : UserControl
    {
        public hashControl()
        {
            InitializeComponent();
        }

        private void md5Button_Click(object sender, RoutedEventArgs e)
        {
            MD5 win2 = new MD5();
            MainWindow myWindow = new MainWindow();
            win2.Show();
            myWindow.Close();
        }

        private void sha1Button_Click(object sender, RoutedEventArgs e)
        {
            SHA1 win2 = new SHA1();
            MainWindow myWindow = new MainWindow();
            win2.Show();
            myWindow.Close();
        }

        private void sha256Button_Click(object sender, RoutedEventArgs e)
        {
            SHA256 win2 = new SHA256();
            MainWindow myWindow = new MainWindow();
            win2.Show();
            myWindow.Close();
        }

       
        
    }
}
