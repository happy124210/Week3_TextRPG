using TextRPG_Team25.Core;

namespace TextRPG_Team25
{
    internal class Battle
    {
        public static void BattleResult()
        {
           if (isVictory)
           {
                Console.WriteLine("Victory!");
                Console.WriteLine($"던전에서 몬스터 {monsters.Count}마리를 잡았습니다.");
                return;
           }

            else
            {
                Console.WriteLine("You Lose...");
                Console.WriteLine($"Lv.{player.level} {player.name}");
                Console.WriteLine($"{previousHP} → 0");
                return;
            }
        }
    }
}
