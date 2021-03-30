using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrillKOMPASUI
{
    public partial class Form1 : Form
    {
        private bool flagMaskedTextBoxL;
        private bool flagMaskedTextBox_l;
        private bool flagMaskedTextBoxD;
        private bool flagMaskedTextBox_a;
        private bool flagMaskedTextBox_b;
        private bool flagMaskedTextBox_c;
        private bool flagMaskedTextBox_d;

        public Form1()
        {
            InitializeComponent();
            flagMaskedTextBoxL = false;
            flagMaskedTextBox_l = false;
            flagMaskedTextBoxD = false;
            flagMaskedTextBox_a = false;
            flagMaskedTextBox_b = false;
            flagMaskedTextBox_c = false;
            flagMaskedTextBox_d = false;
        }

        private void maskedTextBoxL_TextChanged(object sender, EventArgs e)
        {
            flagMaskedTextBoxL = ValidateCorrectInput(maskedTextBoxL, 10, 30, flagMaskedTextBoxL, 5);
        }

        private void maskedTextBox_l_TextChanged(object sender, EventArgs e)
        {
            flagMaskedTextBox_l = ValidateCorrectInput(maskedTextBox_l, 3, 145, flagMaskedTextBox_l, 6);
        }

        private void maskedTextBoxD_TextChanged(object sender, EventArgs e)
        {
            flagMaskedTextBoxD = ValidateCorrectInput(maskedTextBoxD, 0.25, 22, flagMaskedTextBoxD, 5);
        }

        private void maskedTextBox_a_TextChanged(object sender, EventArgs e)
        {
            flagMaskedTextBox_a = ValidateCorrectInput(maskedTextBox_a, 0, 22, flagMaskedTextBox_a, 5);
        }

        private void maskedTextBox_b_TextChanged(object sender, EventArgs e)
        {
            flagMaskedTextBox_b = ValidateCorrectInput(maskedTextBox_b, 0, 22, flagMaskedTextBox_b, 5);
        }

        private void maskedTextBox_c_TextChanged(object sender, EventArgs e)
        {
            flagMaskedTextBox_c = ValidateCorrectInput(maskedTextBox_c, 0, 14, flagMaskedTextBox_c, 5);
        }

        private void maskedTextBox_d_TextChanged(object sender, EventArgs e)
        {
            flagMaskedTextBox_d = ValidateCorrectInput(maskedTextBox_d, 0, 10, flagMaskedTextBox_d, 5);
        }

        /// <summary>
        /// Проверка правильности заполнения полей
        /// </summary>
        /// <param name="maskedTextBox">поле параметра</param>
        /// <param name="min">минимальная граница</param>
        /// <param name="max">максимальная граница</param>
        /// <param name="flagMaskedTextBox">правильность заполнения</param>
        /// <param name="count">количество знаков в маске</param>
        private bool ValidateCorrectInput(Control maskedTextBox, double min, double max, bool flagMaskedTextBox, int count)
        {
            if (maskedTextBox.Text.Length != count)
            {
                maskedTextBox.BackColor = Color.LightCoral;
                return flagMaskedTextBox = false;
            }
            else
            {
                if (Convert.ToDouble(maskedTextBox.Text) < min 
                    || Convert.ToDouble(maskedTextBox.Text) > max)
                {
                    maskedTextBox.BackColor = Color.LightCoral;
                    toolTip.Show("Значение находится вне диапазона", maskedTextBox, 43, 17);
                    return flagMaskedTextBox = false;
                }
                else
                {
                    maskedTextBox.BackColor = Color.White;
                    return flagMaskedTextBox = true;
                }
            }
        }

        private void buttonBuild_Click(object sender, EventArgs e)
        {
            if (flagMaskedTextBoxL == false || flagMaskedTextBox_l == false
                                            || flagMaskedTextBoxD == false || flagMaskedTextBox_a == false
                                            || flagMaskedTextBox_b == false || flagMaskedTextBox_c == false
                                            || flagMaskedTextBox_d == false)
            {
                toolTip.Show("Проверьте правильность заполнения полей", buttonBuild, 160, 20);

                if (flagMaskedTextBoxL == false)
                    maskedTextBoxL.BackColor = Color.LightCoral;
                if (flagMaskedTextBox_l == false)
                    maskedTextBox_l.BackColor = Color.LightCoral;
                if (flagMaskedTextBoxD == false)
                    maskedTextBoxD.BackColor = Color.LightCoral;
                if (flagMaskedTextBox_a == false)
                    maskedTextBox_a.BackColor = Color.LightCoral;
                if (flagMaskedTextBox_b == false)
                    maskedTextBox_b.BackColor = Color.LightCoral;
                if (flagMaskedTextBox_c == false)
                    maskedTextBox_c.BackColor = Color.LightCoral;
                if (flagMaskedTextBox_d == false)
                    maskedTextBox_d.BackColor = Color.LightCoral;
            }
        }
    }
}
