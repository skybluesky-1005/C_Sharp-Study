namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("이름과 나이를 입력하세요");
            Console.Write("이름 : ");
            string name = Console.ReadLine();
            Console.Write("나이 : ");
            string age = Console.ReadLine();
            Console.WriteLine("이름 : " + name + "\n나이 : " + age);

            int num1, num2;

            Console.Write("두 수를 입력하세요\n첫번째 수 : ");
            num1 = int.Parse(Console.ReadLine());
            Console.Write("두번째 수 : ");
            num2 = int.Parse(Console.ReadLine());

            Console.WriteLine(num1 + num2);
            Console.WriteLine(num1 - num2);
            Console.WriteLine(num1 * num2);
            Console.WriteLine(num1 / num2);

            int c, f;
            Console.Write("섭씨 온도를 입력하세요\n℃ : ");
            c = int.Parse(Console.ReadLine());
            f = c * 9 / 5 + 32; //섭씨온도 화씨 변환 공식
            Console.WriteLine("변환 결과 : " + f);

            float heigh, weight, bmi;
            Console.Write("키와 몸무게를 입력하세요\n키 : ");
            heigh = float.Parse(Console.ReadLine());
            Console.Write("몸무게 : ");
            weight = float.Parse(Console.ReadLine());
            bmi = weight / (heigh * heigh / 10000);
            Console.WriteLine(bmi);
            if (bmi < 18.5f) Console.WriteLine("저체중입니다.");
            else if (bmi < 23) Console.WriteLine("정상입니다.");
            else if (bmi < 25) Console.WriteLine("과체중입니다");
            else Console.WriteLine("비만입니다.");
        }
    }
}