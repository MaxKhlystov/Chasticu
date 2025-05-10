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
        public Func<Particle> CreateParticle;
        public int CanvasWidth { get; set; } = 800; 
        public int CanvasHeight { get; set; } = 600;
        public int X; // координата X центра эмиттера, будем ее использовать вместо MousePositionX
        public int Y; // соответствующая координата Y 
        public int Radius = 10; // максимальный радиус частицы
        public int Life = 200; // максимальное время жизни частицы
        public int ParticlesPerTick = 1;
        public Random rand = new Random();
        public void UpdateState(Snake snake = null)
        {
            foreach (var particle in particles.ToList())
            {
                particle.Life--;
                particle.Opacity = Math.Max(0f, particle.Life / 60.0f);

                particle.X += particle.DX;
                particle.Y += particle.DY;

                if (particle.Life <= 0)
                {
                    // Взрыв Coral частицы при исчезновении
                    if (particle.ColorParticle == Color.Coral)
                    {
                        CreateExplosion(particle.X, particle.Y, Color.Red); // Взрыв с красными частицами
                    }
                    particles.Remove(particle);
                }
            }
        }
        public void Render(Graphics g)
        {
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
                particles.Add(particle);
            }
        }
        public int ParticlesCount = 500;
        public virtual void ResetParticle(Particle particle)
        {
            particle.Life = 150;
            particle.Radius = 10;
            particle.X = rand.Next(0, CanvasWidth);  // Используем свойства класса
            particle.Y = rand.Next(0, CanvasHeight);
            particle.ColorParticle = rand.Next(2) == 0 ? Color.Yellow : Color.Red;
        }
        public void CreateExplosion(float x, float y, Color? color = null)
        {
            var explosionColor = color ?? Color.FromArgb(255, 144, 238, 144);

            for (int i = 0; i < 20; i++)
            {
                var particle = new Particle(x, y)
                {
                    Radius = 5,
                    Life = 60,
                    ColorParticle = explosionColor,
                    Opacity = 1.0f
                };

                float angle = (float)(i * (2 * Math.PI / 20));
                float speed = 2f;
                particle.X += (float)(Math.Cos(angle) * 10);
                particle.Y += (float)(Math.Sin(angle) * 10);

                particle.DX = (float)(Math.Cos(angle) * speed);
                particle.DY = (float)(Math.Sin(angle) * speed);

                particles.Add(particle);
            }
        }
    }
}
