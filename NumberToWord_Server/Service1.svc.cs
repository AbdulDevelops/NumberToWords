using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace NumberToWord_Server
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public string ConvertToWord(decimal value)
        {
            return NumberToWords(value);
        }
        /// <summary>
        /// Convert a decimal number to word
        /// </summary>
        /// <param name="doubleNumber"></param>
        /// <returns>The Number in word</returns>
        public string NumberToWords(decimal doubleNumber)
        {
            if (doubleNumber == 0)
                return "zero dollars";
            //Get number 
            int beforeFloatingPoint = (int)Math.Floor(doubleNumber);
            int afterFloatingPoint = (int)((doubleNumber - beforeFloatingPoint) * 100);
            //Convert before floating to word
            string beforeFloatingPointInWords = NumberToWords(beforeFloatingPoint);
            //Convert after floating to word
            string afterFloatingPointInWords = SmallNumberToWord(afterFloatingPoint);
            //Get the suffix according to the number
            string dollorsuffix = beforeFloatingPoint == 1 ? "dollar" : "dollars";
            //Cent suffix
            string centSuffix = afterFloatingPointInWords=="one"?"cent":"cents";
            if ((int)((doubleNumber - beforeFloatingPoint) * 100) > 0)
            {
                //Format the string and return
                return string.Format("{0} {1} and {2} {3}", beforeFloatingPointInWords, dollorsuffix, afterFloatingPointInWords, centSuffix);
            }
            else
            {
                //Format the string and return
                return string.Format("{0} {1}", beforeFloatingPointInWords, dollorsuffix);
            }
        }
        /// <summary>
        /// Convert an int number to word
        /// </summary>
        /// <param name="number"></param>
        /// <returns>The Number in word</returns>
        private string NumberToWords(int number)
        {
            if (number == 0)
                return "zero";
            //If negative
            if (number < 0)
                    return "minus " + NumberToWords(Math.Abs(number));

            var words = "";
            //divide by 1000000000 number 
            if (number / 1000000000 > 0)
            {
                //Conver to word and add billion suffix
                words += NumberToWords(number / 1000000000) + " billion ";
                number %= 1000000000;
            }

            if (number / 1000000 > 0)
            {
                //Convert to word and add million suffix
                words += NumberToWords(number / 1000000) + " million ";
                number %= 1000000;
            }

            if (number / 1000 > 0)
            {
                //Convert to word and add thousand suffix
                words += NumberToWords(number / 1000) + " thousand ";
                number %= 1000;
            }

            if (number / 100 > 0)
            {
                //Convert to word and add hundred suffix
                words += NumberToWords(number / 100) + " hundred ";
                number %= 100;
            }
            //This method is used to convert <100 numbers to words
            words = SmallNumberToWord(number, words);
            return words;
        }
        string[] Units = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
        string[] Tens = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };
        /// <summary>
        /// Conver <100 numbers to word
        /// </summary>
        /// <param name="number"></param>
        /// <param name="words"></param>
        /// <returns>The number in Word</returns>
        private string SmallNumberToWord(int number, string words = "")
        {
            if (number <= 0) return words;
            if (words != "")
            {
                words += " ";
            }
            if (number < 20)
            {
                words += Units[number];
            }
            else
            {
                words += Tens[number / 10];
                if ((number % 10) > 0)
                    words += "-" + Units[number % 10];
            }
            return words;
        }
        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
