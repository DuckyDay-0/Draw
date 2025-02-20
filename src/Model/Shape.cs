using System;
using System.Drawing;
using System.Windows.Forms;


namespace Draw
{
	/// <summary>
	/// Базовия клас на примитивите, който съдържа общите характеристики на примитивите.
	/// </summary>
	public abstract class Shape
	{
		#region Constructors
		
		public Shape()
		{
		}
		
		public Shape(RectangleF rect)
		{
			rectangle = rect;
		}

        public Shape(Shape shape)
        {
            this.ShapeName = shape.ShapeName;
            this.Height = shape.Height;
            this.Width = shape.Width;
            this.Location = shape.Location;
            this.OutlineColor = shape.OutlineColor;
			this.OutlineTickness = shape.OutlineTickness;
            this.FillColor = shape.FillColor;
            this.rectangle = shape.rectangle;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Обхващащ правоъгълник на елемента.
        /// </summary>
        private RectangleF rectangle;		
		public virtual RectangleF Rectangle {
			get { return rectangle; }
			set { rectangle = value; }
		}

		private string shapeName;
		public virtual string ShapeName
		{
			get { return shapeName; }
			set { shapeName = value; }
		}


		private Color outlineColor = Color.Black;
		public virtual Color OutlineColor
		{
			get { return outlineColor; }
			set { outlineColor = value; }
		}

		private float outlineTickness;
		public virtual float OutlineTickness
		{
			get { return outlineTickness; }
			set { outlineTickness = value ; }
		}

		/// <summary>
		/// Широчина на елемента.
		/// </summary>
		public virtual float Width {
			get { return Rectangle.Width; }
			set { rectangle.Width = value; }
		}
		
		/// <summary>
		/// Височина на елемента.
		/// </summary>
		public virtual float Height {
			get { return Rectangle.Height; }
			set { rectangle.Height = value; }
		}
		
		/// <summary>
		/// Горен ляв ъгъл на елемента.
		/// </summary>
		public virtual PointF Location {
			get { return Rectangle.Location; }
			set { rectangle.Location = value; }
		}
		
		/// <summary>
		/// Цвят на елемента.
		/// </summary>
		private Color fillColor;		
		public virtual Color FillColor {
			get { return fillColor; }
			set { fillColor = value; }
		}

		public float RotationAngle { get; set; } = 0;
		#endregion
		

		/// <summary>
		/// Проверка дали точка point принадлежи на елемента.
		/// </summary>
		/// <param name="point">Точка</param>
		/// <returns>Връща true, ако точката принадлежи на елемента и
		/// false, ако не пренадлежи</returns>
		public virtual bool Contains(PointF point)
		{
			return Rectangle.Contains(point.X, point.Y);
		}
		
		/// <summary>
		/// Визуализира елемента.
		/// </summary>
		/// <param name="grfx">Къде да бъде визуализиран елемента.</param>
		public virtual void DrawSelf(Graphics grfx)
		{
			// shape.Rectangle.Inflate(shape.BorderWidth, shape.BorderWidth);
		}
		
	}
}
