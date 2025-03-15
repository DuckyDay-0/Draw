using System;
using System.Drawing;

namespace Draw
{
    /// <summary>
    /// Класът правоъгълник е основен примитив, който е наследник на базовия Shape.
    /// </summary>
    public class EllipseShape : Shape
    {
        #region Constructor

        //public RectangleF Boundaries {  get; set; }

        public EllipseShape(RectangleF ellipse) : base(ellipse)
        {
            //Boundaries = ellipse;
        }

        public EllipseShape(RectangleShape rectangle) : base(rectangle)
        {
        }


        #endregion

        /// <summary>
        /// Проверка за принадлежност на точка point към правоъгълника.
        /// В случая на правоъгълник този метод може да не бъде пренаписван, защото
        /// Реализацията съвпада с тази на абстрактния клас Shape, който проверява
        /// дали точката е в обхващащия правоъгълник на елемента (а той съвпада с
        /// елемента в този случай).
        /// </summary>
        
        public override bool Contains(PointF point)
        {           
            //Намираме центъра на елипсата
           float h = Rectangle.X + Rectangle.Width / 2;
           float k = Rectangle.Y + Rectangle.Height / 2;

            //Намиране на полуоси
           float a = Rectangle.Width / 2;
           float b = Rectangle.Height / 2;

           //Изчисленията
           float value = (float)(Math.Pow((point.X - h) / a, 2)) + (float)(Math.Pow((point.Y - k) / b, 2));

            return value <= 1;
        }

        /// <summary>
        /// Частта, визуализираща конкретния примитив.
        /// </summary>
        public override void DrawSelf(Graphics grfx)
        {
            base.DrawSelf(grfx);
            grfx.TranslateTransform(Rectangle.X + Width / 2, Rectangle.Y + Height / 2);
            grfx.RotateTransform(RotationAngle);
            grfx.TranslateTransform(-Rectangle.X - Width / 2, -Rectangle.Y - Height / 2);
            grfx.FillEllipse(new SolidBrush(FillColor), Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);
            grfx.DrawEllipse(new Pen(OutlineColor,OutlineTickness), Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);
            grfx.ResetTransform();
        }
    }
}
