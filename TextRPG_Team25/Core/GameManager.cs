using System;

namespace TextRPG_Team25.Core
{
    internal class GameManager
    {
        // 싱글톤
        private static GameManager instance;
        public static GameManager Instance => instance ??= new GameManager();

        // 초기화
        public void Initialize() 
        {  
            
        }

        // 게임 실행
        public void Run() 
        {
            Initialize();
            ShowMainMenu();
        }

        // 메인 메뉴
        public void ShowMainMenu() 
        {
            Console.Clear();
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
            Console.WriteLine("이제 전투를 시작할 수 있습니다.\n");
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 전투 시작\n");
            Console.Write("원하시는 행동을 입력해주세요.\n>> ");

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    ShowStatus();
                    break;
                case "2":
                    StartBattle();
                    break;
                default:
                    Console.WriteLine("\n잘못된 입력입니다.");
                    Console.ReadLine();
                    ShowMainMenu();
                    break;
            }
        }
        
        public void ShowStatus()
        {
            Console.Clear();
            Console.ReadLine();
            ShowMainMenu();
        }

        public void StartBattle()
        {
            Console.Clear();
            Console.ReadLine();
            ShowMainMenu();
        }

    }
}
