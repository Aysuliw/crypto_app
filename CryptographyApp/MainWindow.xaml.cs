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
using System.Windows.Forms;


namespace CryptographyApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            

        }
        homeControl homeC = new homeControl();
        classicControl classicC = new classicControl();
        symmetricControl symmetricC = new symmetricControl();
        asymmetricControl asymmetricC = new asymmetricControl();
        hashControl hashC = new hashControl();
        private void homeButton_Click(object sender, RoutedEventArgs e)
        {
            homeButton.BorderThickness = new Thickness(10, 0, 0, 0);
            classicButton.BorderThickness = new Thickness(0, 0, 0, 0);
            symmetricButton.BorderThickness = new Thickness(0, 0, 0, 0);
            asymmetricButton.BorderThickness = new Thickness(0, 0, 0, 0);
            hashButton.BorderThickness = new Thickness(0, 0, 0, 0);
            homeButton.Background = new SolidColorBrush(Color.FromRgb(142, 88, 237));
            classicButton.Background = Brushes.Transparent;
            symmetricButton.Background = Brushes.Transparent;
            asymmetricButton.Background = Brushes.Transparent;
            hashButton.Background = Brushes.Transparent;
            stk.Children.Clear();
            stk.Children.Add(homeC);
        }
        private void classicButton_Click(object sender, RoutedEventArgs e)
        {
            classicButton.BorderThickness = new Thickness(10, 0, 0, 0);
            homeButton.BorderThickness = new Thickness(0, 0, 0, 0);
            symmetricButton.BorderThickness = new Thickness(0, 0, 0, 0);
            asymmetricButton.BorderThickness = new Thickness(0, 0, 0, 0);
            hashButton.BorderThickness = new Thickness(0, 0, 0, 0);
            classicButton.Background = new SolidColorBrush(Color.FromRgb(142, 88, 237));
            homeButton.Background = Brushes.Transparent;
            symmetricButton.Background = Brushes.Transparent;
            asymmetricButton.Background = Brushes.Transparent;
            hashButton.Background = Brushes.Transparent;
            stk.Children.Clear();
            stk.Children.Add(classicC);

        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void symmetricButton_Click(object sender, RoutedEventArgs e)
        {
            symmetricButton.BorderThickness = new Thickness(10, 0, 0, 0);
            classicButton.BorderThickness = new Thickness(0, 0, 0, 0);
            homeButton.BorderThickness = new Thickness(0, 0, 0, 0);
            asymmetricButton.BorderThickness = new Thickness(0, 0, 0, 0);
            hashButton.BorderThickness = new Thickness(0, 0, 0, 0);
            symmetricButton.Background = new SolidColorBrush(Color.FromRgb(142, 88, 237));
            classicButton.Background = Brushes.Transparent;
            homeButton.Background = Brushes.Transparent;
            asymmetricButton.Background = Brushes.Transparent;
            hashButton.Background = Brushes.Transparent;
            stk.Children.Clear();
            stk.Children.Add(symmetricC);
        }

        private void asymmetricButton_Click(object sender, RoutedEventArgs e)
        {
            asymmetricButton.BorderThickness = new Thickness(10, 0, 0, 0);
            classicButton.BorderThickness = new Thickness(0, 0, 0, 0);
            homeButton.BorderThickness = new Thickness(0, 0, 0, 0);
            symmetricButton.BorderThickness = new Thickness(0, 0, 0, 0);
            hashButton.BorderThickness = new Thickness(0, 0, 0, 0);
            asymmetricButton.Background = new SolidColorBrush(Color.FromRgb(142, 88, 237));
            classicButton.Background = Brushes.Transparent;
            symmetricButton.Background = Brushes.Transparent;
            homeButton.Background = Brushes.Transparent;
            hashButton.Background = Brushes.Transparent;
            stk.Children.Clear();
            stk.Children.Add(asymmetricC);
        }

        private void hashButton_Click(object sender, RoutedEventArgs e)
        {
            hashButton.BorderThickness = new Thickness(10, 0, 0, 0);
            classicButton.BorderThickness = new Thickness(0, 0, 0, 0);
            homeButton.BorderThickness = new Thickness(0, 0, 0, 0);
            asymmetricButton.BorderThickness = new Thickness(0, 0, 0, 0);
            symmetricButton.BorderThickness = new Thickness(0, 0, 0, 0);
            hashButton.Background = new SolidColorBrush(Color.FromRgb(142, 88, 237));
            classicButton.Background = Brushes.Transparent;
            symmetricButton.Background = Brushes.Transparent;
            asymmetricButton.Background = Brushes.Transparent;
            homeButton.Background = Brushes.Transparent;
            stk.Children.Clear();
            stk.Children.Add(hashC);
        }

        private void minimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
