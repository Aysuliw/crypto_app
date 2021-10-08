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
using System.Windows.Threading;

namespace CryptographyApp
{
    /// <summary>
    /// Логика взаимодействия для AtbashCipher.xaml
    /// </summary>
    public partial class AtbashCipher : Window
    {
        int counter = 0, len = 0;
        string s, p = "";
        char[] upperLetter1 = new char[26];
        char[] upperLetter2 = new char[26];
        char[] lowerLetter1 = new char[26];
        char[] lowerLetter2 = new char[26];
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        public AtbashCipher()
        {
            InitializeComponent();
            createArrays();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 500);
        }
        public void createArrays()
        {
            for(int i = 0; i < 26; i++)
            {
                upperLetter1[i] = (char)(65 + i);
                upperLetter2[i] = (char)(90 - i);
                lowerLetter1[i] = (char)(97 + i);
                lowerLetter2[i] = (char)(122 - i);
            }
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            result.Text = p.Substring(0, counter);
            ++counter;
            if (counter > len)
            {
                dispatcherTimer.Stop();
            }
        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void encryptButton_Click(object sender, RoutedEventArgs e)
        {
            char c=' ';
            result.Text = String.Empty;
            p = String.Empty;
            counter = 0;
            s = textBox1.Text;
            

            for (int i = 0; i < s.Length; i++)
            {
                if ((int)s[i] != 32)
                {
                    if (char.IsUpper(s[i]))
                    {
                        for(int j = 0; j < 26; j++)
                        {
                            if (s[i].Equals(upperLetter1[j]))
                            {
                                c = upperLetter2[j];
                            }
                        }
                        p += c;
                    }
                    else
                    {
                        for (int j = 0; j < 26; j++)
                        {
                            if ((char)s[i] == lowerLetter1[j])
                            {
                                c = lowerLetter2[j];
                            }
                        }
                        p += c;
                    }
                }
                else
                    p += " ";

            }
            len = p.Length;
            dispatcherTimer.Start();
        }

        private void decryptButton_Click(object sender, RoutedEventArgs e)
        {
            char c=' ';
            result.Text = String.Empty;
            p = String.Empty;
            counter = 0;
            s = textBox1.Text;

            for (int i = 0; i < s.Length; i++)
            {
                if (char.IsLetter(s[i])) { 
                    if (char.IsUpper(s[i]))
                    {
                        for (int j = 0; j < 26; j++)
                        {
                            if ((char)s[i] == upperLetter2[j])
                            {
                                c = upperLetter1[j];
                            }
                        }
                        p += c;
                    }
                    else
                    {
                        for (int j = 0; j < 26; j++)
                        {
                            if ((char)s[i] == lowerLetter2[j])
                            {
                                c = lowerLetter1[j];
                            }
                        }
                        p += c;
                    }
                }
                else
                    p += s[i];

            }
            len = p.Length;
            dispatcherTimer.Start();
        }
    }
}
