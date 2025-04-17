using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Частицы
{
    class Tank : BaseObject
    {
        public int HP { get; private set; } = 100;
        public float Speed { get; set; } = 5f;
        private readonly Pen outlinePen = new Pen(Color.White, 3);

        public Tank(float x, float y, float angle) : base(x, y, angle) { }

        public override void Render(Graphics g)
        {
            // Сохраняем настройки
            var state = g.Save();

            // Применяем трансформации
            g.TranslateTransform(X, Y);
            g.RotateTransform(Angle);

            // 1. Рисуем корпус (100x60 пикселей)
            g.FillRectangle(Brushes.DarkGreen, -50, -30, 100, 60);
            g.DrawRectangle(outlinePen, -50, -30, 100, 60);

            // 2. Рисуем башню (круг диаметром 60 пикселей)
            g.FillEllipse(Brushes.Green, -30, -30, 60, 60);
            g.DrawEllipse(outlinePen, -30, -30, 60, 60);

            // 3. Рисуем дуло (50x12 пикселей)
            g.FillRectangle(Brushes.DarkGreen, 0, -6, 50, 12);
            g.DrawRectangle(outlinePen, 0, -6, 50, 12);

            // 4. Отображаем HP
            g.DrawString($"HP: {HP}", new Font("Arial", 16), Brushes.DeepPink, -40, -40);

            // Восстанавливаем настройки
            g.Restore(state);
        }

        public override GraphicsPath GetGraphicsPath()
        {
            var path = new GraphicsPath();
            path.AddRectangle(new RectangleF(-50, -30, 100, 60)); // Корпус
            path.AddEllipse(-30, -30, 60, 60); // Башня
            return path;
        }

        public void TakeDamage(int damage)
        {
            HP = Math.Max(0, HP - damage);
        }

        public void MoveForward()
        {
            X += (float)Math.Cos(Angle * Math.PI / 180) * Speed;
            Y += (float)Math.Sin(Angle * Math.PI / 180) * Speed;
        }

        public void MoveBackward()
        {
            X -= (float)Math.Cos(Angle * Math.PI / 180) * Speed;
            Y -= (float)Math.Sin(Angle * Math.PI / 180) * Speed;
        }

        public void RotateLeft()
        {
            Angle -= 3f;
        }

        public void RotateRight()
        {
            Angle += 3f;
        }
        public PointF GetGunPosition()
        {
            // Длина дула + небольшой отступ (50 + 10)
            float distance = 60;
            float gunX = X + (float)Math.Cos(Angle * Math.PI / 180) * distance;
            float gunY = Y + (float)Math.Sin(Angle * Math.PI / 180) * distance;

            return new PointF(gunX, gunY);
        }
    }
}