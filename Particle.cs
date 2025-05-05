using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Частицы
{
    public class Particle : BaseObject
    {
        public int Life;
        public int Radius;
        public Color ColorParticle = Color.White;
        public float Opacity = 1.0f;
        public float DX; // Скорость по X
        public float DY;

        public Particle(float x, float y) : base(x, y, 0)
        {
            Radius = 10;
            X = x;
            Y = y;
        }

        public virtual void Draw(Graphics g)
        {
            // Ограничиваем Opacity в пределах 0.0-1.0
            float clampedOpacity = Math.Max(0f, Math.Min(1f, Opacity));

            // Преобразуем в диапазон 0-255 с проверкой границ
            int alpha = (int)(clampedOpacity * 255);
            alpha = Math.Max(0, Math.Min(255, alpha)); // Дополнительная проверка

            var color = Color.FromArgb(alpha, ColorParticle);
            using (var b = new SolidBrush(color))
            {
                g.FillEllipse(b, X - Radius, Y - Radius, Radius * 2, Radius * 2);
            }
        }
        public override bool Overlaps(BaseObject obj, Graphics g)
        {
            var path1 = this.GetGraphicsPath();
            path1.Transform(this.GetTransform());

            var path2 = obj.GetGraphicsPath();
            path2.Transform(obj.GetTransform());

            using (var region = new Region(path1))
            {
                region.Intersect(path2);
                return !region.IsEmpty(g);
            }
        }
        public override GraphicsPath GetGraphicsPath()
        {
            var path = new GraphicsPath();
            path.AddEllipse(-Radius, -Radius, Radius * 2, Radius * 2);
            return path;
        }
    }
}