using Draw.src.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Draw.src.Services
{
    internal class EditShapeServices
    {
        public static void ViewPortMouseDownOnEditBtn(object sender, System.Windows.Forms.MouseEventArgs e,Control viewPort,DialogProcessor dialogProcessor)
        {
            if (dialogProcessor.IsInEditState)
            {
                if (dialogProcessor.Selection != null)
                {
                    dialogProcessor.Selection = dialogProcessor.ContainsPoint(e.Location);
                    EditSelectedShape(dialogProcessor.Selection, viewPort);
                }
            }
        }


        public static void EditSelectedShape(Shape shape,Control viewPort)
        {
            if (shape != null)
            {
                using (EditShapeForm editForm = new EditShapeForm(shape))
                {
                    if (editForm.ShowDialog() == DialogResult.OK)
                    {
                        shape.ShapeName = editForm.editShapeName;
                        shape.OutlineColor = editForm.editShapeOutlineColor;
                        shape.FillColor = editForm.editShapeFillerColor;
                        shape.OutlineTickness = editForm.editOutlineTickness;
                        shape.Transparency = editForm.editShapeTransperancy;

                        viewPort.Invalidate();// Прерисуваме формата
                    }
                }
            }
        }

        


    }
}
