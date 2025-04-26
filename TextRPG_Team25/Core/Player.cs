using TextRPG_Team25.BattleSystem;
using TextRPG_Team25.UI;

namespace TextRPG_Team25.Core
{
    public class Player
    {
        public string name = "";
        public string job = "";

        public int level = 1;
        public int attack = 10;
        public int defense = 5;
        public int hp = 100; // 현재 체력
        public int maxHp = 100;
        public int gold = 1500;
        public int mana = 50;
        public int maxMana = 50;
        public Skill firstSkill;
        public Skill secondSkill;

        private int _activeDefenseBuffTurns;
        private int _temporaryDefenseValue;

        public List<Item> inventory = new List<Item>();

        public void StatusMenu()
        {
            while (true)
            {
                Console.Clear();
                ShowStatus();
                Utils.MenuOption("1", "직업 변경");
                Utils.MenuOption("0", "나가기");
                Console.Write("\n행동을 선택해주세요.\n>> ");

                string input = Console.ReadLine();
                if (input == "0")
                    break;
                else if (input == "1")
                {
                    JobMenu jobMenu = new JobMenu(this);
                    jobMenu.ShowJobSelectionMenu();
                    UpdateStatsBasedOnJob();
                    SetSkillsByJob();
                }
                else
                {
                    Console.WriteLine("\n잘못된 입력입니다.");
                    Console.ReadKey();
                }
            }
        }

        // 플레이어 스탯 출력
        public void ShowStatus(
            bool showName = true,
            bool showLevel = true,
            bool showStats = true,
            bool showHP = true,
            bool showMana = true,
            bool showGold = true,
            bool showEquipment = true)
        {
            Utils.ColoredText("\n[ 캐릭터 정보 ]\n\n", ConsoleColor.DarkCyan);
            Console.WriteLine("==================================\n");

            if (showName) Console.WriteLine($" 이름     : {name} ({job})");
            if (showLevel) Console.WriteLine($" 레벨     : Lv. {level}");

            if (showStats)
            {
                Console.Write($" 공격력   : {attack}\n");
                Console.Write($" 방어력   : {defense}\n");
            }

            if (showHP)
            {
                Console.Write(" 체력     : ");
                Utils.ColoredText($"{hp} / {maxHp}\n", ConsoleColor.Green);
            }

            if (showMana)
            {
                Console.Write(" 마나     : ");
                Utils.ColoredText($"{hp} / {maxHp}\n", ConsoleColor.Cyan);
            }    

            if (showGold)
            {
                Console.Write(" 골드     : ");
                Utils.ColoredText($"{gold}", ConsoleColor.DarkYellow);
                Console.WriteLine(" G");
            }

            Console.WriteLine("\n==================================\n");
        }

        //인벤토리 보기
        public void ShowInventory()
        {
            while (true)
            {
                Console.Clear();
                Utils.ColoredText("\n[ 인벤토리 목록 ]\n\n", ConsoleColor.DarkCyan);

                if (inventory.Count == 0)
                {
                    Console.WriteLine("인벤토리가 비어 있습니다.");
                    Console.ReadKey();
                    break;
                }

                for (int i = 0; i < inventory.Count; i++)
                {
                    Console.Write($"[{i + 1}] ");
                    inventory[i].ShowItem();
                }

                Console.WriteLine();
                Utils.MenuOption("0", "메인 메뉴로 돌아가기");
                Console.WriteLine();
                Console.Write("장착하거나 사용할 아이템 번호를 입력하세요.\n>> ");

                string inputNumber = Console.ReadLine();
                bool valid = int.TryParse(inputNumber, out int number);

                if (!valid)
                {
                    Console.WriteLine("\n잘못된 입력입니다. 아무 키나 누르면 다시 선택할 수 있습니다.");
                    Console.ReadKey();
                    continue;
                }

                if (number == 0)
                {
                    Console.WriteLine("\n메인 메뉴로 돌아갑니다!");
                    Console.ReadKey();
                    break;
                }

                if (number >= 1 && number <= inventory.Count)
                {
                    EquipmentItem(inventory[number - 1]);
                    Console.WriteLine("\n계속 장착하거나 나가려면 선택하세요.");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("\n유효하지 않은 번호입니다.");
                    Console.ReadKey();
                }
            }
        }


        public void UpdateStatsBasedOnJob()
        {
            switch (job)
            {
                case "검투사":
                    attack = 11;
                    defense = 5;
                    maxHp = 100;
                    break;
                case "화염술사":
                    attack = 13;
                    defense = 5;
                    maxHp = 60;
                    break;
                case "얼음술사":
                    attack = 7;
                    defense = 10;
                    maxHp = 150;
                    break;
            }

            hp = maxHp;
        }


        public void AddInventory(string id) // 아이디로 아이템 추가
        {
            Item other = Item.GetItem(id);
            if (other != null)
            {
                inventory.Add(new Item(other));
            }
            else
            {
                Console.WriteLine($"[오류] 아이디 '{id}'에 해당하는 아이템을 찾을 수 없습니다.");
            }
        }


        private void EquipmentItem(Item item)
        {
            switch (item.type)
            {
                case ItemType.Weapon:   // 무기 장착
                    if (!item.isEquip)
                    {
                        attack += item.effect;
                        item.isEquip = true;
                        Utils.ColoredText($"{item.name}을(를) 장착했습니다! (공격력 +{item.effect})", ConsoleColor.Green);
                    }
                    else
                    {
                        attack -= item.effect;
                        item.isEquip = false;
                        Utils.ColoredText($"{item.name}을(를) 해제했습니다! (공격력 -{item.effect})", ConsoleColor.Red);
                    }
                    break;

                case ItemType.Armor:    // 방어구 장착
                    if (!item.isEquip)
                    {
                        defense += item.effect;
                        item.isEquip = true;
                        Utils.ColoredText($"{item.name}을(를) 장착했습니다! (방어력 +{item.effect})", ConsoleColor.Green);
                    }
                    else
                    {
                        defense -= item.effect;
                        item.isEquip = false;
                        Utils.ColoredText($"{item.name}을(를) 해제했습니다! (방어력 -{item.effect})", ConsoleColor.Red);
                    }
                    break;

                case ItemType.Potion:   // 포션 사용
                    hp += item.effect;
                    if (hp > maxHp) hp = maxHp;
                    Utils.ColoredText($"{item.name}을(를) 사용했습니다! (체력 +{item.effect})", ConsoleColor.Cyan);
                    inventory.Remove(item); // 포션은 사용하면 인벤토리에서 삭제
                    break;
            }
        }


        public void TakeDamage(int damage)
        {
            int reduced = defense / 2;
            int finalDamage = Math.Max(1, damage - reduced);
            hp -= finalDamage;

            if (hp <= 0)
            {
                hp = 0;
                Console.WriteLine($"\n{name}이(가) 쓰러졌습니다..."); 
            }

            Console.WriteLine($"HP {hp}");
        }

        // 방어력 버프
        public void AddTemporaryDefense(int amount, int duration)
        {
            defense += amount;
            _activeDefenseBuffTurns = duration;
            _temporaryDefenseValue = amount;
        }


        public void OnTurnEnd()
        {
            if (_activeDefenseBuffTurns > 0)
            {
                _activeDefenseBuffTurns--;
                if (_activeDefenseBuffTurns == 0)
                {
                    defense -= _temporaryDefenseValue;
                    Console.WriteLine("버프가 사라졌습니다.");
                }
            }
        }


        public void SetSkillsByJob()
        {
            switch (job)
            {
                case "검투사":
                    firstSkill = new DecisiveStrike();
                    secondSkill = new Courage();
                    break;

                case "화염술사":
                    firstSkill = new Disintegrate();
                    secondSkill = new MoltenShield();
                    break;

                case "얼음술사":
                    firstSkill = new IceShard();
                    secondSkill = new FrostArrow();
                    break;
            }
        }
    }
}
