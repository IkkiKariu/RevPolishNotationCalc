using System.ComponentModel.DataAnnotations;

namespace PNCalculator
{
    internal class Programm
    {
        enum Operatorz
        {
            Addition,
            Subtraction,
            Multiplication,
            Division,
            DIV,
            MOD,
            Exponentation
        }

        public static void Main(string[] args)
        {
            Calculator calc = new Calculator();
            /*List<string> tokenList = new List<string>();

            tokenList.Add("(");
            tokenList.Add("-4");
            tokenList.Add("+");
            tokenList.Add("-4");
            tokenList.Add(")");
            tokenList.Add("*");
            tokenList.Add("2");
            tokenList.Add("^");
            tokenList.Add("2");
            var res = Calculator.ConvertToRevNotation(tokenList);

            foreach(var i in res)
            {
                Console.Write(i);
            }
            Console.WriteLine();

            var result = Calculator.Calculate(res);
            Console.WriteLine(result);
            Console.WriteLine((-4 + -4) * 4);*/

            string userInput = Console.ReadLine();

            List<string> resultExp = Calculator.ParseExpression(userInput);

            foreach(var token in resultExp)
            {
                Console.Write(token);
            }

            Console.WriteLine();

            foreach (var token in Calculator.ConvertToRevNotation(resultExp))
            {
                Console.Write(token);
            }

            Console.WriteLine();

            BigInteger resultt = Calculator.Calculate(Calculator.ConvertToRevNotation(resultExp));

            if (resultt.isNegative)
                Console.Write('-');

            for (int i = resultt.digits.Count - 1; i >= 0; i--)
            {
                Console.Write(resultt.digits[i]);
            }
        }
    }
}