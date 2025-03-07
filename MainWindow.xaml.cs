﻿using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace AlienBlast
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        Player player;
        double playerX;
        double playerY;
        Pálya pálya;
        public int jelenlegiPályaIndex = 0;
        public int money = 0;
        public List<int> Collected = new List<int>();
        private int deaths = 0;
        public int Deaths
        {
            get => deaths;
            set => deaths = value;
        }
        private Enemy enemy;
        //private Spike spike;
        private static bool firstTime = true;


        public MainWindow()
        {
            InitializeComponent();

            var path = System.IO.Path.Combine(Directory.GetCurrentDirectory(),"háttér.jpg");
            canvas.Background = new ImageBrush
            (
                new BitmapImage(new Uri(path))
            );




            timer.Interval = TimeSpan.FromMilliseconds(15);
            timer.Tick += (sender, e) =>
            {
                timer.Stop();

                Fallen();

                player.MoveUp();
                player.Gravity();
                player.MovePlayer();
                if (player.CheckForEnemyCollision(new List<Enemy> { enemy }, jelenlegiPályaIndex, killedEnemies))
                    Deaths += 1;
                //player.CheckForSpike(new List<Spike> { spike }, jelenlegiPályaIndex);
                EllenőrizPortált();
                if(player.CheckForSpikes()) 
                    Deaths += 1;

                Collected = player.CheckForCoinCollection();

                Restart();

                enemy.Move(sender, e);

                timer.Start();
            };
            timer.Start();



            Loaded += (sender, e) =>
            {
                GenerálPályát();
                if (firstTime) 
                {
                    MessageBox.Show(
                        "Irányítás:\nW,↑ - Ugrás\nA,← - Balra mozgás\nS,↓ - Lefelé mozgás\nD,→ - Jobbra mozgás\nR - Újrakezdés\nESC - Kilépés\n\n" +
                        "⚠️ Ha oldalról nekimész az ellenfélnek, meghalsz!\n" +
                        "🗡️ Az ellenfél fejére kell ugrani, hogy legyőzd!\n" +
                        "💰 Gyűjts minél több érmét!",
                        "Irányítás", MessageBoxButton.OK, MessageBoxImage.Information
                    );

                    firstTime = false; 
                }
            };

            KeyDown += (sender, e) =>
            {
                if (e.Key == Key.Escape)
                {
                    StartWindow menu = new StartWindow();
                    menu.Level = jelenlegiPályaIndex;
                    menu.Collected = player.Collected;
                    menu.Deaths = Deaths;
                    menu.Show();
                    this.Close();
                }
            };
        }

        private HashSet<int> killedEnemies = new HashSet<int>(); // Meghalt ellenségek listája
        private (double, double) FindSpawnPoint()
        {
            for (int y = 0; y < pálya.Pályák[jelenlegiPályaIndex].Length; y++)
            {
                for (int x = 0; x < pálya.Pályák[jelenlegiPályaIndex][y].Length; x++)
                {
                    if (pálya.Pályák[jelenlegiPályaIndex][y][x] == '4') // Megkeressük a 4-est
                    {
                        return (x * 96, y * 96); // A játékos a 4-es szám helyére kerül
                    }
                }
            }

            // Ha nem találunk 4-est, alapértelmezett pozíciót adunk vissza
            return (10, 600);
        }

        private void GenerálPályát()
        {
            if (pálya == null)
            {
                pálya = new Pálya(canvas);
            }

            canvas.Children.Clear();
            pálya.Generálás(jelenlegiPályaIndex, Collected);
            canvas.Children.Add(ErmeSzamlalo.Parent as UIElement);
            ErmeSzamlalo.Text = Collected.Sum().ToString();

            // Megkeressük a 4-es szám pozícióját
            (double spawnX, double spawnY) = FindSpawnPoint();

            if (player != null)
            {
                player.Kill();
            }

            // A játékos a 4-es pozíciójára kerül
            playerX = spawnX;
            playerY = spawnY;
            player = new Player(playerX, playerY, Collected, canvas);

            if (killedEnemies.Contains(jelenlegiPályaIndex))
            {
                enemy = null; // Ha már megölték, ne hozzunk létre újat
            }
            else
            {
                List<(double, double)> enemyPath = pálya.GetEnemyPath(jelenlegiPályaIndex);
                enemy = new Enemy(canvas, enemyPath);
                
            }
            
            if (jelenlegiPályaIndex + 1 >= pálya.Pályák.Count)
            {
                
            }

        }



        private void EllenőrizPortált()
        {
            if (pálya == null || player == null) return;

            if (player.IsTouchingPortal('3')) // Ha a 3-ashoz ért
            {
                if (jelenlegiPályaIndex + 1 < pálya.Pályák.Count)
                {
                    if (Collected.Count() == jelenlegiPályaIndex)
                    {
                        Collected.Add(0);
                    }

                    jelenlegiPályaIndex++;
                    GenerálPályát();
                    TeleportToSpawn(); // A 4-es helyére rakjuk a játékost
                }
                else if (jelenlegiPályaIndex + 1 == pálya.Pályák.Count)
                {
                    player.Kill();
                    jelenlegiPályaIndex = 0;
                    MessageBox.Show($"Pontszám: {
                        Math.Round( 1000 * (double)Collected.Sum() / pálya.Pályák.Count ) - Deaths
                        }", "GYŐZELEM!" , MessageBoxButton.OK, MessageBoxImage.Information);
                    StartWindow menu = new StartWindow();
                    menu.Level = 0;
                    menu.Collected = [];
                    menu.Deaths = 0;
                    menu.Show();
                    this.Close();
                }
            }
        }

        private void TeleportToSpawn()
        {
            for (int y = 0; y < pálya.Pályák[jelenlegiPályaIndex].Length; y++)
            {
                for (int x = 0; x < pálya.Pályák[jelenlegiPályaIndex][y].Length; x++)
                {
                    if (pálya.Pályák[jelenlegiPályaIndex][y][x] == '4') // Megkeressük a 4-est
                    {
                        player.Kill();
                        playerX = x * 96;
                        playerY = y * 96;
                        player = new Player(playerX, playerY, Collected, canvas); // Újra létrehozzuk ott
                        return;
                    }
                }
            }
        }



        private void Restart()
        {
            if (Keyboard.IsKeyDown(Key.R))
            {
                player.Kill();
                player = new Player(playerX, playerY, Collected, canvas);
            }
        }

        private void Fallen()
        {
            if (player.Y > 1080)
            {
                player.Kill();
                Deaths += 1;   
                player = new Player(playerX, playerY, Collected, canvas);
            }
        }
    }
}
