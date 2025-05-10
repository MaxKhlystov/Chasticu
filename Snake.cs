// Snake.cs (Изменения для уровней сложности)
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Частицы
{
    class Snake : BaseObject
    {
        public List<Particle> Body { get; private set; } = new List<Particle>();
        public int Score { get; set; } = 0;
        private PointF targetPosition;
        private float baseSpeed = 3f;
        public bool IsBoosted { get; set; } = false; // Добавлено свойство для проверки ускорения
        private int followDistance = 15;
        private Direction currentDirection = Direction.None; // Добавлено
        public enum Direction
        {
            None,
            Up,
            Down,
            Left,
            Right
        }
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

        public float Speed { get; set; }

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
                head.X += dx / distance * Speed; // Используем Speed
                head.Y += dy / distance * Speed;
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
                    current.X += dx / distance * Speed; // Используем Speed
                    current.Y += dy / distance * Speed;
                }
            }
            CheckSelfCollision();
        }

        public bool CheckSelfCollision()
        {
            if (Body.Count < 5) return false;

            var head = Body[0];
            for (int i = 4; i < Body.Count; i++)
            {
                var segment = Body[i];
                float dx = head.X - segment.X;
                float dy = head.Y - segment.Y;
                float distance = (float)Math.Sqrt(dx * dx + dy * dy);

                if (distance < head.Radius + segment.Radius)
                {
                    return true;
                }
            }
            return false;
        }
        public bool CheckSelfCollisionHard() // Метод для проверки столкновений на сложном уровне
        {
            if (Body.Count < 5) return false; // Не проверяем для очень коротких змеек

            var head = Body[0];
            for (int i = 4; i < Body.Count; i++) // Начинаем проверку с 4-го сегмента
            {
                var segment = Body[i];
                float dx = head.X - segment.X;
                float dy = head.Y - segment.Y;
                float distance = (float)Math.Sqrt(dx * dx + dy * dy);

                if (distance < head.Radius + segment.Radius)
                {
                    return true;
                }
            }
            return false;
        }

        public void RemoveTailSegment()
        {
            if (Body.Count > 1) // Всегда оставляем хотя бы голову
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
            int kol = 1;
            if (particle.ColorParticle == Color.Yellow)
            {
                kol = 1;
                Score++;
                Grow(kol);
            }
            else if (particle.ColorParticle == Color.Red)
            {

            }
            else if (particle.ColorParticle == Color.LimeGreen)
            {
                kol = 2;
                Score += 2;
                Grow(kol);
            }
            else if (particle.ColorParticle == Color.Coral)
            {
                Score += 50;
                kol = 50;
                Grow(kol);
            }
            else if (particle.ColorParticle == Color.Cyan)
            {
                Score = Math.Max(0, Score - 1);
                if (Body.Count > 1) RemoveTailSegment();
            }
            else if (particle.ColorParticle == Color.FromArgb(255, 144, 238, 144))
            {
                Score = Math.Max(0, Score - 1);
                if (Body.Count > 1) RemoveTailSegment();
            }
        }


        public void Grow(int kol)
        {
            if (Body.Count == 0) return;

            var last = Body[Body.Count - 1];
            for (int i = 0; i < kol; i++)
            {
                Body.Add(new Particle(last.X, last.Y)
                {
                    ColorParticle = Color.LimeGreen,
                    Radius = 10
                });
            }
        }

        public override void Render(Graphics g)
        {
            for (int i = Body.Count - 1; i >= 0; i--) // Рисуем с хвоста к голове
            {
                var segment = Body[i];

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