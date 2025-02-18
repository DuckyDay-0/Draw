
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Draw
{
    /// <summary>
    /// Класът, който ще бъде използван при управляване на диалога.
    /// </summary>
    public class DialogProcessor : DisplayProcessor
    {
        #region Constructor
        
        public DialogProcessor()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Избран елемент.
        /// </summary>
        private Shape selection;
        public Shape Selection
        {
            get { return selection; }
            set { selection = value; }
        }

        /// <summary>
        /// Дали в момента диалога е в състояние на "влачене" на избрания елемент.
        /// </summary>
        private bool isDragging;
        public bool IsDragging
        {
            get { return isDragging; }
            set { isDragging = value; }
        }

        private bool isInDeleteState;
        public bool IsInDeleteState
        {
            get { return isInDeleteState; }
            set { isInDeleteState = value; }
        }

        /// <summary>
        /// Последна позиция на мишката при "влачене".
        /// Използва се за определяне на вектора на транслация.
        /// </summary>
        private PointF lastLocation;
        public PointF LastLocation
        {
            get { return lastLocation; }
            set { lastLocation = value; }
        }

        #endregion

        /// <summary>
        /// Добавя примитив - правоъгълник на произволно място върху клиентската област.
        /// </summary>
        public void AddRandomRectangle(string name,Color outline, Color filler)
        {
            Random rnd = new Random();
            
            int x = rnd.Next(100, 1000);
            int y = rnd.Next(100, 600);

            RectangleShape rect = new RectangleShape(new Rectangle(x, y, 100, 200));
            rect.ShapeName = name;
            rect.FillColor = filler;
            rect.OutlineColor = outline;
            

            ShapeList.Add(rect);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="shapeName"></param>
        /// <param name="outline"></param>
        /// <param name="filler"></param>
        public void AddRandomEllipse(string shapeName, Color outline, Color filler)
        {
            Random rnd = new Random();
            int x = rnd.Next(100, 1000);
            int y = rnd.Next(100, 600);

            EllipseShape ellipse = new EllipseShape(new Rectangle(x, y, 100, 200));
            ellipse.ShapeName = shapeName;
            ellipse.OutlineColor = outline;
            ellipse.FillColor = filler;

            ShapeList.Add(ellipse);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="shapeName"></param>
        /// <param name="outline"></param>
        /// <param name="filler"></param>
        public void AddRandomCircle(string shapeName, Color outline, Color filler)
        { 
            Random rnd = new Random();
            int x = rnd.Next(100,1000);
            int y = rnd.Next(100, 600);

            CircleShape circle = new CircleShape(new Rectangle(x - 100, y - 100, 200, 200), 100);
            circle.ShapeName = shapeName;
            circle.OutlineColor = outline;
            circle.FillColor = filler;

            ShapeList.Add(circle);
        }

        public void AddRandomTriangle(string shapeName, Color outline, Color filler)
        {
            // Дефиниране на координатите на върховете
            PointF A = new PointF(200, 100);
            PointF B = new PointF(150, 200);
            PointF C = new PointF(250, 200);

            // Създаване на триъгълника с фиксирани стойности
            TriangleShape triangle = new TriangleShape(A, B, C);
            triangle.ShapeName = shapeName;//Impement further
            triangle.FillColor = filler;
            triangle.OutlineColor = outline;

            // Добавяне в списъка с фигури
            ShapeList.Add(triangle);
        }

        /// <summary>
        /// Проверява дали дадена точка е в елемента.
        /// Обхожда в ред обратен на визуализацията с цел намиране на
        /// "най-горния" елемент т.е. този който виждаме под мишката.
        /// </summary>
        /// <param name="point">Указана точка</param>
        /// <returns>Елемента на изображението, на който принадлежи дадената точка.</returns>
        public Shape ContainsPoint(PointF point)
        {
            for (int i = ShapeList.Count - 1; i >= 0; i--)
            {
                if (ShapeList[i].Contains(point))
                {
                    //ShapeList[i].FillColor = Color.Red;

                    return ShapeList[i];
                }
            }
            return null;
        }

        /// <summary>
        /// Транслация на избраният елемент на вектор определен от <paramref name="p>p</paramref>
        /// </summary>
        /// <param name="p">Вектор на транслация.</param>
        public void TranslateTo(PointF p)
        {
            if (selection != null)
            {
                selection.Location = new PointF(selection.Location.X + p.X - lastLocation.X, selection.Location.Y + p.Y - lastLocation.Y);
                lastLocation = p;
            }
        }

        /// <summary>
        /// Изтрива селектирания примитив
        /// </summary>
        /// <param name="grfx"></param>
        /// <param name="item"></param>
        public virtual void DeleteSelectedShape()
        {
            if (selection != null)
            { 
                ShapeList.Remove(selection);
                Selection = null;
            }

        }

        public virtual void ClearShapeList()
        {
            if (ShapeList != null)
            {
                ShapeList.Clear();
            }
        }

       
    }
}
