using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
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
        private int BugProtector { get; set; } = 0;
        public List<int> Collected { get; set; }
        private Canvas canvas { get; set; }
        private System.Windows.Shapes.Rectangle player { get; set; }
        private string Dir { get; set; } = "R";
        private string Idle
        {
            get
            {
                string playerDirectory = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "player");
                string imagePath = Directory.GetFiles(playerDirectory, "idle.png").FirstOrDefault();
                return imagePath;
            }
        }
        private string Air
        {
            get
            {
                string playerDirectory = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "player");
                string imagePath = Directory.GetFiles(playerDirectory, "fall.png").FirstOrDefault();
                return imagePath;
            }
        }
        private string[] Walk
        {
            get
            {
                string playerDirectory = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "player");
                playerDirectory = System.IO.Path.Combine(playerDirectory, "walk");
                string[] imagePath = Directory.GetFiles(playerDirectory);
                return imagePath;
            }
        }
        private int _walkState = 0;
        private int WalkState
        {
            get => _walkState;
            set
            {
                _walkState = value;
                if (_walkState >= Walk.Length) _walkState = 0;
            }
        }

        public Player(double x, double y, List<int> Collected, Canvas canvas, double v = 10, double w = 90, double h = 90)
        {
            X = x;
            Y = y;
            Velocity = v;
            this.canvas = canvas;
            player = DrawPlayer(X, Y);
            W = w;
            H = h;
            this.Collected = Collected;
            ((canvas.Children.OfType<StackPanel>()).First().Children.OfType<TextBlock>()).First().Text = Collected.Sum().ToString();
        }

        private System.Windows.Shapes.Rectangle DrawPlayer(double x, double y)
        {
            var player = new System.Windows.Shapes.Rectangle
            {
                Width = 96,
                Height = 96,
                Fill = new ImageBrush
                {
                    ImageSource = new BitmapImage(new Uri(Idle))
                }
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
                    if (BugProtector < 30)
                    {
                        Canvas.SetTop(player, Y - 5);
                        Y = Canvas.GetTop(player);
                        G = 1;
                        BugProtector++;
                    }
                    else
                    {
                        MessageBox.Show("A csalás nem szép dolog!", "CSALÓ!", MessageBoxButton.OK, MessageBoxImage.Error);
                        BugProtector = 0;
                        Y = 2000;
                    }
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
                else
                {
                    BugProtector = 0;
                }
            }
        }

        public void MovePlayer()
        {
            Animation("I");
            if (Keyboard.IsKeyDown(Key.Up) || Keyboard.IsKeyDown(Key.W))
            {
                Jump();
            }
            if (Keyboard.IsKeyDown(Key.Right) || Keyboard.IsKeyDown(Key.D))
            {
                Animation("R");
                MoveRight();
                Dir = "R";
            }
            else if (Keyboard.IsKeyDown(Key.Left) || Keyboard.IsKeyDown(Key.A))
            {
                Animation("L");
                MoveLeft();
                Dir = "L";
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
                Animation("A");
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
                Animation("A");
                Jumping = 20;
            }
        }

        private void Animation(string dir)
        {
            if (dir == "A")
            {
                player.Fill = new ImageBrush
                {
                    ImageSource = new BitmapImage(new Uri(Air))
                };
            }
            
            if (dir == "I")
            {
                if (Jumping == -1 && Dir == "R")
                {
                    player.Fill = new ImageBrush
                    {
                        ImageSource = new BitmapImage(new Uri(Idle))
                    };
                }
                else if (Jumping == -1 && Dir == "L")
                {
                    player.Fill = new ImageBrush
                    {
                        ImageSource = new BitmapImage(new Uri(Idle)),
                        Transform = new ScaleTransform(-1, 1, 50, 50)
                    };
                }
                else if (Dir == "R")
                {
                    player.Fill = new ImageBrush
                    {
                        ImageSource = new BitmapImage(new Uri(Air))
                    };
                }
                else if (Dir == "L")
                {
                    player.Fill = new ImageBrush
                    {
                        ImageSource = new BitmapImage(new Uri(Air)),
                        Transform = new ScaleTransform(-1, 1, 50, 50)
                    };
                }
            }
            else if (dir == "R")
            {
                if (Jumping == -1)
                {
                    player.Fill = new ImageBrush
                    {
                        ImageSource = new BitmapImage(new Uri(Walk[WalkState]))
                    };
                    WalkState++;
                }
                else
                {
                    player.Fill = new ImageBrush
                    {
                        ImageSource = new BitmapImage(new Uri(Air))
                    };
                }
            }
            else if (dir == "L")
            {
                if (Jumping == -1)
                {
                    player.Fill = new ImageBrush
                    {
                        ImageSource = new BitmapImage(new Uri(Walk[WalkState])),
                        Transform = new ScaleTransform(-1, 1, 50, 50)
                    };
                    WalkState++;
                }
                else
                {
                    player.Fill = new ImageBrush
                    {
                        ImageSource = new BitmapImage(new Uri(Air)),
                        Transform = new ScaleTransform(-1, 1, 50, 50)
                    };
                }
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
                    if (rectangle is System.Windows.Shapes.Rectangle rect && rect.Tag is char tag && tag == '1')
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
                    if (rectangle is System.Windows.Shapes.Rectangle rect && rect.Tag is char tag && tag == '1')
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
                    if (rectangle is System.Windows.Shapes.Rectangle rect && rect.Tag is char tag && tag == '1')
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
                    if (rectangle is System.Windows.Shapes.Rectangle rect && rect.Tag is char tag && tag == '1')
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


        public List<int> CheckForCoinCollection()
        {
            TextBlock ErmeSzamlalo = ((canvas.Children.OfType<StackPanel>()).First().Children.OfType<TextBlock>()).First();
            foreach (var child in canvas.Children.OfType<Image>())
            {
                if (child.Tag.ToString() == "2")
                {
                    var coinLeft = Canvas.GetLeft(child);
                    var coinTop = Canvas.GetTop(child);
                    var coinRect = new Rect(coinLeft, coinTop, child.Width, child.Height);

                    var playerRect = new Rect(X, Y, W, H);

                    if (coinRect.IntersectsWith(playerRect))
                    {
                        canvas.Children.Remove(child);
                        //int currentCoinCount = int.Parse(ErmeSzamlalo.Text);
                        //ErmeSzamlalo.Text = (currentCoinCount + 1).ToString();
                        Collected.Add(1);
                        ErmeSzamlalo.Text = Collected.Sum().ToString();
                        return Collected;
                    }
                }
            }

            return Collected;
        }
        public System.Windows.Shapes.Rectangle GetRectangle()
        {
            return player;
        }
        private bool isRespawning = false;


        public void CheckForEnemyCollision(List<Enemy> enemies, int currentLevel, HashSet<int> killedEnemies)
        {
            if (isRespawning) return;

            foreach (var enemy in enemies)
            {
                System.Windows.Shapes.Rectangle enemyRect = enemy.GetRectangle();

                if (enemyRect == null) // 🔥 Ha az ellenség már törölve lett, ne folytassuk
                {
                    continue;
                }

                double enemyX = Canvas.GetLeft(enemyRect);
                double enemyY = Canvas.GetTop(enemyRect);
                double enemyW = enemyRect.Width;
                double enemyH = enemyRect.Height;

                Rect playerRect = new Rect(X, Y, W, H);
                Rect enemyBounds = new Rect(enemyX, enemyY, enemyW, enemyH);

                if (playerRect.IntersectsWith(enemyBounds))
                {
                    if ((Y + H - 10) <= enemyY) // Ha a Player az Enemy tetején van (finomhangolt)
                    {
                        enemy.Die(currentLevel, killedEnemies);
                        Jumping = 15; // A játékos visszapattan
                    }
                    else
                    {
                        isRespawning = true;
                        Kill();
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                Kill();
                                X = X * 96;
                                Y = Y * 96;
                                Player player = new Player(X, Y, Collected, canvas);
                            });
                    }
                    break;
                }
            }
        }



        public bool IsTouchingPortal(char portalType)
        {
            double plyrX = Canvas.GetLeft(player);
            double plyrY = Canvas.GetTop(player);
            double plyrW = player.Width;
            double plyrH = player.Height;

            foreach (var child in canvas.Children)
            {
                if (child is System.Windows.Shapes.Rectangle rect && rect.Tag is char tag && tag == portalType)
                {
                    double rectX = Canvas.GetLeft(rect);
                    double rectY = Canvas.GetTop(rect);
                    double rectW = rect.Width;
                    double rectH = rect.Height;

                    if (plyrX + plyrW > rectX && plyrX < rectX + rectW &&
                        plyrY + plyrH > rectY && plyrY < rectY + rectH)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
