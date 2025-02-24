using Draw.src.GUI;
using Draw.src.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
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
		private ToolTip toolTip = new ToolTip();
		private DeleteShapeServices deleteShapeServices;
		private PickUpShapeServices pickUpShapeServices;

		public MainForm()
		{
			InitializeComponent();
			deleteShapeServices = new DeleteShapeServices(dialogProcessor, statusBar, viewPort);
			pickUpShapeServices = new PickUpShapeServices(dialogProcessor, statusBar, viewPort,pickUpSpeedButton); ;
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
					float outlineTickness = addShapeForm.outlineTickness;
					Color filler = addShapeForm.shapeFillerColor;

					dialogProcessor.AddRandomRectangle(shapeName, outline, filler, outlineTickness);
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
					float outlineTickness = addShapeForm.outlineTickness;
					Color fillerColor = addShapeForm.shapeFillerColor;

					dialogProcessor.AddRandomEllipse(shapeName, outlineColor, fillerColor, outlineTickness);

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
					float outlineTickness = addShapeForm.outlineTickness;
					Color fillerColor = addShapeForm.shapeFillerColor;

					dialogProcessor.AddRandomCircle(shapeName, outlineColor, fillerColor, outlineTickness);

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
					float outlineTickness = addShapeForm.outlineTickness;
					Color filler = addShapeForm.shapeFillerColor;

					dialogProcessor.AddRandomTriangle(shapeName, outline, filler, outlineTickness);
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
			OnPickUpButton(sender, e);

			OnDeleteBtn(sender, e);

			OnEditBtn(sender, e);
		}



		void ViewPortDeleteButtonOn(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (deleteButton.Checked)
			{
				statusBar.Items[0].Text = "Режим на триене : Вкл";
				dialogProcessor.IsInDeleteState = true;

				toolStripButton4.Checked = false;
				pickUpSpeedButton.Checked = false;

				dialogProcessor.IsInEditState = false;
				dialogProcessor.IsDragging = false;
			}
			else
			{
				dialogProcessor.IsInDeleteState = false;
			}
		}



		private void EditShapeButton(object sender, EventArgs e)
		{
			if (toolStripButton4.Checked)
			{
				statusBar.Items[0].Text = "Режим на редактиране : Вкл";
				dialogProcessor.IsInEditState = true;


				deleteButton.Checked = false;
				pickUpSpeedButton.Checked = false;

				dialogProcessor.IsInDeleteState = false;
				dialogProcessor.IsDragging = false;
			}
			else
			{
				dialogProcessor.IsInEditState = false;
			}

		}


		/// <summary>
		/// Прихващане на преместването на мишката.
		/// Ако сме в режм на "влачене", то избрания елемент се транслира.
		/// </summary>
		void ViewPortMouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			Shape hoveredAboveShape = dialogProcessor.ContainsPoint(e.Location);

			if (hoveredAboveShape != null)
			{
				toolTip.SetToolTip(viewPort, hoveredAboveShape.ShapeName);
			}
			else
			{
				toolTip.SetToolTip(viewPort, "");
			}
			if (dialogProcessor.IsDragging)
			{
				if (dialogProcessor.Selection != null) statusBar.Items[0].Text = "Последно действие: Влачене";
				dialogProcessor.TranslateTo(e.Location);
				viewPort.Refresh();
			}
		}


		public void OnPickUpButton(object sender, MouseEventArgs e)
		{
			pickUpShapeServices.ViewPortMouseDownOnPickUpBtn(sender, e);
		}
        private void PickUpShapeButton(object sender, EventArgs e)
		{
			if (pickUpSpeedButton.Checked)
			{
				statusBar.Items[0].Text = "Режим на селекция : Вкл";

				deleteButton.Checked = false;
				toolStripButton4.Checked = false;

				dialogProcessor.IsInEditState = false;
				dialogProcessor.IsInDeleteState = false;
			}
			else
			{
                pickUpSpeedButton.Checked = false;
            }
		}



		/// <summary>
		/// Изтриване на селектираната фигура
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void OnDeleteBtn(object sender, System.Windows.Forms.MouseEventArgs e)
		{ 
			deleteShapeServices.ViewPortMouseDownOnDeleteBtn(sender, e);
		}


		/// <summary>
		/// Редакция на селектираната фигура
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void OnEditBtn(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			EditShapeServices.ViewPortMouseDownOnEditBtn(sender,e,viewPort,dialogProcessor);
		}
		
		/// <summary>
		/// Прихващане на отпускането на бутона на мишката.
		/// Излизаме от режим "влачене".
		/// </summary>
		void ViewPortMouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			dialogProcessor.IsDragging = false;
		}



        private void AboutInfoBtn(object sender, EventArgs e)
        {
			MessageBox.Show("asd");//info about project/tutorial 
        }


		/// <summary>
		/// Запазване на изображението като .png/.jpeg/all files формат
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void SaveAsImageButton(object sender, EventArgs e)
		{
			ImageService.SaveCanvasAsImage(viewPort);
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
