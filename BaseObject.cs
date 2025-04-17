using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Частицы
{
    public class BaseObject
    {
        public float X;
        public float Y;
        public float Angle;
        public Action<BaseObject, BaseObject> OnOverlap;
        public BaseObject(float x, float y, float angle)
        {
            X = x;
            Y = y;
            Angle = angle;
        }

        public Matrix GetTransform()
        {
            var matrix = new Matrix();
            matrix.Translate(X, Y);
            matrix.Rotate(Angle);
            return matrix;
        }
        public virtual void Render(Graphics g)
        {

        }

        public virtual GraphicsPath GetGraphicsPath()
        {
            return new GraphicsPath();
        }

        public virtual bool Overlaps(BaseObject obj, Graphics g)
        {
            // Получаем границы частицы
            var particleBounds = this.GetGraphicsPath().GetBounds();
            particleBounds.X += this.X - particleBounds.Width / 2;
            particleBounds.Y += this.Y - particleBounds.Height / 2;

            // Получаем границы прямоугольника
            var rectBounds = obj.GetGraphicsPath().GetBounds();
            rectBounds.X += obj.X - rectBounds.Width / 2;
            rectBounds.Y += obj.Y - rectBounds.Height / 2;

            // Проверяем пересечение прямоугольников
            return particleBounds.IntersectsWith(rectBounds);
        }

        public virtual void Overlap(BaseObject obj)
        {
            if (this.OnOverlap != null)
            {
                this.OnOverlap(this, obj);
            }
        }
    }
}
