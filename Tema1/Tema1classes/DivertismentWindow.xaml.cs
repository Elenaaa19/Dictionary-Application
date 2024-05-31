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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Tema1.Tema1classes
{
    /// <summary>
    /// Interaction logic for DivertismentWindow.xaml
    /// </summary>
    public partial class DivertismentWindow : Window
    {
        private Random random = new Random();
        private List<CUVANT> cuvinteCuImagine;
        private List<CUVANT> cuvinte;
        private int numarCuvinteGhicite;

        public DivertismentWindow()
        {
            InitializeComponent();

            // Incar cuvintele si informatiile asociate din fisier
            LoadWordsFromFile("C:\\FACULTATE AN 2\\SEMESTRUL 2\\MVP\\Tema1\\Tema1\\Tema1classes\\cuvinte.txt");

            // Filtrare cuvinte cu imagine
            cuvinteCuImagine = cuvinte.Where(cuvant => !string.IsNullOrEmpty(cuvant.CaleImagine)).ToList();

            // Afisez un cuvant si o imagine la incarcarea ferestrei
            ShowRandomWordAndImage();
        }

        private void LoadWordsFromFile(string filePath)
        {
            cuvinte = new List<CUVANT>();

            try
            {
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');

                    if (parts.Length >= 4)
                    {
                        CUVANT word = new CUVANT
                        {
                            Cuvant = parts[0].Trim(),
                            Descriere = parts[2].Trim(),
                            Categorie = parts[1].Trim(),
                            CaleImagine = parts[3].Trim()
                        };
                        cuvinte.Add(word);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading words from file: " + ex.Message);
            }
        }

        private void ShowRandomWordAndImage()
        {
            if (cuvinteCuImagine.Count > 0)
            {
                // Aleg un cuvânt random cu imagine asociată
                int index = random.Next(cuvinteCuImagine.Count);
                CUVANT cuvant = cuvinteCuImagine[index];
                // Verific dacă trebuie afișată descrierea sau imaginea
                bool showDescription = random.Next(2) == 0;

                if (showDescription || string.IsNullOrEmpty(cuvant.CaleImagine))
                {
                    // Afișez descrierea cuvântului
                    descriere.Text = cuvant.Descriere;
                    imagine.Source = null; // Ascund imaginea (dacă este afișată)

                    // Afișez un mesaj pentru utilizator
                    MessageBox.Show("Ghiciți cuvântul asociat descrierii: " + cuvant.Descriere);
                }
                else
                {
                    // Afișez imaginea cuvântului
                    descriere.Text = string.Empty; // Ascund descrierea (dacă este afișată)
                    imagine.Source = new BitmapImage(new Uri(cuvant.CaleImagine, UriKind.RelativeOrAbsolute));

                    // Afișez un mesaj pentru utilizator
                    MessageBox.Show("Ghiciți cuvântul asociat imaginii.");
                }
            }
            else
            {
                // Nu există cuvinte cu imagini
                MessageBox.Show("Nu există cuvinte cu imagini.");
            }
        }
        private void CheckAnswer(string guessedWord)
        {
            if (cuvinteCuImagine.Count > 0)
            {
                CUVANT cuvant = cuvinteCuImagine.FirstOrDefault(c => c.Cuvant.Equals(guessedWord, StringComparison.OrdinalIgnoreCase));

                if (cuvant != null)
                {
                    MessageBox.Show("Corect! Cuvântul este: " + cuvant.Cuvant);
                    numarCuvinteGhicite++;

                    if (numarCuvinteGhicite < 3)
                    {
                        ShowRandomWordAndImage();
                    }
                    else
                    {
                        MessageBox.Show("Felicitări! Ai ghicit toate cuvintele!");
                        // Rezultatul final, poți face ce vrei în continuare, de exemplu, închide fereastra.
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Răspunsul nu este corect. Încercați din nou.");
                }
            }
        }

        private void next(object sender, RoutedEventArgs e)
        {
            // Verificați răspunsul introdus de utilizator
            CheckAnswer(raspuns.Text.Trim());
        }
    }
}