using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace AlienBlast
{
    class Enemy
    {
        private Rectangle enemyShape;
        private double X;
        private double Y;
        private double velocity = 5;
        private bool movingRight = true;
        private DispatcherTimer timer;
        private Canvas canvas;
        private double leftLimit;
        private double rightLimit;


        private System.Windows.Shapes.Rectangle DrawPlayer(double x, double y)
        {
            var player = new System.Windows.Shapes.Rectangle
            {
                Width = 96,
                Height = 96,
                Fill = System.Windows.Media.Brushes.Red,
            };
            Canvas.SetLeft(player, X);
            Canvas.SetTop(player, Y);
            canvas.Children.Add(player);

            return player;
        }
    }
}
