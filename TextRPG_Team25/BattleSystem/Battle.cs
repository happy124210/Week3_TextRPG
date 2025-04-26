using TextRPG_Team25.Core;
using TextRPG_Team25.UI;

namespace TextRPG_Team25.BattleSystem
{
    public class Battle
    {
        private Player player;
        private List<Monster> fieldMonsters = new List<Monster>();
        private bool _isBattle;
        private bool _isVictory;
        private Random random = new Random();

        public Battle(Player player)
        {
            this.player = player;
        }

        public void StartBattle()
        {
            Console.Clear();
            fieldMonsters.Clear();
            SpawnEnemy();
            _isBattle = true;
            _isVictory = true;

            while (_isBattle)
            {
                Console.Clear();
                HandleTurnStart();

                if (fieldMonsters.All(m => m.isDead))
                {
                    _isBattle = false;
                    break;
                }

                PrintBattleScreen();
                PlayerPhase();

                if (!_isBattle) break;

                MonsterPhase();
            }

            BattleResult();
        }

        private void HandleTurnStart()
        {
            player.OnTurnEnd();
            foreach (var monster in fieldMonsters)
            {
                monster.OnTurnEnd();
            }
        }

        private void PrintBattleScreen()
        {
            Utils.ColoredText("[ 몬스터 ]\n", ConsoleColor.DarkCyan);

            for (int i = 0; i < fieldMonsters.Count; i++)
            {
                var m = fieldMonsters[i];
                string status = m.isDead ? "Dead" : $"HP {m.hp}";
                ConsoleColor color = m.isDead ? ConsoleColor.DarkGray : ConsoleColor.Gray;
                Utils.ColoredText($"[{i + 1}] Lv.{m.level} {m.name} {status}\n", color);
            }

            Console.WriteLine();
            player.ShowStatus(showMana: true, showEquipment: false);
            Console.WriteLine();
        }

        private void PlayerPhase()
        {
            Console.WriteLine("[플레이어 행동 선택]");
            Utils.MenuOption("1", "일반 공격");
            Utils.MenuOption("2", player.firstSkill.name);
            Utils.MenuOption("3", player.secondSkill.name);
            Utils.MenuOption("0", "도망치기");
            Console.Write("\n>> ");

            string input = Console.ReadLine();
            if (input == "0")
            {
                _isBattle = false;
                _isVictory = false;
                return;
            }

            while (true)
            {
                int targetIndex = SelectTarget();
                if (targetIndex == -1)
                {
                    continue;
                }

                var target = fieldMonsters[targetIndex];
                if (target.isDead)
                {
                    Console.WriteLine("이미 죽은 몬스터입니다.");
                    Console.ReadKey();
                    continue;
                }

                switch (input)
                {
                    case "1":
                        NormalAttack(target);
                        break;

                    case "2":
                        if (!player.firstSkill.TryActivate(player, target))
                        {
                            Console.WriteLine("\n다시 행동을 선택하세요.");
                            Console.ReadKey();
                            continue; // 다시 선택
                        }
                        break;

                    case "3":
                        if (!player.secondSkill.TryActivate(player, target))
                        {
                            Console.WriteLine("\n다시 행동을 선택하세요.");
                            Console.ReadKey();
                            continue; // 다시 선택
                        }
                        break;

                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        Console.ReadKey();
                        continue; // 다시 선택
                }

                Console.ReadKey();
                break;
            }
        }

        private int SelectTarget()
        {
            Console.WriteLine("\n공격할 몬스터 번호를 입력하세요.");
            Console.Write(">> ");
            string rawInput = Console.ReadLine();

            if (int.TryParse(rawInput, out int selection))
            {
                selection--;
                if (selection >= 0 && selection < fieldMonsters.Count)
                    return selection;
            }

            Console.WriteLine("잘못된 번호입니다.");
            Console.ReadKey();
            return -1;
        }

        private void NormalAttack(Monster target)
        {
            int baseAttack = player.attack;
            int offset = (int)Math.Ceiling(baseAttack * 0.1f);
            int damage = random.Next(baseAttack - offset, baseAttack + offset + 1);
            target.TakeDamage(damage);

            Console.WriteLine($"\n{player.name}의 일반 공격!");
            Console.WriteLine($"{target.name}에게 {damage} 데미지를 입혔습니다!");
        }

        private void MonsterPhase()
        {
            foreach (var monster in fieldMonsters)
            {
                if (monster.isDead) continue;

                Console.Clear();

                int attack = monster.attack;

                if (monster.HasStatus(StatusEffect.Freeze))
                {
                    attack = (int)(attack * 0.7f);
                    Console.WriteLine($"{monster.name}이(가) ❄️ 빙결 상태로 약화된 공격을 합니다!");
                }

                // 몬스터, 플레이어 출력
                PrintBattleScreen();
                Console.WriteLine();

                player.TakeDamage(attack);
                Console.WriteLine($"{monster.name}의 공격! {attack} 데미지를 입혔습니다!");

                if (player.hp <= 0)
                {
                    _isBattle = false;
                    _isVictory = false;
                    break;
                }

                Console.WriteLine("\n다음 몬스터의 공격을 보려면 아무 키나 누르세요...");
                Console.ReadKey();
            }

            if (_isBattle)
            {
                Console.WriteLine("\n플레이어 턴으로 돌아갑니다.");
                Console.ReadKey();
            }
        }

        private void BattleResult()
        {
            Console.Clear();
            Utils.ColoredText("[ 전투 종료 ]\n", ConsoleColor.DarkCyan);

            if (_isVictory)
            {
                Utils.ColoredText("Victory!!\n", ConsoleColor.Green);
                Console.WriteLine($"던전에서 {fieldMonsters.Count}마리의 몬스터를 물리쳤습니다!");
            }
            else
            {
                Utils.ColoredText("You Lose...\n", ConsoleColor.Red);
            }

            Console.WriteLine("\n계속하려면 아무 키나 누르세요...");
            Console.ReadKey();
        }

        private void SpawnEnemy()
        {
            int spawnNum = random.Next(1, 5);

            for (int i = 0; i < spawnNum; i++)
            {
                fieldMonsters.Add(Monster.GenerateRandomMonster());
            }
        }
    }
}