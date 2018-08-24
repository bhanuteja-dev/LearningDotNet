using System;
using System.Collections.Generic;
using System.Linq;

namespace LearningDotNet
{
    public static class FindExpression
    {
        public static string GetFinalExpression(IReadOnlyCollection<int> inputs, int result)
        {
            var combinedValue = 0;
            Random rnd = new Random();

            while (true)
            {
                bool ForTheFirstCount(int i)
                {
                    return i == 1;
                }

                var count = 1;
                var expression = "";

                //Useful for getting random numbers and remove picked up numbers
                List<int> inputNumbers = inputs.ToList();

                while (count < inputs.Count)
                {
                    Tuple<string, int> expressionWithCombinedValue;

                    expression = ForTheFirstCount(count)
                        ? GetExpressionForFirstCount(inputNumbers, out expressionWithCombinedValue, rnd)
                        : GetExpressionForRestOfTheCount(inputNumbers, combinedValue, out expressionWithCombinedValue,
                            expression, rnd);

                    combinedValue = expressionWithCombinedValue.Item2;

                    if (combinedValue == result)
                    {
                        return expression;
                    }

                    count++;
                }
            }
        }

        private static string GetExpressionForRestOfTheCount(List<int> inputNumbers, int combinedValue,
            out Tuple<string, int> expressionWithCombinedValue, string expression, Random rnd)
        {
            Tuple<int, int> randomNumberAndIndex = GetRandomNumberAndRemoveItFromCollection(inputNumbers);

            expressionWithCombinedValue =
                CallRandomMethodForExpressionAndValue(rnd.Next(0, 4), combinedValue,
                    randomNumberAndIndex.Item1);

            expression += " " + expressionWithCombinedValue.Item1 + " " + randomNumberAndIndex.Item1;
            return expression;
        }

        private static string GetExpressionForFirstCount(List<int> inputNumbers,
            out Tuple<string, int> expressionWithCombinedValue, Random rnd)
        {
            Tuple<int, int> randomNumberAndIndex1 = GetRandomNumberAndRemoveItFromCollection(inputNumbers);

            Tuple<int, int> randomNumberAndIndex2 = GetRandomNumberAndRemoveItFromCollection(inputNumbers);

            expressionWithCombinedValue = CallRandomMethodForExpressionAndValue(rnd.Next(0, 4),
                randomNumberAndIndex1.Item1,
                randomNumberAndIndex2.Item1);

            var expression = randomNumberAndIndex1.Item1 + " " + expressionWithCombinedValue.Item1 + " " +
                             randomNumberAndIndex2.Item1;

            return expression;
        }

        private static Tuple<int, int> GetRandomNumberAndRemoveItFromCollection(List<int> inputNumbers)
        {
            Tuple<int, int> randomNumberAndIndex1 = GetRandomNumberAndIndex(inputNumbers);
            inputNumbers.RemoveAt(randomNumberAndIndex1.Item2);

            return randomNumberAndIndex1;
        }

        private static Tuple<int, int> GetRandomNumberAndIndex(IReadOnlyList<int> inputNumbers)
        {
            Random rnd = new Random();

            var index = rnd.Next(0, inputNumbers.Count);

            return new Tuple<int, int>(inputNumbers[index], index);
        }

        private static Tuple<string, int> CallRandomMethodForExpressionAndValue(int randomNumber, int input1,
            int input2)
        {
            string expression;
            int value;

            switch (randomNumber)
            {
                case 0:
                    expression = "+";
                    value = Sum(input1, input2);
                    break;
                case 1:
                    expression = "*";
                    value = Multiply(input1, input2);
                    break;
                case 2:
                    expression = "-";
                    value = Subtract(input1, input2);
                    break;
                case 3:
                    expression = "/";
                    value = Divide(input1, input2);
                    break;
                default:
                    throw new ArgumentException("Invalid Case to be considered");
            }

            return new Tuple<string, int>(expression, value);
        }

        private static int Sum(int input1, int input2)
        {
            return input1 + input2;
        }

        private static int Multiply(int input1, int input2)
        {
            return input1 * input2;
        }

        private static int Subtract(int input1, int input2)
        {
            return input1 - input2;
        }

        private static int Divide(int input1, int input2)
        {
            if (input2 <= 0) throw new ArgumentOutOfRangeException(nameof(input2));
            return input1 / input2;
        }
    }
}