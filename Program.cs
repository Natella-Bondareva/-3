using System;

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
        static string ConvertToAdditionalCode(string str)
        {
            string newStr = "";
            int i;
            for (i = (str.Length - 1); i > 0; i--)
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
            i--;
            for (; i >= 0; i--)
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
                string additionalCode = ConvertToAdditionalCode(returnCode);
                directCode[1] = additionalCode;
                
            }
            else
            {
                directCode[0] = "0";
            }
        }
        static void Addition(ref string first, string second, int i) 
        {
            string result = "";
            for (; i > 0; i--)
            {
                if (first[i] == 0 && second[i] == 0)
                {
                    result = ('0' + result);
                }
                else if ((first[i] == 1 && second[i] == 0) || (first[i] == 0 && second[i] == 1)) 
                {
                    result = ('1' + result);
                }
                else if(first[i] == 1 && second[i] == 1) 
                {
                    result = ("0" + result);
                    i--;
                }

            }

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
                if (slide > 0)
                {
                    secondNum[2] = firstNum[2];
                    string scode = secondNum[1];
                    while (slide > 0)
                    {
                        scode = "0" + scode;
                        slide--;
                    }
                    secondNum[1] = scode;
                }
                else
                {
                    firstNum[2] = secondNum[2];
                    string fcode = firstNum[1];
                    while (slide > 0)
                    {
                        fcode = "0" + fcode;
                        slide--;
                    }
                    firstNum[1] = fcode;
                }
                SecondTask(ref firstNum);
                SecondTask(ref secondNum);
            }
        }
    }
}