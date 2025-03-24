using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Частицы
{
    public class Rectangle
    {
        public float X;
        public float Y;
        public float Width;
        public float Height;
        public Color Color;


        public bool Contains(Particle particle)
        {
            return particle.X >= X &&
                   particle.X <= X + Width &&
                   particle.Y >= Y &&
                   particle.Y <= Y + Height;
        }

        public void Draw(Graphics g)
        {
            using (var brush = new SolidBrush(Color))
            {
                g.FillRectangle(brush, X, Y, Width, Height);
            }
        }
    }
}
