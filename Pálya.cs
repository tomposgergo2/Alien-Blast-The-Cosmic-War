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

        public void Generálás(int pályaindex, List<int> Collected)
        {
            if (pályaindex < 0 || pályaindex >= Pályák.Count)
                return; 

            var pályaSor = Pályák[pályaindex];

            List<(int, int)> portal3Positions = new List<(int, int)>(); 
            List<(int, int)> portal4Positions = new List<(int, int)>();

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

                        Canvas.SetLeft(négyzet, x * 96); 
                        Canvas.SetTop(négyzet, y * 96); 
                        canvas.Children.Add(négyzet);
                    }
                    if (pályaSor[y][x] == '3')
                    {
                        portal3Positions.Add((x, y));
                    }
                    if (pályaSor[y][x] == '4')
                    {
                        portal4Positions.Add((x, y));
                    }
                    if (pályaSor[y][x] == '2')
                    {
                        if (Collected.Count() <= pályaindex || Collected[pályaindex] == 0)
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
                            Source = new BitmapImage(new Uri("tüske2.png", UriKind.Relative)),
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
                    //if (pályaSor[y][x] == '9')
                    //{
                    //    var reactor = new Image()
                    //    {
                    //        Width = 576,
                    //        Height = 576,
                    //        Source = new BitmapImage(new Uri("reactor.png", UriKind.Relative)),
                    //        Tag = '9',
                    //        Name = "Reactor"
                    //    };
                    //    Canvas.SetLeft(reactor, x * 96);
                    //    Canvas.SetTop(reactor, y * 96);
                    //    canvas.Children.Add(reactor);

                    //}
                }
            }
            if (portal3Positions.Count > 0)
            {
                int minX3 = portal3Positions.Min(p => p.Item1); 
                int maxX3 = portal3Positions.Max(p => p.Item1); 
                int minY3 = portal3Positions.Min(p => p.Item2); 
                int maxY3 = portal3Positions.Max(p => p.Item2); 

                int width3 = (maxX3 - minX3 + 1) * 96; 
                int height3 = (maxY3 - minY3 + 1) * 96;

                
                if (width3 > 0 && height3 > 0)
                {
                    var portal3 = new System.Windows.Shapes.Rectangle
                    {
                        Width = width3,
                        Height = height3,
                        Fill = new System.Windows.Media.ImageBrush
                        {
                            ImageSource = new System.Windows.Media.Imaging.BitmapImage(new Uri("Portal.png", UriKind.Relative))
                        },
                        Tag = '3',
                        Name = "Portal_3"
                    };
                    Canvas.SetLeft(portal3, minX3 * 96);
                    Canvas.SetTop(portal3, minY3 * 96);
                    canvas.Children.Add(portal3);
                }
            }


            if (portal4Positions.Count > 0)
            {
                int minX4 = portal4Positions.Min(p => p.Item1); 
                int maxX4 = portal4Positions.Max(p => p.Item1); 
                int minY4 = portal4Positions.Min(p => p.Item2); 
                int maxY4 = portal4Positions.Max(p => p.Item2); 

                int width4 = (maxX4 - minX4 + 1) * 96; 
                int height4 = (maxY4 - minY4 + 1) * 96; 

                var portal4 = new System.Windows.Shapes.Rectangle
                {
                    Width = width4,
                    Height = height4,
                    Fill = new System.Windows.Media.ImageBrush
                    {
                        ImageSource = new System.Windows.Media.Imaging.BitmapImage(new Uri("Portal.png", UriKind.Relative))
                    },
                    Tag = '4',
                    Name = "Portal_4"
                };

                Canvas.SetLeft(portal4, minX4 * 96);
                Canvas.SetTop(portal4, minY4 * 96);
                canvas.Children.Add(portal4);
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
