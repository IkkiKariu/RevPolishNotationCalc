using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PNCalculator
{
    internal class Calculator
    {
        private enum Operators
        {
            Addition,
            Subtraction,
            Multiplication,
            Division,
            DIV,
            MOD,
            Exponentation
        }

        private static Dictionary<string, uint> _operatorPriorities = new Dictionary<string, uint>();


        public Calculator()
        {
            _operatorPriorities["+"] = 0;
            _operatorPriorities["-"] = 0;
            _operatorPriorities["*"] = 1;
            _operatorPriorities["/"] = 1;
            _operatorPriorities["div"] = 1;
            _operatorPriorities["mod"] = 1;
            _operatorPriorities["^"] = 2;
        }

        private static Stack<string> _operatorStack = new Stack<string>();


        public static List<string>? ConvertToRevNotation(List<string> expTokens)
        {
            _operatorStack.Clear();

            List<string> output = new List<string>();

            foreach (string token in expTokens)
            {
                if (IsNumber(token))
                {
                    output.Add(token);
                    continue;
                }

                if (_operatorStack.Count == 0)
                {
                    _operatorStack.Push(token);
                    continue;
                }

                switch(token)
                {
                    case "+":
                    case "-":
                    case "*":
                    case "/":
                    case "div":
                    case "mod":
                    case "^":
                        while (_operatorStack.Count != 0)
                        {
                            if (_operatorStack.Peek() == "(")
                            {
                                break;
                            }

                            if (_operatorPriorities[token] > _operatorPriorities[_operatorStack.Peek()])
                            {
                                break;
                            }
                            else
                            {
                                output.Add(_operatorStack.Pop());
                            }


                        }

                        _operatorStack.Push(token);
                        break;
                    case "(":
                        _operatorStack.Push(token);
                        break;
                    case ")":
                        while(_operatorStack.Peek() != "(")
                        {
                            output.Add(_operatorStack.Pop());
                        }
                        _operatorStack.Pop();
                        break;
                    default:
                        Console.WriteLine("Unexpected token");
                        return null;
                }

            }
            while (_operatorStack.Count != 0)
            {
                output.Add(_operatorStack.Pop());
            }
            return output;
        }

        public static BigInteger? Calculate(List<string> polNotationExp)
        {
            Stack<BigInteger> numberStack = new Stack<BigInteger>();

            foreach (string token in polNotationExp)
            {
                
                if (IsNumber(token))
                {
                    double number;
                    Double.TryParse(token, out number);
                    numberStack.Push(new BigInteger(number.ToString()));
                    continue;
                }

                BigInteger operand1;
                BigInteger operand2;

                switch (token)
                {
                    case "+":
                        operand1 = numberStack.Pop();
                        operand2 = numberStack.Pop();

                        numberStack.Push(operand2 + operand1);
                        break;
                    case "-":
                        operand1 = numberStack.Pop();
                        operand2 = numberStack.Pop();

                        numberStack.Push(operand2 - operand1);
                        break;
                    case "*":
                        operand1 = numberStack.Pop();
                        operand2 = numberStack.Pop();

                        numberStack.Push(operand2 * operand1);
                        break;
                    /*case "/":
                        operand1 = new BigInteger(numberStack.Pop().ToString());
                        operand2 = new BigInteger(numberStack.Pop().ToString());

                        numberStack.Push(operand2 / operand1);
                        break;*/
                    case "div":
                        operand1 = numberStack.Pop();
                        operand2 = numberStack.Pop();

                        numberStack.Push(operand2 / operand1);
                        break;
                    case "mod":
                        operand1 = numberStack.Pop();
                        operand2 = numberStack.Pop();

                        numberStack.Push(operand2 % operand1);
                        break;
                    /*case "^":
                        operand1 = numberStack.Pop();
                        operand2 = numberStack.Pop();

                        numberStack.Push(Math.Pow(operand2, operand1));
                        break;*/
                    default:
                        Console.WriteLine("Unexpected token");
                        return null;
                }
            }
            return numberStack.Pop();
        }

        public static List<string>? ParseExpression(string expression)
        {
            expression = expression.Replace(" ", "") + 'e';

            char[] numLetters = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', ',' };
            List<string> resultExp = new List<string>();

            for (int i = -1; i < expression.Length; )
            {
                i++;
                double n;
                try
                {

                    if (Double.TryParse(expression[i].ToString(), out n))
                    {
                        string number = "";

                        var j = i;
                        while (numLetters.Contains(expression[j]))
                        {
                            number += expression[j];
                            j++;
                        }
                        resultExp.Add(number);
                        i += number.Length - 1;

                        continue;
                    }
                }
                catch 
                {
                    return resultExp;
                }
                switch (expression[i])
                {
                    case '+':
                    case '-':
                    case '*':
                    case '/':
                    case '^':
                    case '(':
                    case ')':
                        resultExp.Add(expression[i].ToString());
                        break;
                    case 'd':
                        if (expression[i + 1] == 'i' && expression[i + 2] == 'v')
                        {
                            resultExp.Add("div");
                            i += 2; 
                        }
                        else
                        {
                            Console.WriteLine("Unexpected token");
                            return null;
                        }
                        break;
                    case 'm':
                        if (expression[i + 1] == 'o' && expression[i + 2] == 'd')
                        {
                            resultExp.Add("mod");
                            i += 2;
                        }
                        else
                        {
                            Console.WriteLine("Unexpected token");
                            return null;
                        }
                        break;
                    case 'e':
                        break;
                    default:
                        Console.WriteLine("Unexpected token");
                        return null;
                }
            }

            return resultExp;
        }

        public static bool IsNumber(string token)
        {
            var isNumber = Double.TryParse(token, out var number);

            return isNumber;
        }

        private static void ToSameFormat(string number1, string number2)
        {
            
        }
    }
}
