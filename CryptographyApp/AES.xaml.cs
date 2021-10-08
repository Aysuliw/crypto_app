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


namespace CryptographyApp
{
    /// <summary>
    /// Логика взаимодействия для AES.xaml
    /// </summary>
    public partial class AES : Window
    {
       
        public AES()
        {
            InitializeComponent();
            
            var keyState = new List<uint[,]>();
        }
        
        private void encryptButton_Click(object sender, RoutedEventArgs e)
        {
            string input = inputBox.Text;
            string key = keyBox.Text;
            AESEncryption EncryptionC = new AESEncryption(input, key);
            stackP.Children.Clear();
            stackP.Children.Add(EncryptionC);
        }

       

        private void decryptButton_Click(object sender, RoutedEventArgs e)
        {
            string input = inputBox.Text;
            string key = keyBox.Text;
            AESDecryption DecryptionC = new AESDecryption(input, key);
            stackP.Children.Clear();
            stackP.Children.Add(DecryptionC);
        }
    }
}
