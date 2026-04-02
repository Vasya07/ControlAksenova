using System;
using System.Windows;

namespace ControlAksenova
{
    public partial class MainWindow : Window
    {
        private const double MIN_HEIGHT = 130.0;
        private const double MAX_HEIGHT = 220.0;
        private const double MIN_WEIGHT = 40.0;
        private const double MAX_WEIGHT = 170.0;
        private const double TOLERANCE_KG = 3.0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void CalcButton_Click(object sender, RoutedEventArgs e)
        {
            IdealWeightText.Text = "";
            DeviationText.Text = "";
            RatingText.Text = "";

            if (!double.TryParse(HeightBox.Text, out double height))
            {
                MessageBox.Show("Пожалуйста, введите рост в виде числа (десятичные дроби через запятую)",
                    "Нечисловой ввод", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (height < MIN_HEIGHT || height > MAX_HEIGHT)
            {
                MessageBox.Show($"Выход роста за пределы допустимого диапазона! Разрешённый диапазон: 130 см - 220 см",
                    "Недопустимый рост", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!double.TryParse(WeightBox.Text, out double weight))
            {
                MessageBox.Show("Пожалуйста, введите вес в виде числа (десятичные дроби через запятую)",
                    "Нечисловой ввод", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (weight < MIN_WEIGHT || weight > MAX_WEIGHT)
            {
                MessageBox.Show($"Выход веса за пределы допустимого диапазона! Разрешённый диапазон: 40 кг - 170 кг",
                    "Недопустимый вес", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            double normalWeight;
            string genderLabel;

            if (MaleRadio.IsChecked == true)
            {
                normalWeight = (height - 100) * 1.13;
                genderLabel = "мужчины";
            }
            else
            {
                normalWeight = (height - 100) * 0.90;
                genderLabel = "женщины";
            }

            normalWeight = Math.Round(normalWeight, 1);
            double deviation = weight - normalWeight;
            double absDeviation = Math.Round(Math.Abs(deviation), 1);
            string deviationText;

            if (absDeviation == 0)
            {
                deviationText = "отклонение: отсутствует";
            }
            else
            {
                string deviationSign = deviation > 0 ? "+" : "-";
                deviationText = $"отклонение: {deviationSign}{absDeviation} кг";
            }

            string rating;
            if (deviation < -TOLERANCE_KG)
                rating = "ниже нормы";
            else if (deviation > TOLERANCE_KG)
                rating = "выше нормы";
            else
                rating = "норма";

            IdealWeightText.Text = $"Нормальный вес для {genderLabel}: {normalWeight} кг";
            DeviationText.Text = $"Ваш вес: {weight} кг ({deviationText})";
            RatingText.Text = $"Оценка веса: {rating}";

            if (rating == "норма")
                RatingText.Foreground = System.Windows.Media.Brushes.Green;
            else if (rating == "выше нормы")
                RatingText.Foreground = System.Windows.Media.Brushes.OrangeRed;
            else
                RatingText.Foreground = System.Windows.Media.Brushes.DarkOrange;
        }
    }
}