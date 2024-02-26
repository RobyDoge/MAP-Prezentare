using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Prezentare_Converters
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Text = ""; 
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "Enter text here";
            }
        }


        /*
         * type of converters:
         * 1. using class Convert:
         *  - Convert.To{Type}({objectToConvert})
         * 2. direct casting:
         *  - {object}.To{Type}()
         * 3. using class Parse:
         *  - {Type}.Parse(string)
         */
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var originalTextBox = (TextBox)sender;
            var equation = originalTextBox.Text;

            if (!equation.Contains('=')) return;
            
            var parts = equation.Split('=');
            if (parts.Length != 2) return;
            
            var expression = parts[0].Trim();
            if (string.IsNullOrWhiteSpace(expression)) return;


            string[] numbers;
            double result;
            if (equation.Contains('+'))
            {
                numbers = expression.Split("+");
                result = double.Parse(numbers[0]) + double.Parse(numbers[1]);
                originalTextBox.Text = result.ToString();
                originalTextBox.CaretIndex = originalTextBox.Text.Length;
                return;
            }

            
            if (equation.Contains('-'))
            {
                numbers = expression.Split("-");
                result = Convert.ToDouble(numbers[0]) - Convert.ToDouble(numbers[1]);
                originalTextBox.Text = result.ToString();
                originalTextBox.CaretIndex = originalTextBox.Text.Length;
                return;
            }

            if (equation.Contains('*'))
            {
                numbers = expression.Split("*");
                result = Convert.ToDouble(numbers[0]) * Convert.ToDouble(numbers[1]);
                originalTextBox.Text = result.ToString();
                originalTextBox.CaretIndex = originalTextBox.Text.Length;
                return;
            }

            if (!equation.Contains('/')) return;

            numbers = expression.Split("/");
            result = double.Parse(numbers[0]) / double.Parse(numbers[1]);
            originalTextBox.Text = Convert.ToString(result);
            originalTextBox.CaretIndex = originalTextBox.Text.Length;
        }
    }
}