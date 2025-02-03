using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AlienBlast
{
    internal class Pálya
    {
        public List<string[]> Pályák { get; private set; }
        public Canvas canvas { get; private set; }

        public Pálya(Canvas canvas)
        {         
            Pályák = new List<string[]>();
            Beolvasás();
            this.canvas = canvas;
        }

        public void Generálás(int pályaindex)
        {
           

            foreach (var pályaSor in Pályák)
            {
                for (int y = 0; y < pályaSor.Length; y++)
                {
                    for (int x = 0; x < pályaSor[y].Length; x++)
                    {

                        if (pályaSor[y][x] == '1')
                        {
                            var négyzet = new System.Windows.Shapes.Rectangle
                            {
                                Width = 96,
                                Height = 96,
                                Fill = System.Windows.Media.Brushes.Orange,
                            };
                            // na majd it a Zalánnal lesz pár mondatom

                            Canvas.SetLeft(négyzet, x * 96); // ez az x
                            Canvas.SetTop(négyzet, y * 96);  // ez az y 
                            canvas.Children.Add(négyzet);
                        }
                    }
                }
            }

            
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
