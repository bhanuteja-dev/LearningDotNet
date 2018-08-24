using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace LearningDotNet
{
    public static class Program
    {
        public static void Main()
        {
            Console.WriteLine("Enter Inputs in comma Separated");
            var input = Console.ReadLine();
            
            List<int> inputs;

            if (input != null && CheckIfEachNumberIsSeparatedByComma(input))
            {
                inputs = ConvertCollectionToInt(input.Split(',').ToList());
            }
            else
            {
                throw new ArgumentException("Invalid Input");
            }

            Console.WriteLine("Enter the result: ");
            var result = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("The result expression is: ");
            Console.WriteLine(FindExpression.GetFinalExpression(inputs, result));
        }

        private static bool CheckIfEachNumberIsSeparatedByComma(string input)
        {
            const string pattern = "[0-9,]+";

            return Regex.IsMatch(input, pattern);
        }

        private static List<int> ConvertCollectionToInt(IEnumerable<string> input)
        {
            return input.Select(item => Convert.ToInt32(item)).ToList();
        }
    }
}