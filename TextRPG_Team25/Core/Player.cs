using TextRPG_Team25.BattleSystem;
using TextRPG_Team25.UI;

namespace TextRPG_Team25.Core
{
    public class Player
    {
        public string name = "";
        public string job = "검투사";

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
                Utils.MenuOption("1", "직업 선택");
                Utils.MenuOption("0", "나가기");
                Console.Write("행동을 선택해주세요.\n>> ");

                string input = Console.ReadLine();
                if (input == "0")
                    break;

                else if (input == "1")
                {
                    // 직업 선택 메뉴 호출
                    JobMenu jobMenu = new JobMenu(this);
                    jobMenu.ShowJobSelectionMenu();
                    UpdateStatsBasedOnJob();
                    SetSkillsByJob();
                }

                Console.ReadKey();
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


        public void ShowInventory()  //인벤토리 보기
        {
            Console.Clear();

            // 테스트용 임시 아이템
            AddInventory(0);
            AddInventory(1);

            for(int i = 0; i < inventory.Count; i++)
            {
                inventory[i].ShowItem();
            }
            Console.WriteLine("장착하실 아이템을 선택해주세요");
            Console.WriteLine("계속하려면 아무 키나 누르세요...");
            string inputNumber = Console.ReadLine();
            bool num = int.TryParse(inputNumber, out int number);
            if (num)
            {
                if (number <= inventory.Count)
                {
                    EquipmentItem(inventory[number-1]);
                }
                else
                {
                    Console.WriteLine("유효하지 않은 입력입니다. 메인화면으로 돌아갑니다.");
                }
            }
            else
            { 
                Console.WriteLine("유효하지 않은 입력입니다., 메인화면으로 돌아갑니다.");
                Console.ReadKey();
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


        public void EquipmentManage()
        {
            Console.WriteLine("장착하거나 해제 하실 장비를 선택하세요");
            string inputNum = Console.ReadLine();
            if(!int.TryParse(inputNum, out int input))
            {
                Console.WriteLine("잘못된 입력입니다.");
                Console.ReadKey();
            }
            if (input == 0) 
            {
                Console.WriteLine("메인화면으로 돌아갑니다.");
                Console.ReadKey();
            }
            if (inventory[input - 1].type == ItemType.Potion)
            {
                Console.WriteLine("포션은 장착하거나 해제할 수 없습니다.");
                Console.ReadKey();
            }
            else
            {
                EquipmentItem(inventory[input - 1]);
            }
        }

        public void AddInventory(int index) // 아이템 획득 시 실행
        {
            inventory.Add(Item.AddItem(index));
        }

        private void EquipmentItem(Item item)
        {
            switch (item.type)
            {
                case ItemType.Weapon:   //플레이어 공격력 증가                  
                    if (!item.isEquip)
                    {
                        attack += item.effect;
                        item.isEquip = true;
                    }
                    else
                    {
                        attack -= item.effect;
                        item.isEquip = false;
                    }
                    break;

                case ItemType.Armor:  //플레이어 방어력 증가
                    if (!item.isEquip)
                    {
                        defense += item.effect;
                        item.isEquip = true;
                    }
                    else
                    {
                        defense -= item.effect;
                        item.isEquip = false;
                    }
                    break;

                case ItemType.Potion:   //플레이어 체력 증가
                    hp += item.effect;
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
