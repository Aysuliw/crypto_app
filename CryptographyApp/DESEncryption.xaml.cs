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

namespace CryptographyApp
{
    /// <summary>
    /// Логика взаимодействия для DESEncryption.xaml
    /// </summary>
    public partial class DESEncryption : UserControl
    {
        StringBuilder binaryKey = new StringBuilder();
        StringBuilder binKeyPermutation = new StringBuilder();
        string[] firstHalf = new string[17];
        string[] secondHalf = new string[17];
        string[] KeyGener = new string[16];
        StringBuilder[] K = new StringBuilder[16];
        StringBuilder[] R = new StringBuilder[17];
        StringBuilder[] L = new StringBuilder[17];
        StringBuilder[] RExpand = new StringBuilder[16];
        StringBuilder[] RplusK = new StringBuilder[16];
        string[] str = new string[128];
        string[] ins = new string[128];
        string[] outs = new string[128];
        uint[] a1 = new uint[128];
        uint[] a2 = new uint[128];
        int[] q = new int[128];
        string[] qBinary = new string[128];
        string[] qFullBinary = new string[16];
        StringBuilder[] qPermutationBinary = new StringBuilder[16];
        int count = 0;
        string pText, givenKey;
        public DESEncryption(string s, string t)
        {
            InitializeComponent();
            pText = s;
            givenKey = t;
            encryptFunction();
        }
        private void keyGenerationButton_Click(object sender, RoutedEventArgs e)
        {
            DESKeyGeneration viewWindow = new DESKeyGeneration(binaryKey, binKeyPermutation, firstHalf, secondHalf, KeyGener, K);
            viewWindow.Show();
        }
        static int[] KeyPermutation = new int[56]
        {
            57,  49,  41,  33,  25,  17,  9,   1,   58,  50,  42,  34,  26,  18,
            10,  2,   59,  51,  43,  35,  27,  19,  11,  3,   60,  52,  44,  36,
            63,  55,  47,  39,  31,  23,  15,  7,   62,  54,  46,  38,  30,  22,
            14,  6,   61,  53,  45,  37,  29,  21,  13,  5,   28,  20,  12,  4
        };
        static int[] LastKP = new int[48]
        {
            14,  17,  11,  24,  1,   5,
            3,   28,  15,  6,   21,  10,
            23,  19,  12,  4,   26,  8,
            16,  7,   27,  20,  13,  2,
            41,  52,  31,  37,  47,  55,
            30,  40,  51,  45,  33,  48,
            44,  49,  39,  56,  34,  53,
            46,  42,  50,  36,  29,  32
        };
        static int[] InitalPermutationBox = new int[64]
        {
            58,  50,  42,  34,  26,  18,  10,  2,
            60,  52,  44,  36,  28,  20,  12,  4,
            62,  54,  46,  38,  30,  22,  14,  6,
            64,  56,  48,  40,  32,  24,  16,  8,
            57,  49,  41,  33,  25,  17,  9,   1,
            59,  51,  43,  35,  27,  19,  11,  3,
            61,  53,  45,  37,  29,  21,  13,  5,
            63,  55,  47,  39,  31,  23,  15,  7
        };

        static int[] FinalPermutationBox = new int[64]
        {
            40,  8,   48,  16,  56,  24,  64,  32,  39,  7,   47,  15,  55,  23,  63,  31,
            38,  6,   46,  14,  54,  22,  62,  30,  37,  5,   45,  13,  53,  21,  61,  29,
            36,  4,   44,  12,  52,  20,  60,  28,  35,  3,   43,  11,  51,  19,  59,  27,
            34,  2,   42,  10,  50,  18,  58,  26,  33,  1,   41,  9,   49,  17,  57,  25
        };
        static int[] ExpandBox = new int[48]
        {
            32,  1,   2,   3,   4,   5,
            4,   5,   6,   7,   8,   9,
            8,   9,   10,  11,  12,  13,
            12,  13,  14,  15,  16,  17,
            16,  17,  18,  19,  20,  21,
            20,  21,  22,  23,  24,  25,
            24,  25,  26,  27,  28,  29,
            28,  29,  30,  31,  32,  1
        };

        static int[,] SBox1 = new int[4, 16]
        {
            {14,4,13,1,2,15,11,8,3,10,6,12,5,9,0,7 },
            {0,15,7,4,14,2,13,1,10,6,12,11,9,5,3,8 },
            {4,1,14,8,13,6,2,11,15,12,9,7,13,10,5,0 },
            {15,12,8,2,4,9,1,7,5,11,3,14,10,0,6,13 }
        };
        static int[,] SBox2 = new int[4, 16]
        {
            {15,1,8,14,6,11,3,4,9,7,2,13,12,0,5,10 },
            {3,13,4,7,15,2,8,14,12,0,1,10,6,9,11,5 },
            {0,14,7,11,10,4,13,1,5,8,12,6,9,3,2,15 },
            {13,8,10,1,3,15,4,2,11,6,7,12,0,5,14,9 }
        };
        static int[,] SBox3 = new int[4, 16]
        {
            { 10, 0, 9, 14, 6, 3, 15, 5, 1, 13, 12, 7, 11, 4, 2, 8 },
            { 13, 7, 0, 9, 3, 4, 6, 10, 2, 8, 5, 14, 12, 11, 15, 1 },
            { 13, 6, 4, 9, 8, 15, 3, 0, 11, 1, 2, 12, 5, 10, 14, 7 },
            { 1, 10, 13, 0, 6, 9, 8, 7, 4, 15, 14, 3, 11, 5, 2, 12 }
        };
        static int[,] SBox4 = new int[4, 16]
        {
            { 7, 13, 14, 3, 0, 6, 9, 10, 1, 2, 8, 5, 11, 12, 4, 15 },
            { 13, 8, 11, 5, 6, 15, 0, 3, 4, 7, 2, 12, 1, 10, 14, 9 },
            { 10, 6, 9, 0, 12, 11, 7, 13, 15, 1, 3, 14, 5, 2, 8, 4 },
            { 3, 15, 0, 6, 10, 1, 13, 8, 9, 4, 5, 11, 12, 7, 2, 14 }
        };
        static int[,] SBox5 = new int[4, 16]
        {
            { 2, 12, 4, 1, 7, 10, 11, 6, 8, 5, 3, 15, 13, 0, 14, 9 },
            { 14, 11, 2, 12, 4, 7, 13, 1, 5, 0, 15, 10, 3, 9, 8, 6 },
            { 4, 2, 1, 11, 10, 13, 7, 8, 15, 9, 12, 5, 6, 3, 0, 14 },
            { 11, 8, 12, 7, 1, 14, 2, 13, 6, 15, 0, 9, 10, 4, 5, 3 }
        };
        static int[,] SBox6 = new int[4, 16]
        {
            { 12, 1, 10, 15, 9, 2, 6, 8, 0, 13, 3, 4, 14, 7, 5, 11 },
            { 10, 15, 4, 2, 7, 12, 9, 5, 6, 1, 13, 14, 0, 11, 3, 8 },
            { 9, 14, 15, 5, 2, 8, 12, 3, 7, 0, 4, 10, 1, 13, 11, 6 },
            { 4, 3, 2, 12, 9, 5, 15, 10, 11, 14, 1, 7, 6, 0, 8, 13 }
        };
        static int[,] SBox7 = new int[4, 16]
        {
            { 4, 11, 2, 14, 15, 0, 8, 13, 3, 12, 9, 7, 5, 10, 6, 1 },
            { 13, 0, 11, 7, 4, 9, 1, 10, 14, 3, 5, 12, 2, 15, 8, 6 },
            { 1, 4, 11, 13, 12, 3, 7, 14, 10, 15, 6, 8, 0, 5, 9, 2 },
            { 6, 11, 13, 8, 1, 4, 10, 7, 9, 5, 0, 15, 14, 2, 3, 12 }
        };
        static int[,] SBox8 = new int[4, 16]
        {
            { 13, 2, 8, 4, 6, 15, 11, 1, 10, 9, 3, 14, 5, 0, 12, 7 },
            { 1, 15, 13, 8, 10, 3, 7, 4, 12, 5, 6, 11, 0, 14, 9, 2 },
            { 7, 11, 4, 1, 9, 12, 14, 2, 0, 6, 10, 13, 15, 3, 5, 8 },
            { 2, 1, 14, 7, 4, 10, 8, 13, 15, 12, 9, 0, 3, 5, 6, 11 }
        };
        static int[] pBox = new int[32]
        {
            16,  7,   20,  21,  29,  12,  28,  17,
            1,   15,  23,  26,  5,   18,  31,  10,
            2,   8,   24,  14,  32,  27,  3,   9,
            19,  13,  30,  6,   22,  11,  4,   25
        };
        static int[] LeftShift = new int[16]
        {
             1  ,1,   2,   2,   2,   2,   2,   2,   1,   2,   2,   2,   2,   2,   2,   1
        };
        public StringBuilder KeyPBox(StringBuilder data)
        {
            StringBuilder returnValue = new StringBuilder();
            for (int i = 0; i < 56; i++)
            {
                returnValue.Append(data[KeyPermutation[i] - 1]);

            }
            return returnValue;


        }
        public StringBuilder InitialPermutation(StringBuilder data)
        {
            StringBuilder returnValue = new StringBuilder();
            for (int i = 0; i < 64; i++)
            {
                returnValue.Append(data[InitalPermutationBox[i] - 1]);
            }
            return returnValue;

        }
        public StringBuilder FinalPermutation(string data)
        {
            StringBuilder returnValue = new StringBuilder();
            for (int i = 0; i < 64; i++)
            {
                returnValue.Append(data[FinalPermutationBox[i] - 1]);
            }
            return returnValue;

        }
        public StringBuilder LastKeyPBox(string data)
        {
            StringBuilder returnValue = new StringBuilder();
            for (int i = 0; i < 48; i++)
            {
                returnValue.Append(data[LastKP[i] - 1]);

            }
            return returnValue;

        }
        public StringBuilder MiddleRoundBox(string data)
        {
            StringBuilder returnValue = new StringBuilder();
            for (int i = 0; i < 32; i++)
            {
                returnValue.Append(data[pBox[i] - 1]);

            }
            return returnValue;

        }
        public StringBuilder ExpandPermutation(StringBuilder data)
        {
            StringBuilder returnValue = new StringBuilder();
            for (int i = 0; i < 48; i++)
            {
                returnValue.Append(data[ExpandBox[i] - 1]);

            }
            return returnValue;

        }
        public static string leftrotate(string x, int n)
        {
            string s = "";
            for (int i = 0; i < n; i++)
            {
                s = s + x[i];
            }
            x = x.Remove(0, n);

            return x + s;
        }
        public static ulong BinaryToDecimal(StringBuilder data)
        {
            string s = data.ToString();
            ulong resultDecimal = Convert.ToUInt64(s, 2);
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
        public StringBuilder StringToBinary(string s)
        {
            StringBuilder returnValue = new StringBuilder();
            foreach (char c in s.ToCharArray())
            {
                returnValue.Append(Convert.ToString(c, 2).PadLeft(8, '0'));
            }
            return returnValue;
        }
        public void Rounds(StringBuilder data, StringBuilder K, int n)
        {
            StringBuilder L0 = new StringBuilder();
            StringBuilder R0 = new StringBuilder();
            StringBuilder p = new StringBuilder(Convert.ToString(data));
            if (n == 0)
            {
                for (int i = 0; i < 64; i++)
                {
                    if (i < 32)
                    {
                        L0.Append(p[i]);
                    }
                    else
                    {
                        R0.Append(p[i]);
                    }
                }
                L[n] = L0;
                R[n] = R0;
            }

            RExpand[n] = ExpandPermutation(R[n]);
            ulong t = BinaryToDecimal(RExpand[n]) ^ BinaryToDecimal(K);
            StringBuilder temp = new StringBuilder();
            temp.Append(Convert.ToString((long)t, 2).PadLeft(48, '0'));
            RplusK[n] = temp;

            for (int i = 0; i < 8; i++)
            {
                int k = i + 8 * n;
                str[k] = Convert.ToString(RplusK[n]).Substring(i * 6, 6);

                ins[k] = str[k].Substring(0, 1) + str[k].Substring(5, 1);
                outs[k] = str[k].Substring(1, 4);

                a1[k] = Convert.ToUInt32(ins[k], 2);
                a2[k] = Convert.ToUInt32(outs[k], 2);

                switch (i)
                {
                    case 0:
                        q[k] = SBox1[a1[k], a2[k]];
                        break;
                    case 1:
                        q[k] = SBox2[a1[k], a2[k]];
                        break;
                    case 2:
                        q[k] = SBox3[a1[k], a2[k]];
                        break;
                    case 3:
                        q[k] = SBox4[a1[k], a2[k]];
                        break;
                    case 4:
                        q[k] = SBox5[a1[k], a2[k]];
                        break;
                    case 5:
                        q[k] = SBox6[a1[k], a2[k]];
                        break;
                    case 6:
                        q[k] = SBox7[a1[k], a2[k]];
                        break;
                    case 7:
                        q[k] = SBox8[a1[k], a2[k]];
                        break;
                }
                qBinary[k] = Convert.ToString(q[k], 2).PadLeft(4, '0');

            }
            string s = "";
            for (int j = 0; j < 8; j++)
            {
                s += qBinary[j + 8 * n];
            }
            qFullBinary[n] = s;
            qPermutationBinary[n] = MiddleRoundBox(qFullBinary[n]);
            ulong r = BinaryToDecimal(L[n]) ^ BinaryToDecimal(qPermutationBinary[n]);
            StringBuilder temp1 = new StringBuilder();
            temp1.Append(Convert.ToString((long)r, 2).PadLeft(32, '0'));
            R[n + 1] = temp1;
            L[n + 1] = R[n];



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
            if (count >= 63)
            {
                nextButton.IsEnabled = false;
            }
            else
            {
                nextButton.IsEnabled = true;
            }
        }
        public void textBoxWrite(int c)
        {
            roundBlock.Text = Convert.ToString(c + 1) + "-raund";
            firstL.Text = Convert.ToString(L[c]);
            secondL.Text = Convert.ToString(L[c + 1]);
            firstR.Text = Convert.ToString(R[c]);
            secondR.Text = Convert.ToString(R[c + 1]);
            expandValue.Text = Convert.ToString(RExpand[c]);
            keyValue.Text = Convert.ToString(K[c]);
            RplusKValue.Text = Convert.ToString(RplusK[c]);
            afterSbox.Text = Convert.ToString(qFullBinary[c]);
            afterpbox.Text = Convert.ToString(qPermutationBinary[c]);

        }
        public void finalResultFunction()
        {
            string result;
            string finalResult;
            result = Convert.ToString(R[16]) + Convert.ToString(L[16]);
            finalResult = Convert.ToString(FinalPermutation(result));
            resultBlock.Text = Convert.ToUInt64(finalResult, 2).ToString("X4");

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
            if (count == 6)
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
        private void encryptFunction()
        {
            StringBuilder binaryText = new StringBuilder();
            binaryKey = StringToBinary(givenKey);
            binaryText = StringToBinary(pText);
            int p = 0;
            while (binaryText.Length != 64)
            {
                binaryText.Append('0');
                p++;
            }
            binaryBox.Text = Convert.ToString(binaryText);
            binaryKeyBox.Text = Convert.ToString(binaryKey);
            binaryText = InitialPermutation(binaryText);
            binKeyPermutation = KeyPBox(binaryKey);
            initialPerBox.Text = Convert.ToString(binaryText);
            StringBuilder firstHalfZ = new StringBuilder();
            StringBuilder secondHalfZ = new StringBuilder();

            for (int i = 0; i < 56; i++)
            {
                if (i < 28)
                {
                    firstHalfZ.Append(binKeyPermutation[i]);
                }
                else
                {
                    secondHalfZ.Append(binKeyPermutation[i]);
                }
            }

            firstHalf[0] = firstHalfZ.ToString();
            secondHalf[0] = secondHalfZ.ToString();

            for (int i = 1; i < 17; i++)
            {
                firstHalf[i] = leftrotate(firstHalf[i - 1], LeftShift[i - 1]);
                secondHalf[i] = leftrotate(secondHalf[i - 1], LeftShift[i - 1]);

                KeyGener[i - 1] = firstHalf[i] + secondHalf[i];
            }
            for (int i = 0; i < 16; i++)
            {
                K[i] = LastKeyPBox(KeyGener[i]);
            }

            for (int i = 0; i < 16; i++)
            {
                Rounds(binaryText, K[i], i);

            }

            finalResultFunction();
            for (int i = 0; i < 16; i++)
            {
                keysBlock.Text += K[i] + Environment.NewLine;
            }
        }
    }
}
