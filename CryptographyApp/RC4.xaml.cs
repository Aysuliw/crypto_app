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
    /// Логика взаимодействия для RC4.xaml
    /// </summary>
    public partial class RC4 : Window
    {
        public RC4()
        {
            InitializeComponent();
        }

        public static StringBuilder StringToBinary(string data)
        {
            StringBuilder returnValue = new StringBuilder();
            foreach (char c in data.ToCharArray())
            {
                returnValue.Append(Convert.ToString(c, 2).PadLeft(8, '0'));
            }
            return returnValue;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string K, binaryKey, pText;
            int j = 0, temp;
            K = "123";
            pText = "abcd";
            binaryKey = StringToBinary(K).ToString();
            int[] S = new int[256];

            for (int i = 0; i < 256; i++)
            {
                S[i] = i;
            }

            for (int i = 0; i < 256; i++)
            {
                j = (j + S[i] + K[i % K.Length]) % 256;
                temp = S[i];
                S[i] = S[j];
                S[j] = temp;

            }

            int k = 0, p = 0;

            string binaryP = StringToBinary(pText).ToString();
            int[] z = new int[pText.Length];
            for (int i = 0; i < pText.Length; i++)
            {
                k = (k + 1) % 256;
                p = (p + S[k]) % 256;
                temp = S[k];
                S[k] = S[p];
                S[p] = temp;
                
            }




        }
    }
}
