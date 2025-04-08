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
        public Color ColorParticle = Color.White; 

        public static Random rand = new Random();
        
        public Particle(float x, float y, float angle) : base(x, y, angle)
        {
            var direction = rand.Next(360); 
            var speed = 10; 
            SpeedX = (float)(Math.Cos(direction / 180 * Math.PI) * speed); 
            SpeedY = -(float)(Math.Sin(direction / 180 * Math.PI) * speed); 
            Radius = 2 + rand.Next(10); 
            Life = 200;
        }

        public virtual void Draw(Graphics g)
        {
            var color = ColorParticle;

            using (var b = new SolidBrush(color))
            {
                g.FillEllipse(b, X - Radius, Y - Radius, Radius * 2, Radius * 2);
            }
        }
    }
}