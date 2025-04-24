namespace TextRPG_Team25.Core
{
    using TextRPG_Team25.Quest;

    internal class GameManager
    {
        // 싱글톤
        private static GameManager instance;
        public static GameManager Instance => instance ??= new GameManager();
        public Player player;
        public Battle battle;
        public Quest quest;

        // 초기화
        public void Initialize()
        {
            Console.Write("플레이어의 이름을 입력하세요.\n>> ");
            string name = Console.ReadLine();

            player = new Player();
            player.name = name;
            battle = new Battle(player);
            quest = new Quest();
        }

        // 게임 실행
        public void Run()
        {
            ShowMainMenu();
        }

        // 메인 메뉴
        public void ShowMainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
                Console.WriteLine("이제 전투를 시작할 수 있습니다.\n");
                Console.WriteLine("1. 상태 보기");
                Console.WriteLine("2. 인벤토리 보기");
                Console.WriteLine("3. 전투 시작");
                Console.WriteLine("4. 퀘스트 선택");
                Console.WriteLine("0. 게임 종료\n");
                Console.Write("원하시는 행동을 입력해주세요.\n>> ");

                string input = Console.ReadLine();


                switch (input)
                {
                    case "1":
                        player.ShowStatus();
                        break;
                    case "2":
                        player.ShowInventory();
                        break;
                    case "3":
                        battle.StartBattle();
                        break;
                    case "4":
                        quest.ShowQuestList();
                        break;
                    case "0":
                        Console.WriteLine("\n게임을 종료합니다.");
                        Console.ReadKey();
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