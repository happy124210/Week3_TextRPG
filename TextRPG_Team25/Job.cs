using System;

namespace TextRPG_Team25.Core
{
    internal class JobMenu
    {
        private Player player;

        public JobMenu(Player player)
        {
            this.player = player;
        }

        public void ShowJobSelectionMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("직업을 선택하세요:\n");
                Console.WriteLine("1. 전사 - 공격력 상, 방어력 중, 체력 중");
                Console.WriteLine("2. 도적 - 공격력 최상, 방어력 중, 체력 하");
                Console.WriteLine("3. 기갑병 - 공격력 하, 방어력 상, 체력 상");
                Console.WriteLine("0. 뒤로가기\n");
                Console.Write(">> ");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        player.job = "전사";
                        Console.WriteLine("\n전사를 선택하였습니다!");
                        Console.ReadKey();
                        return;
                    case "2":
                        player.job = "도적";
                        Console.WriteLine("\n도적을 선택하였습니다!");
                        Console.ReadKey();
                        return;
                    case "3":
                        player.job = "기갑병";
                        Console.WriteLine("\n기갑병을 선택하였습니다!");
                        Console.ReadKey();
                        return;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("\n잘못된 입력입니다.");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}