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
    /// Логика взаимодействия для ROT13.xaml
    /// </summary>
    public partial class ROT13 : Window
    {

        int counter = 0,len=0,i=0;
        string s,p = "";
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        public ROT13()
        {
            InitializeComponent();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 500);
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
        public void calculateFunction(int n)
        {
            char c;
            result.Text = String.Empty;
            p = String.Empty;
            counter = 0;
            s = textBox1.Text;

            for (int i = 0; i < s.Length; i++)
            {
                if (char.IsLetter(s[i]))
                {

                    if (char.IsUpper(s[i]))
                    {
                        if (n == 2)
                        { c = (char)(s[i] - 13); if (c < 'A') { c = (char)(c + 26); } }
                        else { c = (char)(((int)s[i] + 13 - 65) % 26 + 65); }
                        p += c;
                    }
                    else
                    {
                        if (n == 2) { c = (char)(s[i] - 13); if (c < 'a') { c = (char)(c + 26); } }
                        else { c = (char)(((int)s[i] + 13 - 97) % 26 + 97); }
                        p += c;
                    }
                }
                else
                    p += s[i];

            }
            len = p.Length;
            dispatcherTimer.Start();
        }
        private void decryptButton_Click(object sender, RoutedEventArgs e)
        {
            i = 2;
            calculateFunction(i);
        }

        private void encryptButton_Click(object sender, RoutedEventArgs e)
        {
            i = 1;
            calculateFunction(i);
        }

    }
}
