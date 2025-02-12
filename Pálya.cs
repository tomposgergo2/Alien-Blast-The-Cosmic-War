using System;
using System.Collections.Generic;
using System.Windows.Shapes;
using System.Windows.Media;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
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

        public void Generálás(int pályaindex, int money)
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
                        if (money < pályaindex + 1)
                        {
                            var érme = new Image()
                            {
                                Width = 96,
                                Height = 96,
                                Source = new BitmapImage(new Uri("érme.png", UriKind.Relative)),
                                Tag = '2',
                                Name = "Coin"
                            };
                            Canvas.SetLeft(érme, x * 96);
                            Canvas.SetTop(érme, y * 96);
                            canvas.Children.Add(érme);
                        }
                    }
                    if (pályaSor[y][x] == '6')
                    {                       
                            var tüske = new Image()
                            {
                                Width = 96,
                                Height = 96,
                                Source = new BitmapImage(new Uri("tüske.png", UriKind.Relative)),
                                Tag = '6',
                                Name = "Tüske"
                            };
                            Canvas.SetLeft(tüske, x * 96);
                            Canvas.SetTop(tüske, y * 96);
                            canvas.Children.Add(tüske);
                        
                    }
                    if (pályaSor[y][x] == '7')
                    {
                        var tüske2 = new Image()
                        {
                            Width = 96,
                            Height = 96,
                            Source = new BitmapImage(new Uri("randomrobbanás.png", UriKind.Relative)),
                            Tag = '7',
                            Name = "tüske2"
                        };
                        Canvas.SetLeft(tüske2, x * 96);
                        Canvas.SetTop(tüske2, y * 96);
                        canvas.Children.Add(tüske2);

                    }
                    if (pályaSor[y][x] == '5')
                    {
                        var enemyZone = new Image
                        {
                            Width = 96,
                            Height = 96,
                            Source = new BitmapImage(new Uri("Enemy.png", UriKind.Relative)),
                            Tag = '5',
                            Name = "Enemy"
                        };
                        Canvas.SetLeft(enemyZone, x * 96);
                        Canvas.SetTop(enemyZone, y * 96);
                        canvas.Children.Add(enemyZone);
                    }
                }
            }
        }

        public List<(double, double)> GetEnemyPath(int pályaIndex)
        {
            List<(double, double)> path = new List<(double, double)>();
            if (pályaIndex < 0 || pályaIndex >= Pályák.Count) return path;

            for (int y = 0; y < Pályák[pályaIndex].Length; y++)
            {
                for (int x = 0; x < Pályák[pályaIndex][y].Length; x++)
                {
                    if (Pályák[pályaIndex][y][x] == '5') // Ha 5-ös számot találunk
                    {
                        path.Add((x * 96, y * 96)); // Koordináták mentése
                    }
                }
            }
            return path;
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
