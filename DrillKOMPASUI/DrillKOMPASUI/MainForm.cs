using System;
using System.Drawing;
using System.Windows.Forms;

using KOMPASConnector;
using DrillKOMPAS;

namespace DrillKOMPASUI
{
    public partial class MainForm : Form
    {
        private KOMPASWrapper _kompsWrapper = new KOMPASWrapper();

        private DrillParameters _modelParameters;

        private bool drillLenghtRangeFlag;
        private bool workingPartLenghtRangeFlag;
        private bool drillDiameterRangeFlag;
        private bool tenonLenghtRangeFlag;
        private bool tenonWidthRangeFlag;
        private bool neckLenghtRangeFlag;
        private bool neckWidthRangeFlag;


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

            drillLenghtRangeFlag = false;
            workingPartLenghtRangeFlag = false;
            drillDiameterRangeFlag = false;
            tenonLenghtRangeFlag = false;
            tenonWidthRangeFlag = false;
            neckLenghtRangeFlag = false;
            neckWidthRangeFlag = false;

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
        private bool ValidateCorrectInput(Control maskedTextBox, 
            double min, double max, bool flagMaskedTextBox, string mask)
        {
            if (maskedTextBox.Text.Length != mask.Length)
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
                    toolTip.Show("Значение находится вне диапазона", 
                        maskedTextBox, 43, 17);
                    return flagMaskedTextBox = false;
                }
                else
                {
                    toolTip.Hide(maskedTextBox);
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
            if (drillDiameter.MaskFull == true && tenonWidth.MaskFull == true &&
                (Convert.ToDouble(drillDiameter.Text) <= Convert.ToDouble(tenonWidth.Text)))
            {
                maskedTextBox.BackColor = Color.LightCoral;
                toolTip.Show("Диаметр сверла(D) должен быть больше ширины лапки(b)", 
                    maskedTextBox, 43, 17);
                return flagMaskedTextBox = false;
            }
            else if (drillDiameter.MaskFull == true && neckWidth.MaskFull == true &&
                     (Convert.ToDouble(drillDiameter.Text) <= Convert.ToDouble(neckWidth.Text)))
            {
                maskedTextBox.BackColor = Color.LightCoral;
                toolTip.Show("Диаметр сверла(D) должен быть больше ширины шейки(c)", 
                    maskedTextBox, 43, 17);
                return flagMaskedTextBox = false;
            }
            else if (drillDiameter.MaskFull == true && workingPartLenght.MaskFull == true &&
                     (Convert.ToDouble(drillDiameter.Text) >= Convert.ToDouble(workingPartLenght.Text)))
            {
                maskedTextBox.BackColor = Color.LightCoral;
                toolTip.Show("Длина рабочей части(l) должена быть больше диаметра сверла(D)", 
                    maskedTextBox, 43, 17);
                return flagMaskedTextBox = false;
            }
            else if (drillLenght.MaskFull == true && workingPartLenght.MaskFull == true &&
                     tenonLenght.MaskFull == true && neckLenght.MaskFull == true &&
                     (Convert.ToDouble(drillLenght.Text) - (Convert.ToDouble(workingPartLenght.Text) +
                                                            Convert.ToDouble(tenonLenght.Text) +
                                                            Convert.ToDouble(neckLenght.Text)) <= 0.5))
            {
                maskedTextBox.BackColor = Color.LightCoral;
                toolTip.Show("Не выполняется условие: L – (l + a + d) > 0.5 ", 
                    maskedTextBox, 43, 17);
                return flagMaskedTextBox = false;
            }

            toolTip.Hide(maskedTextBox);
            maskedTextBox.BackColor = Color.White;
            return flagMaskedTextBox = true;
        }

        /// <summary>
        /// Функция, активирующая кнопку "Построения"
        /// </summary>
        private void ActivateButton()
        {
            if (drillLenghtFlag && workingPartLenghtFlag && drillDiameterFlag && tenonLenghtFlag && tenonWidthFlag &&
                neckLenghtFlag && neckWidthFlag && drillLenghtRangeFlag && workingPartLenghtRangeFlag &&
                drillDiameterRangeFlag && tenonLenghtRangeFlag && tenonWidthRangeFlag && neckLenghtRangeFlag &&
                neckWidthRangeFlag == true)
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
            drillLenghtRangeFlag = ValidateCorrectInput(drillLenght, DrillParameters.DrillLenghtMin,
                DrillParameters.DrillLenghtMax, drillLenghtFlag, drillLenght.Mask);
            if (drillLenghtRangeFlag == true)
            {
                drillLenghtFlag = ConformanceCheck(drillLenght, drillLenghtFlag);
                if (drillLenghtFlag == true)
                {
                    workingPartLenghtFlag = 
                        ConformanceCheck(workingPartLenght, workingPartLenghtFlag);
                    tenonLenghtFlag = 
                        ConformanceCheck(tenonLenght, tenonLenghtFlag);
                    neckLenghtFlag = 
                        ConformanceCheck(neckLenght, neckLenghtFlag);
                }
            }
            ActivateButton();
        }

        private void workingPartLenght_TextChanged(object sender, EventArgs e)
        {
            workingPartLenghtRangeFlag = ValidateCorrectInput(workingPartLenght,
                DrillParameters.WorkingPathMin,
                DrillParameters.WorkingPathMax,
                workingPartLenghtFlag,
                workingPartLenght.Mask);
            if (workingPartLenghtRangeFlag == true)
            {
                workingPartLenghtFlag =
                    ConformanceCheck(workingPartLenght, workingPartLenghtFlag);
                if (workingPartLenghtFlag == true)
                {
                    drillDiameterFlag =
                        ConformanceCheck(drillDiameter, drillDiameterFlag);
                    tenonLenghtFlag =
                        ConformanceCheck(tenonLenght, tenonLenghtFlag);
                    neckLenghtFlag =
                        ConformanceCheck(neckLenght, neckLenghtFlag);
                    drillLenghtFlag =
                        ConformanceCheck(drillLenght, drillLenghtFlag);
                }
            }
            ActivateButton();
        }

        private void drillDiameter_TextChanged(object sender, EventArgs e)
        {
            drillDiameterRangeFlag = ValidateCorrectInput(drillDiameter,
                DrillParameters.DrillDiametrMin,
                DrillParameters.DrillDiametrMax,
                drillDiameterFlag,
                drillDiameter.Mask);
            if (drillDiameterRangeFlag == true)
            {
                drillDiameterFlag =
                    ConformanceCheck(drillDiameter, drillDiameterFlag);
                if (drillDiameterFlag == true)
                {
                    workingPartLenghtFlag =
                        ConformanceCheck(workingPartLenght, workingPartLenghtFlag);
                    neckWidthFlag =
                        ConformanceCheck(neckWidth, neckWidthFlag);
                    tenonWidthFlag =
                        ConformanceCheck(tenonWidth, tenonWidthFlag);
                }
            }
            ActivateButton();
        }

        private void tenonLenght_TextChanged(object sender, EventArgs e)
        {
            tenonLenghtRangeFlag = ValidateCorrectInput(tenonLenght,
                DrillParameters.TenonLenghtMin,
                DrillParameters.TenonLenghtMax,
                tenonLenghtFlag,
                tenonLenght.Mask);
            if (tenonLenghtRangeFlag == true)
            {
                tenonLenghtFlag = ConformanceCheck(tenonLenght, tenonLenghtFlag);
                if (tenonLenghtFlag == true)
                {
                    workingPartLenghtFlag =
                        ConformanceCheck(workingPartLenght, workingPartLenghtFlag);
                    neckLenghtFlag =
                        ConformanceCheck(neckLenght, neckLenghtFlag);
                    drillLenghtFlag =
                        ConformanceCheck(drillLenght, drillLenghtFlag);
                }
            }
            ActivateButton();
        }

        private void tenonWidth_TextChanged(object sender, EventArgs e)
        {
            tenonWidthRangeFlag = ValidateCorrectInput(tenonWidth,
                DrillParameters.TenonWightMin,
                DrillParameters.TenonWightMax,
                tenonWidthFlag,
                tenonWidth.Mask);
            if (tenonWidthRangeFlag == true)
            {
                tenonWidthFlag = ConformanceCheck(tenonWidth, tenonWidthFlag);
                if (tenonWidthFlag == true)
                {
                    drillDiameterFlag =
                        ConformanceCheck(drillDiameter, drillDiameterFlag);
                }
            }
            ActivateButton();
        }

        private void neckLenght_TextChanged(object sender, EventArgs e)
        {
            neckLenghtRangeFlag = ValidateCorrectInput(neckLenght,
                DrillParameters.NeckLenghtMin,
                DrillParameters.NeckLenghtMax,
                neckLenghtFlag,
                neckLenght.Mask);
            if (neckLenghtRangeFlag == true)
            {
                neckLenghtFlag =
                    ConformanceCheck(neckLenght, neckLenghtFlag);
                if (neckLenghtFlag == true)
                {
                    workingPartLenghtFlag =
                        ConformanceCheck(workingPartLenght, workingPartLenghtFlag);
                    tenonLenghtFlag =
                        ConformanceCheck(tenonLenght, tenonLenghtFlag);
                    drillLenghtFlag =
                        ConformanceCheck(drillLenght, drillLenghtFlag);
                }
            }
            ActivateButton();
        }

        private void neckWidth_TextChanged(object sender, EventArgs e)
        {
            neckWidthRangeFlag = ValidateCorrectInput(neckWidth,
                DrillParameters.TenonWightMin,
                DrillParameters.TenonWightMax,
                neckWidthFlag,
                neckWidth.Mask);
            if (neckWidthRangeFlag == true)
            {
                neckWidthFlag =
                    ConformanceCheck(neckWidth, neckWidthFlag);
                if (neckWidthFlag == true)
                {
                    drillDiameterFlag =
                        ConformanceCheck(drillDiameter, drillDiameterFlag);
                }
            }
            ActivateButton();
        }

        //Кнопка отчистки полей
        private void clearButton_Click(object sender, EventArgs e)
        {
            drillDiameter.Text = "";
            drillDiameter.BackColor = Color.White;

            drillLenght.Text = "";
            drillLenght.BackColor = Color.White;

            tenonLenght.Text = "";
            tenonLenght.BackColor = Color.White;

            tenonWidth.Text = "";
            tenonWidth.BackColor = Color.White;

            neckLenght.Text = "";
            neckLenght.BackColor = Color.White;

            neckWidth.Text = "";
            neckWidth.BackColor = Color.White;

            workingPartLenght.Text = "";
            workingPartLenght.BackColor = Color.White;
        }
    }
}
