using System;
using TextRPG_Team25.Quest;

namespace TextRPG_Team25.Core
{
    internal class GameManager
    {
        // 싱글톤
        private static GameManager instance;
        public static GameManager Instance => instance ??= new GameManager();
        public Player player;
        public Battle battle;
        public QuestManager questManager;

        // 초기화
        public void Initialize() 
        {
            Console.WriteLine("플레이어의 이름을 입력하시오.\n>> ");
    
            string name = Console.ReadLine();
            
            
            player = new Player();
            player.Name = name;
            battle = new Battle(player);
            questManager = new QuestManager(player);
        }

        // 게임 실행
        public void Run() 
        {
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
            Console.WriteLine("3. 퀘스트\n");
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
                case "3":
                    ShowQuestList();
                default:
                    Console.WriteLine("\n잘못된 입력입니다.");
                    Console.ReadLine();
                    ShowMainMenu();
                    break;
            }
        }
       

    }
}
