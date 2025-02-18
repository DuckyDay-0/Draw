using Draw;
using System.Drawing;

public class CircleShape : Shape
{
    public float Radius { get; set; }

    public CircleShape(RectangleF bounds, float radius) : base(bounds)
    {
        Radius = radius;
    }

    public CircleShape(CircleShape circle) : base(circle)
    {
        Radius = circle.Radius;
    }

    public PointF Center
    {
        get { return new PointF(Location.X + Radius, Location.Y + Radius); }
        set { Location = new PointF(value.X - Radius, value.Y - Radius); }
    }

    public override bool Contains(PointF point)
    {
        float dx = point.X - Center.X;
        float dy = point.Y - Center.Y;
        return dx * dx + dy * dy <= Radius * Radius;
    }

    public override void DrawSelf(Graphics grfx)
    {
        base.DrawSelf(grfx);
        grfx.FillEllipse(new SolidBrush(FillColor), Location.X, Location.Y, Radius * 2, Radius * 2);
        grfx.DrawEllipse(new Pen(OutlineColor), Location.X, Location.Y, Radius * 2, Radius * 2);
    }
}
