using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Draw.src.Model
{
    internal class NewShape : Shape
    {
        public NewShape(RectangleF rect) : base(rect) 
        {
        }


        public override bool Contains(PointF point)
        {
            return base.Contains(point);
        }
        public override void DrawSelf(Graphics grfx)
        {
            base.DrawSelf(grfx);
            grfx.TranslateTransform(Rectangle.X + Width / 2, Rectangle.Y + Height / 2);
            grfx.RotateTransform(RotationAngle);
            grfx.TranslateTransform(-Rectangle.X - Width / 2, -Rectangle.Y - Height / 2);
            grfx.FillRectangle(new SolidBrush(FillColor), Rectangle.X, Rectangle.Y , Rectangle.Width, Rectangle.Height);
            grfx.DrawRectangle(new Pen(OutlineColor, OutlineTickness), Rectangle.X , Rectangle.Y, Rectangle.Width, Rectangle.Height);
            grfx.ResetTransform();
        }
    }
}
