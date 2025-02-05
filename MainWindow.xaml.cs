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
        int jelenlegiPályaIndex = 0;

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





            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Tick += (sender, e) =>
            {
                timer.Stop();

                //enemy movement
                Fallen();
                player.MoveUp();
                player.Gravity();
                player.MovePlayer();
                EllenőrizPortált();
                Exit();
                Restart();

                timer.Start();
            };
            timer.Start();

            Loaded += (sender, e) =>
            {
                GenerálPályát();
            };
        }

        private void GenerálPályát()
        {
            if (pálya == null)
            {
                pálya = new Pálya(canvas);
            }

            canvas.Children.Clear();
            pálya.Generálás(jelenlegiPályaIndex);

            // Játékos kezdőpozíciót változtassuk meg
            playerX = 200; // Korábban 100 volt
            playerY = 200;

            if (player != null)
            {
                player.Kill();
            }

            player = new Player(playerX, playerY, canvas);
        }


        private void EllenőrizPortált()
        {

            if (pálya == null || player == null) return;

            if (player.KékPortálonÁll())
            {


                if (jelenlegiPályaIndex + 1 < pálya.Pályák.Count)
                {
                    jelenlegiPályaIndex++;
                    GenerálPályát();
                }
            }
        }


        private void Restart()
        {
            if (Keyboard.IsKeyDown(Key.R))
            {
                player.Kill();
                player = new Player(playerX, playerY, canvas);
            }
        }

        private void Fallen()
        {
            if (player.Y > 1080)
            {
                player.Kill();
                player = new Player(playerX, playerY, canvas);
            }
        }

        private void Exit()
        {
            if (Keyboard.IsKeyDown(Key.Escape))
            {
                Close();
            }
        }
    }
}
