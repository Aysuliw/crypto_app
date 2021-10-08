using System;
using System.IO;
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
using System.Security.Cryptography;

namespace CryptographyApp
{
    /// <summary>
    /// Логика взаимодействия для SHA256.xaml
    /// </summary>
    public partial class SHA256 : Window
    {
        uint[] hexM = new uint[16];
        uint[] M = new uint[16];
        uint[] W = new uint[64];

        uint[] entryA = new uint[64];
        uint[] entryB = new uint[64];
        uint[] entryC = new uint[64];
        uint[] entryD = new uint[64];
        uint[] entryE = new uint[64];
        uint[] entryF = new uint[64];
        uint[] entryG = new uint[64];
        uint[] entryH = new uint[64];
        uint[] finalA = new uint[64];
        uint[] finalB = new uint[64];
        uint[] finalC = new uint[64];
        uint[] finalD = new uint[64];
        uint[] finalE = new uint[64];
        uint[] finalF = new uint[64];
        uint[] finalG = new uint[64];
        uint[] finalH = new uint[64];
        uint s0, s1;
        uint[] S0 = new uint[64];
        uint[] S1 = new uint[64];
        uint[] CH = new uint[64];
        uint[] Ma = new uint[64];
        uint[] rotateV1 = new uint[80];
        uint[] rotateV2 = new uint[80];
        uint[] F = new uint[80];
        int count = 0;
        
        public SHA256()
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
        public static uint rightrotate(uint x, int c)
        {
            return (x >> c) | (x << (32 - c));
        }
        static uint[] valueK = new uint[64] {
           0x428a2f98, 0x71374491, 0xb5c0fbcf, 0xe9b5dba5, 0x3956c25b, 0x59f111f1, 0x923f82a4, 0xab1c5ed5,
   0xd807aa98, 0x12835b01, 0x243185be, 0x550c7dc3, 0x72be5d74, 0x80deb1fe, 0x9bdc06a7, 0xc19bf174,
   0xe49b69c1, 0xefbe4786, 0x0fc19dc6, 0x240ca1cc, 0x2de92c6f, 0x4a7484aa, 0x5cb0a9dc, 0x76f988da,
   0x983e5152, 0xa831c66d, 0xb00327c8, 0xbf597fc7, 0xc6e00bf3, 0xd5a79147, 0x06ca6351, 0x14292967,
   0x27b70a85, 0x2e1b2138, 0x4d2c6dfc, 0x53380d13, 0x650a7354, 0x766a0abb, 0x81c2c92e, 0x92722c85,
   0xa2bfe8a1, 0xa81a664b, 0xc24b8b70, 0xc76c51a3, 0xd192e819, 0xd6990624, 0xf40e3585, 0x106aa070,
   0x19a4c116, 0x1e376c08, 0x2748774c, 0x34b0bcb5, 0x391c0cb3, 0x4ed8aa4a, 0x5b9cca4f, 0x682e6ff3,
   0x748f82ee, 0x78a5636f, 0x84c87814, 0x8cc70208, 0x90befffa, 0xa4506ceb, 0xbef9a3f7, 0xc67178f2
        };

        public void calculateRoundsFunction(StringBuilder binaryValue)
        {
            entryA[0] = 1779033703;
            entryB[0] = 3144134277;
            entryC[0] = 1013904242;
            entryD[0] = 2773480762;
            entryE[0] = 1359893119;
            entryF[0] = 2600822924;
            entryG[0] = 528734635;
            entryH[0] = 1541459225;
            for (int m = 0; m < 16; m++)
            {

                string st = binaryValue.ToString(m * 32, 32);

                StringBuilder sb = new StringBuilder();

                sb.Append(st);
                M[m] = BinaryToDecimal(sb);
                hexM[m] = DecimalToHex(BinaryToDecimal(sb));

            }
            for (int i = 0; i < 16; i++)
            {
                W[i] = hexM[i];
                
            }
            for (int i = 16; i < 64; i++)
            {
                s0 = (rightrotate(W[i - 15], 7)^ rightrotate(W[i - 15], 18)^(W[i-15]>>3));
                s1 = (rightrotate(W[i - 2], 17) ^ rightrotate(W[i - 2], 19) ^ (W[i - 2] >> 10));
                W[i] = W[i - 16] + s0 + W[i - 7] + s1;
            }
            for (uint k = 0; k < 64; k++)
            {
                uint temp1,temp2;
                S0[k] = rightrotate(entryA[k], 2) ^ rightrotate(entryA[k], 13) ^ rightrotate(entryA[k], 22);
                Ma[k] = (entryA[k] & entryB[k]) ^ (entryA[k] & entryC[k]) ^ (entryB[k] & entryC[k]);
                temp2 = S0[k] + Ma[k];
                S1[k]=rightrotate(entryE[k],6) ^ rightrotate(entryE[k], 11) ^ rightrotate(entryE[k], 25);
                CH[k] = (entryE[k] & entryF[k]) ^ ((~entryE[k]) & entryG[k]) & entryG[k];
                temp1 = entryH[k] + S1[k] + CH[k] + valueK[k] + W[k];
                
                
                finalH[k] = entryG[k];
                finalG[k] = entryF[k];
                finalF[k] = entryE[k];
                finalE[k] = entryD[k]+temp1;
                finalD[k] = entryC[k];
                finalC[k] = entryB[k];
                finalB[k] = entryA[k];
                finalA[k] = temp1 + temp2;
                if (k < 63)
                {
                    entryA[k + 1] = finalA[k];
                    entryB[k + 1] = finalB[k];
                    entryC[k + 1] = finalC[k];
                    entryD[k + 1] = finalD[k];
                    entryE[k + 1] = finalE[k];
                    entryF[k + 1] = finalF[k];
                    entryG[k + 1] = finalG[k];
                    entryH[k + 1] = finalH[k];
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
            entryValueA.Text = Convert.ToString(DecimalToHex(entryA[c]));
            entryValueB.Text = Convert.ToString(DecimalToHex(entryB[c]));
            entryValueC.Text = Convert.ToString(DecimalToHex(entryC[c]));
            entryValueD.Text = Convert.ToString(entryD[c]);
            entryValueE.Text = Convert.ToString(entryE[c]);
            entryValueF.Text = Convert.ToString(entryF[c]);
            entryValueG.Text = Convert.ToString(entryG[c]);
            entryValueH.Text = Convert.ToString(entryH[c]);
            ChValue.Text = Convert.ToString(CH[c]);
            S1Value.Text = Convert.ToString(S1[c]);
            S0Value.Text = Convert.ToString(S0[c]);
            MaValue.Text = Convert.ToString(Ma[c]);
            WValue.Text = Convert.ToString(W[c]);
            kValue.Text = Convert.ToString(valueK[c / 20]);
            //leftrotate1.Text = Convert.ToString(rotateV1[c]);
            //leftrotate2.Text = Convert.ToString(rotateV2[c]);
            finishValueA.Text = Convert.ToString(finalA[c]);
            finishValueB.Text = Convert.ToString(finalB[c]);
            finishValueC.Text = Convert.ToString(finalC[c]);
            finishValueD.Text = Convert.ToString(finalD[c]);
            finishValueE.Text = Convert.ToString(finalE[c]);
            finishValueF.Text = Convert.ToString(finalF[c]);
            finishValueG.Text = Convert.ToString(finalG[c]);
            finishValueH.Text = Convert.ToString(finalH[c]);


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
            uint A, B, C, D, E, F, G, H;
            string HexResult;
            A = entryA[0] + finalA[63];
            B = entryB[0] + finalB[63];
            C = entryC[0] + finalC[63];
            D = entryD[0] + finalD[63];
            E = entryE[0] + finalE[63];
            F = entryF[0] + finalF[63];
            G = entryG[0] + finalG[63];
            H = entryH[0] + finalH[63];
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
                HexResult += B.ToString("x8");
            }
            if (C > Math.Pow(2, 32))
            {
                finalResult.Text = finalResult.Text + "C=" + Convert.ToString(C - Math.Pow(2, 32)) + Environment.NewLine;
                HexResult += (C - Math.Pow(2, 32)).ToString("x8");
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
            if (F > Math.Pow(2, 32))
            {
                finalResult.Text = finalResult.Text + "F=" + Convert.ToString(F - Math.Pow(2, 32)) + Environment.NewLine;
                HexResult += (F - Math.Pow(2, 32)).ToString("x8");
            }
            else
            {
                finalResult.Text = finalResult.Text + "F=" + Convert.ToString(F) + Environment.NewLine;
                HexResult += F.ToString("x8");
            }
            if (G > Math.Pow(2, 32))
            {
                finalResult.Text = finalResult.Text + "G=" + Convert.ToString(G - Math.Pow(2, 32)) + Environment.NewLine;
                HexResult += (G - Math.Pow(2, 32)).ToString("x8");
            }
            else
            {
                finalResult.Text = finalResult.Text + "G=" + Convert.ToString(G) + Environment.NewLine;
                HexResult += G.ToString("x8");
            }
            if (H > Math.Pow(2, 32))
            {
                finalResult.Text = finalResult.Text + "H=" + Convert.ToString(H - Math.Pow(2, 32)) + Environment.NewLine;
                HexResult += (H - Math.Pow(2, 32)).ToString("x8");
            }
            else
            {
                finalResult.Text = finalResult.Text + "H=" + Convert.ToString(H) + Environment.NewLine;
                HexResult += H.ToString("x8");
            }
            finalHexResult.Text = HexResult;
        }
    }
}
