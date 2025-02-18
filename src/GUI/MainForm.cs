using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Draw
{
	/// <summary>
	/// Върху главната форма е поставен потребителски контрол,
	/// в който се осъществява визуализацията
	/// </summary>
	public partial class MainForm : Form
	{
		/// <summary>
		/// Агрегирания диалогов процесор във формата улеснява манипулацията на модела.
		/// </summary>
		private DialogProcessor dialogProcessor = new DialogProcessor();
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}

		/// <summary>
		/// Изход от програмата. Затваря главната форма, а с това и програмата.
		/// </summary>
		void ExitToolStripMenuItemClick(object sender, EventArgs e)
		{
			Close();
		}
		
		/// <summary>
		/// Събитието, което се прихваща, за да се превизуализира при изменение на модела.
		/// </summary>
		void ViewPortPaint(object sender, PaintEventArgs e)
		{
			dialogProcessor.ReDraw(sender, e);
		}
		
		/// <summary>
		/// Бутон, който поставя на произволно място правоъгълник със зададените размери.
		/// Променя се лентата със състоянието и се инвалидира контрола, в който визуализираме.
		/// </summary>
		void DrawRectangleSpeedButtonClick(object sender, EventArgs e)
		{
            using (AddShapeForm addShapeForm = new AddShapeForm())
            {
                if (addShapeForm.ShowDialog() == DialogResult.OK)
                {
                    string shapeName = addShapeForm.shapeName;
                    Color outline = addShapeForm.shapeOutlineColor;
                    Color filler = addShapeForm.shapeFillerColor;

                    dialogProcessor.AddRandomRectangle(shapeName, outline, filler);
                    statusBar.Items[0].Text = "Последно действие: Рисуване на правоъгълник";

                    viewPort.Invalidate();
                }
            }           
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        void DrawEllipseSpeedButtonClick(object sender, EventArgs e)
        {
			using (AddShapeForm addShapeForm = new AddShapeForm())
			{
				if (addShapeForm.ShowDialog() == DialogResult.OK)
				{
					string shapeName = addShapeForm.shapeName;
					Color outlineColor = addShapeForm.shapeOutlineColor;
					Color fillerColor = addShapeForm.shapeFillerColor;

                    dialogProcessor.AddRandomEllipse(shapeName, outlineColor, fillerColor);

                    statusBar.Items[0].Text = "Последно действие: Рисуване на елипса";

                    viewPort.Invalidate();
                }
            }
        }

        private void DrawCircleButtonClick(object sender, EventArgs e)
        {
			using (AddShapeForm addShapeForm = new AddShapeForm())
			{
				if (addShapeForm.ShowDialog() == DialogResult.OK)
				{
					string shapeName = addShapeForm.shapeName;
					Color outlineColor = addShapeForm.shapeOutlineColor;
					Color fillerColor = addShapeForm.shapeFillerColor;

					dialogProcessor.AddRandomCircle(shapeName, outlineColor, fillerColor);

                    statusBar.Items[0].Text = "Последно действие: Рисуване на кръг";

                    viewPort.Invalidate();
                }
			}
        }

        private void DrawTriangleButtonClick(object sender, EventArgs e)
        {
			using (AddShapeForm addShapeForm = new AddShapeForm())
			{
				if (addShapeForm.ShowDialog() == DialogResult.OK)
				{ 
					string shapeName = addShapeForm.shapeName;
					Color outline = addShapeForm.shapeOutlineColor;
					Color filler = addShapeForm.shapeFillerColor;

					dialogProcessor.AddRandomTriangle(shapeName, outline, filler);
					statusBar.Items[0].Text = "Последно действие: Рисуване на триъгълник";
					viewPort.Invalidate();
				}
			}
        }
        /// <summary>
        /// Прихващане на координатите при натискането на бутон на мишката и проверка (в обратен ред) дали не е
        /// щракнато върху елемент. Ако е така то той се отбелязва като селектиран и започва процес на "влачене".
        /// Промяна на статуса и инвалидиране на контрола, в който визуализираме.
        /// Реализацията се диалогът с потребителя, при който се избира "най-горния" елемент от екрана.
        /// </summary>
        void ViewPortMouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (pickUpSpeedButton.Checked) {
				dialogProcessor.Selection = dialogProcessor.ContainsPoint(e.Location);
				if (dialogProcessor.Selection != null) {
					statusBar.Items[0].Text = "Последно действие: Селекция на примитив";
					dialogProcessor.IsDragging = true;
					dialogProcessor.LastLocation = e.Location;
					viewPort.Invalidate();
				}
			}
			else if (dialogProcessor.IsInDeleteState)
			{
                dialogProcessor.Selection = dialogProcessor.ContainsPoint(e.Location);

                if (dialogProcessor.Selection != null)
                {
                    statusBar.Items[0].Text = "Последно действие: Изтриване";
                    dialogProcessor.DeleteSelectedShape();
                    viewPort.Refresh();
                }
            }
		}
        void ViewPortDeleteButtonOn(object sender, System.Windows.Forms.MouseEventArgs e)
        {
			if (deleteButton.Checked)
			{
				statusBar.Items[0].Text = "Режим на триене : Вкл";
				dialogProcessor.IsInDeleteState = true;
			}
			else
			{ 
				dialogProcessor.IsInDeleteState = false;
			}
        }

        /// <summary>
        /// Прихващане на преместването на мишката.
        /// Ако сме в режм на "влачене", то избрания елемент се транслира.
        /// </summary>
        void ViewPortMouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (dialogProcessor.IsDragging) {
				if (dialogProcessor.Selection != null) statusBar.Items[0].Text = "Последно действие: Влачене";
				dialogProcessor.TranslateTo(e.Location);
				viewPort.Refresh();
			}
		}

		/// <summary>
		/// Прихващане на отпускането на бутона на мишката.
		/// Излизаме от режим "влачене".
		/// </summary>
		void ViewPortMouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			dialogProcessor.IsDragging = false;
			//dialogProcessor.IsInDeleteState = false;
		}

        /// <summary>
        /// Bitmap - масив от пиксели ,който специфицира цвета на всички пиксели в квадратна форма
        /// </summary>

        private void SaveCanvasAsImage()
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "PNG Image|*.png";
				saveFileDialog.Filter = "JPEG Image|*.jpeg";
                saveFileDialog.Title = "Запази изображението";
                saveFileDialog.FileName = "untitled.png";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {

                    using (Bitmap bitmap = new Bitmap(viewPort.Width, viewPort.Height))
                    {
                        viewPort.DrawToBitmap(bitmap, new Rectangle(0, 0, viewPort.Width, viewPort.Height));
                        bitmap.Save(saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
                    }

                    MessageBox.Show("Изображението е запазено успешно!", "Запазване", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void SaveAsImageButton(object sender, EventArgs e)
        {
			SaveCanvasAsImage();
        }

		/// <summary>
		/// Изтрива всички примитиви в ShapeList след това обновява екрана
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
       
        private void ClearCanvasButton(object sender, EventArgs e)
        {
            dialogProcessor.ClearShapeList();
            viewPort.Refresh();
        }
    }
}
