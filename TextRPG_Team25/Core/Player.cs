namespace TextRPG_Team25.Core
{
    public class Player
    {
        public string name = "";
        public string job = "검투사";

        public int level = 1;
        public int attack;
        public int defense;
        public int maxHp;
        public int hp; // 현재 체력
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

                Console.WriteLine("1. 직업 선택\n");
                Console.WriteLine("0. 나가기");

                Console.Write(">> ");

                string input = Console.ReadLine();
                if (input == "0")
                    break;
                else if (input == "1")
                {
                    // 직업 선택 메뉴 호출
                    JobMenu jobMenu = new JobMenu(this);  // Player 객체를 전달
                    jobMenu.ShowJobSelectionMenu();
                    UpdateStatsBasedOnJob();  // 직업 선택 후 능력치 업데이트
                }
            }
        }

        // 직업을 변경할 때마다 능력치를 업데이트하는 메서드
        public void UpdateStatsBasedOnJob()
        {
            switch (job)
            {
                case "검투사":
                    attack = 11;
                    defense = 5;
                    maxHp = 100;
                    break;
                case "화염술사":
                    attack = 13;
                    defense = 5;
                    maxHp = 60;
                    break;
                case "얼음술사":
                    attack = 7;
                    defense = 10;
                    maxHp = 150;
                    break;
            }

            // 변경된 능력치에 맞게 현재 체력을 갱신
            hp = maxHp;
        }
    }
}


