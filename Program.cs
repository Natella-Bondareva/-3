using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace лаб_3
{
    internal class Program
    {
        static string ConvertToReturnCode(string str)
        {
            string newStr = "";
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == '1')
                {
                    newStr += '0';
                }
                else
                {
                    newStr += '1';
                }
            }
            return newStr;
        }
        static string ConvertToAdditionalCode(string str, int i)
        {
            string newStr = "";
            for (int j = str.Length -1 ; j>i; j--)
            {
                newStr = (str[j] + newStr);
            }
            for (; i >= 0; i--)
            {
                if (str[i] == '1')
                {
                    newStr = ('0'+newStr);
                }
                else if (str[i] == '0')
                {
                    newStr = ('1' + newStr);
                    break;
                }
            }         
            for (i--; i >= 0; i--)
            {
                newStr = (str[i]+ newStr);
            }
            return newStr;
        }
        static void SecondTask(ref string[] directCode)
        {
            while (directCode[1].Length < 15)
            {
                directCode[1] = ( directCode[1]+ "0");
            }             
            string sing = directCode[0];
            if (sing[0] == '-')
            {
                directCode[0] = "1";
                string returnCode = ConvertToReturnCode(directCode[1]);
                string additionalCode = ConvertToAdditionalCode(returnCode, returnCode.Length-1);
                directCode[1] = additionalCode;              
            }
            else
            {
                directCode[0] = "0";
            }
        }
        static string Addition(ref string first, string second, int i) 
        {
            string result = "";
            for (; i >= 0; i--)
            {
                if (first[i] == '0' && second[i] == '0')
                {
                    result = ('0' + result);
                }
                else if ((first[i] == '1' && second[i] == '0') | (first[i] == '0' && second[i] == '1')) 
                {
                    result = ('1' + result);
                }
                else if(first[i] == '1' && second[i] == '1') 
                {
                    result = ('0' + result);
                    i--;
                    first = ConvertToAdditionalCode(first, i);
                    i++;
                }
            }
            return result;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Введіть мантису першого числа(не більше 16 символів після коми) \nта порядок числа через вертикальну риску(|): ");
            string[] firstNum = Console.ReadLine().Trim().Split('.', ',','|');
            int firstPower = Convert.ToInt32(firstNum[2], 2);
            Console.WriteLine("Введіть мантису другого числа (не більше 16 символів після коми) \nта порядок числа через вертикальну риску(|): ");
            string[] secondNum = Console.ReadLine().Trim().Split('.', ',', '|');
            int secondPower = Convert.ToInt32(secondNum[2], 2);
            if (firstNum[1].Length > 16 || secondNum[1].Length > 16)
            {
                Console.WriteLine("Не првильний ввід чисел");
            }
            else
            {
                int slide = firstPower - secondPower;
                string power;
                if (slide > 0)
                {
                    power = firstNum[2];
                    Slide(ref secondNum[1], slide);
                }
                else
                {
                    power = secondNum[2];
                    Slide(ref firstNum[1], slide);
                }
                SecondTask(ref firstNum);
                SecondTask(ref secondNum);
                string sum = Addition(ref firstNum[1], secondNum[1], 14);
                string sign = "";
                FindSingn(ref sign, ref sum, ref power, firstNum, secondNum);
                ConvertToDirect(ref sum);
                Normalize(ref sum, ref power);

                Console.WriteLine($"Результат: {sign}0,{sum}|{power}");
            }
        }
        static void FindSingn(ref string sign, ref string sum, ref string power, string[] firstNum, string[] secondNum) 
        {
            if (firstNum[0] == "0" && secondNum[0] == "0")
            {
                sign = "";
            }
            else if ((firstNum[0] == "0" && secondNum[0] == "1") | (firstNum[0] == "1" && secondNum[0] == "0"))
            {
                sign = "-";
            }
            else if (firstNum[0] == "1" && secondNum[0] == "1")
            {
                sign = "-";
                sum = "0" + sum;
                power = Convert.ToString((Convert.ToInt32(power, 2) + 1), 2);
            }
        }
        static void Normalize (ref string res, ref string power)
        {
            int i;
            for ( i= 0; i < res.Length; i++)
            {
                if (res[i] == '1')
                {
                    break;
                }
            }
            string copy = res.Substring(i);
            while (i > 0)
            {
                power = Convert.ToString((Convert.ToInt32(power, 2) - 1), 2);
                copy += '0';
                i--;
            }
            res = copy;
        }
        static void ConvertToDirect(ref string result)
        {
            int i;
            string newRes = "";
            for (i = result.Length-1; i >=0 ; i--)
            {
                if (result[i]=='1')
                {
                    newRes = '0' + newRes;
                    i--;
                    break;
                }
                else
                {
                    newRes = '1' + newRes;
                }
            }
            for (; i >= 0; i--)
            {
                 newRes = result[i] + newRes;
            }
            result = newRes;
            result = ConvertToReturnCode(result);
        }
        static void Slide(ref string num, int slide)
        {
            while (slide > 0)
            {
                num = "0" + num;
                slide--;
            }
        }
    }
}