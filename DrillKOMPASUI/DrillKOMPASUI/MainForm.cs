using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using KompasWrapper;
using DrillKOMPAS;

namespace DrillKOMPASUI
{
    public partial class MainForm : Form
    {
        private Class1 _kompsWrapper = new Class1();

        private DrillParameters _modelParameters;

        private bool drillLenghtFlag;
        private bool workingPartLenghtFlag;
        private bool drillDiameterFlag;
        private bool tenonLenghtFlag;
        private bool tenonWidthFlag;
        private bool neckLenghtFlag;
        private bool neckWidthFlag;

        public MainForm()
        {
            InitializeComponent();

            _modelParameters = new DrillParameters();
            
            drillLenghtFlag = false;
            workingPartLenghtFlag = false;
            drillDiameterFlag = false;
            tenonLenghtFlag = false;
            tenonWidthFlag = false;
            neckLenghtFlag = false;
            neckWidthFlag = false;
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

        /// <summary>
        /// Функция, активирующая кнопку "Построения"
        /// </summary>
        private void ActivateButton()
        {
            if (drillLenghtFlag == true && workingPartLenghtFlag == true
                                           && drillDiameterFlag == true && tenonLenghtFlag == true
                                           && tenonWidthFlag == true && neckLenghtFlag == true
                                           && neckWidthFlag == true)
            {
                buttonBuild.Enabled = true;
            }
            else
            {
                buttonBuild.Enabled = false;
            }
        }

        /// <summary>
        /// Функция, предающая параметры с формы в проект
        /// </summary>
        private void ValidateModelParameters()
        {
            _modelParameters.DrillLenght = Convert.ToDouble(drillLenght.Text);
            _modelParameters.WorkingPartLenght = Convert.ToDouble(workingPartLenght.Text);
            _modelParameters.DrillDiameter = Convert.ToDouble(drillDiameter.Text);
            _modelParameters.TenonLenght = Convert.ToDouble(tenonLenght.Text);
            _modelParameters.TenonWidth = Convert.ToDouble(tenonWidth.Text);
            _modelParameters.NeckLenght = Convert.ToDouble(neckLenght.Text);
            _modelParameters.NeckWidth = Convert.ToDouble(neckWidth.Text);
        }

        //Нажатие кнопки
        private void buttonBuild_Click(object sender, EventArgs e)
        {
            int amountErrors = 0;
            int amountDependencies = 4;
            string [] errorMessage = new string[amountDependencies];

            if (Convert.ToDouble(drillDiameter.Text) <= Convert.ToDouble(neckLenght.Text))
            {
                errorMessage[amountErrors] = "Диаметр сверла (D) > Ширина шейки (c)";
                amountErrors++;
            }

            if (Convert.ToDouble(drillDiameter.Text) <= Convert.ToDouble(tenonWidth.Text))
            {
                errorMessage[amountErrors] = "Диаметр сверла (D) > Ширина лапки (b)";
                amountErrors++;
            }

            if (Convert.ToDouble(drillLenght.Text) <= Convert.ToDouble(workingPartLenght.Text))
            {
                errorMessage[amountErrors] = "Длина сверла (L) > Длина рабочей части (l)";
                amountErrors++;
            }

            if (Convert.ToDouble(drillLenght.Text) - (Convert.ToDouble(workingPartLenght.Text) +
                                                      Convert.ToDouble(tenonLenght.Text) +
                                                      Convert.ToDouble(neckWidth.Text)) < 5)
            {
                errorMessage[amountErrors] = "Выражение неверно: L – (l + a + d) > 5";
                amountErrors++;
            }

            if (amountErrors != 0)
            {
                MessageBox.Show(String.Join("\n", errorMessage), "Не соблюдены следующие условия:", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                ValidateModelParameters();
                //_kompsWrapper.Main();
            }
        }

        // Проверка полей на правильное заполненение
        private void drillLenght_TextChanged(object sender, EventArgs e)
        {
            drillLenghtFlag = ValidateCorrectInput( drillLenght, 3, 145, drillLenghtFlag, 6);
            ActivateButton();
        }

        private void workingPartLenght_TextChanged(object sender, EventArgs e)
        {
            workingPartLenghtFlag = ValidateCorrectInput(workingPartLenght, 10, 30, workingPartLenghtFlag, 5);
            ActivateButton();
        }

        private void drillDiameter_TextChanged(object sender, EventArgs e)
        {
            drillDiameterFlag = ValidateCorrectInput(drillDiameter, 0.25, 22, drillDiameterFlag, 5);
            ActivateButton();
        }

        private void tenonLenght_TextChanged(object sender, EventArgs e)
        {
            tenonLenghtFlag = ValidateCorrectInput(tenonLenght, 0, 22, tenonLenghtFlag, 5);
            ActivateButton();
        }

        private void tenonWidth_TextChanged(object sender, EventArgs e)
        {
            tenonWidthFlag = ValidateCorrectInput(tenonWidth, 0, 22, tenonWidthFlag, 5);
            ActivateButton();
        }

        private void neckLenght_TextChanged(object sender, EventArgs e)
        {
            neckLenghtFlag = ValidateCorrectInput(neckLenght, 0, 14, neckLenghtFlag, 5);
            ActivateButton();
        }

        private void neckWidth_TextChanged(object sender, EventArgs e)
        {
            neckWidthFlag = ValidateCorrectInput(neckWidth, 0, 10, neckWidthFlag, 5);
            ActivateButton();
        }
    }
}
