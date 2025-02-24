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

        public EditShapeForm(Shape shape)
        {
            if (shape == null)
            {
                MessageBox.Show("Грешка: Подадената фигура е null!", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            InitializeComponent();
            this.editShapeName = shape.ShapeName;//error
            this.editShapeOutlineColor = shape.OutlineColor;
            this.editOutlineTickness = shape.OutlineTickness;
            this.editShapeFillerColor = shape.FillColor;

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
