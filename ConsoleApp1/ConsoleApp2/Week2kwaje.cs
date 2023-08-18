//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Week2
//{
//    internal class Week2kwaje
//    {
//        static void Gugudan()
//        {
//            for (int i = 1; i < 10; i++)
//            {
//                for (int j = 1; j < 10; j++)
//                {
//                    Console.WriteLine($"{i} × {j} = {i * j}");
//                }
//            }
//        }

//        static void Star()
//        {
//            for (int i = 1; i <= 5; i++) // 오른쪽으로 기울어진 직각삼각형
//            {
//                for (int j = 1; j <= i; j++)
//                    Console.Write("*");
//                Console.WriteLine();
//            }

//            for (int i = 5; i >= 0; i--)
//            {
//                for (int j = 1; j <= i; j++)
//                    Console.Write("*");
//                Console.WriteLine();
//            }

//            for (int i = 1; i <= 5; i++)
//            {
//                for (int j = 1; j <= 5 - i; j++)
//                    Console.Write(" ");

//                for (int j = 1; j <= 2 * i - 1; j++)
//                {
//                    Console.Write("*");
//                }
//                Console.WriteLine();
//            }
//        }

//        static void MaxAndMin()
//        {
//            int max = 0;
//            int min = 0;
//            int inputNum = 0;
//            int inputCount = 5;

//            for (int i = 1; i <= inputCount; i++)
//            {
//                Console.Write($"숫자를 입력하세요: ");
//                inputNum = int.Parse(Console.ReadLine());

//                if (max == 0 && min == 0) max = min = inputNum;
//                if (inputNum > max)
//                {
//                    int temp = max; // temp 변수를 사용하여 값 교환
//                    max = inputNum;
//                    inputNum = temp;
//                }

//                if (inputNum < min)
//                {
//                    min = inputNum;
//                }
//            }

//            Console.Write($"최대값: {max}\n최소값: {min}");
//        }

//        static bool IsPrime(int num)
//        {
//            if (num < 1) return false;
//            else if (1 <= num && num <= 3) return true;
//            else if (num % 2 == 0 || num % 3 == 0) return false;

//            for (int i = 1; i <= num; i++)
//            {
//                if (num % i == 0) return false;
//            }
//            return true;
//        }

//        static void Main()
//        {
//            Console.Write("숫자를 입력하세요: "); // 사용자에게 숫자 입력 요청
//            int num = int.Parse(Console.ReadLine()); // 사용자가 입력한 값을 정수로 변환하여 저장

//            if (IsPrime(num)) // 입력 받은 숫자가 소수라면
//            {
//                Console.WriteLine(num + "은 소수입니다."); // 소수임을 출력
//            }
//            else // 소수가 아니라면
//            {
//                Console.WriteLine(num + "은 소수가 아닙니다."); // 소수가 아님을 출력
//            }
//        }
//    }
//}
