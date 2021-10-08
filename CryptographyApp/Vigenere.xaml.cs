using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace CryptographyApp
{
    /// <summary>
    /// Логика взаимодействия для Vigenere.xaml
    /// </summary>
    public partial class Vigenere : Window
    {
        List<int[]> indexes = new List<int[]>();
        char[] Alp = { 'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'};
        int count = 0;
        string inputText, keyText;
        public Vigenere()
        {
            InitializeComponent();
            generateDataGrid();
            

            
        }

        public class DataItem
        {
            public string Column1 { get; set; }
            public string Column2 { get; set; }
            public string Column3 { get; set; }
            public string Column4 { get; set; }
            public string Column5 { get; set; }
            public string Column6 { get; set; }
            public string Column7 { get; set; }
            public string Column8 { get; set; }
            public string Column9 { get; set; }
            public string Column10 { get; set; }
            public string Column11 { get; set; }
            public string Column12 { get; set; }
            public string Column13 { get; set; }
            public string Column14 { get; set; }
            public string Column15 { get; set; }
            public string Column16 { get; set; }
            public string Column17 { get; set; }
            public string Column18 { get; set; }
            public string Column19 { get; set; }
            public string Column20 { get; set; }
            public string Column21 { get; set; }
            public string Column22 { get; set; }
            public string Column23 { get; set; }
            public string Column24 { get; set; }
            public string Column25 { get; set; }
            public string Column26 { get; set; }

        }
        public class MyObject
        {
            public string Row { get; set; }
            
        }
        public void generateDataGrid()
        {
            for (int i = 1; i <= 26; ++i)
            {
                var column = new DataGridTextColumn();
                var row = new DataGridRow();
                
                column.Header = Alp[i-1];
                column.Binding = new Binding("Column" + i);
                
                row.Header = Alp[i - 1];
                datagrid1.Columns.Add(column);
                
            }
            
            for (int i = 0; i < 26; i++)
            {
                int j =-1;
                
                datagrid1.Items.Add(new DataItem { Column1 = Alp[(i+(++j))%26].ToString(), Column2 = Alp[(i + (++j)) % 26].ToString(), 
                        Column3 = Alp[(i + (++j)) % 26].ToString(), Column4 = Alp[(i + (++j)) % 26].ToString(), Column5 = Alp[(i + (++j)) % 26].ToString(), Column6 = Alp[(i + (++j)) % 26].ToString(), 
                        Column7 = Alp[(i + (++j)) % 26].ToString(), Column8 = Alp[(i + (++j)) % 26].ToString(), Column9 = Alp[(i + (++j)) % 26].ToString(), Column10 = Alp[(i + (++j)) % 26].ToString(), 
                        Column11 = Alp[(i + (++j)) % 26].ToString(), Column12 = Alp[(i + (++j)) % 26].ToString(), Column13 = Alp[(i + (++j)) % 26].ToString(), Column14 = Alp[(i + (++j)) % 26].ToString(), 
                        Column15 = Alp[(i + (++j)) % 26].ToString(), Column16 = Alp[(i + (++j)) % 26].ToString(), Column17 = Alp[(i + (++j)) % 26].ToString(), Column18 = Alp[(i + (++j)) % 26].ToString(), 
                        Column19 = Alp[(i + (++j)) % 26].ToString(), Column20 = Alp[(i + (++j)) % 26].ToString(), Column21 = Alp[(i + (++j)) % 26].ToString(), Column22 = Alp[(i + (++j)) % 26].ToString(), 
                        Column23 = Alp[(i + (++j)) % 26].ToString(), Column24 = Alp[(i  + (++j)) % 26].ToString(), Column25 = Alp[(i + (++j)) % 26].ToString(), Column26 = Alp[(i + (++j)) % 26].ToString()
                });
                
            }

        }
        public string encryptFunction(string s, string key)
        {
            StringBuilder str = new StringBuilder();
            int j = 0;
            s = s.ToUpper();
            key = key.ToUpper();
            char c=' ';
            indexes.Clear();
            for (int i = 0; i < s.Length; i++)
            {
                if (j == key.Length) { j = 0; }
                if (char.IsLetter(s[i]))
                {
                    int[] ind = new int[2];
                    for (int k = 0; k < 26; k++)
                    {
                        if (s[i] == Alp[k])
                        {
                            ind[0] = k;
                        }
                        if (key[j] == Alp[k])
                        {
                            ind[1] = k;
                        }
                    }
                    indexes.Add(ind);
                    c= (char)((s[i] + key[j]) % 26 + 65);
                }

                str.Append(c);
                j++;

            }
            return str.ToString();
        }

        public string decryptFunction(string s, string key)
        {
            StringBuilder str=new StringBuilder();
            int j = 0;
            indexes.Clear();
            s = s.ToUpper();
            key = key.ToUpper();
            char c = ' ';
            for (int i = 0; i < s.Length; i++)
            {
                if (j == key.Length) { j = 0; }
                if (char.IsLetter(s[i]))
                {
                    int[] ind = new int[2];
                    for (int k = 0; k < 26; k++)
                    {
                        if (s[i] == Alp[k])
                        {
                            ind[0] = k;
                        }
                        if (key[j] == Alp[k])
                        {
                            ind[1] = k;
                        }
                    }
                    indexes.Add(ind);
                    c = (char)((s[i] - key[j]+26) % 26 + 65);
                }
                str.Append(c);
                j++;

            }
            return str.ToString();
        }
        public DataGridCell GetCell(int rowIndex, int columnIndex, DataGrid dg)
        {
            DataGridRow row = dg.ItemContainerGenerator.ContainerFromIndex(rowIndex) as DataGridRow;
            DataGridCellsPresenter p = GetVisualChild<DataGridCellsPresenter>(row);
            DataGridCell cell = p.ItemContainerGenerator.ContainerFromIndex(columnIndex) as DataGridCell;
            return cell;
        }

        static T GetVisualChild<T>(Visual parent) where T : Visual
        {
            T child = default(T);
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }
       public void colorRCDecryption(byte r, byte g, byte b)
        {
            for (int i = 0; i < inputText.Length; i++)
            {
                
                int[] indx = indexes[i];
                int  tmp=((26 - indx[1]) +indx[0]) % 26;
                for (int k = 0; k < indx[1]; k++)
                {
                    DataGridCell cell1 = GetCell(k, tmp, datagrid1);

                    cell1.Background = new SolidColorBrush(Color.FromRgb(r, g, b));
                }
                for (int k = 0; k < tmp; k++)
                {
                    DataGridCell cell1 = GetCell(indx[1], k, datagrid1);

                    cell1.Background = new SolidColorBrush(Color.FromRgb(r, g, b));
                }

                count++;

            }
        }
        public void colorRC(byte r,byte g, byte b)
        {
            for (int i = 0; i < inputText.Length; i++)
            {
                int[] indx = indexes[i];
               
                for(int k=0; k < indx[0]; k++)
                {
                    DataGridCell cell1 = GetCell(k, indx[1], datagrid1);
                    
                    cell1.Background = new SolidColorBrush(Color.FromRgb(r, g, b));    
                }
                for (int k = 0; k < indx[1]; k++)
                {
                    DataGridCell cell1 = GetCell(indx[0], k, datagrid1);

                    cell1.Background = new SolidColorBrush(Color.FromRgb(r, g, b));
                }
                count++;
            }
        }
        public void colorCell(byte r,byte g, byte b)
        {
            
            for (int i = 0; i < inputText.Length; i++)
            {
                int[] indx = indexes[i];
                
                    DataGridCell cell = GetCell(indx[0], indx[1], datagrid1);
                
                cell.Background = new SolidColorBrush(Color.FromRgb(r,g,b));
                count++;
            }
        }
        public void colorCellDecryption(byte r, byte g, byte b)
        {
            for (int i = 0; i < inputText.Length; i++)
            {
                int[] indx = indexes[i];
                int tmp1= ((26 - indx[1]) + indx[0]) % 26;
                DataGridCell cell = GetCell(indx[1], tmp1, datagrid1);

                cell.Background = new SolidColorBrush(Color.FromRgb(r, g, b));
                count++;
            }
        }
        public void clearCell()
        {
            for(int i = 0; i < 26; i++)
            {
                for(int j = 0; j < 26; j++)
                {
                    

                    DataGridCell cell = GetCell(i, j, datagrid1);

                    cell.Background = new SolidColorBrush(Color.FromRgb(0, 255, 255));
                }
            }
        }
        private void encryptButton_Click(object sender, RoutedEventArgs e)
        {
            LightGreenBlock.Text = " - Ashiq tekst";
            CornflowerBlueBlock.Text = " - Gilt";
            OrangeBlock.Text = " - Shifr tekst";

            if (count > 0)
            {
                clearCell();
                
            }
            inputText =inputBox.Text;
            keyText = keyBox.Text;
            result.Text = encryptFunction(inputText, keyText);
            colorRC(255,0,0);
            colorCell(255,165,0);
        }

        private void decryptButton_Click(object sender, RoutedEventArgs e)
        {
            LightGreenBlock.Text = " - Gilt";
            CornflowerBlueBlock.Text = " - Ashiq tekst";
            OrangeBlock.Text = " - Shifr tekst";
            if (count > 0)
            {
                clearCell();

            }
            inputText = inputBox.Text;
            keyText = keyBox.Text;
            result.Text = decryptFunction(inputText, keyText);
            
            colorRCDecryption(255,0,0);
            colorCellDecryption(255, 165, 0);

        }

        private void datagrid1_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            var id = e.Row.GetIndex();


            switch (id)
            {
                case 0:
                    {
                        e.Row.Header = "A";
                        break;
                    }
                case 1:
                    {
                        e.Row.Header = "B";
                        break;
                    }
                case 2:
                    {
                        e.Row.Header = "C";
                        break;
                    }
                case 3:
                    {
                        e.Row.Header = "D";
                        break;
                    }
                case 4:
                    {
                        e.Row.Header = "E";
                        break;
                    }
                case 5:
                    {
                        e.Row.Header = "F";
                        break;
                    }
                case 6:
                    {
                        e.Row.Header = "G";
                        break;
                    }
                case 7:
                    {
                        e.Row.Header = "H";
                        break;
                    }
                case 8:
                    {
                        e.Row.Header = "I";
                        break;
                    }
                case 9:
                    {
                        e.Row.Header = "J";
                        break;
                    }
                case 10:
                    {
                        e.Row.Header = "K";
                        break;
                    }
                case 11:
                    {
                        e.Row.Header = "L";
                        break;
                    }
                case 12:
                    {
                        e.Row.Header = "M";
                        break;
                    }
                case 13:
                    {
                        e.Row.Header = "N";
                        break;
                    }
                case 14:
                    {
                        e.Row.Header = "O";
                        break;
                    }
                case 15:
                    {
                        e.Row.Header = "P";
                        break;
                    }
                case 16:
                    {
                        e.Row.Header = "Q";
                        break;
                    }
                case 17:
                    {
                        e.Row.Header = "R";
                        break;
                    }
                case 18:
                    {
                        e.Row.Header = "S";
                        break;
                    }
                case 19:
                    {
                        e.Row.Header = "T";
                        break;
                    }
                case 20:
                    {
                        e.Row.Header = "U";
                        break;
                    }
                case 21:
                    {
                        e.Row.Header = "V";
                        break;
                    }
                case 22:
                    {
                        e.Row.Header = "W";
                        break;
                    }
                case 23:
                    {
                        e.Row.Header = "X";
                        break;
                    }
                case 24:
                    {
                        e.Row.Header = "Y";
                        break;
                    }
                case 25:
                    {
                        e.Row.Header = "Z";
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
    }
}
