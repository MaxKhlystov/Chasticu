using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Частицы
{
    class Tank : BaseObject, IIntersection 
    {
        public Action<Rectangle> OnRectangleOverlap;
        public Action<Particle> OnParticleOverlap;
        public int HP = 100;
        public Tank(float x, float y, float angle) : base(x, y, angle)
        {

        }
        public override void Render(Graphics g)
        {
            g.DrawString($"HP: {HP}", new Font("Arial", 10), Brushes.Red, -30, -40);
            g.DrawRectangle(
                new Pen(Color.Black, 2),
                -35, -30, 70, 60
            );
            g.FillEllipse(
                new SolidBrush(Color.DeepSkyBlue),
                -25, -25, 50, 50
            );
            g.DrawEllipse(
                new Pen(Color.Black, 2),
                -25, -25, 50, 50
            );
            g.DrawLine(
                new Pen(Color.Black, 3),
                0, 0, 30, 0
            );
        }
        public override GraphicsPath GetGraphicsPath()
        {
            var path = new GraphicsPath();
            path.AddEllipse(-25, -25, 50, 50);
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
