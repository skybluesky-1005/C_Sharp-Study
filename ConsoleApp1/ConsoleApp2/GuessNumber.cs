//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ConsoleApp1
//{
//    internal class GuessNumber
//    {

//        static void Main()
//        {
//            Random random = new Random();
//            int numberToGuess = random.Next(1, 101);
//            int tryCount = 0;
//            bool isCorrect = false;

//            Console.WriteLine("숫자 맞추기 게임을 시작합니다. 1에서 100까지의 숫자 중 하나를 맞춰보세요.");
//            do
//            {
//                Console.Write("숫자를 입력하세요: ");
//                int num = int.Parse(Console.ReadLine());
//                tryCount++;

//                if (numberToGuess > num) Console.WriteLine("너무 작습니다!");
//                else if (numberToGuess < num) Console.WriteLine("너무 큽니다!");
//                else
//                {
//                    Console.WriteLine("축하합니다! " + tryCount + "번 만에 숫자를 맞추셨습니다.");
//                    isCorrect = true;
//                }
//            }
//            while (!isCorrect);
//        }
//    }
//}
