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
    /// Логика взаимодействия для DiffieHellman.xaml
    /// </summary>
    public partial class DiffieHellman : Window
    {
        public DiffieHellman()
        {
            InitializeComponent();
        }

        public BigInteger calculateFunction(BigInteger x, BigInteger y, BigInteger p)
        {

            if (y == 1)
            {
                return x;
            }
            else
            {
                return ((BigInteger.Pow(x, (int)y)) % p);
            }
        }
        private void beginButton_Click(object sender, RoutedEventArgs e)
        {
            BigInteger p = new BigInteger(), g = new BigInteger(), A = new BigInteger(), B = new BigInteger();
            BigInteger a = new BigInteger(), b = new BigInteger(), Ka = new BigInteger(), Kb = new BigInteger();

            p = Convert.ToInt64(pBox.Text);
            g = Convert.ToInt64(gBox.Text);
            a = Convert.ToInt64(aBox.Text);
            b = Convert.ToInt64(bBox.Text);

            A = calculateFunction(g, a, p);
            B = calculateFunction(g, b, p);

            ABlock.Text = Convert.ToString(A);
            BBlock.Text = Convert.ToString(B);

            Ka = calculateFunction(B, a, p);
            Kb = calculateFunction(A, b, p);
            KaBlock.Text = Convert.ToString(Ka);
            KbBlock.Text = Convert.ToString(Kb);

        }
    }
}
