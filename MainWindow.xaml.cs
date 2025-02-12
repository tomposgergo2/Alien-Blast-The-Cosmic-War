using System.Diagnostics;
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
        private Enemy enemy;

        public MainWindow()
        {
            InitializeComponent();

            var path = System.IO.Path.Combine(Directory.GetCurrentDirectory(),"háttér.jpg");
            canvas.Background = new ImageBrush
            (
                new BitmapImage(new Uri(path))
            );



            // no meg az hogy miért a másodikat tölti be a pályákból?

            //var négyzet = new System.Windows.Shapes.Rectangle
            //{
            //    Width = 96,
            //    Height = 96,
            //    Fill = System.Windows.Media.Brushes.Orange,
            //};
            //// na majd it a Zalánnal lesz pár mondatom

            //Canvas.SetLeft(négyzet, 100); // ez az x
            //Canvas.SetTop(négyzet, 100);  // ez az y 
            //canvas.Children.Add(négyzet);            



            timer.Interval = TimeSpan.FromMilliseconds(15);
            timer.Tick += (sender, e) =>
            {
                timer.Stop();

                Fallen();

                player.MoveUp();
                player.Gravity();
                player.MovePlayer();

                EllenőrizPortált();

                money = player.CheckForCoinCollection();

                Restart();

                enemy.Move(sender, e);

                timer.Start();
            };
            timer.Start();

            Loaded += (sender, e) =>
            {
                GenerálPályát();
            };

            KeyDown += (sender, e) =>
            {
                if (e.Key == Key.Escape)
                {
                    StartWindow menu = new StartWindow();
                    menu.Level = jelenlegiPályaIndex;
                    menu.Money = player.Money;
                    menu.Show();
                    this.Close();
                }
            };
        }

        private void GenerálPályát()
        {
            if (pálya == null)
            {
                pálya = new Pálya(canvas);
            }

            canvas.Children.Clear();
            pálya.Generálás(jelenlegiPályaIndex, money);
            canvas.Children.Add(ErmeSzamlalo.Parent as UIElement);
            ErmeSzamlalo.Text = money.ToString();


            // Játékos kezdőpozíciót változtassuk meg
            playerX = 10; // Korábban 100 volt
            playerY = 600;

            if (player != null)
            {
                player.Kill();
            }

            player = new Player(playerX, playerY, money, canvas);
            List<(double, double)> enemyPath = pálya.GetEnemyPath(jelenlegiPályaIndex);
            enemy = new Enemy(canvas, enemyPath);
        }


        private void EllenőrizPortált()
        {
            if (pálya == null || player == null) return;

            if (player.IsTouchingPortal('3')) // Ha a 3-ashoz ért
            {
                if (jelenlegiPályaIndex + 1 < pálya.Pályák.Count)
                {
                    jelenlegiPályaIndex++;
                    GenerálPályát();
                    TeleportToSpawn(); // A 4-es helyére rakjuk a játékost
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
                        player = new Player(playerX, playerY, money, canvas); // Újra létrehozzuk ott
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
                player = new Player(playerX, playerY, money, canvas);
            }
        }

        private void Fallen()
        {
            if (player.Y > 1080)
            {
                player.Kill();
                player = new Player(playerX, playerY, money, canvas);
            }
        }
    }
}
