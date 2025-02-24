using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Draw.src.Services
{
    internal class PickUpShapeServices
    {
        DialogProcessor _dialogProcessor;
        StatusStrip _statusBar;
        Control _viewPort;
        ToolStripButton _pickUpSpeedButton;

        public PickUpShapeServices(DialogProcessor dialogProcessor, StatusStrip statusBar, Control viewPort,ToolStripButton pickUpSpeedButton)
        { 
            _dialogProcessor = dialogProcessor;
            _statusBar = statusBar;
            _viewPort = viewPort;
            _pickUpSpeedButton = pickUpSpeedButton;
        }

        public void ViewPortMouseDownOnPickUpBtn(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (_pickUpSpeedButton.Checked)
            {
                _dialogProcessor.Selection = _dialogProcessor.ContainsPoint(e.Location);
                if (_dialogProcessor.Selection != null)
                {
                    _statusBar.Items[0].Text = "Последно действие: Селекция на примитив";
                    _dialogProcessor.LastLocation = e.Location;
                    _dialogProcessor.IsDragging = true;
                    _viewPort.Invalidate();
                }
            }
            else
            {
                _pickUpSpeedButton.Checked = false;
            }
        }
    }
}
