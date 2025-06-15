using System;
using System.Drawing;
using System.Windows.Forms;

namespace Draw
{
    public partial class AddShapeForm : Form
    {

        public string shapeName { get; set; }
        public Color shapeOutlineColor { get; set; } = Color.Black;
        public float outlineTickness { get; set; }
        public Color shapeFillerColor { get; set; } = Color.White;
        public int shapeTransparency { get; set; } = 255;

        private TrackBar trackBar;
        //добави видимост на контур или цвят
        public AddShapeForm()
        {
            InitializeComponent();
            OnTrackBarTransperancy();
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

        private void OnTrackBarTransperancy()
        {
            transparency.Minimum = 0;
            transparency.Maximum = 100;
            transparency.Value = 100; // Започва напълно видимо
            transparency.TickFrequency = 10;
            transparency.Scroll += TransparencyScroll; // Свързваме събитието
        }


        private void TransparencyScroll(object sender, EventArgs e)
        {
            // Преобразуваме процента (0-100) в алфа канал (0-255)
            shapeTransparency = (int)(255 * (transparency.Value / 100.0));

            // Задаваме новата прозрачност на цветовете
            shapeFillerColor = Color.FromArgb(shapeTransparency, shapeFillerColor.R, shapeFillerColor.G, shapeFillerColor.B);
            shapeOutlineColor = Color.FromArgb(shapeTransparency, shapeOutlineColor.R, shapeOutlineColor.G, shapeOutlineColor.B);
        }
        private void OnBtnOK(object sender, EventArgs e)
        {
            ValidateShapeName(shapeName);
            if (!ValidateNumericUpDown(outlineTickness))
            {
                return;
            }
            else
            {
                outlineTickness = (float)numericUpDown1.Value;
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
