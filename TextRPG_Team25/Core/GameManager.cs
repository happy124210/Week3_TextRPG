namespace TextRPG_Team25.Core
{
    using TextRPG_Team25.Quest;
    using TextRPG_Team25.UI;
    using TextRPG_Team25.BattleSystem;

    internal class GameManager
    {
        // 싱글톤
        private static GameManager instance;
        public static GameManager Instance => instance ??= new GameManager();

        public Player player;
        public Shop shop;
        public Battle battle;
        public Quest quest;
        public QuestManager questManager;

        // 초기화
        public void Initialize()
        {
            Console.Clear();

            // 이름 받기
            Console.Write("이름을 입력하세요.\n>> ");
            string name = Console.ReadLine();

            player = new Player();
            player.name = name;

            Console.Clear();

            // 직업 선택
            JobMenu jobMenu = new JobMenu(player);
            jobMenu.ShowJobSelectionMenu();

            player.UpdateStatsBasedOnJob();
            player.SetSkillsByJob();

            // 생성
            shop = new Shop();
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
                // 메뉴 출력부
                Console.Clear();
                Utils.ColoredText("╔════════════════════════════╗\n", ConsoleColor.DarkCyan);
                Utils.ColoredText("║   TEXT RPG GAME by Team25  ║\n", ConsoleColor.DarkCyan);
                Utils.ColoredText("╚════════════════════════════╝\n", ConsoleColor.DarkCyan);
                Console.WriteLine();
                Utils.MenuOption("1", "내 정보");
                Utils.MenuOption("2", "인벤토리");
                Utils.MenuOption("3", "상점");
                Utils.MenuOption("4", "전투");
                Utils.MenuOption("5", "여관");
                Utils.MenuOption("6", "퀘스트");
                Console.WriteLine();
                Utils.MenuOption("0", "게임 종료\n");
                Console.Write("원하시는 행동을 입력해주세요.\n>> ");

                // 입력 처리
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        player.StatusMenu();
                        break;
                    case "2":
                        player.ShowInventory();
                        break;
                    case "3":
                        shop.ShowShop(player);
                        break;
                    case "4":
                        battle.StartBattle();
                        break;
                    case "5":
                        player.hp = 100;
                        Console.WriteLine("체력이 회복되었습니다!");
                        Console.ReadKey();
                        break;
                    case "6":
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