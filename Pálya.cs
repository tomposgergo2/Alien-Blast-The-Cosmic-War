using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlienBlast
{
    internal class Pálya
    {
        public List<string[]> Pályák { get; private set; }

        public Pálya()
        {
            Pályák = new List<string[]>();
            Beolvasás();
        }

        private void Beolvasás()
        {
            string[] sorok = File.ReadAllLines("Pálya.txt");



            List<string> pálya = new List<string>();
            foreach (var sor in sorok)
            {
                if (string.IsNullOrWhiteSpace(sor)) //Zalán ez nézi a spacet
                {
                    if (pálya.Count > 0) //Zalán ha van egy akár egy sor akkor hozzáadja
                    {
                        Pályák.Add(pálya.ToArray());
                        pálya.Clear(); //Zalán az aktuális listát ürítjük hogy nézzük a kövi pályát
                    }
                }
                else
                {
                    pálya.Add(sor); 
                }
            }

            if (pálya.Count > 0)
            {
                Pályák.Add(pálya.ToArray()); // ez majd a utolsó pályánál fontos hogy hozzáadja
            }
        }

    }
}
