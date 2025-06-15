using System.Drawing.Drawing2D;
using System.Drawing;
using System;
using Draw;


public class TestFigure : Shape
{
    public TestFigure(RectangleF rect) : base(rect) 
    {
    
    }
public override void DrawSelf(Graphics grfx)
{

    using (Matrix matrix = new Matrix())
    {
        base.DrawSelf(grfx);

        // Центърът на кръга (въртенето става около него)
        float centerX = Rectangle.X + Rectangle.Width / 2f;
        float centerY = Rectangle.Y + Rectangle.Height / 2f;
        PointF center = new PointF(centerX, centerY);

        // Въртим около центъра на фигурата
        matrix.RotateAt(RotationAngle, center);
        grfx.Transform = matrix;

        // Радиус - взимаме половината от по-малката страна, за да не излиза от правоъгълника
        float radius = Math.Min(Rectangle.Width, Rectangle.Height) / 2f;

        // Рисуваме запълнен кръг
        using (Brush fillBrush = new SolidBrush(FillColor))
        {
            grfx.FillEllipse(fillBrush, Rectangle);
        }

        // Рисуваме контур на кръга
        grfx.DrawEllipse(Pens.Black, Rectangle);

        // Рисуваме линиите, разделящи кръга на 6 сектора
        using (Pen linePen = new Pen(Color.Black, 1))
        {
            // преписване на всеки един градос
            double angle = (Math.PI * 4) / 6; // 60 градуса = π/3 радиана
            

            // Изчисляваме края на линията от центъра на кръга
            float x = centerX + (float)(radius * Math.Cos(angle));
            float y = centerY + (float)(radius * Math.Sin(angle));


            grfx.DrawLine(linePen, center, new PointF(x, y));
        }

        // Възстановяваме трансформацията
        grfx.ResetTransform();
    }
}
}
