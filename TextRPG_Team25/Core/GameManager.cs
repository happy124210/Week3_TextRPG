namespace TextRPG_Team25.Core
{
    using TextRPG_Team25.Quest;
    using TextRPG_Team25.UI;

    internal class GameManager
    {
        // 싱글톤
        private static GameManager instance;
        public static GameManager Instance => instance ??= new GameManager();
        public Player player;
        public Battle battle;
        public Quest quest;
        public QuestManager questManager;

        // 초기화
        public void Initialize()
        {
            Console.Write("이름을 입력하세요.\n>> ");
            string name = Console.ReadLine();

            player = new Player();
            player.name = name;
            battle = new Battle(player);
            quest = new Quest();
            questManager = new QuestManager();

            questManager.InitQuests();
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
                Console.WriteLine("25조 Text RPG");
                Console.WriteLine("");
                Utils.MenuOption("1", "내 정보");
                Utils.MenuOption("2", "인벤토리");
                Utils.MenuOption("3", "전투");
                Utils.MenuOption("4", "여관");
                Utils.MenuOption("5", "퀘스트");
                Utils.MenuOption("0", "게임 종료\n");
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
                        player.hp = 100;
                        break;
                    case "5":
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