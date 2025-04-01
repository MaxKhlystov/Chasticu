using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Частицы
{
    class Rectangle : BaseObject
    {
        public new float X;
        public new float Y;
        public float Width;
        public float Height;
        public Color Color;
        public Action<Particle> OnParticleOverlap;

        public Rectangle(float x, float y, float angle) : base(x, y, angle)
        {
            Width = 100;
            Height = 500;
            Color = Color.Red;
        }

        public override void Render(Graphics g)
        {
            using (var brush = new SolidBrush(this.Color))
            {
                g.FillRectangle(brush,- Width / 2,- Height / 2, Width, Height);
            }
        }
        public override GraphicsPath GetGraphicsPath()
        {
            var path = new GraphicsPath();
            path.AddRectangle(new RectangleF(-Width / 2, -Height / 2, Width, Height));
            return path;
        }
        public override void Overlap(BaseObject obj)
        {
            base.Overlap(obj);
            if (obj is Particle particle)
            {
                OnParticleOverlap?.Invoke(particle);
            }
        }

    }
}
