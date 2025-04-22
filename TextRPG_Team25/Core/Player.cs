namespace TextRPG_Team25.Core
{
    public class Player
    {
        public string name = "";
        public string job = "전사";

        public int level = 1;
        public int attack = 10;
        public int defense = 5;
        public int maxHp = 100;
        public int hp = 100; // 현재 체력
        public int gold = 1500;


        // 상태 보기 메서드: 7개 속성만 출력
        public void ShowStatus()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("상태 보기\n");

                // 필수 속성 출력
                Console.WriteLine($"Lv.{level:D2}");
                Console.WriteLine($"{name} ( {job} )");
                Console.WriteLine($"공격력 : {attack}");
                Console.WriteLine($"방어력 : {defense}");
                Console.WriteLine($"체력 : {hp} / {maxHp}");
                Console.WriteLine($"Gold : {gold} G\n");

                Console.WriteLine("0. 나가기\n");
                Console.Write("  >> ");

                if (Console.ReadLine() == "0")
                    break;

                // 잘못된 입력 처리
                Console.WriteLine("잘못된 입력입니다");
                Console.WriteLine("계속하려면 아무 키나 누르세요...");
                Console.ReadKey();
            }
        }
    }
}
