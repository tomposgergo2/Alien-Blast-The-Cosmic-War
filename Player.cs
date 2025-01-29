using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AlienBlast
{
    class Player
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Velocity { get; set; }
        public string img { get; set; }
        public Canvas canvas { get; set; }
        public System.Windows.Shapes.Rectangle player { get; set; }

        public Player(double x, double y, Canvas canvas, double v = 10)
        {
            X = x;
            Y = y;
            Velocity = v;
            this.canvas = canvas;
            player = DrawPlayer(X, Y, img);
        }

        private System.Windows.Shapes.Rectangle DrawPlayer(double x, double y, string img)
        {  
            var player = new System.Windows.Shapes.Rectangle
            {
                Width = 96,
                Height = 96,
                Fill = Brushes.Blue
            };
            Canvas.SetLeft(player, X);
            Canvas.SetTop(player, Y);
            canvas.Children.Add(player);

            return player;
        }

        public void MoveLeft()
        {
            X = Canvas.GetLeft(player);
            Canvas.SetLeft(player, X - Velocity);
        }
        public void MoveRight()
        {
            X = Canvas.GetLeft(player);
            Canvas.SetLeft(player, X + Velocity);
        }
    }
}
