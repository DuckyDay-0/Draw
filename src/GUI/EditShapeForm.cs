using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Draw.src.GUI
{
    public partial class EditShapeForm : Form
    {
        public string editShapeName { get; set; } = "";
        public Color editShapeOutlineColor { get; set; } = Color.Black;
        public float editOutlineTickness { get; set; } = 1;
        public Color editShapeFillerColor { get; set; } = Color.White;
        public int editShapeTransperancy { get; set; } = 255;

        private void EditShapeOutlineCollor(object sender ,EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                { 
                    this.editShapeOutlineColor = colorDialog.Color;
                    button3.BackColor = editShapeOutlineColor;
                }

            }
        }

        private void EditShapeFillerCollor(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    this.editShapeFillerColor = colorDialog.Color;
                    button2.BackColor = editShapeFillerColor;
                }
            }
        }

        private void OnEditTrackBarTransparency()
        {
            editTransparency.Minimum = 0;
            editTransparency.Maximum = 100;
            editTransparency.Value = 100;
            editTransparency.TickFrequency = 10;
            editTransparency.Scroll += Transparency_Scroll;
        }

        private void Transparency_Scroll(object sender, EventArgs e)
        {
            editShapeTransperancy = (int)(255 * (editTransparency.Value / 100.0));

            editShapeOutlineColor = Color.FromArgb(editShapeTransperancy, editShapeOutlineColor.R, editShapeOutlineColor.G, editShapeOutlineColor.B);
            editShapeFillerColor = Color.FromArgb(editShapeTransperancy, editShapeFillerColor.R, editShapeFillerColor.G, editShapeFillerColor.B);

        }

        public EditShapeForm(Shape shape)
        {
            if (shape == null)
            {
                MessageBox.Show("Грешка: Подадената фигура е null!", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            InitializeComponent();
            OnEditTrackBarTransparency();

            this.editShapeName = shape.ShapeName;//error
            this.editShapeOutlineColor = shape.OutlineColor;
            this.editOutlineTickness = shape.OutlineTickness;
            this.editShapeFillerColor = shape.FillColor;
            this.editShapeTransperancy = shape.Transparency;

            textBox1.Text = this.editShapeName;
            button3.BackColor = this.editShapeOutlineColor;
            button2.BackColor = this.editShapeFillerColor;
            numericUpDown1.Value = (decimal)this.editOutlineTickness;
            
        }

        private void OnBtnOK(object sender, EventArgs e)
        {
            this.editShapeName = textBox1.Text;
            editOutlineTickness = (float)numericUpDown1.Value;
            DialogResult = DialogResult.OK;
            Close();
        }

        
    }
}
