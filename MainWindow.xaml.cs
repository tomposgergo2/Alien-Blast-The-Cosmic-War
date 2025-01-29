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

            timer.Interval = TimeSpan.FromMilliseconds(1);
            timer.Tick += (sender, e) =>
            {
                timer.Stop();
                //enemy movement
                MovePlayer(sender, e);
                timer.Start();
            };
            timer.Start();

            Loaded += (sender, e) =>
            {
                //map generálás
                player = new Player(100, 100, canvas);
            };

            //KeyDown += (sender, e) =>
            //{
            //    if (player != null)
            //    {
            //        switch (e.Key)
            //        {
            //            case Key.W:
            //                break;
            //            case Key.S:
            //                break;
            //            case Key.A:
            //                player.MoveLeft();
            //                break;
            //            case Key.D:
            //                player.MoveRight();
            //                break;

            //            case Key.Up:
            //                break;
            //            case Key.Down:
            //                break;
            //            case Key.Left:
            //                player.MoveLeft();
            //                break;
            //            case Key.Right:
            //                player.MoveRight();
            //                break;

            //            default:
            //                break;
            //        }
            //    }
            //};

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