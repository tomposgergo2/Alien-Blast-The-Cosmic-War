using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlienBlast
{
    public List<string[]> Pályák { get; private set; }

    class Pálya
    {
        string[] sorok = File.ReadAllLines("Pálya.txt");
    }
}
