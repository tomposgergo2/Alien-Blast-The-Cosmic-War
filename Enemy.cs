using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AlienBlast
{
    class Enemy
    {
        private Rectangle enemy;
        private Canvas canvas;
        private double X;
        private double Y;
        private double StartX;
        private double EndX;
        private double Speed = 3;
        private bool MovingRight = true; // Jobbra indul alapból

        public Enemy(Canvas canvas, List<(double, double)> path)
        {
            this.canvas = canvas;

            if (path.Count == 0)
                return;

            X = path[0].Item1;
            Y = path[0].Item2;
            StartX = X;
            EndX = path[path.Count - 1].Item1; // Az ellenség az útvonal végéig mozog

            enemy = new Rectangle
            {
                Width = 96,
                Height = 96,
                Fill = new ImageBrush
                {
                    ImageSource = new BitmapImage(new Uri("Enemy.png", UriKind.Relative))
                },
                Tag = "Enemy"
            };

            Canvas.SetLeft(enemy, X);
            Canvas.SetTop(enemy, Y);
            canvas.Children.Add(enemy);
        }

public void Move(object sender, EventArgs e)
{
    if (enemy == null) return;

    if (MovingRight)
    {
        X += Speed;
        if (X >= EndX) MovingRight = false;
    }
    else
    {
        X -= Speed;
        if (X <= StartX) MovingRight = true;
    }

    Canvas.SetLeft(enemy, X);
    UpdateSpriteDirection();
}


        private void UpdateSpriteDirection()
        {
            if (enemy.Fill is ImageBrush imageBrush)
            {
                TransformGroup transformGroup = new TransformGroup();
                if (!MovingRight)
                {
                    transformGroup.Children.Add(new ScaleTransform(-1, 1, 48, 48)); // Tükrözés balra
                }
                imageBrush.Transform = transformGroup;
            }
        }

        public Rectangle GetRectangle()
        {
            return enemy;
        }

        public void Die(int currentLevel, HashSet<int> killedEnemies)
        {
            killedEnemies.Add(currentLevel); // Elmentjük, hogy ezen a pályán meghalt az ellenség
            canvas.Children.Remove(enemy);
            enemy = null;
        }

    }
}
