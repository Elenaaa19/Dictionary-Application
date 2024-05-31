using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Tema1.Tema1classes
{
    internal class CUVANT:INotifyPropertyChanged
    {
            private string cuvant;
            public string Cuvant
            {
                get
                {
                    return cuvant;
                }
                set
                {
                    cuvant = value;
                    NotifyPropertyChanged("Cuvant:");
                }
            }

            private string descriere;
            public string Descriere
            {
                get
                {
                    return descriere;
                }
                set
                {
                    descriere = value;
                    NotifyPropertyChanged("Descriere");
                }
            }

        private string categorie;
        public string Categorie
        {
            get
            {
                return categorie;
            }
            set
            {
                categorie = value;
                NotifyPropertyChanged("Categorie");
            }
        }
        private BitmapImage imagine;
        internal string Text;

        public BitmapImage Imagine
        {
            get
            {
                return imagine;
            }
            set
            {
                imagine = value;
                NotifyPropertyChanged("Imagine");
            }
        }
        private string caleImagine;
        public string CaleImagine
        {
            get
            {
                // Returnează doar numele fișierului din calea completă
                return caleImagine;
            }
            set
            {
                caleImagine = value;
                NotifyPropertyChanged("CaleImagine");
            }
        }
        private bool afisareDescriere;
        public bool AfisareDescriere
        {
            get { return afisareDescriere; }
            set
            {
                afisareDescriere = value;
                NotifyPropertyChanged("AfisareDescriere");
            }
        }
        public bool RaspunsUtilizatorCorect { get; set; }


        public string FullCuvant
            {
                get
                {
                    return cuvant + " " + descriere+" "+categorie;
                }
                set
                {
                    NotifyPropertyChanged("Cuvant");
                    NotifyPropertyChanged("Descriere");
                    NotifyPropertyChanged("Categorie");
                    //NotifyPropertyChanged("Imagine");
                     //NotifyPropertyChanged("CaleImagine");
                    NotifyPropertyChanged("AfisareDescriere");

            }
        }
        

        public event PropertyChangedEventHandler PropertyChanged;
            protected void NotifyPropertyChanged(string propertyName)
            {
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

