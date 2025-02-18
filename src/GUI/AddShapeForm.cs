using System;
using System.Drawing;
using System.Windows.Forms;

namespace Draw
{
    public partial class AddShapeForm : Form
    {

        public string shapeName { get; set; }
        public Color shapeOutlineColor { get; set; }
        public Color shapeFillerColor { get; set; }

        public AddShapeForm()
        {
            InitializeComponent();
        }

        private void OnBtnShapeOutlineColor(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    shapeOutlineColor = colorDialog.Color;
                    button3.BackColor = shapeOutlineColor;  
                }
            }
        }

        private void OnBtnShapeFillerColor(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                { 
                    shapeFillerColor = colorDialog.Color;
                    button2.BackColor = shapeFillerColor;
                }
            }
        }

        private void OnBtnOK(object sender, EventArgs e)
        {
            shapeName = textBox1.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void OnBtnCancel(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
