﻿using System.Globalization;
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