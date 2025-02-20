using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Draw
{
    public class TriangleShape : Shape
    {
        public PointF A { get; set; }
        public PointF B { get; set; }
        public PointF C { get; set; }

        public TriangleShape(PointF a, PointF b, PointF c)
        {
            A = a;
            B = b;
            C = c;
        }

        public override bool Contains(PointF point)
        {
            return IsPointInTriangle(point, A, B, C);
        }

        public override void DrawSelf(Graphics grfx)
        {
            base.DrawSelf(grfx);

            using (SolidBrush brush = new SolidBrush(FillColor))
            {
                PointF[] points = { A, B, C };
                grfx.FillPolygon(brush, points);
                grfx.DrawPolygon(new Pen(OutlineColor, OutlineTickness), points);
            }
        }

        public override PointF Location
        {
            get { return A; } // Връщаме първия връх
            set
            {
                float dx = value.X - A.X;
                float dy = value.Y - A.Y;

                A = new PointF(A.X + dx, A.Y + dy);
                B = new PointF(B.X + dx, B.Y + dy);
                C = new PointF(C.X + dx, C.Y + dy);
            }
        }

        private bool IsPointInTriangle(PointF p, PointF a, PointF b, PointF c)
        {
            float areaOrig = TriangleArea(a, b, c);
            float area1 = TriangleArea(p, b, c);
            float area2 = TriangleArea(a, p, c);
            float area3 = TriangleArea(a, b, p);

            return Math.Abs(areaOrig - (area1 + area2 + area3)) < 0.01f;
        }

        private float TriangleArea(PointF a, PointF b, PointF c)
        {
            return Math.Abs((a.X * (b.Y - c.Y) + b.X * (c.Y - a.Y) + c.X * (a.Y - b.Y)) / 2.0f);
        }
    }
}
