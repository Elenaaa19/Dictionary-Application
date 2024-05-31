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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace Tema1.Tema1classes
{
    /// <summary>
    /// Interaction logic for SearchWords.xaml
    /// </summary>
    public partial class SearchWords : Window
    {
        public SearchWords()
        {
            InitializeComponent();
        }

        private void Search_Click(object sender, TextChangedEventArgs e)
        {
            // Verific dacă a fost selectată o categorie
            //string selectedCategory = CategoryComboBox.SelectedItem as string;
            string selectedCategory = CategoryComboBox.SelectedItem != null ? CategoryComboBox.SelectedItem.ToString() : null;


            // Obțin textul introdus de utilizator
            string searchText = searchbox.Text.Trim();

            if (string.IsNullOrEmpty(searchText))
            {
                listBox.Items.Clear(); // Curăț lista de sugestii
                return; // Ieș din metoda pentru a nu continua procesarea
            }

            // Rezultatele căutării
            List<string> searchResults = new List<string>();

            try
            {
                string[] lines = File.ReadAllLines("C:\\FACULTATE AN 2\\SEMESTRUL 2\\MVP\\Tema1\\Tema1\\Tema1classes\\cuvinte.txt");

                foreach (string line in lines)
                {
                    string[] values = line.Split(',');
                   
                    if (values.Length >= 3)
                    {
                        
                        // Verific dacă trebuie să căutați în categoria selectată sau în toate categoriile
                        if (selectedCategory != null && selectedCategory != "Toate categoriile")

                        {
                            

                            // Dacă a fost selectată o categorie, caut cuvintele doar în acea categorie
                            if (values[1].Trim() == selectedCategory && values[0].StartsWith(searchText, StringComparison.OrdinalIgnoreCase))
                            {
                                searchResults.Add(line);
                            }
                        }
                        else
                        {
                            // Dacă nu a fost selectată nicio categorie, caut cuvintele în toate categoriile
                            if (values[0].StartsWith(searchText, StringComparison.OrdinalIgnoreCase))
                            {
                                searchResults.Add(line);
                            }
                        }
                    }
                    // Elimin duplicările
                    searchResults = searchResults.Distinct().ToList();

                    // Curăț lista de sugestii înainte de a adăuga noi sugestii
                    listBox.Items.Clear();
                }

                // Afișez rezultatele căutării sau mesajul corespunzător
                if (searchResults.Count > 0)
                {
                    // Afișez rezultatele căutării într-o listă sau în alt control corespunzător
                    foreach (string result in searchResults)
                    {

                        string[] values = result.Split(',');
                        string cuvant = values[0].Trim();
                        string descriere = values[2].Trim();
                        string categorie = values[1].Trim();
                        listBox.Items.Add($"Cuvânt: {cuvant}, Categorie: {categorie},Descriere: {descriere}");



                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("Nu s-au găsit rezultate pentru căutarea specificată.");
                }
               
            }
            catch (IOException ex)
            {
                System.Windows.MessageBox.Show("Eroare la citirea datelor din fișier: " + ex.Message);
            }

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            

        }


        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
