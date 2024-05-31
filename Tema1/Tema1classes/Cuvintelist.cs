using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema1.Tema1classes
{
    internal class Cuvintelist
    {
            public ObservableCollection<CUVANT> Cuvinte { get; set; }
            public CUVANT CuvinteSelecate { get; set; }
            public Cuvintelist()
            {
                Cuvinte = new ObservableCollection<CUVANT>();
            }
        
    }
}
