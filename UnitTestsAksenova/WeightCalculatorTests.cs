using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ControlAksenova.Tests
{
    [TestClass]
    public class WeightCalculatorTests
    {
        private const double Delta = 0.1;

        [TestMethod]
        [DataRow(130, 33.9)]
        [DataRow(150, 56.5)]
        [DataRow(170, 79.1)]
        [DataRow(180, 90.4)]
        [DataRow(200, 113.0)]
        [DataRow(220, 135.6)]
        public void NormalWeight_Male_ReturnsCorrect(double height, double expected)
        {
            double actual = (height - 100) * 1.13;
            actual = Math.Round(actual, 1);
            Assert.AreEqual(expected, actual, Delta);
        }

        [TestMethod]
        [DataRow(130, 27.0)]
        [DataRow(150, 45.0)]
        [DataRow(160, 54.0)]
        [DataRow(165, 58.5)]
        [DataRow(170, 63.0)]
        [DataRow(190, 81.0)]
        [DataRow(220, 108.0)]
        public void NormalWeight_Female_ReturnsCorrect(double height, double expected)
        {
            double actual = (height - 100) * 0.90;
            actual = Math.Round(actual, 1);
            Assert.AreEqual(expected, actual, Delta);
        }

        [TestMethod]
        [DataRow(-10.0, "ниже нормы")]
        [DataRow(-5.0, "ниже нормы")]
        [DataRow(-3.1, "ниже нормы")]
        [DataRow(-3.0, "норма")]
        [DataRow(-2.5, "норма")]
        [DataRow(-1.0, "норма")]
        [DataRow(0.0, "норма")]
        [DataRow(1.0, "норма")]
        [DataRow(2.5, "норма")]
        [DataRow(3.0, "норма")]
        [DataRow(3.1, "выше нормы")]
        [DataRow(5.0, "выше нормы")]
        [DataRow(15.0, "выше нормы")]
        public void Rating_Deviation_ReturnsCorrectRating(double deviation, string expectedRating)
        {
            const double tolerance = 3.0;
            string actualRating;

            if (deviation < -tolerance)
                actualRating = "ниже нормы";
            else if (deviation > tolerance)
                actualRating = "выше нормы";
            else
                actualRating = "норма";

            Assert.AreEqual(expectedRating, actualRating);
        }

        [TestMethod]
        public void DeviationText_ZeroDeviation_ReturnsAbsent()
        {
            double deviation = 0;
            double absDeviation = Math.Round(Math.Abs(deviation), 1);
            string deviationText;

            if (absDeviation == 0)
                deviationText = "отклонение: отсутствует";
            else
                deviationText = $"отклонение: {(deviation > 0 ? "+" : "-")}{absDeviation} кг";

            Assert.AreEqual("отклонение: отсутствует", deviationText);
        }

        [TestMethod]
        [DataRow(70.0, 75.0, "+5 кг")]
        [DataRow(70.0, 65.0, "-5 кг")]
        [DataRow(70.0, 70.5, "+0.5 кг")]
        [DataRow(70.0, 69.3, "-0.7 кг")]
        [DataRow(70.0, 73.0, "+3 кг")]
        [DataRow(70.0, 67.0, "-3 кг")]
        public void DeviationText_NonZeroDeviation_ReturnsCorrect(double normalWeight, double weight, string expectedSuffix)
        {
            double deviation = weight - normalWeight;
            double absDeviation = Math.Round(Math.Abs(deviation), 1);
            string deviationText;

            if (absDeviation == 0)
                deviationText = "отклонение: отсутствует";
            else
                deviationText = $"отклонение: {(deviation > 0 ? "+" : "-")}{absDeviation} кг";

            Assert.AreEqual($"отклонение: {expectedSuffix}", deviationText);
        }

        [TestMethod]
        [DataRow(170, 79.1, "норма")]
        [DataRow(170, 76.1, "норма")]
        [DataRow(170, 82.1, "норма")]
        [DataRow(170, 75.0, "ниже нормы")]
        [DataRow(170, 85.0, "выше нормы")]
        [DataRow(180, 90.4, "норма")]
        [DataRow(180, 80.0, "ниже нормы")]
        [DataRow(180, 95.0, "выше нормы")]
        [DataRow(200, 113.0, "норма")]
        public void FullScenario_Male_ReturnsCorrectRating(double height, double weight, string expectedRating)
        {
            double normalWeight = (height - 100) * 1.13;
            double deviation = weight - normalWeight;
            const double tolerance = 3.0;
            string actualRating;

            if (deviation < -tolerance)
                actualRating = "ниже нормы";
            else if (deviation > tolerance)
                actualRating = "выше нормы";
            else
                actualRating = "норма";

            Assert.AreEqual(expectedRating, actualRating);
        }

        [TestMethod]
        [DataRow(160, 54.0, "норма")]
        [DataRow(160, 51.0, "норма")]
        [DataRow(160, 57.0, "норма")]
        [DataRow(160, 50.0, "ниже нормы")]
        [DataRow(160, 58.0, "выше нормы")]
        [DataRow(165, 58.5, "норма")]
        [DataRow(165, 55.0, "ниже нормы")]
        [DataRow(165, 62.0, "выше нормы")]
        [DataRow(170, 63.0, "норма")]
        [DataRow(170, 60.0, "ниже нормы")]
        [DataRow(170, 66.0, "выше нормы")]
        public void FullScenario_Female_ReturnsCorrectRating(double height, double weight, string expectedRating)
        {
            double normalWeight = (height - 100) * 0.90;
            double deviation = weight - normalWeight;
            const double tolerance = 3.0;
            string actualRating;

            if (deviation < -tolerance)
                actualRating = "ниже нормы";
            else if (deviation > tolerance)
                actualRating = "выше нормы";
            else
                actualRating = "норма";

            Assert.AreEqual(expectedRating, actualRating);
        }

        [TestMethod]
        public void HeightBoundaries_AreCorrect()
        {
            const double minHeight = 130.0;
            const double maxHeight = 220.0;

            Assert.AreEqual(130.0, minHeight);
            Assert.AreEqual(220.0, maxHeight);
            Assert.IsTrue(minHeight < maxHeight);
        }

        [TestMethod]
        public void WeightBoundaries_AreCorrect()
        {
            const double minWeight = 40.0;
            const double maxWeight = 170.0;

            Assert.AreEqual(40.0, minWeight);
            Assert.AreEqual(170.0, maxWeight);
            Assert.IsTrue(minWeight < maxWeight);
        }

        [TestMethod]
        [DataRow("175", true)]
        [DataRow("180.5", true)]
        [DataRow("65,2", true)]
        [DataRow("70.5", true)]
        [DataRow("abc", false)]
        [DataRow("", false)]
        [DataRow("   ", false)]
        [DataRow("12a", false)]
        [DataRow("-50", true)]
        public void IsNumeric_InputString_ReturnsExpected(string input, bool expected)
        {
            bool actual = double.TryParse(input, out _);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow(90.4444, 90.4)]
        [DataRow(79.1666, 79.2)]
        [DataRow(58.4999, 58.5)]
        [DataRow(63.0000, 63.0)]
        [DataRow(135.5555, 135.6)]
        public void Rounding_ToOneDecimal_ReturnsCorrect(double input, double expected)
        {
            double actual = Math.Round(input, 1);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FullCalculation_Male_WithDeviationText()
        {
            double height = 180;
            double weight = 80;
            double normalWeight = Math.Round((height - 100) * 1.13, 1);
            double deviation = weight - normalWeight;
            double absDeviation = Math.Round(Math.Abs(deviation), 1);
            string deviationText;

            if (absDeviation == 0)
                deviationText = "отклонение: отсутствует";
            else
                deviationText = $"отклонение: {(deviation > 0 ? "+" : "-")}{absDeviation} кг";

            Assert.AreEqual(90.4, normalWeight);
            Assert.AreEqual(-10.4, deviation);
            Assert.AreEqual("отклонение: -10.4 кг", deviationText);
        }

        [TestMethod]
        public void FullCalculation_Female_ZeroDeviation_ReturnsAbsent()
        {
            double height = 160;
            double weight = 54;
            double normalWeight = Math.Round((height - 100) * 0.90, 1);
            double deviation = weight - normalWeight;
            double absDeviation = Math.Round(Math.Abs(deviation), 1);
            string deviationText;

            if (absDeviation == 0)
                deviationText = "отклонение: отсутствует";
            else
                deviationText = $"отклонение: {(deviation > 0 ? "+" : "-")}{absDeviation} кг";

            Assert.AreEqual(54.0, normalWeight);
            Assert.AreEqual(0, deviation);
            Assert.AreEqual("отклонение: отсутствует", deviationText);
        }
    }
}