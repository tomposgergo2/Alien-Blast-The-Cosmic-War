using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

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
            if (pályaindex < 0 || pályaindex >= Pályák.Count)
                return; 

            var pályaSor = Pályák[pályaindex];


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
                            Fill = System.Windows.Media.Brushes.Gray,
                            Tag = '1'
                        };

                        Canvas.SetLeft(négyzet, x * 96); // x koordináta
                        Canvas.SetTop(négyzet, y * 96); // y koordináta
                        canvas.Children.Add(négyzet);
                    }                    
                    if (pályaSor[y][x] == '3')
                    {
                        var portal = new System.Windows.Shapes.Rectangle
                        {
                            Width = 96,
                            Height = 96,
                            Fill = System.Windows.Media.Brushes.Blue,
                            Tag = '3',
                            Name = "Portal"
                        };

                        Canvas.SetLeft(portal, x * 96);
                        Canvas.SetTop(portal, y * 96);
                        canvas.Children.Add(portal);
                    }

                    if (pályaSor[y][x] == '4')
                    {
                        var portal = new System.Windows.Shapes.Rectangle
                        {
                            Width = 96,
                            Height = 96,
                            Fill = System.Windows.Media.Brushes.Pink,
                            Tag = '4',
                            Name = "Portal"
                        };

                        Canvas.SetLeft(portal, x * 96);
                        Canvas.SetTop(portal, y * 96);
                        canvas.Children.Add(portal);
                    }
                    if (pályaSor[y][x] == '2')
                    {
                        var érme = new Image()
                        {
                            Width = 96,
                            Height = 96,
                            Source = new BitmapImage(new Uri("érme.png", UriKind.Relative)),
 //már megint nem jó valami a képnél    zalán már az érme is megjelenik
                            Tag = '2',
                            Name = "Coin"
                        };

                        Canvas.SetLeft(érme, x * 96);
                        Canvas.SetTop(érme, y * 96);
                        canvas.Children.Add(érme);
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
