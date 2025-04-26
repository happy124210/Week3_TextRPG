using TextRPG_Team25.UI;
using TextRPG_Team25.Core;
using TextRPG_Team25.BattleSystem;
namespace TextRPG_Team25
{
    public class Battle(Player player)
    {
        private Utils utils = new Utils();

        private Random _random = new Random();
        private int _previousHP = player.hp;
        private bool _isBattle = true;
        private bool _isVictory = true;

        private List<Monster> fieldMonsters = new List<Monster>();

        // 배틀 진행
        public void StartBattle()
        {
            Console.Clear();
            fieldMonsters.Clear();
            _isBattle = true;
            _isVictory = true;

            SpawnEnemy();

            while (_isBattle)
            {
                // 상태이상 및 쿨타임 체크
                player.OnTurnEnd();
                foreach (var monster in fieldMonsters)
                    monster.OnTurnEnd();

                int deadNum = 0;

                // 몬스터 전원 처치 확인
                if (fieldMonsters.All(m => m.isDead))
                {
                    _isBattle = false;
                    break;
                }

                Console.WriteLine();

                // 몬스터 상태 출력
                for (int i = 0; i < fieldMonsters.Count; i++)
                {
                    var m = fieldMonsters[i];
                    string status = m.isDead ? "Dead" : $"HP {m.hp}";
                    ConsoleColor color = m.isDead ? ConsoleColor.DarkGray : ConsoleColor.Gray;
                    Utils.ColoredText($"[{i + 1}] " , ConsoleColor.Yellow);
                    Utils.ColoredText($"Lv.{m.level} {m.name}  {status}\n\n", color);
                }

                player.ShowStatus(showGold: false, showEquipment: false);
                Console.WriteLine();
                Utils.MenuOption("0", "게임 종료");
                Console.WriteLine("공격할 대상을 선택해주세요.");
                Console.Write(">> ");

                string rawInput = Console.ReadLine();
                if (!int.TryParse(rawInput, out int input))
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.ReadKey();
                    continue;
                }

                // 전투 취소
                if (input == 0)
                {
                    Console.WriteLine("전투를 취소합니다.");
                    _isBattle = false;
                    _isVictory = false;
                    Console.ReadKey();
                    return;
                }
                // 플레이어 턴 진행
                else if (input > 0 && input <= fieldMonsters.Count)
                {
                    // 죽은 몬스터 선택할 경우
                    if (fieldMonsters[input - 1].isDead)
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                        Console.ReadKey();
                        continue;
                    }

                    PlayerPhase(fieldMonsters[input - 1]);
                }
                // 예외 처리
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.ReadKey();
                }

                if (!_isBattle) break;
                
                // 몬스터 턴 진행
                MonsterPhase();
            }

            BattleResult();
        }

        private void PlayerPhase(Monster selected)
        {
            Utils.MenuOption("1", "일반 공격");
            Utils.MenuOption("2", $"{player.firstSkill.name}");
            Utils.MenuOption("3", $"{player.secondSkill.name}");
            Console.Write(">> ");
            string skillChoice = Console.ReadLine();

            if (skillChoice == "2")
                player.firstSkill.TryActivate(player, selected);
            else if (skillChoice == "3")
                player.secondSkill.TryActivate(player, selected);
            else
            {
                if (IsEvasion()) // 공격 실패 시
                {
                    Console.WriteLine($"{selected.name} 이(가) 공격을 회피했습니다");
                }

                else //공격 성공 시
                {
                    int baseAttack = player.attack;
                    int offset = (int)Math.Ceiling(baseAttack * 0.1f);
                    int damage = _random.Next(baseAttack - offset, baseAttack + offset + 1);

                    if (IsCritical())
                    {
                        damage = (int)Math.Ceiling(damage * 1.6f);
                        Console.WriteLine("크리티컬 발동!");
                    }

                    selected.TakeDamage(damage);
                    Console.WriteLine($"\n{player.name}의 공격!");
                    Console.WriteLine($"{selected.name}을 맞췄습니다. [데미지 : {damage}]");
                }

                Console.WriteLine("몬스터 턴으로 이동합니다.");
                Console.ReadKey();
            }
        }

        private void MonsterPhase()
        {
            foreach (Monster monster in fieldMonsters)
            {
                if (monster.isDead) continue;

                int offset = (int)Math.Ceiling(monster.attack * 0.1f);
                int damage = _random.Next(monster.attack - offset, monster.attack + offset + 1);

                if (monster.HasStatus(StatusEffect.Freeze))
                {
                    damage = (int)(damage * 0.7f);
                    Console.WriteLine($"{monster.name}이(가) 빙결 상태로 약하게 공격합니다. ❄️");
                }

                Console.WriteLine($"Lv.{monster.level} {monster.name}의 공격!");
                player.TakeDamage(damage);
            }

            Console.WriteLine("플레이어 턴으로 이동합니다.");
            Console.ReadKey();
        }

        private void BattleResult()
        {
            Utils.ColoredText("\nBattle! -Result-\n", ConsoleColor.DarkYellow);

            if (_isVictory)
            {
                Utils.ColoredText("Victory!\n", ConsoleColor.Green);
                Console.WriteLine($"던전에서 몬스터 {fieldMonsters.Count}마리를 잡았습니다.\n");
            }
            else
            {
                Utils.ColoredText("You Lose . . .\n", ConsoleColor.Magenta);
            }

            Console.WriteLine($"Lv.{player.level} {player.name}");
            Console.WriteLine($"HP {_previousHP} → {player.hp}");
            Console.ReadKey();
        }

        private void SpawnEnemy()
        {
            int spawnNum = _random.Next(1, 5);
            Console.WriteLine($"[디버깅] {spawnNum}마리 소환 시도");
            for (int i = 0; i < spawnNum; i++)
            {
                fieldMonsters.Add(Monster.GenerateRandomMonster());
            }
        }

        private bool IsEvasion()
        {
            return _random.Next(1, 101) <= 10;
        }

        private bool IsCritical()
        {
            return _random.Next(1, 101) <= 15;
        }
    }
}
