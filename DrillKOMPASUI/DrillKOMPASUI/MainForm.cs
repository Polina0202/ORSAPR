using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using KOMPASConnector;
using DrillKOMPAS;

namespace DrillKOMPASUI
{
    public partial class MainForm : Form
    {
        private KOMPASWrapper _kompsWrapper = new KOMPASWrapper();

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
            
            drillLenghtFlag = false;
            workingPartLenghtFlag = false;
            drillDiameterFlag = false;
            tenonLenghtFlag = false;
            tenonWidthFlag = false;
            neckLenghtFlag = false;
            neckWidthFlag = false;
        }

        /// <summary>
        /// Проверка параметров на соотвествие диапозону заданных значений
        /// </summary>
        /// <param name="maskedTextBox">проверяемое поле параметра</param>
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
        /// Проверка параметров на соотвествия зависимостям 
        /// </summary>
        /// <param name="maskedTextBox">проверяемое поле параметра</param>
        /// <param name="flagMaskedTextBox">правильность заполнения</param>
        /// <returns></returns>
        private bool ConformanceCheck(Control maskedTextBox, bool flagMaskedTextBox)
        {
            if (drillDiameter.MaskFull == true && tenonWidth.MaskFull == true && (Convert.ToDouble(drillDiameter.Text) <= Convert.ToDouble(tenonWidth.Text)))
            {
                maskedTextBox.BackColor = Color.LightCoral;
                toolTip.Show("Диаметр сверла(D) должен быть больше ширины лапки(b)", maskedTextBox, 43, 17);
                return flagMaskedTextBox = false;
            }

            if (drillDiameter.MaskFull == true && neckWidth.MaskFull == true && (Convert.ToDouble(drillDiameter.Text) <= Convert.ToDouble(neckWidth.Text)))
            {
                maskedTextBox.BackColor = Color.LightCoral;
                toolTip.Show("Диаметр сверла(D) должен быть больше ширины шейки(c)", maskedTextBox, 43, 17);
                return flagMaskedTextBox = false;
            }

            if (drillLenght.MaskFull == true && workingPartLenght.MaskFull == true && tenonLenght.MaskFull == true && neckLenght.MaskFull == true &&
                (Convert.ToDouble(drillLenght.Text) - (Convert.ToDouble(workingPartLenght.Text)+ Convert.ToDouble(tenonLenght.Text) + Convert.ToDouble(neckLenght.Text)) <= 5))
            {
                maskedTextBox.BackColor = Color.LightCoral;
                toolTip.Show("Не выполняется условие: L – (l + a + d) > 5 ", maskedTextBox, 43, 17);
                return flagMaskedTextBox = false;
            }

            maskedTextBox.BackColor = Color.White;
            return flagMaskedTextBox = true;
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
            _modelParameters = new DrillParameters();
            ValidateModelParameters();
            _kompsWrapper.OpenKOMPAS();
            _kompsWrapper.BuildModel(_modelParameters);
        }

        // Проверка полей на правильное заполненение
        private void drillLenght_TextChanged(object sender, EventArgs e)
        {
            drillLenghtFlag = ValidateCorrectInput( drillLenght, 3, 145, drillLenghtFlag, 6);
            if (drillLenghtFlag == true)
                drillLenghtFlag = ConformanceCheck(drillLenght, drillLenghtFlag);
            ActivateButton();
        }

        private void workingPartLenght_TextChanged(object sender, EventArgs e)
        {
            workingPartLenghtFlag = ValidateCorrectInput(workingPartLenght, 10, 140, workingPartLenghtFlag, 5);
            if (workingPartLenghtFlag == true)
                workingPartLenghtFlag = ConformanceCheck(workingPartLenght, workingPartLenghtFlag);
            ActivateButton();
        }

        private void drillDiameter_TextChanged(object sender, EventArgs e)
        {
            drillDiameterFlag = ValidateCorrectInput(drillDiameter, 0.25, 22, drillDiameterFlag, 5);
            if (drillDiameterFlag == true)
                drillDiameterFlag = ConformanceCheck(drillDiameter, drillDiameterFlag);
            ActivateButton();
        }

        private void tenonLenght_TextChanged(object sender, EventArgs e)
        {
            tenonLenghtFlag = ValidateCorrectInput(tenonLenght, 0, 22, tenonLenghtFlag, 5);
            if (tenonLenghtFlag == true)
                tenonLenghtFlag = ConformanceCheck(tenonLenght, tenonLenghtFlag);
            ActivateButton();
        }

        private void tenonWidth_TextChanged(object sender, EventArgs e)
        {
            tenonWidthFlag = ValidateCorrectInput(tenonWidth, 0, 22, tenonWidthFlag, 5);
            if (tenonWidthFlag == true)
                tenonWidthFlag = ConformanceCheck(tenonWidth, tenonWidthFlag);
            ActivateButton();
        }

        private void neckLenght_TextChanged(object sender, EventArgs e)
        {
            neckLenghtFlag = ValidateCorrectInput(neckLenght, 0, 10, neckLenghtFlag, 5);
            if (neckLenghtFlag == true)
                neckLenghtFlag = ConformanceCheck(neckLenght, neckLenghtFlag);
            ActivateButton();
        }

        private void neckWidth_TextChanged(object sender, EventArgs e)
        {
            neckWidthFlag = ValidateCorrectInput(neckWidth, 0, 20, neckWidthFlag, 5);
            if (neckWidthFlag == true)
                neckWidthFlag = ConformanceCheck(neckWidth, neckWidthFlag);
            ActivateButton();
        }
    }
}
