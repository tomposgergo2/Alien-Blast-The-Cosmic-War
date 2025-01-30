using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlienBlast
{

    class Pálya
    {
        public List<string[]> Pályák { get; private set; }

        public Pálya()
        {
            Pályák = new List<string[]>();
            Beolvasás();
        }





        string[] sorok = File.ReadAllLines("Pálya.txt");
    }
}
