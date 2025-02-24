using Draw.src.GUI;
using Draw.src.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Draw.src.Services
{
    internal class ImageService
    {
        //DoubleBufferedPanel viewPort;
        /// <summary>
        /// Bitmap - масив от пиксели ,който специфицира цвета на всички пиксели в квадратна форма
        /// </summary>
       
   
        public static void SaveCanvasAsImage(Control viewPort)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "PNG Image|*.png|JPEG Image|*.jpeg|All Files|*.*";
                saveFileDialog.Title = "Запази изображението";
                saveFileDialog.FileName = "untitled";

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
    }
}
