using System;
using System.Drawing;
using System.Windows.Forms;

using KOMPASConnector;
using DrillKOMPAS;

namespace DrillKOMPASUI
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Экземпляр компаса KOMPASWrapper
        /// </summary>
        private KOMPASWrapper _kompsWrapper = new KOMPASWrapper();

        /// <summary>
        /// Экземпляр класса параметров 
        /// </summary>
        private DrillParameters _modelParameters;

        /// <summary>
        /// Входит ли параметр длина сверла в диапазон
        /// </summary>
        private bool _isDrillLenghtInRange;
        /// <summary>
        /// Входит ли параметр рабочая часть в диапазон
        /// </summary>
        private bool _isWorkingPartInRange;
        /// <summary>
        /// Входит ли параметр диаметр сверла в диапазон
        /// </summary>
        private bool _isDrillDiameterInRange;
        /// <summary>
        /// Входит ли параметр длина лапки в диапазон
        /// </summary>
        private bool _isTenonLenghtInRange;
        /// <summary>
        /// Входит ли параметр ширина лапки в диапазон
        /// </summary>
        private bool _isTenonWidthInRange;
        /// <summary>
        /// Входит ли параметр длина шейки в диапазон
        /// </summary>
        private bool _isNeckLenghtInRange;
        /// <summary>
        /// Входит ли параметр ширина шейки в диапазон
        /// </summary>
        private bool _isNeckWidthInRange;

        /// <summary>
        /// Соответсвие длины сверла заданным зависимостям
        /// </summary>
        private bool _drillLenghtCondition;
        /// <summary>
        /// Соответсвие рабочей части заданным зависимостям
        /// </summary>
        private bool _workingPartCondition;
        /// <summary>
        /// Соответсвие диаметра сверла заданным зависимостям
        /// </summary>
        private bool _drillDiameterCondition;
        /// <summary>
        /// Соответсвие длины лапки заданным зависимостям
        /// </summary>
        private bool _tenonLenghtCondition;
        /// <summary>
        /// Соответсвие ширины лапки заданным зависимостям
        /// </summary>
        private bool _tenonWidthCondition;
        /// <summary>
        /// Соответсвие длины шейки заданным зависимостям
        /// </summary>
        private bool _neckLenghtCondition;
        /// <summary>
        /// Соответсвие ширины шейки заданным зависимостям
        /// </summary>
        private bool _neckWidthCondition;

        /// <summary>
        /// Цвет текстового поля с ошибкой
        /// </summary>
        private Color _errorColor = Color.LightCoral;

        public MainForm()
        {
            InitializeComponent();

            _isDrillLenghtInRange = false;
            _isWorkingPartInRange = false;
            _isDrillDiameterInRange = false;
            _isTenonLenghtInRange = false;
            _isTenonWidthInRange = false;
            _isNeckLenghtInRange = false;
            _isNeckWidthInRange = false;

            _drillLenghtCondition = false;
            _workingPartCondition = false;
            _drillDiameterCondition = false;
            _tenonLenghtCondition = false;
            _tenonWidthCondition = false;
            _neckLenghtCondition = false;
            _neckWidthCondition = false;
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
            double min, double max, string mask)
        {
            if (maskedTextBox.Text.Length != mask.Length)
            {
                maskedTextBox.BackColor = _errorColor;
                return false;
            }
            else
            {
                if (Convert.ToDouble(maskedTextBox.Text) < min 
                    || Convert.ToDouble(maskedTextBox.Text) > max)
                {
                    maskedTextBox.BackColor = _errorColor;
                    toolTip.Show("Значение находится вне диапазона", 
                        maskedTextBox, 43, 17);
                    return false;
                }
                else
                {
                    toolTip.Hide(maskedTextBox);
                    maskedTextBox.BackColor = Color.White;
                    return true;
                }
            }
        }

        /// <summary>
        /// Проверка параметров на соотвествия зависимостям 
        /// </summary>
        /// <param name="maskedTextBox">проверяемое поле параметра</param>
        /// <param name="flagMaskedTextBox">правильность заполнения</param>
        /// <returns></returns>
        private bool ConformanceCheck(Control maskedTextBox)
        {
            if (drillDiameter.MaskFull && tenonWidth.MaskFull &&
                (Convert.ToDouble(drillDiameter.Text) <= Convert.ToDouble(tenonWidth.Text)))
            {
                maskedTextBox.BackColor = _errorColor;
                toolTip.Show("Диаметр сверла(D) должен быть больше ширины лапки(b)", 
                    maskedTextBox, 43, 17);
                return false;
            }
            else if (drillDiameter.MaskFull && neckWidth.MaskFull &&
                     (Convert.ToDouble(drillDiameter.Text) <= Convert.ToDouble(neckWidth.Text)))
            {
                maskedTextBox.BackColor = _errorColor;
                toolTip.Show("Диаметр сверла(D) должен быть больше ширины шейки(c)", 
                    maskedTextBox, 43, 17);
                return false;
            }
            else if (drillDiameter.MaskFull && workingPartLenght.MaskFull &&
                     (Convert.ToDouble(drillDiameter.Text) >= Convert.ToDouble(workingPartLenght.Text)))
            {
                maskedTextBox.BackColor = _errorColor;
                toolTip.Show("Длина рабочей части(l) должена быть больше диаметра сверла(D)", 
                    maskedTextBox, 43, 17);
                return false;
            }
            else if (drillLenght.MaskFull && workingPartLenght.MaskFull &&
                     tenonLenght.MaskFull && neckLenght.MaskFull &&
                     (Convert.ToDouble(drillLenght.Text) - (Convert.ToDouble(workingPartLenght.Text) +
                                                            Convert.ToDouble(tenonLenght.Text) +
                                                            Convert.ToDouble(neckLenght.Text)) <= 0.5))
            {
                maskedTextBox.BackColor = _errorColor;
                toolTip.Show("Не выполняется условие: L – (l + a + d) > 0.5 ", 
                    maskedTextBox, 43, 17);
                return false;
            }

            toolTip.Hide(maskedTextBox);
            maskedTextBox.BackColor = Color.White;
            return true;
        }

        /// <summary>
        /// Функция, активирующая кнопку "Построения"
        /// </summary>
        private void ActivateButton()
        {
            if (_drillLenghtCondition && _workingPartCondition 
                && _drillDiameterCondition && _tenonLenghtCondition 
                && _tenonWidthCondition && _neckLenghtCondition 
                && _neckWidthCondition && _isDrillLenghtInRange 
                && _isWorkingPartInRange && _isDrillDiameterInRange 
                && _isTenonLenghtInRange && _isTenonWidthInRange 
                && _isNeckLenghtInRange && _isNeckWidthInRange)
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

            if (filletCheckBox.Checked)
                _modelParameters.AddFillet = true;
            else
                _modelParameters.AddFillet = false;

            if (onWoodCheckBox.Checked)
                _modelParameters.IsTipOnWood = true;
            else
                _modelParameters.IsTipOnWood = false;
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
            _isDrillLenghtInRange = ValidateCorrectInput(drillLenght, DrillParameters.DrillLenghtMin,
                DrillParameters.DrillLenghtMax, drillLenght.Mask);
            if (ValidateCorrectInput(drillLenght, DrillParameters.DrillLenghtMin,
                DrillParameters.DrillLenghtMax, drillLenght.Mask))
            {
                _drillLenghtCondition = ConformanceCheck(drillLenght);
                if (_drillLenghtCondition)
                {
                    _workingPartCondition = 
                        ConformanceCheck(workingPartLenght);
                    _tenonLenghtCondition = 
                        ConformanceCheck(tenonLenght);
                    _neckLenghtCondition = 
                        ConformanceCheck(neckLenght);
                }
            }
            ActivateButton();
        }

        private void workingPartLenght_TextChanged(object sender, EventArgs e)
        {
            _isWorkingPartInRange = ValidateCorrectInput(workingPartLenght,
                DrillParameters.WorkingPathMin,
                DrillParameters.WorkingPathMax,
                workingPartLenght.Mask);
            if (ValidateCorrectInput(workingPartLenght,
                DrillParameters.WorkingPathMin,
                DrillParameters.WorkingPathMax,
                workingPartLenght.Mask))
            {
                _workingPartCondition =
                    ConformanceCheck(workingPartLenght);
                if (_workingPartCondition)
                {
                    _drillDiameterCondition =
                        ConformanceCheck(drillDiameter);
                    _tenonLenghtCondition =
                        ConformanceCheck(tenonLenght);
                    _neckLenghtCondition =
                        ConformanceCheck(neckLenght);
                    _drillLenghtCondition =
                        ConformanceCheck(drillLenght);
                }
            }
            ActivateButton();
        }

        private void drillDiameter_TextChanged(object sender, EventArgs e)
        {
            _isDrillDiameterInRange = ValidateCorrectInput(drillDiameter,
                DrillParameters.DrillDiametrMin,
                DrillParameters.DrillDiametrMax,
                drillDiameter.Mask);
            if (ValidateCorrectInput(drillDiameter,
                DrillParameters.DrillDiametrMin,
                DrillParameters.DrillDiametrMax,
                drillDiameter.Mask))
            {
                _drillDiameterCondition =
                    ConformanceCheck(drillDiameter);
                if (_drillDiameterCondition)
                {
                    _workingPartCondition =
                        ConformanceCheck(workingPartLenght);
                    _neckWidthCondition =
                        ConformanceCheck(neckWidth);
                    _tenonWidthCondition =
                        ConformanceCheck(tenonWidth);
                }
            }
            ActivateButton();
        }

        private void tenonLenght_TextChanged(object sender, EventArgs e)
        {
            _isTenonLenghtInRange = ValidateCorrectInput(tenonLenght,
                DrillParameters.TenonLenghtMin,
                DrillParameters.TenonLenghtMax,
                tenonLenght.Mask);
            if (ValidateCorrectInput(tenonLenght,
                DrillParameters.TenonLenghtMin,
                DrillParameters.TenonLenghtMax,
                tenonLenght.Mask))
            {
                _tenonLenghtCondition = ConformanceCheck(tenonLenght);
                if (_tenonLenghtCondition)
                {
                    _workingPartCondition =
                        ConformanceCheck(workingPartLenght);
                    _neckLenghtCondition =
                        ConformanceCheck(neckLenght);
                    _drillLenghtCondition =
                        ConformanceCheck(drillLenght);
                }
            }
            ActivateButton();
        }

        private void tenonWidth_TextChanged(object sender, EventArgs e)
        {
            _isTenonWidthInRange = ValidateCorrectInput(tenonWidth,
                DrillParameters.TenonWightMin,
                DrillParameters.TenonWightMax,
                tenonWidth.Mask);
            if (ValidateCorrectInput(tenonWidth,
                DrillParameters.TenonWightMin,
                DrillParameters.TenonWightMax,
                tenonWidth.Mask))
            {
                _tenonWidthCondition = ConformanceCheck(tenonWidth);
                if (_tenonWidthCondition)
                {
                    _drillDiameterCondition =
                        ConformanceCheck(drillDiameter);
                }
            }
            ActivateButton();
        }

        private void neckLenght_TextChanged(object sender, EventArgs e)
        {
            _isNeckLenghtInRange = ValidateCorrectInput(neckLenght,
                DrillParameters.NeckLenghtMin,
                DrillParameters.NeckLenghtMax,
                neckLenght.Mask);
            if (ValidateCorrectInput(neckLenght,
                DrillParameters.NeckLenghtMin,
                DrillParameters.NeckLenghtMax,
                neckLenght.Mask))
            {
                _neckLenghtCondition =
                    ConformanceCheck(neckLenght);
                if (_neckLenghtCondition)
                {
                    _workingPartCondition =
                        ConformanceCheck(workingPartLenght);
                    _tenonLenghtCondition =
                        ConformanceCheck(tenonLenght);
                    _drillLenghtCondition =
                        ConformanceCheck(drillLenght);
                }
            }
            ActivateButton();
        }

        private void neckWidth_TextChanged(object sender, EventArgs e)
        {
            _isNeckWidthInRange = ValidateCorrectInput(neckWidth,
                DrillParameters.TenonWightMin,
                DrillParameters.TenonWightMax,
                neckWidth.Mask);
            if (ValidateCorrectInput(neckWidth,
                DrillParameters.TenonWightMin,
                DrillParameters.TenonWightMax,
                neckWidth.Mask))
            {
                _neckWidthCondition =
                    ConformanceCheck(neckWidth);
                if (_neckWidthCondition)
                {
                    _drillDiameterCondition =
                        ConformanceCheck(drillDiameter);
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
