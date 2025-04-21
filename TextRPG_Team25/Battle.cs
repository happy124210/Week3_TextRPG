using TextRPG_Team25.Core;

namespace TextRPG_Team25
{
    internal class Battle
    {
        public static void BattleResult(List<Monster> monsters, Player player, int previousHP)
        {
            bool allDead = true;

            foreach (Monster m in monsters)
            {
                if (m.hp > 0)
                {
                    allDead = false;
                    break;
                }
            }

            if (allDead)
            {
                Console.WriteLine("Victory!");
                Console.WriteLine($"던전에서 몬스터 {monsters.Count}마리를 잡았습니다.");
                return;
            }

            if (player.hp <= 0)
            {
                Console.WriteLine("You Lose...");
                Console.WriteLine($"Lv.{player.level} {player.name}");
                Console.WriteLine($"{previousHP} → 0");
                return;
            }
        }
    }
}
