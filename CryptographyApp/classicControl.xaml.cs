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
    /// Логика взаимодействия для classicControl.xaml
    /// </summary>
    public partial class classicControl : UserControl
    {
        public classicControl()
        {
            InitializeComponent();
        }

        private void caesarButton_Click(object sender, RoutedEventArgs e)
        {
            CaesarCipher win2 = new CaesarCipher();
            MainWindow myWindow = new MainWindow();
            win2.Show();
            myWindow.Close();
            
        }

        

        private void ROT13Button_Click(object sender, RoutedEventArgs e)
        {
            ROT13 win2 = new ROT13();
            MainWindow myWindow = new MainWindow();
            win2.Show();
            myWindow.Close();

        }

        private void atbashButton_Click(object sender, RoutedEventArgs e)
        {
            AtbashCipher win2 = new AtbashCipher();
            MainWindow myWindow = new MainWindow();
            win2.Show();
            myWindow.Close();
        }

        private void VigenereButton_Click(object sender, RoutedEventArgs e)
        {
            Vigenere win2 = new Vigenere();
            MainWindow myWindow = new MainWindow();
            win2.Show();
            myWindow.Close();
        }
    }
}
