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
    /// Логика взаимодействия для symmetricControl.xaml
    /// </summary>
    public partial class symmetricControl : UserControl
    {
        public symmetricControl()
        {
            InitializeComponent();
        }

        private void DESButton_Click(object sender, RoutedEventArgs e)
        {
            DES win2 = new DES();
            MainWindow myWindow = new MainWindow();
            win2.Show();
            myWindow.Close();
        }

        private void AESButton_Click(object sender, RoutedEventArgs e)
        {
            AES win2 = new AES();
            MainWindow myWindow = new MainWindow();
            win2.Show();
            myWindow.Close();
        }
    }
}
