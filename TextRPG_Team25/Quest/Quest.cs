using System;
using System.Numerics;
using System.Runtime.Serialization;
using TextRPG_Team25.Core;
using TextRPG_Team25.UI;

namespace TextRPG_Team25.Quest
{
    internal class Quest
    {
        public string title; // 퀘스트 제목
        public string description; // 퀘스트 설명
        public int goalCount; // 목표 마릿수
        public int currentCount; // 현재 마릿수
        public string rewardItem; // 퀘스트 보상 아이템
        public int rewardGold; // 퀘스트 보상 골드
        public bool isCompleted; // 퀘스트 완료 여부

        public static void ShowQuestList()
        {
            Console.Clear();
            Utils.ColoredText("Quest!!\n\n", ConsoleColor.DarkYellow);

            for (int i = 0; i < GameManager.Instance.questManager.questList.Count; i++)
            {
                Quest q = GameManager.Instance.questManager.questList[i];

                Utils.ColoredText($"{i + 1}", ConsoleColor.Yellow);
                Console.WriteLine($"{q.title}\n");
            }

            Console.WriteLine();

            int selection = 0;

            while (true)
            {
                Console.Write("원하시는 퀘스트 번호를 입력해주세요.\n>> ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out selection) &&
                    selection >= 1 && selection <= GameManager.Instance.questManager.questList.Count)
                {
                    break;
                }

                Utils.ColoredText("\n잘못된 입력입니다. 다시 입력해주세요.\n", ConsoleColor.Red);
            }
        }

        public static void ShowQuestDetail(int index)
        {
            Console.Clear();
            Quest q = GameManager.Instance.questManager.questList[index];

            Utils.ColoredText("Quest!\n\n", ConsoleColor.DarkYellow);
            Utils.ColoredText($"{q.title}\n\n", ConsoleColor.Yellow);
            Console.WriteLine(q.description);

            Console.WriteLine($"- 미니언 {q.goalCount}마리 처치 ({q.currentCount}/{q.goalCount}");
            Console.WriteLine("[ 보상 ]");
            Console.WriteLine($"{q.rewardItem} x 1");
            Console.WriteLine($"{q.rewardGold} G\n");

            if (q.isCompleted)
            {
                Console.WriteLine("1. 보상받기");
                Console.WriteLine("2. 돌아가기");
            }
            else
            {
                Console.WriteLine("1. 수락");
                Console.WriteLine("2. 거절");
            }

            Console.Write("\n원하시는 행동을 입력해주세요.\n>> ");
            string input = Console.ReadLine();

            if (input == "1")
            {
                if (q.isCompleted)
                {
                    Console.WriteLine($"\n{q.rewardItem}을(를) 받았습니다!");
                    Console.WriteLine($"{q.rewardGold} G를 획득했습니다.");
                    GameManager.Instance.player.Gold += q.rewardGold;
                }
                else
                {
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