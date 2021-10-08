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
    /// Логика взаимодействия для SHA1.xaml
    /// </summary>
    public partial class SHA1 : Window
    {
        uint[] hexM = new uint[64],M = new uint[64], W = new uint[80];

        uint[] entryA = new uint[80],entryB = new uint[80], entryC = new uint[80],entryD = new uint[80], entryE = new uint[80];
        uint[] finalA = new uint[80],finalB = new uint[80],finalC = new uint[80],finalD = new uint[80],finalE = new uint[80];
       
        uint[] rotateV1 = new uint[80], rotateV2 = new uint[80],F = new uint[80];
        int count = 0;
        
        public SHA1()
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
        public static uint BinaryToDecimal(StringBuilder data)
        {
            string s = data.ToString();
            uint resultDecimal = Convert.ToUInt32(s, 2);
            return resultDecimal;
        }
        public static uint DecimalToHex(uint data)
        {
            string resultHex = data.ToString("X4");
            return Convert.ToUInt32(resultHex, 16);
        }
        public static string HexToBinary(uint data)
        {
            string str = "";
            str = Convert.ToString(data, 2);
            return str;
        }
        public static uint leftrotate(uint x, int c)
        {
            return (x << c) | (x >> (32 - c));
        }
        
        static uint[] valueK = new uint[4] {
            0x5A827999,
                            0x6ED9EBA1,
                            0x8F1BBCDC,
                            0xCA62C1D6
        };

        public void calculateRoundsFunction(StringBuilder binaryValue)
        {
            entryA[0] = 1732584193;
            entryB[0] = 4023233417;
            entryC[0] = 2562383102;
            entryD[0] = 271733878;
            entryE[0] = 3285377520;
            for (int m = 0; m < 64; m++)
            {
                
                string st = binaryValue.ToString(m * 8, 8);

                StringBuilder sb = new StringBuilder();
                
                sb.Append(st);
                M[m] = BinaryToDecimal(sb);
                hexM[m] = DecimalToHex(BinaryToDecimal(sb));

            }
            for(int i = 0; i < 16; i++)
            {
                W[i] = hexM[i * 4] << 24;
                W[i] |= hexM[i * 4 + 1] << 16;
                W[i] |= hexM[i * 4 + 2] << 8;
                W[i] |= hexM[i * 4 + 3];
            }
            for(int i = 16; i < 80; i++)
            {
                W[i] = leftrotate((W[i - 3] ^ W[i - 8] ^ W[i - 14] ^ W[i - 16]), 1);
            }
            for (uint k = 0; k < 80; k++)
            {

                if (k <= 19)
                {
                    F[k] = (entryB[k] & entryC[k]) | (~entryB[k] & entryD[k]);
                    

                }
                else if (k >= 20 && k <= 39)
                {
                    F[k] = entryB[k] ^ entryC[k] ^ entryD[k];


                }
                else if (k >= 40 && k <= 59)
                {
                    F[k] = (entryB[k] & entryC[k]) | (entryB[k] & entryD[k]) | (entryC[k] & entryD[k]); 


                }
                else if (k >= 60)
                {
                    F[k] = entryB[k] ^ entryC[k] ^ entryD[k];

                }

                
                rotateV1[k] = leftrotate(entryA[k],5);
                rotateV2[k] = leftrotate(entryB[k], 30);
                uint temp = rotateV1[k]+F[k]+entryE[k]+W[k]+valueK[k/20];
                finalE[k] = entryD[k];
                finalD[k] = entryC[k];
                finalC[k] = rotateV2[k];
                finalB[k] = entryA[k];
                finalA[k] = temp;
                if (k < 79)
                {
                    entryA[k + 1] = finalA[k];
                    entryB[k + 1] = finalB[k];
                    entryC[k + 1] = finalC[k];
                    entryD[k + 1] = finalD[k];
                    entryE[k + 1] = finalE[k];
                }

            }
        }
        private void hashButton_Click(object sender, RoutedEventArgs e)
        {
            string givenValue;
            StringBuilder binaryValue, bBinary = new StringBuilder();

            int b, i = 0;


            givenValue = inputBox.Text;
            binaryValue = StringToBinary(givenValue);
            byte[] bytess = Encoding.ASCII.GetBytes(givenValue);
            b = binaryValue.Length;
            binaryBox.Text = binaryValue.ToString();
            lengthBox.Text = b.ToString();
            binaryValue.Append('1');
            oneBox.Text = binaryValue.ToString();
            ZeroLength.Text = Convert.ToString(512 * (b / 512 + 1) - 64);
            while ((b + 1) + i != 512 * (b / 512 + 1) - 64)
            {
                binaryValue.Append('0');
                i++;
            }
            zeroBox.Text = binaryValue.ToString();
            textLength.Text = "=" + lengthBox.Text + "=" + StringToBinary(lengthBox.Text).ToString();
            //label5.Text = StringToBinary(lengthBox.Text).ToString();
            string varBinLength = Convert.ToString(b, 2).PadLeft(64, '0');
            binaryLength.Text = "L=" + varBinLength;
            for (int j = 8; j <= 64; j += 8)
            {
                bBinary.Append(varBinLength.Substring(j - 8, 8));
            }
            
            fullBox.Text = binaryValue.Append(bBinary).ToString();
            calculateRoundsFunction(binaryValue);
            backButton.IsEnabled = false;

        }
        public void textBoxWrite(int c)
        {
            roundBlock.Text = Convert.ToString(c + 1) + "-raund";
            entryValueA.Text = Convert.ToString(entryA[c]);
            entryValueB.Text = Convert.ToString(entryB[c]);
            entryValueC.Text = Convert.ToString(entryC[c]);
            entryValueD.Text = Convert.ToString(entryD[c]);
            entryValueE.Text = Convert.ToString(entryE[c]);
            functionValue.Text = Convert.ToString(F[c]);
            
            mValue.Text = Convert.ToString(W[c]);
            kValue.Text = Convert.ToString(valueK[c/20]);
            leftrotate1.Text = Convert.ToString(rotateV1[c]);
            leftrotate2.Text = Convert.ToString(rotateV2[c]);
            finishValueA.Text = Convert.ToString(finalA[c]);
            finishValueB.Text = Convert.ToString(finalB[c]);
            finishValueC.Text = Convert.ToString(finalC[c]);
            finishValueD.Text = Convert.ToString(finalD[c]);
            finishValueE.Text = Convert.ToString(finalE[c]);


        }
        private void enableDisable(int count)
        {
            if (count <= 0)
            {
                backButton.IsEnabled = false;
            }
            else
            {
                backButton.IsEnabled = true;
            }
            if (count >= 79)
            {
                nextButton.IsEnabled = false;
            }
            else
            {
                nextButton.IsEnabled = true;
            }
        }
        private void beginButton_Click(object sender, RoutedEventArgs e)
        {

            count = 0;
            textBoxWrite(count);
            enableDisable(count);
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            enableDisable(count);

            count += 1;
            if (count == 79)
            {
                finalResultFunction();
            }
            textBoxWrite(count);
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            enableDisable(count);
            count -= 1;
            textBoxWrite(count);

        }

        private void finalResultButton_Click(object sender, RoutedEventArgs e)
        {
            finalResultFunction();
        }
        
        private void finalResultFunction()
        {
            uint A, B, C, D,E;
            string HexResult;
            A = entryA[0] + finalA[79];
            B = entryB[0] + finalB[79];
            C = entryC[0] + finalC[79];
            D = entryD[0] + finalD[79];
            E = entryE[0] + finalE[79];
            if (A > Math.Pow(2, 32))
            {
                finalResult.Text = "A=" + Convert.ToString(A - Math.Pow(2, 32)) + Environment.NewLine;
                HexResult = (A - Math.Pow(2, 32)).ToString("x8");
            }
            else
            {
                finalResult.Text = "A=" + Convert.ToString(A) + Environment.NewLine;
                HexResult = A.ToString("x8");
            }
            if (B > Math.Pow(2, 32))
            {
                finalResult.Text = finalResult.Text + "B=" + Convert.ToString(B - Math.Pow(2, 32)) + Environment.NewLine;
                HexResult += (B - Math.Pow(2, 32)).ToString("x8");
            }
            else
            {
                finalResult.Text = finalResult.Text + "B=" + Convert.ToString(B) + Environment.NewLine;
                HexResult +=B.ToString("x8");
            }
            if (C > Math.Pow(2, 32))
            {
                finalResult.Text = finalResult.Text + "C=" + Convert.ToString(C - Math.Pow(2, 32)) + Environment.NewLine;
                HexResult +=(C - Math.Pow(2, 32)).ToString("x8");
            }
            else
            {
                finalResult.Text = finalResult.Text + "C=" + Convert.ToString(C) + Environment.NewLine;
                HexResult += C.ToString("x8");
            }
            if (D > Math.Pow(2, 32))
            {
                finalResult.Text = finalResult.Text + "D=" + Convert.ToString(D - Math.Pow(2, 32)) + Environment.NewLine;
                HexResult += (D - Math.Pow(2, 32)).ToString("x8");
            }
            else
            {
                finalResult.Text = finalResult.Text + "D=" + Convert.ToString(D) + Environment.NewLine;
                HexResult += D.ToString("x8");
            }
            if (E > Math.Pow(2, 32))
            {
                finalResult.Text = finalResult.Text + "E=" + Convert.ToString(E - Math.Pow(2, 32)) + Environment.NewLine;
                HexResult += (E - Math.Pow(2, 32)).ToString("x8");
            }
            else
            {
                finalResult.Text = finalResult.Text + "E=" + Convert.ToString(E) + Environment.NewLine;
                HexResult += E.ToString("x8");
            }
            finalHexResult.Text = HexResult;
        }
    }

}
