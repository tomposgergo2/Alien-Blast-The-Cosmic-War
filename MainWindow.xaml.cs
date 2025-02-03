using System.Diagnostics;
using System.Globalization;
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
        
        public MainWindow()
        {
            InitializeComponent();

            


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
                player.MovePlayer(); //ZALÁN EZ MI?? //EZ MOZGATJA A KARAKTERT
                Exit();
                Restart();

                timer.Start();
            };
            timer.Start();

            Loaded += (sender, e) =>
            {
                //map generálás
                Pálya pálya = new Pálya(canvas);
                pálya.Generálás(3);
                playerX = 100;
                playerY = 100;
                player = new Player(playerX, playerY, canvas);
            };

            void Restart()
            {
                if (Keyboard.IsKeyDown(Key.R))
                {
                    player.Kill(); //ZALÁN EZ MI? a varázslásról nem volt szó. most már az első pálya jelenik meg. //EZ ÖLI MEG A KARATKERT TEHÁT TÖRLI HOGY NE LEGYEN KETTŐ AMIKOR ÚJAT HOZ LÉTRE
                    player = new Player(playerX, playerY, canvas);
                }
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