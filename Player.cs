using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Shapes;

namespace AlienBlast
{
    class Player
    {
        public double X { get; set; }
        public double Y { get; set; }
        private double W { get; set; }
        private double H { get; set; }
        private double Velocity { get; set; }
        private double G { get; set; } = 1;
        private double Jumping { get; set; } = -1;
        private string img { get; set; }
        private Canvas canvas { get; set; }
        private System.Windows.Shapes.Rectangle player { get; set; }

        public Player(double x, double y, Canvas canvas, double v = 10, double w = 94, double h = 94)
        {
            X = x;
            Y = y;
            Velocity = v;
            this.canvas = canvas;
            player = DrawPlayer(X, Y, img);
            W = w;
            H = h;
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
        public void Gravity()
        {
            if (Jumping <= 0)
            {
                var collision = CollisionCheck("B");
                if (collision != null && (bool)collision)
                {
                    Canvas.SetTop(player, Y - 5);
                    Y = Canvas.GetTop(player);
                    G = 1;
                }
                else if (collision != null && !(bool)collision)
                {
                    Canvas.SetTop(player, Y + G);
                    Y = Canvas.GetTop(player);
                    if (G < 10)
                    {
                        G++;
                    }
                    Jumping = 0;
                }
            }
        }

        public void MovePlayer()
        {
            if (Keyboard.IsKeyDown(Key.Left) || Keyboard.IsKeyDown(Key.A))
            {
                MoveLeft();
            }
            if (Keyboard.IsKeyDown(Key.Right) || Keyboard.IsKeyDown(Key.D))
            {
                MoveRight();
            }
            if (Keyboard.IsKeyDown(Key.Up) || Keyboard.IsKeyDown(Key.W))
            {
                Jump();
            }
        }

        public void Kill()
        {
            canvas.Children.Remove(player);
        }

        private void MoveLeft()
        {
            var collision = CollisionCheck("L");
            if (collision != null && !(bool)collision)
            {
                Canvas.SetLeft(player, X - Velocity);
                X = Canvas.GetLeft(player);
                Velocity = 10;
            }
            else if (collision != null && (bool)collision)
            {
                Canvas.SetLeft(player, X + 3);
                X = Canvas.GetLeft(player);
                Velocity = 1;
            }
        }
        private void MoveRight()
        {
            var collision = CollisionCheck("R");
            if (collision != null && !(bool)collision)
            {
                Canvas.SetLeft(player, X + Velocity);
                X = Canvas.GetLeft(player);
                Velocity = 10;
            }
            else if (collision != null && (bool)collision)
            {
                Canvas.SetLeft(player, X - 3);
                X = Canvas.GetLeft(player);
                Velocity = 1;
            }
        }
        public void MoveUp()
        {
            if (Jumping != -1)
            {
                var collision = CollisionCheck("T");
                if (collision != null && (bool)collision)
                {
                    Canvas.SetTop(player, Y + 20);
                    Y = Canvas.GetTop(player);
                    Jumping = -1;
                }
                else if (collision != null && !(bool)collision)
                {
                    Canvas.SetTop(player, Y - Jumping);
                    Y = Canvas.GetTop(player);
                    Jumping--;
                    if (Jumping < -1) 
                    {
                        Jumping = -1;
                    }
                }
            }
        }
        private void Jump()
        {
            if (Jumping == -1)
            {
                Jumping = 20;
            }
        }

        private bool? CollisionCheck(string dir)
        {
            if (dir == "B")
            {
                var plyrX1 = Canvas.GetLeft(player);
                var plyrX2 = Canvas.GetLeft(player) + W;
                var plyrY = Canvas.GetTop(player) + H;

                foreach (var rectangle in canvas.Children)
                {
                    var rect = (System.Windows.UIElement)rectangle;
                    if (rect != player)
                    {
                        if (Canvas.GetLeft(rect) + 96 >= plyrX1 && Canvas.GetLeft(rect) <= plyrX2 && Canvas.GetTop(rect) - 1 == plyrY)
                        {
                            return null;
                        }
                        else if (Canvas.GetLeft(rect) + 96 >= plyrX1 && Canvas.GetLeft(rect) <= plyrX2 && (Canvas.GetTop(rect) <= plyrY && Canvas.GetTop(rect) + 96 > plyrY - H))
                        {
                            return true;
                        }
                    }
                }
                return false;
            }

            if (dir == "L")
            {
                var plyrX1 = Canvas.GetLeft(player);
                var plyrX2 = Canvas.GetLeft(player) + W;
                var plyrY1 = Canvas.GetTop(player);
                var plyrY2 = Canvas.GetTop(player) + H;

                foreach (var rectangle in canvas.Children)
                {
                    var rect = (System.Windows.UIElement)rectangle;
                    if (rect != player)
                    {
                        if (Canvas.GetTop(rect) <= plyrY2 && Canvas.GetTop(rect) + 96 >= plyrY1 && Canvas.GetLeft(rect) + 97 == plyrX1)
                        {
                            return null;
                        }
                        else if (Canvas.GetTop(rect) <= plyrY2 && Canvas.GetTop(rect) + 96 >= plyrY1 && Canvas.GetLeft(rect) + 96 >= plyrX1 && Canvas.GetLeft(rect) <= plyrX2)
                        {
                            return true;
                        }
                    }
                }
            }

            if (dir == "R")
            {
                var plyrX1 = Canvas.GetLeft(player);
                var plyrX2 = Canvas.GetLeft(player) + W;
                var plyrY = Canvas.GetTop(player) + H;

                foreach (var rectangle in canvas.Children)
                {
                    var rect = (System.Windows.UIElement)rectangle;
                    if (rect != player)
                    {
                        if (Canvas.GetLeft(rect) - 1 == plyrX2 && (Canvas.GetTop(rect) <= plyrY && Canvas.GetTop(rect) + 96 > plyrY - H))
                        {
                            return null;
                        }
                        else if (Canvas.GetLeft(rect) >= plyrX1 && Canvas.GetLeft(rect) <= plyrX2 && (Canvas.GetTop(rect) <= plyrY && Canvas.GetTop(rect) + 96 > plyrY - H))
                        {
                            return true;
                        }
                    }
                }
            }

            if (dir == "T")
            {
                var plyrX1 = Canvas.GetLeft(player);
                var plyrX2 = Canvas.GetLeft(player) + W;
                var plyrY = Canvas.GetTop(player);

                foreach (var rectangle in canvas.Children)
                {
                    var rect = (System.Windows.UIElement)rectangle;
                    if (rect != player)
                    {
                        if (Canvas.GetLeft(rect) + 96 >= plyrX1 && Canvas.GetLeft(rect) <= plyrX2 && (Canvas.GetTop(rect) <= plyrY && Canvas.GetTop(rect) + 96 >= plyrY))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

    }
}
