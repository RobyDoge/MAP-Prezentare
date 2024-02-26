using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;


namespace Prezentare_MultiValueConverter
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged_3(object sender, TextChangedEventArgs e)
        {
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
        }

        private void molesBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void TextBox_TextChanged_2(object sender, TextChangedEventArgs e)
        {
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Get values from TextBoxes
            string volume = volumeBox.Text;
            string moles = molesBox.Text;
            string pressure = pressureBox.Text;
            string temperature = temperatureBox.Text;
            string constant = constantBox.Text;

            // Invoke the converter with the TextBox values
            var sumConverter = new SumConverter();
            sumConverter.ButtonClicked();
            object result = sumConverter.Convert(new object[] { volume, moles, pressure, temperature, constant }, typeof(object), null, CultureInfo.CurrentCulture);

            // Update UI with the result
            if (result != null)
            {
                resultTextBox.Text = result.ToString();
            }
        }

    }
    public class SumConverter : IMultiValueConverter
    {
        private bool isButtonClicked = false;
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (!isButtonClicked) return null;

            Dictionary<string,float> dict = new();
            foreach (var value in values)
            {
                string str = value as string;
                if (str == null)
                {
                    return null;
                }
                var parts = str.Split('=');
                if (parts[0].Contains("Constanta")) continue;

                var symbol = parts[0].Trim();
                var number = float.Parse(parts[1].Trim());
                dict.Add(symbol, number);
            }
            

            var nullSymbol = dict.FirstOrDefault(x => x.Value == 0.0f).Key;
            dict["Temperatura"] += 273;
            string result = nullSymbol+" = ";
            switch (nullSymbol)
            {
                case "Volum":
                    return result +((dict["Temperatura"] * dict["Moli"] * 0.0821) / dict["Presiune"]);
                case "Presiune":
                    return result + ((dict["Temperatura"] * dict["Moli"] * 0.0821) / dict["Volum"]);
                case "Temperatura":
                    return result + (((dict["Presiune"] * dict["Volum"]) / (dict["Moli"] * 0.0821))-273);
                case "Moli":
                    return result + ((dict["Presiune"] * dict["Volum"]) / (dict["Temperatura"] * 0.0821));
                default:
                    return null;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public void ButtonClicked()
        {
            isButtonClicked = true;
        }
    }
}