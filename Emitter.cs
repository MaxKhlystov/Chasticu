using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Частицы.Emitter;

namespace Частицы
{
    class Emitter 
    {
        public List<Particle> particles = new List<Particle>();
        public List<Rectangle> Rectangles = new List<Rectangle>();
        public Func<Particle> CreateParticle;
        public int CanvasWidth { get; set; } = 800; 
        public int CanvasHeight { get; set; } = 600;
        public int X; // координата X центра эмиттера, будем ее использовать вместо MousePositionX
        public int Y; // соответствующая координата Y 
        public int Radius = 10; // максимальный радиус частицы
        public int Life = 25; // максимальное время жизни частицы
        public int ParticlesPerTick = 1;
        public Random rand = new Random();
        public void UpdateState()
        {
            using (var g = Graphics.FromImage(new Bitmap(1, 1))) // Создаем временный Graphics
            {
                foreach (var particle in particles.ToList())
                {
                    if (particle.Life <= 0)
                    {
                        particles.Remove(particle);
                    }
                    else
                    {
                        foreach (var rect in Rectangles)
                        {
                            if (particle.Overlaps(rect, g)) // Используем Overlaps с Graphics
                            {
                                particle.Life=0;
                                break;
                            }
                            if (particle.OnRectangleOverlap != null)
                            {
                                particle.OnRectangleOverlap(particle);
                            }
                        }
                    }
                }
            }
        }
        public void Render(Graphics g)
        {
            foreach (var rect in Rectangles)
            {
                rect.Render(g);
            }

            foreach (var particle in particles)
            {
                particle.Draw(g);
            }
        }
        public void EmitParticles(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var particle = CreateParticle();
                ResetParticle(particle);
                particles.Add(particle);
            }
        }
        public int ParticlesCount = 500;
        public virtual void ResetParticle(Particle particle)
        {
            particle.Life = 25;
            particle.Radius = 10;
            particle.X = rand.Next(0, CanvasWidth);  // Используем свойства класса
            particle.Y = rand.Next(0, CanvasHeight);
            particle.ColorParticle = rand.Next(2) == 0 ? Color.Yellow : Color.Red;
        }
    }
}
