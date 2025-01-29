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
        public int X { get; set; }
        public int Y { get; set; }
        public int Velocity { get; set; }
        public string img { get; set; }
        public Canvas canvas { get; set; }

        public Player(int x, int y, Canvas canvas,int v = 1)
        {
            X = x;
            Y = y;
            Velocity = v;
        }

        private void DrawPlayer(int x, int y, string img)
        {  
            var player = new System.Windows.Shapes.Rectangle
            {
                Width = 96,
                Height = 96,
                Fill = Brushes.Blue
            };
            
        }

        public void MoveLeft()
        {
            X--;
        }
        public void MoveRight()
        {
            X++;
        }
    }
}
