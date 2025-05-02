// Human.cs
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Частицы
{
    class Human : BaseObject
    {
        public int HP { get; set; } = 100;
        public float Speed { get; set; } = 5f;
        private readonly float protectionRadius = 80f;

        public Human(float x, float y) : base(x, y, 0) 
        {
            X = x; 
            Y = y;
        }

        public override void Render(Graphics g)
        {
            g.FillEllipse(Brushes.LightGray, X - 15, Y - 30, 30, 30); // Голова
            g.DrawLine(new Pen(Color.LightGray, 3), X, Y, X, Y + 40);      // Тело
            g.DrawLine(new Pen(Color.LightGray, 3), X - 20, Y + 10, X + 20, Y + 10); // Руки
            g.DrawLine(new Pen(Color.LightGray, 3), X - 15, Y + 70, X, Y + 40);      // Ноги
            g.DrawLine(new Pen(Color.LightGray, 3), X + 15, Y + 70, X, Y + 40);
            // Отображаем HP
            g.DrawString($"HP: {HP}", new Font("Arial", 12), Brushes.Red, X - 20, Y - 50);
        }

        public override GraphicsPath GetGraphicsPath()
        {
            var path = new GraphicsPath();
            // Добавляем только тело человечка (без защитного круга)
            path.AddEllipse(-15, -30, 30, 30); // голова
            path.AddRectangle(new RectangleF(-3, 0, 6, 40)); // тело
            return path;
        }

        public void TakeDamage(int damage)
        {
            HP = Math.Max(0, HP - damage);
        }

        public void Move(float dx, float dy)
        {
            X += dx * Speed;
            Y += dy * Speed;
        }

        public bool IsInProtectionZone(float x, float y)
        {
            float dx = X - x;
            float dy = Y - y;
            return dx * dx + dy * dy <= protectionRadius * protectionRadius;
        }
    }
}
