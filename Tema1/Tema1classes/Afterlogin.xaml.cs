using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;
using static System.Net.Mime.MediaTypeNames;
using ListBox = System.Windows.Forms.ListBox;
using MessageBox = System.Windows.Forms.MessageBox;




namespace Tema1.Tema1classes
{
    /// <summary>
    /// Interaction logic for Afterlogin.xaml
    /// </summary>
    public partial class Afterlogin : Window
    {
        public Afterlogin()
        {
            InitializeComponent();
            DataContext = new Cuvintelist();
            
            IncarcaDateleDinFisier();




        }

        private void IncarcaDateleDinFisier()
        {
            string caleFisier = "C:\\FACULTATE AN 2\\SEMESTRUL 2\\MVP\\Tema1\\Tema1\\Tema1classes\\cuvinte.txt";
            string directorImagini = "C:\\FACULTATE AN 2\\SEMESTRUL 2\\MVP\\Tema1\\Tema1\\imagini";
          

            try
            {
                using (StreamReader sr = new StreamReader(caleFisier))
                {
                    string linie;
                    while ((linie = sr.ReadLine()) != null)
                    {
                        // Separ linia în cuvinte și descriere (presupunând că sunt separate de un separator, cum ar fi virgula)
                        string[] elemente = linie.Split(',');
                        if (elemente.Length >= 4)
                        {
                            // Creez un nou obiect CUVANT și il adaugă în colecția de cuvinte
                            CUVANT cuvant = new CUVANT() { Cuvant = elemente[0], Descriere = elemente[2], Categorie = elemente[1], CaleImagine = elemente[3] };
                            if (elemente.Length >= 3) // Verific dacă linia conține cel puțin 3 elemente (categorie, cuvânt, descriere)
                            {
                                string categorie = elemente[1]; // Iau categoria de pe poziția a doua (index 1)

                                // Adaug categoria în ComboBox, dacă nu există deja
                                if (!comboBoxCategorii.Items.Contains(categorie))
                                {
                                    comboBoxCategorii.Items.Add(categorie);
                                }
                            }
                            string caleImagine =directorImagini+"//"+ elemente[3];


                            if ((File.Exists(caleImagine)))
                            {
                                
                                //Incarc imaginea si o asociez cu  obiectul CUVANT
                                BitmapImage bitmapImage = new BitmapImage(new Uri(caleImagine));
                                cuvant.Imagine = bitmapImage;
                            }
                            else
                            {
                                
                                // Încarc o imagine implicită dacă fișierul de imagine specificat nu există
                                BitmapImage bitmapImageImplicita = new BitmapImage(new Uri("C:\\FACULTATE AN 2\\SEMESTRUL 2\\MVP\\Tema1\\Tema1\\imagini\\implicit.jpg"));
                                cuvant.Imagine = bitmapImageImplicita;
                            }


                            
                            (DataContext as Cuvintelist).Cuvinte.Add(cuvant);
                          

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("A apărut o eroare la încărcarea datelor din fișier: " + ex.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SelectImage_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Fișiere imagine (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png|Toate fișierele (*.*)|*.*";

            // Numele imaginii implicite
            string defaultImageName = "implicit.jpg";

            try
            {
                string selectedImagePath = string.Empty;
                string selectedImageName = string.Empty;

                if (openFileDialog.ShowDialog() == true)
                {
                    selectedImagePath = openFileDialog.FileName;
                    selectedImageName = System.IO.Path.GetFileName(selectedImagePath);
                }

                // Dacă nu s-a selectat nicio imagine, folosesc  imaginea implicită
                if (string.IsNullOrEmpty(selectedImagePath))
                {
                    selectedImageName = defaultImageName;
                    selectedImagePath = System.IO.Path.Combine("C:\\FACULTATE AN 2\\SEMESTRUL 2\\MVP\\Tema1\\Tema1\\imagini", selectedImageName);
                    // Dacă imaginea implicită nu există, copiaz dintr-un loc prestabilit
                    if (!System.IO.File.Exists(selectedImagePath))
                    {
                        string defaultImagePath = "C:\\FACULTATE AN 2\\SEMESTRUL 2\\MVP\\Tema1\\Tema1\\imagini\\implicit.jpg"; // Specifica calea către imaginea implicită
                        System.IO.File.Copy(defaultImagePath, selectedImagePath);
                    }
                }
                else
                {
                    // Copiez imaginea selectată în directorul corespunzător
                    string destinationPath = System.IO.Path.Combine("C:\\FACULTATE AN 2\\SEMESTRUL 2\\MVP\\Tema1\\Tema1\\imagini", selectedImageName);
                    System.IO.File.Copy(selectedImagePath, destinationPath);
                }

                // Adăugare nume fișier în fișierul text
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter("C:\\FACULTATE AN 2\\SEMESTRUL 2\\MVP\\Tema1\\Tema1\\Tema1classes\\cuvinte.txt", true))
                {
                    sw.WriteLine($"{cuvant.Text},{descriere.Text},{comboBoxCategorii.SelectedItem},{selectedImageName}");
                }

                MessageBox.Show("Imaginea a fost adăugată cu succes în directorul de imagini!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("A apărut o eroare la adăugarea imaginii în directorul de imagini: " + ex.Message);
            }
        }


        
        private void ADD(object sender, RoutedEventArgs e)
        {
            try
            {
                // Obținem categoria selectată din combobox
                string categorie = comboBoxCategorii.SelectedItem as string;

                // Dacă categoria nu este selectată, verificați dacă a fost introdusă o nouă categorie în textbox
                if (string.IsNullOrEmpty(categorie) && !string.IsNullOrEmpty(categorienoua.Text))
                {
                    categorie = categorienoua.Text;
                    // Adăugați noua categorie în combobox, dacă nu există deja
                    if (!comboBoxCategorii.Items.Contains(categorie))
                    {
                        comboBoxCategorii.Items.Add(categorie);
                    }
                }

                // Dacă categoria este încă nulă sau gol, afișez un mesaj de eroare și ies din metodă
                if (string.IsNullOrEmpty(categorie))
                {
                    MessageBox.Show("Selectați o categorie existentă sau introduceți o categorie nouă.");
                    return;
                }

                // Cree o instanță a unui obiect CUVANT cu datele introduse în câmpurile text și categoria selectată
                CUVANT C = new CUVANT() { Cuvant = cuvant.Text, Descriere = descriere.Text, Categorie = categorie };

                // Verific dacă s-a selectat o imagine; dacă nu, utilizez imaginea implicită
                //string imagePath = ImagineCuvant.Source != null ? ImagineCuvant.Source.ToString() : "C:\\FACULTATE AN 2\\SEMESTRUL 2\\MVP\\Tema1\\Tema1\\Tema1classes\\implicite.jpg"; // Înlocuiți cu calea către imaginea implicită dorită
                string imagePath = ImagineCuvant.Source != null ? System.IO.Path.GetFileName(ImagineCuvant.Source.ToString()) : "implicit.jpg";
                //C.Imagine = new BitmapImage(new Uri($"C:\\FACULTATE AN 2\\SEMESTRUL 2\\MVP\\Tema1\\Tema1\\Tema1classes\\imagini\\{imagePath}"));
                
                // Adaug cuvântul în colecția de cuvinte
                (DataContext as Cuvintelist).Cuvinte.Add(C);

                // Actualizez afișarea în ListBox
                ListBoxCuvinte.ItemsSource = (DataContext as Cuvintelist).Cuvinte;
                // Actualizez imaginea în ImageBox
                ImagineCuvant.Source = C.Imagine;

                // Afisez un mesaj de succes
                MessageBox.Show("Cuvânt adăugat!");

                // Deschid fișierul pentru scriere, adăugând cuvântul, descrierea, categoria și calea imaginii
                string caleFisier = "C:\\FACULTATE AN 2\\SEMESTRUL 2\\MVP\\Tema1\\Tema1\\Tema1classes\\cuvinte.txt"; // Specifică calea către fișierul tău
                using (StreamWriter sw = new StreamWriter(caleFisier, true))
                {
                    string numeImagine = System.IO.Path.GetFileName(imagePath);
                    sw.WriteLine($"{C.Cuvant},{C.Categorie},{C.Descriere},{numeImagine}");
                }

                // Golesc text box-urile după ce datele sunt introduse
                cuvant.Text = "";
                descriere.Text = "";
                categorienoua.Text = "";
                comboBoxCategorii.SelectedItem = null;
            }
            catch (Exception ex)
            {
                // În caz de eroare, afișez  un mesaj de eroare
                MessageBox.Show($"A apărut o eroare: {ex.Message}");
            }
        }




        private void Search(object sender, TextChangedEventArgs e)
        {
            string searchTerm = search.Text.ToLower(); // Convertesc termenul de căutare la litere mici pentru a face căutarea nesensibilă la majuscule/minuscule
                                                       // Verific dacă termenul de căutare este gol
            if (string.IsNullOrEmpty(searchTerm))
            {
                // Dacă da, șterg textul din celelalte câmpuri text
                cuvant.Text = "";
                descriere.Text = "";
                comboBoxCategorii.SelectedItem = null;
                ImagineCuvant.Source = null; // Și imaginea, dacă este cazul
                return;
            }

            // Iterez prin fiecare element din ListBox
            foreach (CUVANT cuvant in ListBoxCuvinte.Items)
            {
                // Verific dacă termenul de căutare se găsește în Cuvant sau în Descriere
                if (cuvant.Cuvant.ToLower().Contains(searchTerm) || cuvant.Descriere.ToLower().Contains(searchTerm))
                {
                    // Dacă găsesc un cuvânt care corespunde căutării, îl selectez în ListBox
                    ListBoxCuvinte.SelectedItem = cuvant;
                    ListBoxCuvinte.ScrollIntoView(cuvant); // Derulez în jos pentru a face elementul vizibil, în caz că este în afara ecranului
                    // Actualizez valorile din celelalte câmpuri text
                     cuvant.Text = cuvant.Cuvant;
                    descriere.Text = cuvant.Descriere;
                    comboBoxCategorii.SelectedItem = cuvant.Categorie;
                    ImagineCuvant.Source = cuvant.Imagine; 

                    
                    return; // Opresc căutarea după ce am găsit primul rezultat
                }
            }

            // Dacă nu găsesc niciun rezultat, deselectez orice element selectat anterior
            ListBoxCuvinte.SelectedItem = null;
        }








        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Listcuvinte(object sender, SelectionChangedEventArgs e)
        {
            if (ListBoxCuvinte.SelectedItem != null)
            {
                // Obțin obiectul CUVANT corespunzător elementului selectat în ListBox
                CUVANT cuvantSelectat = ListBoxCuvinte.SelectedItem as CUVANT;

                if (cuvantSelectat != null)
                {
                    // Actualizez valorile din TextBox-uri
                    cuvant.Text = cuvantSelectat.Cuvant;
                    comboBoxCategorii.SelectedItem = cuvantSelectat.Categorie;
                    descriere.Text = cuvantSelectat.Descriere;



                    // Setează imaginea asociată în ImageBox
                    //ImagineCuvant.Source = cuvantSelectat.Imagine;
                    // Extrageți numele imaginii din calea completă
                    //string caleImagineCompleta = cuvantSelectat.CaleImagine;
                    //string numeImagine = System.IO.Path.GetFileName(caleImagineCompleta);

                    // Setează imaginea asociată în ImageBox
                    //ImagineCuvant.Source = cuvantSelectat.Imagine;

                    

                }
            }
        }


        private void Modifica(object sender, RoutedEventArgs e)
        {
            // Verific dacă un cuvânt este selectat în ListBox
            if (ListBoxCuvinte.SelectedItem != null)
            {
                // Obțin obiectul CUVANT corespunzător cuvântului selectat
                CUVANT cuvantSelectat = ListBoxCuvinte.SelectedItem as CUVANT;

                // Actualizez proprietățile cuvântului selectat cu noile valori
                cuvantSelectat.Cuvant = cuvant.Text; // Actualizarea numelui cuvântului
                cuvantSelectat.Categorie = comboBoxCategorii.SelectedItem as string; // Actualizarea categoriei
                cuvantSelectat.Descriere = descriere.Text; // Actualizarea descrierii
                // Salvare modificări în fișier
                SalvareModificariInFisier(cuvantSelectat);

                // Actualizare afișare în ListBox
                ListBoxCuvinte.Items.Refresh();

                
                MessageBox.Show("Cuvântul a fost modificat cu succes.");
            }
            else
            {
                // Afisez un mesaj de avertizare dacă nu este selectat niciun cuvânt
                MessageBox.Show("Nu a fost selectat niciun cuvânt pentru modificare.");
            }
        }
        private void SalvareModificariInFisier(CUVANT  cuvantSelectat)
        {
            try
            {
                // Citirea tuturor liniilor din fișier
                string[] lines = File.ReadAllLines("C:\\FACULTATE AN 2\\SEMESTRUL 2\\MVP\\Tema1\\Tema1\\Tema1classes\\cuvinte.txt");

                // Parcurgerea fiecărei linii pentru a găsi și actualiza cuvântul modificat
                for (int i = 0; i < lines.Length; i++)
                {
                    // Divizarea liniei în valori separate
                    string[] values = lines[i].Split(',');
                    
                    // Verificarea dacă linia corespunde cuvântului selectat
                    if (values.Length >= 3 && values[0].Trim().ToUpper() == cuvantSelectat.Cuvant.ToUpper())
                    {

                        // Actualizarea liniei cu noile valori
                        lines[i] = $"{cuvantSelectat.Cuvant},{cuvantSelectat.Categorie},{cuvantSelectat.Descriere},";
                        break; // Terminarea buclei după găsirea și actualizarea cuvântului
                    }
                }

                // Rescrierea fișierului cu modificările făcute
                File.WriteAllLines("C:\\FACULTATE AN 2\\SEMESTRUL 2\\MVP\\Tema1\\Tema1\\Tema1classes\\cuvinte.txt", lines);

                // Afișarea unui mesaj de succes
                MessageBox.Show("Fișierul a fost actualizat cu succes.");
            }
            catch (IOException ex)
            {
                // Afișarea unui mesaj de eroare în caz de eșec la salvare
                MessageBox.Show("Eroare la salvarea modificărilor în fișier: " + ex.Message);
            }
        }
        


        private void Sterge(object sender, RoutedEventArgs e)
        {
            if (ListBoxCuvinte.SelectedItem != null)
            {
                // Obțin elementul selectat din lista ListBox
                CUVANT cuvantSelectat = ListBoxCuvinte.SelectedItem as CUVANT;

                try
                {
                    // Citirea tuturor liniilor din fișier
                    string[] lines = File.ReadAllLines("C:\\FACULTATE AN 2\\SEMESTRUL 2\\MVP\\Tema1\\Tema1\\Tema1classes\\cuvinte.txt");

                    // Crearea unei liste de linii noi, fără linia care trebuie ștearsă
                    List<string> newLines = new List<string>();

                    foreach (string line in lines)
                    {
                        string[] elements = line.Split(',');
                        if (elements.Length >= 4 && elements[0].Trim() == cuvantSelectat.Cuvant)
                        {
                            // Șterge imaginea corespunzătoare din directorul de imagini
                            string imagePath = System.IO.Path.Combine("C:\\FACULTATE AN 2\\SEMESTRUL 2\\MVP\\Tema1\\Tema1\\imagini", elements[3].Trim());
                            if (File.Exists(imagePath))
                            {
                                // Închid orice referință la fișierul imagine
                                FileStream fileStream = null;
                                try
                                {
                                    fileStream = File.Open(imagePath, FileMode.Open, FileAccess.Read, FileShare.None);
                                    // Dacă ajung aici, fișierul nu este utilizat de niciun alt proces
                                    fileStream.Close();
                                    File.Delete(imagePath); // Șterg fișierul imagine
                                }
                                catch (Exception ex)
                                {
                                    // Afișez  un mesaj de eroare în cazul în care ștergerea nu a reușit
                                    MessageBox.Show("Eroare la ștergerea imaginii: " + ex.Message);
                                }
                                finally
                                {
                                    // Ma asigur că fișierul este închis chiar și în cazul în care apare o excepție
                                    if (fileStream != null)
                                        fileStream.Close();
                                }
                            
                        }
                            continue; // Omiterea liniei de șters
                        }
                        newLines.Add(line);
                    }

                    // Rescrierea fișierului cu linii noi
                    File.WriteAllLines("C:\\FACULTATE AN 2\\SEMESTRUL 2\\MVP\\Tema1\\Tema1\\Tema1classes\\cuvinte.txt", newLines);
                    // Eliminarea elementului selectat din sursa de date a ListBox-ului
                    (DataContext as Cuvintelist).Cuvinte.Remove(cuvantSelectat);
                    // Șterg categoria corespunzătoare din combobox
                    comboBoxCategorii.Items.Remove(cuvantSelectat.Categorie);
                    // Șterge textul din TextBox-uri
                    cuvant.Text = "";
                    descriere.Text = "";
                    comboBoxCategorii.SelectedItem = null;

                    
                    // Afișarea unui mesaj de succes
                    MessageBox.Show("Elementul a fost șters cu succes din fișier și din listă.");
                }
                catch (IOException ex)
                {
                    // Afișarea unui mesaj de eroare în caz de eșec la ștergere
                    MessageBox.Show("A apărut o eroare la ștergerea elementului din fișier și din listă: " + ex.Message);
                }
            }
            else
            {
                // Afișarea unui mesaj de avertizare dacă nu este selectat niciun element pentru ștergere
                MessageBox.Show("Vă rugăm să selectați un element pentru a fi șters.");
            }
        }
    }
}

