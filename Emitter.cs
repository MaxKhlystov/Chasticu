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
        List<Particle> particles = new List<Particle>();
        public List<Rectangle> Rectangles = new List<Rectangle>();
        public int MousePositionX;
        public int MousePositionY;
        public float TargetX; // X-координата цели (курсора)
        public float TargetY;
        public int X; // координата X центра эмиттера, будем ее использовать вместо MousePositionX
        public int Y; // соответствующая координата Y 
        public int Direction = 0; // вектор направления в градусах куда сыпет эмиттер
        public int Spreading = 360; // разброс частиц относительно Direction
        public int SpeedMin = 1; // начальная минимальная скорость движения частицы
        public int SpeedMax = 10; // начальная максимальная скорость движения частицы
        public int RadiusMin = 10; // минимальный радиус частицы
        public int RadiusMax = 10; // максимальный радиус частицы
        public int LifeMin = 20; // минимальное время жизни частицы
        public int LifeMax = 100; // максимальное время жизни частицы
        public int ParticlesPerTick = 1;

        public Color ColorFrom = Color.White; // начальный цвет частицы
        public Color ColorTo = Color.FromArgb(0, Color.Black); // конечный цвет частиц

        public virtual Particle CreateParticle()
        {
            var particle = new Particle(X, Y, 0)
            {
                ColorFrom = this.ColorFrom,
                ColorTo = this.ColorTo
            };
            return particle;
        }
        public void UpdateState()
         {
             int particlesToCreate = ParticlesPerTick;

             foreach (var particle in particles.ToList())
             {
                 /*foreach (var rect in Rectangles)
                 {
                     if (rect.Overlap(particle))
                     {
                         particle.Life = 0;
                         break;
                     }
                 }*/
                 if (particle.Life <= 0)
                 {
                     particles.Remove(particle);
                 }
                 else
                 {
                     particle.X += particle.SpeedX;
                     particle.Y += particle.SpeedY;
                     particle.Life -= 1;
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
            particle.Life = 70;
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
            var speed = Particle.rand.Next(SpeedMin, SpeedMax);
            particle.SpeedX = dx * speed;
            particle.SpeedY = dy * speed;

            particle.Radius = Particle.rand.Next(RadiusMin, RadiusMax);
        }


    }
    class TopEmitter : Emitter
    {
        public int Width; // длина экрана

        public override void ResetParticle(Particle particle)
        {
            base.ResetParticle(particle); // вызываем базовый сброс частицы, там жизнь переопределяется и все такое

            // а теперь тут уже подкручиваем параметры движения
            particle.X = Particle.rand.Next(Width); // позиция X -- произвольная точка от 0 до Width
            particle.Y = 0;  // ноль -- это верх экрана 

            particle.SpeedY = 0; // падаем вниз по умолчанию
            //particle.SpeedX = Particle.rand.Next(-2, 2); // разброс влево и вправа у частиц 
        }
    }
}
