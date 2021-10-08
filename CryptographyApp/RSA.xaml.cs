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
using System.Numerics;

namespace CryptographyApp
{
    /// <summary>
    /// Логика взаимодействия для RSA.xaml
    /// </summary>
    public partial class RSA : Window
    {
        public RSA()
        {
            InitializeComponent();
        }

        
        public BigInteger findD(BigInteger x, BigInteger y)
        {
            for(int i = 0; i < y; i++)
            {
                if(x*i%y == 1)
                {
                    return i;
                }
            }
            return 0;
        }

        public int findAlphabetPosition(char s)
        {

            return  (char.ToUpper(s) - 64);
             
        }
        public char findAlphabetLetter(int p)
        {

            return (char)(p+64);

        }
        private void encryptButton_Click(object sender, RoutedEventArgs e)
        {
            resultBlock.Text = String.Empty;
            string msg = inputBox.Text;
            int lengthMsg = msg.Length;
            string[] subs = msg.Split();
            BigInteger p = new BigInteger(), q = new BigInteger(), numberE = new BigInteger(), n = new BigInteger();
            p = Convert.ToInt64(pBox.Text);
            q = Convert.ToInt64(qBox.Text);
            numberE = Convert.ToInt64(eBox.Text);
            BigInteger st = new BigInteger();
            n = p * q;
            
            for(int i = 0; i < subs.Length; i++)
            {
                st = BigInteger.Pow(Convert.ToInt32(subs[i]), (int)numberE) % n;
                resultBlock.Text =resultBlock.Text+" "+  Convert.ToString(st);
            }
            
            
        }

        private void decryptButton_Click(object sender, RoutedEventArgs e)
        {
            resultBlock.Text = String.Empty;
            string msg = inputBox.Text;
            string[] subs = msg.Split();
            int lengthMsg = msg.Length;
            BigInteger p = new BigInteger(), q = new BigInteger(), d = new BigInteger(), numberE = new BigInteger();
            BigInteger phi = new BigInteger(), st = new BigInteger() ,n = new BigInteger();
            p = Convert.ToInt64(pBox.Text);
            q = Convert.ToInt64(qBox.Text);
            numberE = Convert.ToInt64(eBox.Text);
            n = p * q;
            phi = (p - 1) * (q - 1);
            
            d = findD(numberE, phi);
            //resultBlock.Text =  Convert.ToString(n)+" " + Convert.ToString(phi) +" " + Convert.ToString(d);
            for (int i = 0; i < subs.Length; i++)
            {
                st = BigInteger.Pow(Convert.ToInt32(subs[i]), (int)d) % n;
                resultBlock.Text = resultBlock.Text + " " + Convert.ToString(st);
            }
            

        }
    }
}
