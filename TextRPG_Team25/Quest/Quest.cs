using TextRPG_Team25.Core;
using TextRPG_Team25.UI;

namespace TextRPG_Team25.Quest
{
    public class Quest
    {
        public string title;
        public string description;
        public int goalCount;
        public int currentCount;
        public bool isCompleted;
        public bool isRewarded;
        public bool isAccepted;

        public string rewardItemId;
        public int rewardGold;

        public void ShowQuestList()
        {
            Console.Clear();
            Utils.ColoredText("Quest!!\n\n", ConsoleColor.DarkYellow);

            for (int i = 0; i < GameManager.Instance.questManager.questList.Count; i++)
            {
                Quest q = GameManager.Instance.questManager.questList[i];

                if (q.isCompleted && q.isRewarded)
                {
                    Utils.ColoredText("[완료] ", ConsoleColor.DarkGray);
                    Console.WriteLine($"{q.title}\n");
                }
                else
                {
                    Utils.ColoredText($"[{i + 1}] ", ConsoleColor.Yellow);
                    Console.WriteLine($"{q.title}\n");
                }
            }

            Utils.MenuOption("0", "나가기");
            Console.WriteLine();

            int selection = 0;

            while (true)
            {
                Console.Write("원하시는 퀘스트 번호를 입력해주세요.\n>> ");
                string input = Console.ReadLine();

                if (input == "0")
                { 
                    break;                    
                }

                if (int.TryParse(input, out selection) &&
                    selection >= 1 && selection <= GameManager.Instance.questManager.questList.Count)
                {
                    Quest selected = GameManager.Instance.questManager.questList[selection - 1];

                    if (selected.isCompleted && selected.isRewarded)
                    {
                        Utils.ColoredText("\n이미 완료된 퀘스트입니다.\n", ConsoleColor.DarkGray);
                        Console.ReadKey();
                        continue;
                    }

                    ShowQuestDetail(selection - 1);
                    break;
                }

                Utils.ColoredText("\n잘못된 입력입니다. 다시 입력해주세요.\n", ConsoleColor.Red);
            }
        }

        public void ShowQuestDetail(int index)
        {
            Console.Clear();
            Quest q = GameManager.Instance.questManager.questList[index];

            Utils.ColoredText("Quest!!\n\n", ConsoleColor.DarkYellow);
            Utils.ColoredText($"{q.title}\n\n", ConsoleColor.Yellow);
            Console.WriteLine($"{q.description}\n");

            if (q.title == "마을을 위협하는 미니언 처치")
            {
                Console.WriteLine($"- 미니언 {q.goalCount}마리 처치 ({q.currentCount}/{q.goalCount})\n");
                Utils.ColoredText("[ 보상 ]\n", ConsoleColor.Green);
                Console.WriteLine($"{q.rewardItemId} x 1");
                Console.WriteLine($"{q.rewardGold} G\n");
            }
            else if (q.title == "장비를 장착해보자")
            {
                Console.WriteLine($"- 장비 {q.goalCount}개 장착 ({q.currentCount}/{q.goalCount})\n");
                Utils.ColoredText("[ 보상 ]\n", ConsoleColor.Green);
                Console.WriteLine($"{q.rewardItemId} x 1");
                Console.WriteLine($"{q.rewardGold} G\n");
            }
            else if (q.title == "더욱 더 강해지기")
            {
                Console.WriteLine($"- 레벨 {q.goalCount} 도달 (현재 레벨: {GameManager.Instance.player.level})\n");
                Utils.ColoredText("[ 보상 ]\n", ConsoleColor.Green);
                Console.WriteLine($"{q.rewardItemId} x 1");
                Console.WriteLine($"{q.rewardGold} G\n");
            }

            if (q.isCompleted)
            {
                Utils.MenuOption("1", "보상받기");
                Utils.MenuOption("2", "돌아가기");
            }
            else
            {
                Utils.MenuOption("1", "수락");
                Utils.MenuOption("2", "거절");
                Utils.MenuOption("0", "나가기");
            }

            Console.Write("\n원하시는 행동을 입력해주세요.\n>> ");
            string input = Console.ReadLine();

            if (input == "1")
            {
                if (q.isCompleted && !q.isRewarded)
                {
                    Console.WriteLine($"\n{q.rewardItemId}을(를) 받았습니다!");
                    Console.WriteLine($"{q.rewardGold} G를 획득했습니다.");
                    GameManager.Instance.player.gold += q.rewardGold;
                    GameManager.Instance.player.AddInventory(q.rewardItemId);
                    q.isRewarded = true;
                }
                else
                {
                    if (!q.isAccepted)
                    {
                        q.isAccepted = true;
                    }
                    Console.WriteLine("\n퀘스트를 수락했습니다!");
                }

                Console.WriteLine("\n계속하려면 아무 키나 누르세요...");
                Console.ReadKey();
                ShowQuestList();
            }
            else
            {
                ShowQuestList();
            }
        }

        
    }


}