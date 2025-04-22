using TextRPG_Team25.UI;
using TextRPG_Team25.Core;

namespace TextRPG_Team25
{
    internal class Battle(Player player)
    {
        private int _previousHP = player.hp;
        private bool _isBattle = true;
        private bool _isVictory = true;
        private List<Monster> fieldMonsters = new List<Monster>();

        public void StartBattle()
        {
            // 입력 분기 처리

            while (_isBattle)
            {
                int input = int.Parse(Console.ReadLine());
                PlayerPhase(fieldMonsters[input]);

                MonsterPhase();
            }

            BattleResult();
        }

        private void PlayerPhase(Monster selected)
        {
            int baseAttack = player.attack;
            int currentMonsterHp = selected.hp;
            int offset = (int)Math.Ceiling(baseAttack * 0.1f);
            int damage = new Random().Next(baseAttack - offset, baseAttack + offset + 1);

            selected.hp -= damage;

            // 메세지 출력
            Console.WriteLine($"{player.name}의 공격!");
            Console.WriteLine($"{selected.name}을 맞췄습니다. [데미지 : {damage}]");
            Console.Write($" Lv.");
            Utils.ColoredText($"{selected.level} ", ConsoleColor.Magenta);
            Console.WriteLine($"{selected.name}");

            if (selected.hp <= 0) // 몬스터 죽었을 때
            {
                selected.hp = 0;
                selected.isLive = false;

                // 메세지 출력     
                Console.WriteLine($"HP "); 
                Utils.ColoredText($"{currentMonsterHp} ", ConsoleColor.Green);
                Console.WriteLine($"-> Dead");

                // 해당 텍스트 회색으로 처리 코드 추가
            }

            else
            {
                Console.WriteLine($"HP {currentMonsterHp}-> HP {selected.hp}");
            }
        }

        public void MonsterPhase()
        {
            Random rand = new Random();

            foreach (Monster monster in fieldMonsters)
            {
                if (!monster.isLive) continue;

                // 기본 공격력 ±10% 랜덤 오차 계산
                int offset = (int)Math.Ceiling(monster.attack * 0.1f);
                int damage = rand.Next(monster.attack - offset, monster.attack + offset + 1);

                // 로그 출력
                Console.WriteLine($"\nLv.{monster.level} {monster.name} 의 공격!");
                Console.WriteLine($"Lv.{player.level} {player.name}");
                Console.WriteLine($"HP {player.hp} → {Math.Max(player.hp - damage, 0)}");
                Console.WriteLine($"받은 피해: {damage}");

                // 체력 감소 처리
                player.hp -= damage;
                if (player.hp < 0) player.hp = 0;

                if (player.hp == 0)
                {
                    Console.WriteLine($"\n{player.name}이(가) 쓰러졌습니다...");
                    _isBattle = false;
                    _isVictory = false;
                    break;
                }
            }
        }


        public void BattleResult()
        {
            if (_isVictory)
            {
                Utils.ColoredText("Battle! -Result-", ConsoleColor.DarkYellow);
                Utils.ColoredText("Victory!", ConsoleColor.Green);
                Console.WriteLine("던전에서 몬스터 ");
                Utils.ColoredText($"{fieldMonsters.Count}", ConsoleColor.Red);
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
                Console.WriteLine($"HP {_previousHP} → ");
                Utils.ColoredText("0", ConsoleColor.Red);
            }
        }
    }
}
