using System;
using NUnit.Framework;

namespace DrillKOMPAS.UnitTests
{
    [TestFixture]
    public class DrillParametersTest
    {
        [Test]
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

        [Test]
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

        [Test]
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

        [Test]
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

        [Test]
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

        [Test]
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

        [Test]
        public void DrillDiameter_CorrectValue_ReturnCorrectValue()
        {
            //Setup
            var parameters = new DrillParameters();
            var sourceValue = 20.80;
            var expectedValue = sourceValue;

            //Act
            parameters.DrillDiameter = sourceValue;
            var actualValue = parameters.DrillDiameter;

            //Assert
            NUnit.Framework.Assert.AreEqual(expectedValue, actualValue);
        }

        [Test]
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

        [Test]
        public void DrillDiameter_ValueLessNeckWidth_ThrowsException()
        {
            //Setup
            var parameters = new DrillParameters
            {
                DrillLenght = 40.00,
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

        [Test]
        public void DrillDiameter_ValueLessTenonWidth_ThrowsException()
        {
            //Setup
            var parameters = new DrillParameters
            {
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

        [Test]
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

        [Test]
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

        [Test]
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

        [Test]
        public void TenonWidth_CorrectValue_ReturnCorrectValue()
        {
            //Setup
            var parameters = new DrillParameters
            {
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

        [Test]
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

        [Test]
        public void TenonWidth_ValueLessDrillDiameter_ThrowsException()
        {
            //Setup
            var parameters = new DrillParameters
            {
                DrillDiameter = 15.00
            };
            var sourceValue = 20.50;

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

        [Test]
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

        [Test]
        public void NeckLenght_OutOfRangeValue_ThrowsException()
        {
            //Setup
            var parameters = new DrillParameters();
            var sourceValue = 20.00;

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

        [Test]
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

        [Test]
        public void NeckWidth_CorrectValue_ReturnCorrectValue()
        {
            //Setup
            var parameters = new DrillParameters
            {
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

        [Test]
        public void NeckWidth_OutOfRangeValue_ThrowsException()
        {
            //Setup
            var parameters = new DrillParameters();
            var sourceValue = 25.00;

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

        [Test]
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
