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

        public void LoadMap(string FilePath)
        {
            string[] sorok = File.ReadAllLines("Pálya.txt", Encoding.UTF8);

            int sorHossz = sorok.Length;
            int sorMagasság = sorok.Length;
        }

        
    }
}
