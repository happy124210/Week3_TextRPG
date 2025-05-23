﻿using TextRPG_Team25.Core;
using TextRPG_Team25.UI;

namespace TextRPG_Team25.BattleSystem
{
    public class Battle
    {
        private Player player;
        private List<Monster> fieldMonsters = new List<Monster>();
        private bool isBattle;
        private bool isVictory;
        private Random random = new Random();

        public Battle(Player player)
        {
            this.player = player;
        }

        // 배틀 전체 흐름
        public void StartBattle()
        {
            Console.Clear();
            
            // 초기 세팅
            fieldMonsters.Clear();
            SpawnMonsters();
            isBattle = true;
            isVictory = true;

            // 전투 진행
            while (isBattle)
            {
                Console.Clear();
                HandleTurnStart();

                if (fieldMonsters.All(m => m.isDead))
                {
                    isBattle = false;
                    break;
                }

                PrintBattleScreen();
                PlayerPhase();

                if (!isBattle) break;

                MonsterPhase();
            }

            BattleResult();
        }

        // 턴 시작 시 플레이어, 몬스터 상태이상 확인
        private void HandleTurnStart()
        {
            player.OnTurnStart();
            foreach (var monster in fieldMonsters)
            {
                monster.OnTurnStart();
            }
        }

        // 배틀 UI 출력
        private void PrintBattleScreen()
        {
            Utils.ColoredText("[ 전투 상황 ]\n\n", ConsoleColor.DarkCyan);

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

        // 플레이어 턴
        private void PlayerPhase()
        {
            while (true)
            {
                // 행동 선택 메뉴 출력
                Console.WriteLine("[플레이어 행동 선택]");
                Utils.MenuOption("1", "일반 공격");
                Console.WriteLine();
                Utils.MenuOption("2", player.firstSkill.name);
                Console.Write($"{player.firstSkill.description} - 필요 마나: ");
                Utils.ColoredText($"{player.firstSkill.manaCost}\n\n", ConsoleColor.Cyan);

                Utils.MenuOption("3", player.secondSkill.name);
                Console.Write($"{player.secondSkill.description} - 필요 마나: ");
                Utils.ColoredText($"{player.secondSkill.manaCost}\n\n", ConsoleColor.Cyan);

                Utils.MenuOption("0", "도망치기");
                Console.Write("\n>> ");

                string input = Console.ReadLine();
                if (input == "0")
                {
                    isBattle = false;
                    isVictory = false;
                    return;
                }

                int targetIndex = SelectTarget();
                if (targetIndex == -1) continue;

                var target = fieldMonsters[targetIndex];
                if (target.isDead)
                {
                    Console.WriteLine("이미 죽은 몬스터입니다.");
                    Console.ReadKey();
                    continue;
                }

                int damage = 0;

                switch (input)
                {
                    case "1":
                        damage = NormalAttack(target);
                        break;
                    case "2":
                        damage = player.firstSkill.TryActivate(player, target);
                        break;
                    case "3":
                        damage = player.secondSkill.TryActivate(player, target);
                        break;
                    default:
                        Console.WriteLine("\n잘못된 입력입니다.");
                        Console.ReadKey();
                        continue;
                }

                // 마나 부족, 쿨타임 부족 시 damage = -1 반환됨
                if (damage < 0)
                {
                    Console.WriteLine("\n행동에 실패했습니다. 다시 선택하세요.");
                    Console.ReadKey();
                    continue;
                }

                Console.WriteLine($"\n{target.name}을(를) 공격했습니다!");
                Console.WriteLine($"{target.name} HP {Math.Max(target.hp + damage, 0)} → {Math.Max(target.hp, 0)}");

                Console.ReadKey();
                break;
            }
        }

        // 선택 로직
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

        // 일반 공격 처리
        private int NormalAttack(Monster target)
        {
            int baseAttack = player.attack;
            int offset = (int)Math.Ceiling(baseAttack * 0.1f);
            int damage = random.Next(baseAttack - offset, baseAttack + offset + 1);

            // 회피 판정
            if (IsEvasion())
            {
                Utils.ColoredText($"{target.name}이(가) 공격을 회피했습니다!\n", ConsoleColor.Yellow);
                return 0; // 공격 실패
            }

            // 치명타 판정
            if (IsCritical())
            {
                damage = (int)Math.Ceiling(damage * 1.6f);
                Utils.ColoredText("크리티컬 발동!!\n", ConsoleColor.Red);
            }

            // 데미지 적용
            target.TakeDamage(damage);

            return damage;
        }

        // 몬스터 턴
        private void MonsterPhase()
        {
            foreach (var monster in fieldMonsters)
            {
                if (monster.isDead) continue;

                Console.Clear();
                PrintBattleScreen();
                Console.WriteLine();

                int attack = monster.attack;

                if (monster.HasStatus(StatusEffect.Freeze))
                {
                    attack = (int)(attack * 0.7f);
                    Console.WriteLine($"{monster.name}이(가) 빙결 상태로 약화된 공격을 합니다! ❄️");
                }

                player.TakeDamage(attack);
                Console.WriteLine($"{monster.name}의 공격! {attack} 데미지를 입혔습니다!");
                Console.WriteLine();

                if (player.hp <= 0)
                {
                    isBattle = false;
                    isVictory = false;
                    break;
                }

                Console.WriteLine("\n다음 몬스터 공격을 보려면 아무 키나 누르세요...");
                Console.ReadKey();
            }

            if (isBattle)
            {
                Console.WriteLine("\n플레이어 턴으로 돌아갑니다.");
                Console.ReadKey();
            }
        }

        // 배틀 결과 출력
        private void BattleResult()
        {
            Console.Clear();
            Utils.ColoredText("[ 전투 종료 ]\n", ConsoleColor.DarkCyan);

            if (isVictory)
            {
                Utils.ColoredText("Victory!!\n", ConsoleColor.Green);
                Console.WriteLine($"던전에서 {fieldMonsters.Count}마리의 몬스터를 처치했습니다!");
            }
            else
            {
                Utils.ColoredText("You Lose...\n", ConsoleColor.Red);
            }

            Console.WriteLine("\n계속하려면 아무 키나 누르세요...");
            Console.ReadKey();
        }

        // 초기 몬스터 스폰
        private void SpawnMonsters()
        {
            int spawnNum = random.Next(1, 5);

            for (int i = 0; i < spawnNum; i++)
            {
                fieldMonsters.Add(Monster.GenerateRandomMonster());
            }
        }

        private bool IsEvasion()
        {
            int evasion = random.Next(1, 101);
            return evasion <= 10;
        }

        private bool IsCritical()
        {
            int critical = random.Next(1, 101);
            return critical <= 15;
        }
    }
}
