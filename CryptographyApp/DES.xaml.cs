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
using System.Windows.Forms;

namespace CryptographyApp
{
    /// <summary>
    /// Логика взаимодействия для DES.xaml
    /// </summary>
    public partial class DES : Window
    {

        public DES()
        {
            InitializeComponent();
            
            
        }

        private void encryptButton_Click(object sender, RoutedEventArgs e)
        {
            string input = inputBox.Text;
            string key = keyBox.Text;
            DESEncryption EncryptionC = new DESEncryption(input,key);
            stackP.Children.Clear();
            stackP.Children.Add(EncryptionC);

        }

        private void decryptButton_Click(object sender, RoutedEventArgs e)
        {
            string input = inputBox.Text;
            string key = keyBox.Text;
            DESDecryption DecryptionC = new DESDecryption(input, key);
            stackP.Children.Clear();
            stackP.Children.Add(DecryptionC);
        }
    }
}
