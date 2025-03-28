﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Частицы
{
    public class Particle
    {
        public float Life;
        public int Radius;
        public float X;
        public float Y;

        public float SpeedX;
        public float SpeedY;

        public static Random rand = new Random();
        //public Action<Rectangle> OnRectangleOverlap;

        public Particle()
        {
            var direction = rand.Next(360); 
            var speed = 1 + rand.Next(10); 
            SpeedX = (float)(Math.Cos(direction / 180 * Math.PI) * speed); 
            SpeedY = -(float)(Math.Sin(direction / 180 * Math.PI) * speed); 
            Radius = 2 + rand.Next(10); 
            Life = 100; 
        }
        public virtual void Draw(Graphics g)
        {
            float k = Math.Min(1f, Life / 100);
            int alpha = (int)(k * 255);
            var color = Color.FromArgb(alpha, Color.White);
            var b = new SolidBrush(color);
            g.FillEllipse(b, X - Radius, Y - Radius, Radius * 2, Radius * 2);

            b.Dispose();
        }
    }
    public class ParticleColorful : Particle
    {
        public override void Draw(Graphics g)
        {
            float k = Math.Min(1f, Life / 100);
            int alpha = (int)(k * 255);
            var color = Color.FromArgb(alpha, Color.White);
            var b = new SolidBrush(color);

            g.FillEllipse(b, X - Radius, Y - Radius, Radius * 2, Radius * 2);

            b.Dispose();
        }

    }
}
