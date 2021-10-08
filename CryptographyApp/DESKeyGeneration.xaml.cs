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
    /// Логика взаимодействия для DESKeyGeneration.xaml
    /// </summary>
    public partial class DESKeyGeneration : Window
    {
        
        public DESKeyGeneration(StringBuilder binaryKey,StringBuilder binKeyPermutation,string[] firstHalf,string[] secondHalf,string[] KeyGener,StringBuilder[] K)
        {
            InitializeComponent();
            binaryKeyBlock.Text = Convert.ToString( binaryKey);
            binKeyPermutationBlock.Text = Convert.ToString(binKeyPermutation);
            for(int i = 1; i < 17; i++)
            {
                firstHalfBlock.Text = firstHalfBlock.Text + Environment.NewLine + firstHalf[i];
                secondHalfBlock.Text = secondHalfBlock.Text + Environment.NewLine + secondHalf[i];
            }
            for(int i = 0; i < 16; i++)
            {
                KeyGenerBlock.Text = KeyGenerBlock.Text + Environment.NewLine + KeyGener[i];
                ReadyKeyBlock.Text = ReadyKeyBlock.Text + Environment.NewLine + Convert.ToString(K[i]);
            }
        }
        
    }
}
