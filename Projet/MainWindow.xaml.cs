using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Projet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        Database database = new Database();

        private void Panel_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        

        private void Panel_Initialized(object sender, EventArgs e)
        {
            for (int i = 1; i <= 5; i++)
            {
                for (int r = 1; r <= 5; r++)
                {

                    ToggleButton button = new ToggleButton();


                    button.Width = 35;
                    button.Height = 35;
                    button.BorderThickness = new Thickness(10);

                    Panel.RegisterName("Button" + i+r, button);
                    Panel.Children.Add(button);
                    button.Click += ToggleButton_Click;
                }
            }
        }

        private void Letters_Initialized(object sender, EventArgs e)
        {
            for (char character = 'A'; character <= 'Z'; character++)
                Letters.Items.Add(character);
            
        }

        private void ClearButtons()
        {
            for (int i = 1; i <= 5; i++)
                for (int r = 1; r <= 5; r++)
                    (FindName("Button" + i + r) as ToggleButton).IsChecked = false;
        }

        private void Letters_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClearButtons();
            Letters.IsEnabled = false;
            Letter.Content = Letters.SelectedItem.ToString();
            Count();
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ClearButtons();
            Count();
        }

        private void Reversal_Click(object sender, RoutedEventArgs e)
        {
            for(int i=1; i<=5;i++)
                for(int r=1;r<=5;r++)
                (FindName("Button" + i+r) as ToggleButton).IsChecked = !(FindName("Button" + i+r) as ToggleButton).IsChecked;
            Count();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            for (int i = 1; i <= 5; i++)
                for(int r=1;r<=5;r++)
                (FindName("Button" + i+r) as ToggleButton).IsChecked = true;
            Count();
        }

        private void Frame_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 1; i <= 5; i++)
            {
                if (i == 1 || i == 5)
                {
                    for (int r = 1; r <= 5; r++)

                        (FindName("Button" + i + r) as ToggleButton).IsChecked = true;
                }
                else
                {
                    (FindName("Button" + i + 1) as ToggleButton).IsChecked = true;
                    (FindName("Button" + i + 5) as ToggleButton).IsChecked = true;
                }
                Count();

            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            database.MakeAnArray(slowo.Text.ToString().ToUpper());
            string tekst = database.CreateText().ToString();

            using (StreamWriter writer = File.CreateText(@"napis.txt"))
            {
                
                    writer.WriteLine("{0}\t", tekst);

            }
        }

        private void SaveLetter_Click(object sender, RoutedEventArgs e)
        {
            Letters.IsEnabled= true;

            if (Letters.SelectedIndex >-1)
            {
                bool[,] zn = new bool[5, 5];
                for (int i = 0; i < 5; i++)
                {
                    for (int r = 0; r < 5; r++)
                    {

                        zn[i, r] = (FindName("Button" + (i + 1) + (r + 1)) as ToggleButton).IsChecked.Value ? true : false;
                    }
                }

                database.Add(Letters.SelectedItem.ToString(), zn);
            }
        }

        private void Panel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void Stats_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            

        }

        private void Count()
        {
            int count = 0;
            for (int i = 1; i <= 5; i++)
                for (int r = 1; r <= 5; r++)
                    if ((FindName("Button"+i+r) as ToggleButton).IsChecked.Value) count++;

            Stats.Content = count+"/25";

        }
        private void ToggleButton_Click(object sender, EventArgs e)
        {
            
            
            Count();
            
        }

        public bool[,] GenerateEmptyFont()
        {
            bool[,] tmp = new bool[5,5];
            for(int i=0;i<5;i++)
            {
                for(int x=0;x<5;x++)
                    tmp[i,x]=false;
            }
            return tmp;
        }

        private bool CheckIfFilled(List<CharClass> worklist,char znak)
        {
            foreach (CharClass work in worklist)
            {
                if (work.character == znak)

                    return true;

            }
            return false;

        }

        public void FillEmptyFonts()
        {int count=0;
        List<CharClass> WorkList = new List<CharClass>();
            foreach (CharClass element in database.Charlist)
            {
                foreach (char znak in Letters.Items)
                {
                    if (element.character != znak)
                    {
                        if (!CheckIfFilled(WorkList, znak))
                        {
                            WorkList.Add(new CharClass() { character = znak, TabCharacter = GenerateEmptyFont() }); count++;
                        }

                    }
                }
            }
            MessageBox.Show(count.ToString());
            database.Add(WorkList);
        }
        
        private void ExportFonts_Click(object sender, RoutedEventArgs e)
        {
            FillEmptyFonts();
            string text = null ;
            foreach (char letter in Letters.Items)
                text += letter.ToString();
            database.MakeAnArray(text);

            string fonts = database.CreateText().ToString();

            using (StreamWriter writer = File.CreateText(@"czcionki.txt"))
            {
                
                    writer.WriteLine("{0}\t", fonts);

            }

        }
                    
    }
}
