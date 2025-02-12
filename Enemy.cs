using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace AlienBlast
{
    class Enemy
    {
        private Rectangle enemy;
        private Canvas canvas;
        private List<(double, double)> path;
        private int currentIndex = 0;
        private int direction = 1; // 1 előre, -1 vissza
        private double speed = 4;
        private DispatcherTimer timer;
        public void Die(int currentLevel, HashSet<int> killedEnemies)
        {
            killedEnemies.Add(currentLevel); // Elmentjük, hogy ezen a pályán meghalt az ellenség
            canvas.Children.Remove(enemy); // Eltávolítjuk az ellenséget a pályáról
            enemy = null; // Null-ra állítjuk, hogy ne mozduljon tovább
        }

        public Enemy(Canvas canvas, List<(double, double)> path)
        {
            this.canvas = canvas;
            this.path = path;

            if (path.Count == 0)
                return;

            enemy = new Rectangle
            {
                Width = 96,
                Height = 96,
                Fill = Brushes.Red,
                Tag = "Enemy"
            };

            Canvas.SetLeft(enemy, path[0].Item1);
            Canvas.SetTop(enemy, path[0].Item2);
            canvas.Children.Add(enemy);
        }

        // VISSZAADJA AZ ELLENSÉGET MINT RECTANGLE
        public Rectangle GetRectangle()
        {
            return enemy;
        }


        public void Move(object sender, EventArgs e)
        {
            if (enemy == null || path.Count == 0)
                return; // Ha az enemy meghalt, ne mozogjon tovább

            double targetX = path[currentIndex].Item1;
            double currentX = Canvas.GetLeft(enemy);

            if (Math.Abs(currentX - targetX) < speed)
            {
                currentIndex += direction;
                if (currentIndex >= path.Count || currentIndex < 0)
                {
                    direction *= -1;
                    currentIndex += direction;
                }
            }
            else
            {
                Canvas.SetLeft(enemy, currentX + (direction * speed));
            }
        }

    }
}
