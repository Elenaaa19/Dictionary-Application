using System;
using System.Collections.Generic;
using System.IO;
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
using Tema1.Tema1classes;

namespace Tema1
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

        private void b1_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow aWind = new AdminWindow();
            aWind.ShowDialog();
        }

        private void b2_Click(object sender, RoutedEventArgs e)
        {
            SearchWords search=new SearchWords();

            // Citirea categoriilor din fișier
            List<string> categorii = new List<string>();
            try
            {
                string[] lines = File.ReadAllLines("C:\\FACULTATE AN 2\\SEMESTRUL 2\\MVP\\Tema1\\Tema1\\Tema1classes\\cuvinte.txt");
                foreach (string line in lines)
                {
                    string[] values = line.Split(',');
                    if (values.Length >= 3) // Verificăm dacă există cel puțin 3 valori în linie
                    {
                        categorii.Add(values[1].Trim()); // Adăugăm categoria de pe poziția 2 (indexat de la 0)
                    }
                }
            }
            catch (IOException ex)
                {
                    MessageBox.Show("Eroare la citirea categoriilor din fișier: " + ex.Message);
                }

                // Adăugarea categoriilor în ComboBox
                search.CategoryComboBox.ItemsSource = categorii;

                search.ShowDialog();
            


        }

        private void DIVERTISMENT_Click(object sender, RoutedEventArgs e)
        {
            DivertismentWindow dWind = new DivertismentWindow();
            dWind.ShowDialog();
        }
    }
}
