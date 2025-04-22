using System;

namespace TextRPG_Team25.Core
{
    // Player 클래스: 플레이어 캐릭터의 상태 정보를 관리하는 클래스
    public class Player
    {
        // 캐릭터 이름
        public string name { get; private set; }

        // 직업
        public string job { get; private set; }

        // 레벨
        public int level { get; private set; }

        // 공격력
        public int attack { get; private set; }

        // 방어력
        public int defense { get; private set; }

        // 최대 체력
        public int maxhp { get; private set; }

        // 현재 체력
        public int hp { get; set; }

        // 소지 금화
        public int gold { get; private set; }

        // 생성자: 초기 속성 설정 (hp 값을 maxhp와 현재 hp로 모두 설정)
        public Player(string name, string job, int level, int attack, int defense, int hp, int gold)
        {
            this.name = name;
            this.job = job;
            this.level = level;
            this.attack = attack;
            this.defense = defense;
            this.maxhp = hp;   // 생성 시 전달된 hp 값을 최대 체력으로 설정
            this.hp = hp;      // 현재 체력도 동일하게 초기화
            this.gold = gold;
        }

        // 플레이어 상태 보기: hp를 우선 사용하여 7개 속성을 콘솔에 출력하고, '0' 입력 시 종료
        public void ShowStatus()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("상태 보기\n");
                Console.WriteLine($"Lv.{level:D2}");
                Console.WriteLine($"{name} ( {job} )");
                Console.WriteLine($"공격력 : {attack}");
                Console.WriteLine($"방어력 : {defense}");
                Console.WriteLine($"체력 : {hp}");
                Console.WriteLine($"Gold : {gold} G\n");
                Console.WriteLine("0. 나가기\n");
                Console.Write(">> ");

                var input = Console.ReadLine();
                if (input == "0")
                    break;

                Console.WriteLine("잘못된 입력입니다");
                Console.WriteLine("계속하려면 아무 키나 누르세요...");
                Console.ReadKey();
            }
        }
    }
}
