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

        public void LoadMap()
        {
            string[] sorok = File.ReadAllLines("Pálya.txt", Encoding.UTF8);


            int index = 0;
            int sorHossz = sorok.Length;
            int sorMagasság = sorok.Length; //ez így még nem jó

            int[,] map = new int[sorHossz, sorMagasság];

            foreach (var sor in sorok)
            {
                if(sorok.)
            }






            return map;

        }

        
    }
}
