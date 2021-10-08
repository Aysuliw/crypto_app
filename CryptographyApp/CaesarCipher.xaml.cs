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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Timers;
using System.Windows.Threading;



namespace CryptographyApp
{
    /// <summary>
    /// Логика взаимодействия для CaesarCipher.xaml
    /// </summary>
    public partial class CaesarCipher : Window
    {
        int key;
        double angle;
        string s, p = "";
        int counter = 0 ,len =0,i=0;
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        public CaesarCipher()
        {
            InitializeComponent();
            
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            
        }
        
        public void rotateImage(int counter, double angle)
        {
            RotateTransform rotate = new RotateTransform(angle);
            Storyboard storyboard = new Storyboard();
            storyboard.Duration = new Duration(TimeSpan.FromSeconds(counter * 0.5));
            DoubleAnimation rotateAnimation = new DoubleAnimation()
            {
                From = 0,
                To = angle,
                Duration = storyboard.Duration,
                RepeatBehavior = new RepeatBehavior(1)
            };

            Storyboard.SetTarget(rotateAnimation, img);
            Storyboard.SetTargetProperty(rotateAnimation, new PropertyPath("(Image.RenderTransform).(RotateTransform.Angle)"));
            img.RenderTransform = rotate;
            storyboard.Children.Add(rotateAnimation);
            storyboard.Begin();
        }
        public void calculateFunction(int n)
        {
            char c;
            result.Text = String.Empty;
            p = String.Empty;
            counter = 0;
            s = textBox1.Text;
            key = Convert.ToInt32(textBox2.Text);
            angle = key * 360 / 26;
            rotateImage(key, angle);
            for (int i = 0; i < s.Length; i++)
            {
                if (char.IsLetter(s[i]))
                {
                    if (char.IsUpper(s[i]))
                    {
                        if (n == 2)
                        { c = (char)(s[i] - key); if (c < 'A') { c = (char)(c + 26); } }
                        else { c = (char)(((int)s[i] + key - 65) % 26 + 65); }
                        p += c;
                    }
                    else
                    {
                        if (n == 2) { c = (char)(s[i] - key); if (c < 'a') { c = (char)(c + 26); } }
                        else { c = (char)(((int)s[i] + key - 97) % 26 + 97); }
                        p += c;
                    }
                 }
                    else
                        p += s[i];

                }
                len = p.Length;
                dispatcherTimer.Start();
            
        }
        private void encryptButton_Click(object sender, RoutedEventArgs e)
        {
            i = 1;
            calculateFunction(i);
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            result.Text = p.Substring(0,counter);
            ++counter;
            if (counter > len)
            {
                dispatcherTimer.Stop();
            }
        }
        private void decryptButton_Click(object sender, RoutedEventArgs e)
        {
            i = 2;
            calculateFunction(i);
        }
    }
}
