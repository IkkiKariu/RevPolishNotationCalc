using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNCalculator
{
    class BigInteger
    {
        public List<int> digits;
        public bool isNegative;

        public BigInteger()
        {
            digits = new List<int>();
        }

        public BigInteger(string number)
        {
            digits = new List<int>();
            for (int i = number.Length - 1; i >= 0; i--)
            {
                if (number[i] == '-')
                {
                    isNegative = true;
                    continue;
                }

                digits.Add(int.Parse(number[i].ToString()));
            }
        }

        public bool IsMoreThan(BigInteger num)
        {
            if (this.digits.Count == 0 && num.digits.Count != 0)
                return false;

            if (this.digits.Count != 0 && num.digits.Count == 0)
                return true;

            if (this.digits.Count > num.digits.Count)
            {
                return true;
            }
            else if (this.digits.Count < num.digits.Count)
            {
                return false;
            }

            for (var i = this.digits.Count - 1; i >= 0; i--)
            {
                if (this.digits[i] > num.digits[i])
                {
                    return true;
                }
                else if (this.digits[i] < num.digits[i])
                {
                    return false;
                }
                else
                {
                    continue;
                }
            }

            return false;
        }

        public static bool operator ==(BigInteger a, BigInteger b)
        {
            if (a.digits.Count != b.digits.Count)
                return false;

            for (int i = a.digits.Count - 1; i >= 0; i--)
            {
                if (a.digits[i] != b.digits[i])
                    return false;
            }

            return true;
        }

        public static bool operator !=(BigInteger a, BigInteger b)
        {
            if (a.digits.Count != b.digits.Count)
                return true;

            for (int i = a.digits.Count - 1; i >= 0; i--)
            {
                if (a.digits[i] != b.digits[i])
                    return true;
            }

            return false;
        }
        public static BigInteger operator +(BigInteger num1, BigInteger num2)
        {
            BigInteger result = new BigInteger();
            int carryOut = 0;
            int maxLength = Math.Max(num1.digits.Count, num2.digits.Count);


            for (int i = 0; i < maxLength; i++)
            {
                int sum = carryOut;
                if (i < num1.digits.Count)
                {
                    sum += num1.digits[i];
                }
                if (i < num2.digits.Count)
                {
                    sum += num2.digits[i];
                }

                result.digits.Add(sum % 10);
                carryOut = sum / 10;
            }

            if (carryOut > 0)
                result.digits.Add(carryOut);

            return result;
        }

        public static BigInteger operator -(BigInteger num1, BigInteger num2)
        {
            BigInteger result = new BigInteger();
            int borrow = 0;
            int maxLength = Math.Max(num1.digits.Count, num2.digits.Count);

            if (num1.IsMoreThan(num2) || num1 == num2)
            {
                for (int i = 0; i < maxLength; i++)
                {
                    int diff = borrow;

                    if (i < num1.digits.Count)
                    {
                        diff += num1.digits[i];
                    }
                    if (i < num2.digits.Count)
                    {
                        diff -= num2.digits[i];
                    }

                    if (diff < 0)
                    {
                        borrow = -1;
                        diff += 10;
                    }
                    else
                    {
                        borrow = 0;
                    }

                    result.digits.Add(diff);
                }

                while (result.digits.Count > 1 && result.digits[result.digits.Count - 1] == 0)
                {
                    result.digits.RemoveAt(result.digits.Count - 1);
                }

                return result;
            }
            else
            {
                for (int i = 0; i < maxLength; i++)
                {
                    int diff = borrow;

                    if (i < num2.digits.Count)
                    {
                        diff += num2.digits[i];
                    }
                    if (i < num1.digits.Count)
                    {
                        diff -= num1.digits[i];
                    }

                    if (diff < 0)
                    {
                        borrow = -1;
                        diff += 10;
                    }
                    else
                    {
                        borrow = 0;
                    }

                    result.digits.Add(diff);
                }

                while (result.digits.Count > 1 && result.digits[result.digits.Count - 1] == 0)
                {
                    result.digits.RemoveAt(result.digits.Count - 1);
                }

                result.isNegative = true;

                result.digits.Reverse();
                return result;
            }
        }

        public static BigInteger operator *(BigInteger num1, BigInteger num2)
        {
            if ((num1.digits.Count == 1 && num1.digits[0] == 0) || (num2.digits.Count == 1 && num2.digits[0] == 0))
                return new BigInteger("0");

            BigInteger result = new BigInteger();

            for (int i = 0; i < num1.digits.Count; i++)
            {
                int carryOut = 0;
                BigInteger tmpResult = new BigInteger();

                for (int j = 0; j < num2.digits.Count; j++)
                {
                    int product = num1.digits[i] * num2.digits[j] + carryOut;
                    tmpResult.digits.Add(product % 10);
                    carryOut = product / 10;
                }

                if (carryOut > 0)
                {
                    tmpResult.digits.Add(carryOut);
                }

                for (int k = 0; k < i; k++)
                {
                    tmpResult.digits.Insert(0, 0);
                }

                result += tmpResult;
            }

            return result;
        }

        public static BigInteger operator /(BigInteger num1, BigInteger num2)
        {
            BigInteger quotient = new BigInteger();
            int tmpQuotient = 0;
            BigInteger remainder = new BigInteger();

            BigInteger divisible = new BigInteger();
            for (int i = 0; i < num1.digits.Count; i++)
            {
                divisible.digits.Add(num1.digits[i]);
            }

            BigInteger divisor = new BigInteger();
            for (int i = 0; i < num2.digits.Count; i++)
            {
                divisor.digits.Add(num2.digits[i]);
            }

            if (divisor.IsMoreThan(divisible))
                return new BigInteger("0");

            if (divisor == divisible)
                return new BigInteger("1");

            for (int i = 0; i < divisible.digits.Count; i++)
            {
                if (divisor.IsMoreThan(remainder))
                {
                    remainder.digits.Insert(0, divisible.digits[divisible.digits.Count - 1 - i]);

                    if (quotient.digits.Count > 0 && divisor.IsMoreThan(remainder))
                    {
                        quotient.digits.Add(0);
                    }
                }

                if (remainder.digits.Count == 1 && remainder.digits[0] == 0)
                {
                    quotient.digits.Add(0);
                    remainder.digits.Clear();
                    continue;
                }

                if (divisor.IsMoreThan(remainder))
                {
                    continue;
                }

                while (remainder.IsMoreThan(divisor) || remainder == divisor)
                {
                    remainder = remainder - divisor;
                    tmpQuotient++;
                }
                quotient.digits.Add(tmpQuotient);
                tmpQuotient = 0;

                if (remainder.digits[remainder.digits.Count - 1] == 0)
                    remainder.digits.Clear();
            }

            if (quotient.digits[0] == 0)
                quotient.digits.RemoveAt(0);

            quotient.digits.Reverse();
            return quotient;
        }

        public static BigInteger operator %(BigInteger num1, BigInteger num2)
        {
            BigInteger quotient = new BigInteger();
            int tmpQuotient = 0;
            BigInteger remainder = new BigInteger();

            BigInteger divisible = new BigInteger();
            for (int i = 0; i < num1.digits.Count; i++)
            {
                divisible.digits.Add(num1.digits[i]);
            }

            BigInteger divisor = new BigInteger();
            for (int i = 0; i < num2.digits.Count; i++)
            {
                divisor.digits.Add(num2.digits[i]);
            }

            if (divisor.IsMoreThan(divisible))
                return divisible;

            if (divisor == divisible)
                return new BigInteger("0");

            for (int i = 0; i < divisible.digits.Count; i++)
            {
                if (divisor.IsMoreThan(remainder))
                {
                    remainder.digits.Insert(0, divisible.digits[divisible.digits.Count - 1 - i]);

                    if (remainder.digits.Count == divisor.digits.Count && divisor.IsMoreThan(remainder))
                    {
                        quotient.digits.Add(0);
                    }
                }

                if (remainder.digits.Count == 1 && remainder.digits[0] == 0)
                {
                    quotient.digits.Add(0);
                    remainder.digits.Clear();
                    continue;
                }

                if (divisor.IsMoreThan(remainder))
                {
                    continue;
                }

                while (remainder.IsMoreThan(divisor) || remainder == divisor)
                {
                    remainder = remainder - divisor;
                    tmpQuotient++;
                }
                quotient.digits.Add(tmpQuotient);
                tmpQuotient = 0;

                if (remainder.digits[remainder.digits.Count - 1] == 0)
                    remainder.digits.Clear();
            }
            if (quotient.digits[0] == 0)
                quotient.digits.RemoveAt(0);
            return remainder;
        }
    }
}
