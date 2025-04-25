using TextRPG_Team25.UI;
using TextRPG_Team25.Core;
using System.Threading;
namespace TextRPG_Team25
{
    internal class Battle(Player player)
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
                Console.Clear();
                int deadNum = 0;                              //검사 진행을 위한 변수
                //몬스터 전원 처치 확인
                for (int i = 0; i < fieldMonsters.Count; i++)
                {
                    if (fieldMonsters[i].isDead) deadNum++;
                }
                if (deadNum == fieldMonsters.Count)
                {
                    _isBattle = false;
                    break;
                }
                // 메세지 출력
                for (int i = 0; i < fieldMonsters.Count; i++)
                {
                    var m = fieldMonsters[i];
                    string status = m.isDead ? "Dead" : $"HP {m.hp}";
                    Console.WriteLine($"{i + 1}. Lv.{m.level} {m.name}  {status}");
                }

                Console.WriteLine("");
                Console.WriteLine("[내정보]");
                Console.WriteLine($"Lv.{player.level}  {player.name} ({player.job})");
                Console.WriteLine($"HP {player.hp}/{player.maxHp}\n");
                Console.WriteLine("0. 취소\n");
                Console.WriteLine("대상을 선택해주세요.");
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
            if (IsEvasion()) { Console.WriteLine($"{selected.name} 이(가) 공격을 회피했습니다"); } //공격 성공 여부 판단
            else //공격 성공시
            {

                int baseAttack = player.attack;
                int currentMonsterHp = selected.hp;
                int offset = (int)Math.Ceiling(baseAttack * 0.1f);
                int damage = _random.Next(baseAttack - offset, baseAttack + offset + 1);

                if (IsCritical()) { damage = (int)Math.Ceiling(damage * 1.6f); Console.WriteLine("크리티컬 발동!"); } //크리티컬 여부판단
                selected.hp -= damage;

                Console.WriteLine($"\n{player.name}의 공격!");
                Console.WriteLine($"{selected.name}을 맞췄습니다. [데미지 : {damage}]");
                Console.WriteLine($"HP {currentMonsterHp} → {(selected.hp > 0 ? selected.hp.ToString() : "Dead")}");

                if (selected.hp <= 0)
                {
                    selected.hp = 0;
                    selected.isDead = true;

                    foreach (TextRPG_Team25.Quest.Quest quest in GameManager.Instance.questManager.questList)
                    {
                        if (!quest.isCompleted && quest.title == "마을을 위협하는 미니언 처치")
                        {
                            quest.currentCount++;
                            if(quest.currentCount >=quest.goalCount)
                            {
                                quest.isCompleted = true;
                                Console.WriteLine("\n퀘스트 완료! [마을을 위협하는 미니언 처치]");
                            }
                        }
                    }
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

                Console.WriteLine($"\nLv.{monster.level} {monster.name}의 공격!");
                Console.WriteLine($"Lv.{player.level} {player.name}");
                Console.WriteLine($"HP {player.hp} → {Math.Max(player.hp - damage, 0)}");
                Console.WriteLine($"받은 피해: {damage}");

                player.hp -= damage;

                if (player.hp <= 0)
                {
                    player.hp = 0;
                    Console.WriteLine($"\n{player.name}이(가) 쓰러졌습니다...");
                    _isBattle = false;
                    _isVictory = false;
                    break;
                }
                Console.ReadKey();
            }

            if (_isBattle)
            {
                Console.WriteLine("플레이어 턴으로 이동합니다.");
            }
            else
            {
                Console.WriteLine("전투 결과로 이동합니다.");
            }

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

            for (int i = 0; i < spawnNum; i++)
            {
                fieldMonsters.Add(Monster.GenerateRandomMonster());
            }
        }

        private bool IsEvasion()
        {
            int evasion = _random.Next(1, 101);
            if (evasion <= 10) { return true; }
            else { return false; }
        }

        private bool IsCritical()
        {
            int critical = _random.Next(1, 101);
            if (critical <= 15) { return true; }
            else { return false; }
        }


    }
}