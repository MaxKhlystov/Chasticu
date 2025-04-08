
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

        public float Width;
        public float Height;
        public Color Color;

        Random rand = new Random();

        public Rectangle(float x, float y, float angle) : base(x, y, angle)
        {
            Width = rand.Next(50, 101);
            Height = rand.Next(30, 101);
            Color = Color.Red;
        }

        public override void Render(Graphics g)
        {
            using (var brush = new SolidBrush(this.Color))
            {
                // Правильные параметры: x, y, width, height
                g.FillRectangle(brush, X - Width / 2, Y - Height / 2, Width, Height);
            }
        }
        public override GraphicsPath GetGraphicsPath()
        {
            var path = new GraphicsPath();
            path.AddRectangle(new RectangleF(-Width / 2, -Height / 2, Width, Height));
            return path;
        }
    }
}
