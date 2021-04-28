using System;

namespace DrillKOMPAS
{
    /// <summary>
    /// Класс с параметрами
    /// </summary>
    public class DrillParameters
    {

        /// <summary>
        /// Минимальная длина сверла
        /// </summary>
        public const double DrillLenghtMin = 3;
        /// <summary>
        /// Максимальная длина сверла
        /// </summary>
        public const double DrillLenghtMax = 145;

        /// <summary>
        /// Минимальная длина рабочей части
        /// </summary>
        public const double WorkingPathMin = 1.5;
        /// <summary>
        /// Максимальная длина рабочей части
        /// </summary>
        public const double WorkingPathMax = 129;

        /// <summary>
        /// Минимальный диаметр сверла
        /// </summary>
        public const double DrillDiametrMin = 0.25;
        /// <summary>
        /// Максимальный диаметр сверла
        /// </summary>
        public const double DrillDiametrMax = 22;

        /// <summary>
        /// Минимальная длина лапки
        /// </summary>
        public const double TenonLenghtMin = 0.45;
        /// <summary>
        /// Максимальная длина лапки
        /// </summary>
        public const double TenonLenghtMax = 22;

        /// <summary>
        /// Минимальная ширина лапки
        /// </summary>
        public const double TenonWightMin = 0.24;
        /// <summary>
        /// Максимальная ширина лапки
        /// </summary>
        public const double TenonWightMax = 20;

        /// <summary>
        /// Минимальная длина шейки
        /// </summary>
        public const double NeckLenghtMin = 0.45;
        /// <summary>
        /// Максимальная длина шейки
        /// </summary>
        public const double NeckLenghtMax = 10;

        /// <summary>
        /// Минимальная ширина шейки
        /// </summary>
        public const double NeckWightMin = 0.24;
        /// <summary>
        /// Максимальная ширина шейки
        /// </summary>
        public const double NeckWightMax = 20;

        /// <summary>
        /// Длина дрели
        /// </summary>
        private double _drillLenght;

        /// <summary>
        /// Длина рабочей части
        /// </summary>
        private double _workingPartLenght;

        /// <summary>
        /// Диаметр дрели
        /// </summary>
        private double _drillDiameter;

        /// <summary>
        /// Длина лапки
        /// </summary>
        private double _tenonLenght;

        /// <summary>
        /// Ширина лапки
        /// </summary>
        private double _tenonWidth;

        /// <summary>
        /// Длина шейки
        /// </summary>
        private double _neckLenght;

        /// <summary>
        /// Ширина шейки
        /// </summary>
        private double _neckWidth;

        /// <summary>
        /// Свойство праметра скругления
        /// </summary>
        public bool AddFillet { get; set; }

        /// <summary>
        /// Свойство параметра кончика
        /// </summary>
        public bool IsTipOnWood { get; set; }

        /// <summary>
        /// Свойство параметра: длина дрели
        /// Диапазон параметра ограничен [3 .. 145] мм
        /// </summary>
        public double DrillLenght
        {
            get
            {
                return _drillLenght;
            }
            set
            {
                if (value < DrillLenghtMin || value > DrillLenghtMax)
                {
                    throw new ArgumentException(
                        "Длина дрели не может быть меньше 3 мм и больше 145 мм");
                }
                else if (value - (WorkingPartLenght + TenonLenght + NeckLenght) < 0.5)
                {
                    throw new ArgumentException(
                        "Не соблюдено условие: L - (a+d+l) > 0.5");
                }
                else
                {
                    _drillLenght = value;
                }
            }
        }

        /// <summary>
        /// Свойство параметра: длина рабочей части
        /// Диапазон параметра ограничен [1.5 .. 129] мм
        /// </summary>
        public double WorkingPartLenght
        {
            get
            {
                return _workingPartLenght;
            }
            set
            {
                if (value < WorkingPathMin || value > WorkingPathMax)
                {
                    throw new ArgumentException(
                        "Длина рабочей части не может быть меньше 10 мм и больше 129 мм");
                }
                else if (DrillLenght - (TenonLenght + value + NeckLenght) < 0.5)
                {
                    throw new ArgumentException(
                        "Не соблюдено условие: L-(a+d+l) > 0.5");
                }
                else if (value < DrillDiameter)
                {
                    throw new ArgumentException(
                        "Длина рабочей части не может быть меньше диаметра сверла");
                }
                else
                {
                    _workingPartLenght = value;
                }
            }
        }

        /// <summary>
        /// Свойство параметра: диаметр сверла
        /// Диапазон параметра ограничен [0,25 .. 22] мм
        /// </summary>
        public double DrillDiameter
        {
            get
            {
                return _drillDiameter;
            }
            set
            {
                if (value < DrillDiametrMin || value > DrillDiametrMax)
                {
                    throw new ArgumentException(
                        "Диаметр сверла не может быть меньше 0,25 мм и больше 22 мм");
                }
                else if (value < NeckWidth)
                {
                    throw new ArgumentException(
                        "Диаметр сверла не может быть меньше ширины шейки");
                }
                else if (value < TenonWidth)
                {
                    throw new ArgumentException(
                        "Диаметр дрели не может быть меньше ширины лапки");
                }
                else if (value > WorkingPartLenght)
                {
                    throw new ArgumentException(
                        "Диаметр дрели не может быть больше длины рабочей части");
                }
                else
                {
                    _drillDiameter = value;
                }
            }
        }

        /// <summary>
        /// Свойство параметра: длина лапки
        /// Диапазон параметра ограничен [0.45 .. 22] мм
        /// </summary>
        public double TenonLenght
        {
            get
            {
                return _tenonLenght;
            }
            set
            {
                if (value < TenonLenghtMin || value > TenonLenghtMax)
                {
                    throw new ArgumentException(
                        "Длина лапки не может быть меньше 0.45 мм и больше 22 мм");
                }
                else if (DrillLenght - (WorkingPartLenght + value + NeckLenght) < 0.5)
                {
                    throw new ArgumentException(
                        "Не соблюдено условие: L - (a+d+l) > 0.5");
                }
                else
                {
                    _tenonLenght = value;
                }
            }
        }

        /// <summary>
        /// Свойство параметра: Ширина лапки
        /// Диапазон параметра ограничен [0.24 .. 20] мм
        /// </summary>
        public double TenonWidth
        {
            get
            {
                return _tenonWidth;
            }
            set
            {
                if (value < TenonWightMin || value > TenonWightMax)
                {
                    throw new ArgumentException(
                        "Ширина лапки не может быть меньше 0.24 мм и больше 20 мм");
                }
                else if (value > DrillDiameter)
                {
                    throw new ArgumentException(
                        "Ширина лапки не может быть больше диаметра сверла");
                }
                else
                {
                    _tenonWidth = value;
                }
            }
        }

        /// <summary>
        /// Свойство параметра: длина шейки
        /// Диапазон параметра ограничен [0.45 .. 10] мм
        /// </summary>
        public double NeckLenght
        {
            get
            {
                return _neckLenght;
            }
            set
            {
                if (value < NeckLenghtMin || value > NeckLenghtMax)
                {
                    throw new ArgumentException(
                        "Длина шейки не может быть меньше 0.45 мм и больше 10 мм");
                }
                else if (DrillLenght - (WorkingPartLenght + TenonLenght + value) < 0.5)
                {
                    throw new ArgumentException(
                        "Не соблюдено условие: L - (a+d+l) > 0.5");
                }
                else
                {
                    _neckLenght = value;
                }
            }
        }

        /// <summary>
        /// Свойство параметра: ширина шейки
        /// Диапазон параметра ограничен [0.24 .. 20] мм
        /// </summary>
        public double NeckWidth
        {
            get
            {
                return _neckWidth;
            }
            set
            {
                if (value < NeckWightMin || value > NeckWightMax)
                {
                    throw new ArgumentException(
                        "Ширина шейки не может быть меньше 0.24 мм и больше 20 мм");
                }
                else if (value > DrillDiameter)
                {
                    throw new ArgumentException(
                        "Ширина шейки не может быть больше диаметра сверла");
                }
                else
                {
                    _neckWidth = value;
                }
            }
        }
    }
}
