using System;
using TextRPG_Team25.Core;

namespace TextRPG_Team25.Core
{
    public class Player
    {
        // =======================================
        // Player 클래스: 플레이어 캐릭터의 상태 정보를 관리
        // 7개 속성만 표시 (레벨/이름/직업/공격력/방어력/체력/Gold)
        // 변수 이름은 소문자 시작 규칙 준수
        // =======================================

        public string name { get; private set; }    // 캐릭터 이름
        public string job { get; private set; }     // 직업
        public int level { get; private set; }      // 레벨
        public int attack { get; private set; }     // 공격력
        public int defense { get; private set; }    // 방어력
        public int hp { get; set; }                 // 체력 (통합)
        public int gold { get; private set; }       // 소지 금화

        // 생성자: 초기 속성 설정
        public Player(string name, string job, int level,
                      int attack, int defense,
                      int hp, int gold)
        {
            this.name = name;    // 이름 설정
            this.job = job;     // 직업 설정
            this.level = level;   // 레벨 설정
            this.attack = attack;  // 공격력 설정
            this.defense = defense; // 방어력 설정
            this.hp = hp;      // 체력 설정
            this.gold = gold;    // 금화 설정
        }

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
                Console.WriteLine($"체력 : {hp}");
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
