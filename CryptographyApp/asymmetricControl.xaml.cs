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
    /// Логика взаимодействия для asymmetricControl.xaml
    /// </summary>
    public partial class asymmetricControl : UserControl
    {
        public asymmetricControl()
        {
            InitializeComponent();
        }

        private void DiffieHellman_Click(object sender, RoutedEventArgs e)
        {
            DiffieHellman win2 = new DiffieHellman();
            MainWindow myWindow = new MainWindow();
            win2.Show();
            myWindow.Close();
        }

        private void RSAButton_Click(object sender, RoutedEventArgs e)
        {
            RSA win2 = new RSA();
            MainWindow myWindow = new MainWindow();
            win2.Show();
            myWindow.Close();
        }
    }
}
