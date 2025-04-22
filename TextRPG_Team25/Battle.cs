using System.Numerics;
using System.Threading;
using TextRPG_Team25.Core;
using TextRPG_Team25.UI;

namespace TextRPG_Team25
{
    internal class Battle
    {
        public static void BattleResult()
        {
           if (isVictory)
           {
                Utils.ColoredText("Battle! -Result-", ConsoleColor.DarkYellow);
                Utils.ColoredText("Victory!", ConsoleColor.Green);
                Console.WriteLine("던전에서 몬스터 ");
                Utils.ColoredText($"{monster.Count}", ConsoleColor.Red);
                Console.WriteLine("마리를 잡았습니다.");
                return;
           }

            else
            {
                Utils.ColoredText("Battle! -Result-", ConsoleColor.DarkYellow);
                Utils.ColoredText("You Lose . . .", ConsoleColor.Magenta);
                Console.WriteLine("Lv. ");
                Utils.ColoredText($"{player.level} ", ConsoleColor.Red);
                Utils.ColoredText($"{player.name}", ConsoleColor.Yellow);
                Console.WriteLine($"HP {previousHP} → ");
                Utils.ColoredText("0", ConsoleColor.Red);
            }
        }
    }
}
