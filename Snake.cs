// Snake.cs
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Частицы
{
    class Snake : BaseObject
    {
        public List<Particle> Body { get; private set; } = new List<Particle>();
        public int Score { get; private set; } = 0;
        private PointF targetPosition;
        private float speed = 3f;
        private int followDistance = 15;

        public Snake(float x, float y) : base(x, y, 0)
        {
            Body.Add(new Particle(x, y)
            {
                ColorParticle = Color.Green,
                Radius = 15
            });

            for (int i = 1; i < 5; i++)
            {
                Body.Add(new Particle(x - i * followDistance, y)
                {
                    ColorParticle = Color.LimeGreen,
                    Radius = 10
                });
            }
        }

        public void SetTarget(PointF target) => targetPosition = target;

        public void Move()
        {
            if (Body.Count == 0) return;

            // Движение головы
            var head = Body[0];
            float dx = targetPosition.X - head.X;
            float dy = targetPosition.Y - head.Y;
            float distance = (float)Math.Sqrt(dx * dx + dy * dy);

            if (distance > 0)
            {
                head.X += dx / distance * speed;
                head.Y += dy / distance * speed;
            }

            // Движение тела
            for (int i = 1; i < Body.Count; i++)
            {
                var prev = Body[i - 1];
                var current = Body[i];

                dx = prev.X - current.X;
                dy = prev.Y - current.Y;
                distance = (float)Math.Sqrt(dx * dx + dy * dy);

                if (distance > followDistance)
                {
                    current.X += dx / distance * speed;
                    current.Y += dy / distance * speed;
                }
            }

            // Проверка самопересечения
            CheckSelfCollision();
        }

        private void CheckSelfCollision()
        {
            if (Body.Count < 5) return; // Не проверяем для очень коротких змеек

            var head = Body[0];
            for (int i = 4; i < Body.Count; i++) // Начинаем проверку с 4-го сегмента
            {
                var segment = Body[i];
                float dx = head.X - segment.X;
                float dy = head.Y - segment.Y;
                float distance = (float)Math.Sqrt(dx * dx + dy * dy);

                if (distance < head.Radius + segment.Radius)
                {
                    RemoveTailSegment();
                    break;
                }
            }
        }

        private void RemoveTailSegment()
        {
            if (Body.Count > 1)
            {
                Body.RemoveAt(Body.Count - 1);
            }
        }

        public bool CheckCollision(Particle particle)
        {
            if (Body.Count == 0) return false;

            var head = Body[0];
            float dx = head.X - particle.X;
            float dy = head.Y - particle.Y;
            float distance = (float)Math.Sqrt(dx * dx + dy * dy);

            return distance < (head.Radius + particle.Radius);
        }

        public void ProcessParticleCollision(Particle particle, Emitter emitter)
        {
            if (particle.ColorParticle == Color.Yellow)
            {
                Score++;
                Grow();
            }
            else if (particle.ColorParticle == Color.Red)
            {
                Score = Math.Max(0, Score - 1);
                if (Body.Count > 1) RemoveTailSegment();
            }
            else if (particle.ColorParticle == Color.LimeGreen)
            {
                Score += 2;
                Grow();
                Grow();
             
                emitter.CreateExplosion(particle.X, particle.Y);
            }
        }

        public void Grow()
        {
            if (Body.Count == 0) return;

            var last = Body[Body.Count - 1];
            Body.Add(new Particle(last.X, last.Y)
            {
                ColorParticle = Color.LimeGreen,
                Radius = 10
            });
        }

        public override void Render(Graphics g)
        {
            for (int i = Body.Count - 1; i >= 0; i--) // Рисуем с хвоста к голове
            {
                var segment = Body[i];

                // Для головы используем более темный зеленый
                Color color = (i == 0) ? Color.Green : Color.LimeGreen;

                using (var brush = new SolidBrush(color))
                {
                    g.FillEllipse(brush,
                                 segment.X - segment.Radius,
                                 segment.Y - segment.Radius,
                                 segment.Radius * 2,
                                 segment.Radius * 2);
                }
            }

            // Рисуем счет над головой
            if (Body.Count > 0)
            {
                var head = Body[0];
                g.DrawString(Score.ToString(),
                            new Font("Arial", 12, FontStyle.Bold),
                            Brushes.White,
                            head.X - 10,
                            head.Y - 30);
            }
        }
        public bool CheckBorderCollision(int canvasWidth, int canvasHeight)
        {
            if (Body.Count == 0) return false;

            var head = Body[0];
            return head.X - head.Radius <= 0 ||
                   head.X + head.Radius >= canvasWidth ||
                   head.Y - head.Radius <= 0 ||
                   head.Y + head.Radius >= canvasHeight;
        }

        public override GraphicsPath GetGraphicsPath()
        {
            var path = new GraphicsPath();
            if (Body.Count > 0)
            {
                path.AddEllipse(Body[0].X - Body[0].Radius, Body[0].Y - Body[0].Radius,
                               Body[0].Radius * 2, Body[0].Radius * 2);
            }
            return path;
        }
    }
}