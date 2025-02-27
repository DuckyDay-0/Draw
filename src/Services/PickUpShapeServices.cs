using System;
using System.Collections.Generic;
using System.Drawing;
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

        Color originalOutlineColor;
        public PickUpShapeServices(DialogProcessor dialogProcessor, StatusStrip statusBar, Control viewPort,ToolStripButton pickUpSpeedButton)
        { 
            _dialogProcessor = dialogProcessor;
            _statusBar = statusBar;
            _viewPort = viewPort;
            _pickUpSpeedButton = pickUpSpeedButton;

            _viewPort.MouseUp += ViewPortMouseUp;
        }

       
        /// <summary>
        /// При кликване на бутона селектираме примитив ,запазваме цвета на контура му и го променяме на 
        /// червен да подчертае ,че е селектиран
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ViewPortMouseDownOnPickUpBtn(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            var selectedShape = _dialogProcessor.ContainsPoint(e.Location);

            if (_pickUpSpeedButton.Checked)
            {
               // _dialogProcessor.Selection = _dialogProcessor.ContainsPoint(e.Location);
                if (selectedShape != null)
                {
                    _statusBar.Items[0].Text = "Последно действие: Селекция на примитив";
                    _dialogProcessor.LastLocation = e.Location;
                    _dialogProcessor.IsDragging = true;

                    originalOutlineColor = selectedShape.OutlineColor;
                    selectedShape.OutlineColor = Color.Red;
                    _dialogProcessor.Selection = selectedShape;

                    _viewPort.Invalidate();
                }

            }
            else
            {
                _pickUpSpeedButton.Checked = false;
            }
        }



        /// <summary>
        /// При вдигане на натиснатия бутон на мишката връщаме оригиналния цвят на контура 
        /// да подчертаем ,че вече не е селектиран
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewPortMouseUp(object sender, MouseEventArgs e)
        {
           
            if (_dialogProcessor.Selection != null)
            {
                _dialogProcessor.Selection.OutlineColor = originalOutlineColor;
            }
            _viewPort.Invalidate();

        }
    }
}
