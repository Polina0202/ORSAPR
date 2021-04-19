﻿using System;

namespace DrillKOMPAS
{
    /// <summary>
    /// Класс с параметрами
    /// </summary>
    public class DrillParameters
    {
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
                if (value < 3 || value > 145)
                {
                    throw new ArgumentException("Длина дрели не может быть меньше 3 мм и больше 145 мм");
                }
                else if (value - (WorkingPartLenght + TenonLenght + NeckLenght) < 0.5)
                {
                    throw new ArgumentException("Не соблюдено условие: L - (a+d+l)>0.5");
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
                if (value < 1.5 || value > 129)
                {
                    throw new ArgumentException("Длина рабочей части не может быть меньше 10 мм и больше 129 мм");
                }
                else if (DrillLenght - (TenonLenght + value + NeckLenght) < 0.5)
                {
                    throw new ArgumentException("Не соблюдено условие: L - (a+d+l)>0.5");
                }
                else if (value < DrillDiameter)
                {
                    throw new ArgumentException("Длина рабочей части не может быть меньше диаметра сверла");
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
                if (value < 0.25 || value > 22)
                {
                    throw new ArgumentException("Диаметр сверла не может быть меньше 0,25 мм и больше 22 мм");
                }
                else if (value < NeckWidth)
                {
                    throw new ArgumentException("Диаметр сверла не может быть меньше ширины шейки");
                }
                else if (value < TenonWidth)
                {
                    throw new ArgumentException("Диаметр дрели не может быть меньше ширины лапки");
                }
                else if (value > WorkingPartLenght)
                {
                    throw new ArgumentException("Диаметр дрели не может быть больше длины рабочей части");
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
                if (value < 0.45 || value > 22)
                {
                    throw new ArgumentException("Длина лапки не может быть меньше 0.45 мм и больше 22 мм");
                }
                else if (DrillLenght - (WorkingPartLenght + value + NeckLenght) < 0.5)
                {
                    throw new ArgumentException("Не соблюдено условие: L - (a+d+l)>0.5");
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
                if (value < 0.24 || value > 20)
                {
                    throw new ArgumentException("Ширина лапки не может быть меньше 0.24 мм и больше 20 мм");
                }
                else if (value > DrillDiameter)
                {
                    throw new ArgumentException("Ширина лапки не может быть больше диаметра сверла");
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
                if (value < 0.45 || value > 10)
                {
                    throw new ArgumentException("Длина шейки не может быть меньше 0.45 мм и больше 10 мм");
                }
                else if (DrillLenght - (WorkingPartLenght + TenonLenght + value) < 0.5)
                {
                    throw new ArgumentException("Не соблюдено условие: L - (a+d+l)>0.5");
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
                if (value < 0.24 || value > 20)
                {
                    throw new ArgumentException("Ширина шейки не может быть меньше 0.24 мм и больше 20 мм");
                }
                else if (value > DrillDiameter)
                {
                    throw new ArgumentException("Ширина шейки не может быть больше диаметра сверла");
                }
                else
                {
                    _neckWidth = value;
                }
            }
        }
    }
}
