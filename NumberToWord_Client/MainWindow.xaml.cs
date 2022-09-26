using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NumberToWord_Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly ServiceReference1.Service1Client numberToWordService = new ServiceReference1.Service1Client();
        public MainWindow()
        {
            InitializeComponent();
        }
        CultureInfo CultureInfo = CultureInfo.CreateSpecificCulture("fr-FR");
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            // Use SelectionStart property to find the caret position.
            // Insert the previewed text into the existing text in the textbox.
            var fullText = textBox.Text.Insert(textBox.SelectionStart, e.Text);
            bool flag=decimal.TryParse(fullText, NumberStyles.AllowDecimalPoint, CultureInfo, out decimal num);
            if (flag)
            {
                flag = num <= 999_999_999;
                if (flag&&fullText.Contains(","))
                {
                    flag = fullText.Substring(fullText.IndexOf(",")).Length <= 2;
                }
            }
            e.Handled = !flag;
        }

        private void txtInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            decimal.TryParse(txtInput.Text, NumberStyles.AllowDecimalPoint, CultureInfo, out decimal num);
            txtResult.Text = numberToWordService.ConvertToWord(num);
        }
    }
}
