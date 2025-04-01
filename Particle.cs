using System;
using System.Drawing;

namespace Частицы
{
    public class Particle : BaseObject
    {
        public float Life;
        public int Radius;
        public float X;
        public float Y;

        public float SpeedX;
        public float SpeedY;
        public Color ColorFrom = Color.White; // Добавляем свойства цвета
        public Color ColorTo = Color.FromArgb(0, Color.Black);

        public static Random rand = new Random();
        
        public Particle(float x, float y, float angle) : base(x, y, angle)
        {
            var direction = rand.Next(360); 
            var speed = 10; 
            SpeedX = (float)(Math.Cos(direction / 180 * Math.PI) * speed); 
            SpeedY = -(float)(Math.Sin(direction / 180 * Math.PI) * speed); 
            Radius = 2 + rand.Next(10); 
            Life = 100;
        }

        public virtual void Draw(Graphics g)
        {
            float k = Math.Min(1f, Life / 100);
            
            // Интерполяция цвета
            var color = Color.FromArgb(
                (int)(ColorFrom.A + (ColorTo.A - ColorFrom.A) * (1 - k)),
                (int)(ColorFrom.R + (ColorTo.R - ColorFrom.R) * (1 - k)),
                (int)(ColorFrom.G + (ColorTo.G - ColorFrom.G) * (1 - k)),
                (int)(ColorFrom.B + (ColorTo.B - ColorFrom.B) * (1 - k)));

            using (var b = new SolidBrush(color))
            {
                g.FillEllipse(b, X - Radius, Y - Radius, Radius * 2, Radius * 2);
            }
        }
    }
}