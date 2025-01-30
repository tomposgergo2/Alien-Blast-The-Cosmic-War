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
        public MainWindow()
        {
            InitializeComponent();

            Pálya pálya = new Pálya();

            foreach (var pályaSor in pálya.Pályák)
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

            // no meg az hogy miért a másodikat tölti be a pályákból?







            timer.Interval = TimeSpan.FromMilliseconds(1);
            timer.Tick += (sender, e) =>
            {
                timer.Stop();
                //enemy movement
                player.Gravity();
                MovePlayer(sender, e);
                timer.Start();
            };
            timer.Start();

            Loaded += (sender, e) =>
            {
                //map generálás
                player = new Player(100, 100, canvas);
            };

            void MovePlayer(object sender, EventArgs e)
            {
                if (Keyboard.IsKeyDown(Key.Left) || Keyboard.IsKeyDown(Key.A))
                {
                    player.MoveLeft();
                }
                if (Keyboard.IsKeyDown(Key.Right) || Keyboard.IsKeyDown(Key.D))
                {
                    player.MoveRight();
                }
            }
        }


    }
}