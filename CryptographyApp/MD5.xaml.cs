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
using System.Timers;
using System.Windows.Threading;

namespace CryptographyApp
{
    /// <summary>
    /// Логика взаимодействия для MD5.xaml
    /// </summary>
    public partial class MD5 : Window
    {
        
        public MD5()
        {
            InitializeComponent();
            
        }
        List<uint> hexM=new List<uint>();
        List<uint> M = new List<uint>();
        List<uint> entryA = new List<uint>();
        List<uint> entryB = new List<uint>();
        List<uint> entryC = new List<uint>();
        List<uint> entryD = new List<uint>();
        List<uint> finalA = new List<uint>();
        List<uint> finalB = new List<uint>();
        List<uint> finalC = new List<uint>();
        List<uint> finalD = new List<uint>();
        List<uint> firstV = new List<uint>();
        List<uint> secondV = new List<uint>();
        List<uint> thirdV = new List<uint>();
        List<uint> fourthV = new List<uint>();
        List<uint> rotateV = new List<uint>();
        List<uint> F = new List<uint>();
        int count = 0;
        int g = 0;
        int block = 0;
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
        static int[] shiftN = new int[64] {
            7,12,17,22,7,12,17,22,7,12,17,22,7,12,17,22,
            5,9,14,20,5,9,14,20,5,9,14,20,5,9,14,20,
            4,11,16,23,4,11,16,23,4,11,16,23,4,11,16,23,
            6,10,15,21,6,10,15,21,6,10,15,21,6,10,15,21
        };
        static uint[] valueK = new uint[64] {
            3614090360, 0xe8c7b756, 0x242070db, 0xc1bdceee,
            0xf57c0faf, 0x4787c62a, 0xa8304613, 0xfd469501,
            0x698098d8, 0x8b44f7af, 0xffff5bb1, 0x895cd7be,
            0x6b901122, 0xfd987193, 0xa679438e, 0x49b40821,
            0xf61e2562, 0xc040b340, 0x265e5a51, 0xe9b6c7aa,
            0xd62f105d, 0x02441453, 0xd8a1e681, 0xe7d3fbc8,
            0x21e1cde6, 0xc33707d6, 0xf4d50d87, 0x455a14ed,
            0xa9e3e905, 0xfcefa3f8, 0x676f02d9, 0x8d2a4c8a,
            0xfffa3942, 0x8771f681, 0x6d9d6122, 0xfde5380c,
            0xa4beea44, 0x4bdecfa9, 0xf6bb4b60, 0xbebfbc70,
            0x289b7ec6, 0xeaa127fa, 0xd4ef3085, 0x04881d05,
            0xd9d4d039, 0xe6db99e5, 0x1fa27cf8, 0xc4ac5665,
            0xf4292244, 0x432aff97, 0xab9423a7, 0xfc93a039,
            0x655b59c3, 0x8f0ccc92, 0xffeff47d, 0x85845dd1,
            0x6fa87e4f, 0xfe2ce6e0, 0xa3014314, 0x4e0811a1,
            0xf7537e82, 0xbd3af235, 0x2ad7d2bb, 0xeb86d391
        };
      
        public void calculateRoundsFunction(StringBuilder binaryValue)
        {
            entryA.Add(1732584193);
            entryB.Add( 4023233417);
            entryC.Add( 2562383102);
            entryD.Add(271733878);
            for (int m = 0; m < 16+block/512*16; m++)
            {

                string st = binaryValue.ToString(m * 32, 32);

                StringBuilder sb = new StringBuilder();
                for (int l = 32; l >= 8; l -= 8)
                {
                    sb.Append(st.Substring(l - 8, 8));
                }
                M.Add(BinaryToDecimal(sb));
                hexM.Add( DecimalToHex(BinaryToDecimal(sb)));

            }
            int tmp = 0;
            List<uint> tmpM = new List<uint>();
            for (int k = 0; k < 64+block/512*64 ; k++)
            {
                if (k % 64 == 0)
                {
                    tmpM.Clear();
                    tmpM = M.GetRange(16 * tmp, 16);
                }
                if (k%64 <= 15)
                {
                    F.Add((entryB[k] & entryC[k]) | (~entryB[k] & entryD[k]));
                    g = k%64;

                }
                else if (k%64 >= 16 && k%64 <= 31)
                {
                    F.Add((entryD[k] & entryB[k]) | (~entryD[k] & entryC[k]));
                    g = ((5 * (k%64)) + 1) % 16;

                }
                else if (k%64 >= 32 && k%64 <= 47)
                {
                    F.Add(entryB[k] ^ entryC[k] ^ entryD[k]);
                    g = ((3 *( k%64)) + 5) % 16;

                }
                else if (k%64 >= 48)
                {
                    F.Add(entryC[k] ^ (entryB[k] | ~entryD[k]));
                    g = (7 * (k%64)) % 16;
                }
                firstV.Add(entryA[k] + F[k]);
                secondV.Add(firstV[k] + tmpM[g]);
               
                thirdV.Add(secondV[k] + valueK[k%64]);
                rotateV.Add(leftrotate((thirdV[k]), shiftN[k%64]));
                uint temp = entryD[k];
                finalD.Add(entryC[k]);
                finalC.Add(entryB[k]);
                finalB.Add( entryB[k] + rotateV[k]);
                finalA.Add(temp);
                fourthV.Add(finalB[k]);
                
                if (k< 63+tmp*64)
                {
                    entryA.Add(finalA[k]);
                    entryB.Add(finalB[k]);
                    entryC.Add( finalC[k]);
                    entryD.Add(finalD[k]);
                }
                if (k % (tmp*64+63) == 0&&k!=0)
                {
                    entryA.Add(entryA[tmp*64] + finalA[tmp*64+63]);
                    entryB.Add(entryB[tmp * 64] + finalB[tmp * 64 + 63]);
                    entryC.Add(entryC[tmp * 64] + finalC[tmp * 64 + 63]);
                    entryD.Add(entryD[tmp * 64] + finalD[tmp * 64 + 63]);
                    tmp++;
                    
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
            block = 512 * (b / 448 + 1) - 64;
            binaryBox.Text = binaryValue.ToString();
            lengthBox.Text = b.ToString();
            binaryValue.Append('1');
            oneBox.Text = binaryValue.ToString();
            ZeroLength.Text = Convert.ToString (block);
            while ((b + 1) + i != block&&(b+1+i)<block)
            {
                binaryValue.Append('0');
                i++;
            }
            zeroBox.Text = binaryValue.ToString();
            textLength.Text ="="+ lengthBox.Text+"="+ StringToBinary(lengthBox.Text).ToString();
            //label5.Text = StringToBinary(lengthBox.Text).ToString();
            string varBinLength = Convert.ToString(b, 2).PadLeft(64, '0');
            binaryLength.Text = "L="+varBinLength;
            for (int j = 64; j >= 8; j -= 8)
            {
                bBinary.Append(varBinLength.Substring(j - 8, 8));
            }
            littleEndianBox.Text = bBinary.ToString();
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
            functionValue.Text = Convert.ToString(F[c]);
            firstValue.Text = Convert.ToString(firstV[c]);
            secondValue.Text = Convert.ToString(secondV[c]);
            thirdValue.Text = Convert.ToString(thirdV[c]);
            mValue.Text = Convert.ToString(M[c / 4]);
            kValue.Text = Convert.ToString(valueK[c%64]);
            leftRotate.Text = Convert.ToString(rotateV[c]);
            fourthValue.Text = Convert.ToString(fourthV[c]);
            finishValueA.Text = Convert.ToString(finalA[c]);
            finishValueB.Text = Convert.ToString(finalB[c]);
            finishValueC.Text = Convert.ToString(finalC[c]);
            finishValueD.Text = Convert.ToString(finalD[c]);


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
            if (count >= 63+block/512*64)
            {
                nextButton.IsEnabled = false;
            }
            else
            {
                nextButton.IsEnabled = true;
            }
        }
        static string LittleEndian(string num)
        {
            int number = Convert.ToInt32(num, 16);
            byte[] bytes = BitConverter.GetBytes(number);
            string retval = "";
            foreach(byte b in bytes)
            {
                retval += b.ToString("x2");
            }
            return retval;
        }
        private void finalResultFunction()
        {
            uint A, B, C, D;
            string HexResult;
            A = entryA[entryA.Count - 1];
            B = entryB[entryB.Count - 1];
            C = entryC[entryC.Count - 1];
            D = entryD[entryD.Count - 1];
            if (A > Math.Pow(2, 32))
            {
                finalResult.Text="A="+Convert.ToString(A - Math.Pow(2, 32))+Environment.NewLine;
                HexResult = LittleEndian((A - Math.Pow(2, 32)).ToString("x8"));
            }
            else
            {
                finalResult.Text = "A="+ Convert.ToString(A) + Environment.NewLine;
                HexResult =LittleEndian( A.ToString("x8"));
            }
            if (B > Math.Pow(2, 32))
            {
                finalResult.Text = finalResult.Text+"B="+ Convert.ToString(B - Math.Pow(2, 32)) + Environment.NewLine;
                HexResult+= LittleEndian((B - Math.Pow(2, 32)).ToString("x8"));
            }
            else
            {
                finalResult.Text = finalResult.Text+ "B=" + Convert.ToString(B) + Environment.NewLine;
                HexResult+= LittleEndian( B.ToString("x8"));
            }
            if (C > Math.Pow(2, 32))
            {
                finalResult.Text = finalResult.Text+ "C=" + Convert.ToString(C - Math.Pow(2, 32)) + Environment.NewLine;
                HexResult+=LittleEndian( (C - Math.Pow(2, 32)).ToString("x8"));
            }
            else
            {
                finalResult.Text = finalResult.Text+ "C=" + Convert.ToString(C) + Environment.NewLine;
                HexResult+=LittleEndian( C.ToString("x8"));
            }
            if (D > Math.Pow(2, 32))
            {
                finalResult.Text = finalResult.Text+ "D=" + Convert.ToString(D - Math.Pow(2, 32)) + Environment.NewLine;
                HexResult+=LittleEndian( (D - Math.Pow(2, 32)).ToString("x8"));
            }
            else
            {
                finalResult.Text = finalResult.Text+ "D=" + Convert.ToString(D) + Environment.NewLine;
                HexResult+=LittleEndian( D.ToString("x8"));
            }
            finalHexResult.Text = HexResult;
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
            if (count == 16+block / 512 * 16)
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
    }
}
