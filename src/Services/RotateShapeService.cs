using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Draw.src.Services
{
    internal class RotateShapeService
    {
        private readonly Control viewPort;
        private readonly DialogProcessor dialogProcessor;

        public RotateShapeService(Control _viewPort, DialogProcessor _dialogProcessor)
        { 
            viewPort = _viewPort;
            dialogProcessor = _dialogProcessor;
            viewPort.MouseWheel += ViewPortMouseWheel;

        }

        public void ViewPortMouseWheel(object sender, MouseEventArgs e)
        {
            if (dialogProcessor.Selection != null)
            {
                dialogProcessor.Selection.RotationAngle += e.Delta > 0 ? 5 : -5;

                viewPort.Invalidate();
            }
        }

    }
}
