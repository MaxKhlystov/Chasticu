using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Частицы
{
    public class Particle : BaseObject
    {
        public float Life;
        public int Radius;
        public float X;
        public float Y;
        public Color ColorParticle = Color.White;
        public Action <Particle> OnRectangleOverlap;

        public Particle(float x, float y) : base(x, y, 0)
        {
            Radius = 10; 
        }

        public virtual void Draw(Graphics g)
        {
            var color = ColorParticle;

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