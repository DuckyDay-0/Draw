using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace Draw.src.Services
{
    internal class DeleteShapeServices
    {
        private readonly DialogProcessor dialogProcessor;
        private readonly StatusStrip statusBar;
        private readonly Control viewPort;

        public DeleteShapeServices(DialogProcessor _dialogProcessor, StatusStrip _statusBar,Control _viewPort)
        { 
            dialogProcessor = _dialogProcessor;
            statusBar = _statusBar;
            viewPort = _viewPort;
        }
        public void ViewPortMouseDownOnDeleteBtn(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (dialogProcessor.IsInDeleteState)
            {

                dialogProcessor.Selection = dialogProcessor.ContainsPoint(e.Location);

                if (dialogProcessor.Selection != null)
                {
                    statusBar.Items[0].Text = "Последно действие: Изтриване";
                    DeleteSelectedShape(dialogProcessor.Selection);
                    viewPort.Refresh();
                }
            }
        }

        public virtual void DeleteSelectedShape(Shape selection)
        {
            if (selection != null)
            {
                dialogProcessor.ShapeList.Remove(selection);        
                dialogProcessor.Selection = null;
            }

        }
    }
}
