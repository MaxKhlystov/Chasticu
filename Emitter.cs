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
        public List<IImpactPoint> impactPoints = new List<IImpactPoint>();
        public List<Particle> particles = new List<Particle>();
        public List<Rectangle> Rectangles = new List<Rectangle>();
        public Func<Particle> CreateParticle;
        public int MousePositionX;
        public int MousePositionY;
        public float TargetX; // X-координата цели (курсора)
        public float TargetY;
        public int X; // координата X центра эмиттера, будем ее использовать вместо MousePositionX
        public int Y; // соответствующая координата Y 
        public int Direction = 0; // вектор направления в градусах куда сыпет эмиттер
        public int Spreading = 360; // разброс частиц относительно Direction
        public int Speed = 10; // начальная максимальная скорость движения частицы
        public int Radius = 10; // максимальный радиус частицы
        public int Life = 100; // максимальное время жизни частицы
        public int ParticlesPerTick = 1;

        public Color ColorFrom = Color.White; // начальный цвет частицы 
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
                        particle.X += particle.SpeedX;
                        particle.Y += particle.SpeedY;

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
        public abstract class IImpactPoint
        {
            public float X;
            public float Y;

            // абстрактный метод с помощью которого будем изменять состояние частиц
            // например притягивать
            public abstract void ImpactParticle(Particle particle);

            // базовый класс для отрисовки точечки
            public virtual void Render(Graphics g)
            {
                g.FillEllipse(
                        new SolidBrush(Color.Red),
                        X - 5,
                        Y - 5,
                        10,
                        10
                    );
            }
        }
        public int ParticlesCount = 500;
        public virtual void ResetParticle(Particle particle)
        {
            particle.Life = 200;
            particle.X = X;
            particle.Y = Y;

            // Рассчитываем направление к курсору
            float dx = TargetX - X;
            float dy = TargetY - Y;
            float distance = (float)Math.Sqrt(dx * dx + dy * dy);

            // Нормализуем вектор направления (чтобы скорость была постоянной)
            if (distance > 0)
            {
                dx /= distance;
                dy /= distance;
            }

            // Добавляем небольшой случайный разброс (Spreading)
            float spreadFactor = (float)(Particle.rand.NextDouble() * Spreading - Spreading / 2) / 100f;
            dx += spreadFactor;
            dy += spreadFactor;

            // Устанавливаем скорость частицыф
            var speed = Speed;
            particle.SpeedX = dx * speed;
            particle.SpeedY = dy * speed;

            particle.Radius = Radius;
        }


    }
}
