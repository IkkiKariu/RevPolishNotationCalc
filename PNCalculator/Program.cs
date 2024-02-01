using System.ComponentModel.DataAnnotations;

namespace PNCalculator
{
    internal class Programm
    {
        /*enum Operatorz
        {
            Addition,
            Subtraction,
            Multiplication,
            Division,
            DIV,
            MOD,
            Exponentation
        }*/

        public static void Main(string[] args)
        {
            Calculator calc = new Calculator();

            while (true)
            {
                Console.WriteLine("Введите выражение: ");
                string userInput = Console.ReadLine();

                List<string> parsedExp = Calculator.ParseExpression(userInput);

               /* foreach (var token in parsedExp)
                {
                    Console.Write(token);
                }*/

                Console.Write("\nВыражение в обратной польской нотаци:\n");

                
                foreach (var token in Calculator.ConvertToRevNotation(parsedExp))
                {
                    Console.Write(token + " ");
                }

                Console.WriteLine("\n");

                BigInteger resultt = Calculator.Calculate(Calculator.ConvertToRevNotation(parsedExp));

                Console.WriteLine("Результат: ");

                if (resultt.isNegative)
                    Console.Write('-');

                for (int i = resultt.digits.Count - 1; i >= 0; i--)
                {
                    Console.Write(resultt.digits[i]);
                }
                Console.WriteLine();
                Console.WriteLine("---\n");
            }
        }
    }
}