using System;
using System.Drawing;
using System.Windows.Forms;

namespace Draw
{
    public partial class AddShapeForm : Form
    {

        public string shapeName { get; set; }
        public Color shapeOutlineColor  { get; set; } = Color.Black;
        public float outlineTickness { get; set; } = 1;
        public Color shapeFillerColor { get; set; } = Color.White;

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
                    if (shapeOutlineColor != null)
                    {
                        shapeOutlineColor = colorDialog.Color;
                        button3.BackColor = shapeOutlineColor;
                    }
                    else
                    {
                        shapeOutlineColor = Color.Black;
                        button3.BackColor = shapeOutlineColor;
                    }
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
            ValidateShapeName(shapeName);
            if (!ValidateNumericUpDown(outlineTickness))
            {
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void OnBtnCancel(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        public void ValidateShapeName(string shapeName)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                textBox1.Text = "defaultName";
            }
           
            this.shapeName = textBox1.Text;
        }

        public bool ValidateNumericUpDown(float numericUpDown)
        {
            if (numericUpDown1.Value < 1 || numericUpDown1.Value > 50)
            {
                MessageBox.Show("Дебелината на контура трябва да бъде между 1 и 50!", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            
            numericUpDown = (float)numericUpDown1.Value;
            return true;
        }
    }
}
