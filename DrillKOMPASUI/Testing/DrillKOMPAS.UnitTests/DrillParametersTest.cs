using System;
using NUnit.Framework;

namespace DrillKOMPAS.UnitTests
{
    [TestFixture]
    public class DrillParametersTest
    {
        [TestCase(TestName = "Параметр скругления: позитивный тест")]
        public void AddFillet_CorrectValue_ReturnCorrectValue()
        {
            //Setup
            var parameters = new DrillParameters();
            var sourceValue = false;
            var expectedValue = sourceValue;

            //Act
            parameters.AddFillet = sourceValue;
            var actualValue = parameters.AddFillet;

            //Assert
            NUnit.Framework.Assert.AreEqual(expectedValue, actualValue);
        }

        [TestCase(TestName = "Параметр типа кончика: позитивный тест")]
        public void IsTipOnWood_CorrectValue_ReturnCorrectValue()
        {
            //Setup
            var parameters = new DrillParameters();
            var sourceValue = true;
            var expectedValue = sourceValue;

            //Act
            parameters.IsTipOnWood = sourceValue;
            var actualValue = parameters.IsTipOnWood;

            //Assert
            NUnit.Framework.Assert.AreEqual(expectedValue, actualValue);
        }

        [TestCase(TestName = "Длина сверла: позитивный тест")]
        public void DrillLenght_CorrectValue_ReturnCorrectValue()
        {
            //Setup
            var parameters = new DrillParameters();
            var sourceValue = 100.80;
            var expectedValue = sourceValue;

            //Act
            parameters.DrillLenght = sourceValue;
            var actualValue = parameters.DrillLenght;

            //Assert
            NUnit.Framework.Assert.AreEqual(expectedValue, actualValue);
        }

        [TestCase(TestName = "Длина сверла: выход за диапазон значений")]
        public void DrillLenght_OutOfRangeValue_ThrowsException()
        {
            //Setup
            var parameters = new DrillParameters();
            var sourceValue = 999.99;

            //Act
            NUnit.Framework.Assert.Throws<ArgumentException>
            (
                () =>
                {
                    //Act
                    parameters.DrillLenght = sourceValue;
                }
            );
        }

        [TestCase(TestName = "Длина сверла: не соблюдение условия L - (a+d+l) > 0.5")]
        public void DrillLenght_IncorrectValue_ThrowsException()
        {
            //Setup
            var parameters = new DrillParameters
            {
                DrillLenght = 40.00,
                WorkingPartLenght = 20.00,
                TenonLenght = 5.00,
                NeckLenght = 5.00
            };
            var sourceValue = 10.00;

            //Act
            NUnit.Framework.Assert.Throws<ArgumentException>
            (
                () =>
                {
                    //Act
                    parameters.DrillLenght = sourceValue;
                }
            );
        }

        [TestCase(TestName = "Рабочая часть: позитивный тест")]
        public void WorkingPartLenght_CorrectValue_ReturnCorrectValue()
        {
            //Setup
            var parameters = new DrillParameters
            {
                DrillLenght = 40.00
            };
            var sourceValue = 20.80;
            var expectedValue = sourceValue;

            //Act
            parameters.WorkingPartLenght = sourceValue;
            var actualValue = parameters.WorkingPartLenght;

            //Assert
            NUnit.Framework.Assert.AreEqual(expectedValue, actualValue);
        }

        [TestCase(TestName = "Рабочая часть: выход за диапазон значений")]
        public void WorkingPartLenght_OutOfRangeValue_ThrowsException()
        {
            //Setup
            var parameters = new DrillParameters();
            var sourceValue = 0.00;

            //Act
            NUnit.Framework.Assert.Throws<ArgumentException>
            (
                () =>
                {
                    //Act
                    parameters.WorkingPartLenght = sourceValue;
                }
            );
        }

        [TestCase(TestName = "Рабочая часть: не соблюдение условия L-(a+d+l) > 0.5")]
        public void WorkingPartLenght_IncorrectValue_ThrowsException()
        {
            //Setup
            var parameters = new DrillParameters
            {
                DrillLenght = 20.00
            };
            var sourceValue = 26.50;

            //Act
            NUnit.Framework.Assert.Throws<ArgumentException>
            (
                () =>
                {
                    //Act
                    parameters.WorkingPartLenght = sourceValue;
                }
            );
        }

        [TestCase(TestName = "Длина рабочей части не может быть меньше диаметра сверла")]
        public void WorkingPartLenght_ValueLessDrillDiametr_ThrowsException()
        {
            //Setup
            var parameters = new DrillParameters
            {
                DrillLenght = 20.00,
                WorkingPartLenght = 14,
                DrillDiameter = 12
            };
            var sourceValue = 10.50;

            //Act
            NUnit.Framework.Assert.Throws<ArgumentException>
            (
                () =>
                {
                    //Act
                    parameters.WorkingPartLenght = sourceValue;
                }
            );
        }

        [TestCase(TestName = "Диаметр сверла: позитивный тест")]
        public void DrillDiameter_CorrectValue_ReturnCorrectValue()
        {
            //Setup
            var parameters = new DrillParameters
            {
                DrillLenght = 100,
                WorkingPartLenght = 60
            };
            var sourceValue = 20.80;
            var expectedValue = sourceValue;

            //Act
            parameters.DrillDiameter = sourceValue;
            var actualValue = parameters.DrillDiameter;

            //Assert
            NUnit.Framework.Assert.AreEqual(expectedValue, actualValue);
        }

        [TestCase(TestName = "Диаметр сверла: выход за диапазон значений")]
        public void DrillDiameter_OutOfRangeValue_ThrowsException()
        {
            //Setup
            var parameters = new DrillParameters();
            var sourceValue = 50.50;

            //Act
            NUnit.Framework.Assert.Throws<ArgumentException>
            (
                () =>
                {
                    //Act
                    parameters.DrillDiameter = sourceValue;
                }
            );
        }

        [TestCase(TestName = "Диаметр сверла не может быть меньше ширины шейки")]
        public void DrillDiameter_ValueLessNeckWidth_ThrowsException()
        {
            //Setup
            var parameters = new DrillParameters
            {
                DrillLenght = 70.00,
                WorkingPartLenght = 30,
                DrillDiameter = 20,
                NeckWidth = 10.00
            };
            var sourceValue = 5.50;

            //Act
            NUnit.Framework.Assert.Throws<ArgumentException>
            (
                () =>
                {
                    //Act
                    parameters.DrillDiameter = sourceValue;
                }
            );
        }

        [TestCase(TestName = "Диаметр дрели не может быть меньше ширины лапки")]
        public void DrillDiameter_ValueLessTenonWidth_ThrowsException()
        {
            //Setup
            var parameters = new DrillParameters
            {
                DrillLenght = 100,
                WorkingPartLenght = 50,
                DrillDiameter = 20,
                TenonWidth = 10.00
            };
            var sourceValue = 5.50;

            //Act
            NUnit.Framework.Assert.Throws<ArgumentException>
            (
                () =>
                {
                    //Act
                    parameters.DrillDiameter = sourceValue;
                }
            );
        }

        [TestCase(TestName = "Диаметр дрели не может быть больше длины рабочей части")]
        public void DrillDiameter_ValueLessWorkingPartLenght_ThrowsException()
        {
            //Setup
            var parameters = new DrillParameters
            {
                DrillLenght = 40.00,
                WorkingPartLenght = 14,
                DrillDiameter = 10,
                NeckWidth = 10.00,
                TenonWidth = 10.00,
            };
            var sourceValue = 15.50;

            //Act
            NUnit.Framework.Assert.Throws<ArgumentException>
            (
                () =>
                {
                    //Act
                    parameters.DrillDiameter = sourceValue;
                }
            );
        }

        [TestCase(TestName = "Длина лапки: позитивный тест")]
        public void TenonLenght_CorrectValue_ReturnCorrectValue()
        {
            //Setup
            var parameters = new DrillParameters
            {
                DrillLenght = 100.50
            };
            var sourceValue = 20.50;
            var expectedValue = sourceValue;

            //Act
            parameters.TenonLenght = sourceValue;
            var actualValue = parameters.TenonLenght;

            //Assert
            NUnit.Framework.Assert.AreEqual(expectedValue, actualValue);
        }

        [TestCase(TestName = "Длина лапки: выход за диапазон значений")]
        public void TenonLenght_OutOfRangeValue_ThrowsException()
        {
            //Setup
            var parameters = new DrillParameters();
            var sourceValue = 105.50;

            //Act
            NUnit.Framework.Assert.Throws<ArgumentException>
            (
                () =>
                {
                    //Act
                    parameters.TenonLenght = sourceValue;
                }
            );
        }

        [TestCase(TestName = "Длина лапки: не соблюдение условия L - (a+d+l) > 0.5")]
        public void TenonLenght_IncorrectValue_ThrowsException()
        {
            //Setup
            var parameters = new DrillParameters()
            {
                DrillLenght = 50.00,
                WorkingPartLenght = 20.00,
                NeckLenght = 10.00
            };
            var sourceValue = 20.50;
            try
            {
                parameters.TenonLenght = sourceValue;
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
            //Act
            NUnit.Framework.Assert.Throws<ArgumentException>
            (
                () =>
                {
                    //Act
                    parameters.TenonLenght = sourceValue;
                }
            );
        }

        [TestCase(TestName = "Ширна лапки: позитивный тест")]
        public void TenonWidth_CorrectValue_ReturnCorrectValue()
        {
            //Setup
            var parameters = new DrillParameters
            {
                DrillLenght = 100,
                WorkingPartLenght = 50,
                DrillDiameter = 20.00
            };
            var sourceValue = 10.50;
            var expectedValue = sourceValue;

            //Act
            parameters.TenonWidth = sourceValue;
            var actualValue = parameters.TenonWidth;

            //Assert
            NUnit.Framework.Assert.AreEqual(expectedValue, actualValue);
        }

        [TestCase(TestName = "Ширина лапки: выход за границы диапазона")]
        public void TenonWidth_OutOfRangeValue_ThrowsException()
        {
            //Setup
            var parameters = new DrillParameters();
            var sourceValue = 100.50;

            //Act
            NUnit.Framework.Assert.Throws<ArgumentException>
            (
                () =>
                {
                    //Act
                    parameters.TenonWidth = sourceValue;
                }
            );
        }

        [TestCase(TestName = "Ширина лапки не может быть больше диаметра сверла")]
        public void TenonWidth_ValueLessDrillDiameter_ThrowsException()
        {
            //Setup
            var parameters = new DrillParameters
            {
                DrillLenght = 100,
                WorkingPartLenght = 50,
                DrillDiameter = 15.00
            };
            var sourceValue = 19.50;
            try
            {
                parameters.TenonWidth = sourceValue;
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
            //Act
            NUnit.Framework.Assert.Throws<ArgumentException>
            (
                () =>
                {
                    //Act
                    parameters.TenonWidth = sourceValue;
                }
            );
        }

        [TestCase(TestName = "Длина шейки: позитивный тест")]
        public void NeckLenght_CorrectValue_ReturnCorrectValue()
        {
            //Setup
            var parameters = new DrillParameters
            {
                DrillLenght = 20.50
            };
            var sourceValue = 9.50;
            var expectedValue = sourceValue;

            //Act
            parameters.NeckLenght = sourceValue;
            var actualValue = parameters.NeckLenght;

            //Assert
            NUnit.Framework.Assert.AreEqual(expectedValue, actualValue);
        }

        [TestCase(TestName = "Длина шейки: выход за диапазон массива")]
        public void NeckLenght_OutOfRangeValue_ThrowsException()
        {
            //Setup
            var parameters = new DrillParameters();
            var sourceValue = 200.00;

            //Act
            NUnit.Framework.Assert.Throws<ArgumentException>
            (
                () =>
                {
                    //Act
                    parameters.NeckLenght = sourceValue;
                }
            );
        }

        [TestCase(TestName = "Длина шейки: не выполнение условия L - (a+d+l) > 0.5")]
        public void NeckLenght_IncorrectValue_ThrowsException()
        {
            //Setup
            var parameters = new DrillParameters
            {
                DrillLenght = 50.00,
                WorkingPartLenght = 20.00,
                TenonLenght = 20.00
            };
            var sourceValue = 10.00;

            //Act
            NUnit.Framework.Assert.Throws<ArgumentException>
            (
                () =>
                {
                    //Act
                    parameters.NeckLenght = sourceValue;
                }
            );
        }

        [TestCase(TestName = "Ширина шейки: позитивный тест")]
        public void NeckWidth_CorrectValue_ReturnCorrectValue()
        {
            //Setup
            var parameters = new DrillParameters
            {
                DrillLenght = 100,
                WorkingPartLenght = 50,
                DrillDiameter = 20.50
            };
            var sourceValue = 9.50;
            var expectedValue = sourceValue;

            //Act
            parameters.NeckWidth = sourceValue;
            var actualValue = parameters.NeckWidth;

            //Assert
            NUnit.Framework.Assert.AreEqual(expectedValue, actualValue);
        }

        [TestCase(TestName = "Шириан шейки: выход за диапазон значений")]
        public void NeckWidth_OutOfRangeValue_ThrowsException()
        {
            //Setup
            var parameters = new DrillParameters();
            var sourceValue = 250.00;

            //Act
            NUnit.Framework.Assert.Throws<ArgumentException>
            (
                () =>
                {
                    //Act
                    parameters.NeckWidth = sourceValue;
                }
            );
        }

        [TestCase(TestName = "Ширина шейки не может быть больше диаметра сверла")]
        public void NeckWidth_ValueLessDrillDiameter_ThrowsException()
        {
            //Setup
            var parameters = new DrillParameters();
            var sourceValue = 9.00;

            //Act
            NUnit.Framework.Assert.Throws<ArgumentException>
            (
                () =>
                {
                    //Act
                    parameters.NeckWidth = sourceValue;
                }
            );
        }
    }
}