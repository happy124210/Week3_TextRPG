namespace TextRPG_Team25.Core
{
    public class Player
    {
        public string name = "";
        public string job = "전사";

        public int level = 1;
        public int attack = 10;
        public int defense = 5;
        public int maxHp = 100;
        public int hp = 100; // 현재 체력
        public int gold = 1500;
        

        internal List<Item> inventory = new List<Item>();

        private Item equippedWeapon = null;
        private Item equippedArmor = null;

        public void ShowStatus()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("상태 보기\n");

                // 필수 속성 출력
                Console.WriteLine($"Lv.{level:D2}");
                Console.WriteLine($"{name} ( {job} )");
                Console.WriteLine($"공격력 : {attack}");
                Console.WriteLine($"방어력 : {defense}");
                Console.WriteLine($"체력 : {hp} / {maxHp}");
                Console.WriteLine($"Gold : {gold} G\n");

                Console.WriteLine("0. 나가기\n");
                Console.Write(">> ");

                string input = Console.ReadLine();
                if (input == "0")
                    break;

                // 잘못된 입력 처리
                Console.WriteLine("잘못된 입력입니다");
                Console.WriteLine("계속하려면 아무 키나 누르세요...");
                Console.ReadKey();
            }
        }

        public void ShowInventory()
        {
            Console.Clear();

            // 테스트용 아이템
            AddInventory("doranSword");
            AddInventory("doranShield");

            if (inventory.Count == 0)
            {
                Console.WriteLine("인벤토리가 비어 있습니다.");
            }
            else
            {
                Console.WriteLine("인벤토리 목록\n");

                for (int i = 0; i < inventory.Count; i++)
                {
                    var item = inventory[i];
                    string typeLabel = item.type switch
                    {
                        ItemType.Weapon => "공격력",
                        ItemType.Armor => "방어력",
                        ItemType.Potion => "회복량",
                    };

                    string equipLabel = (item == equippedWeapon || item == equippedArmor) ? "[장착중] " : "";
                    Console.Write($"{i + 1}. {equipLabel}{item.name} ");
                    Console.WriteLine($"| {typeLabel} +{item.stat}");
                }
            }

            Console.WriteLine("\n계속하려면 아무 키나 누르세요...");
            Console.ReadKey();
        }

        public void EquipItemById(string id)
        {
            Item item = inventory.FirstOrDefault(i => i.id == id);

            if (item == null)
            {
                Console.WriteLine($"인벤토리에 '{id}' 아이템이 없습니다.");
                return;
            }

            switch (item.type)
            {
                case ItemType.Weapon:
                    if (equippedWeapon != null)
                    {
                        attack -= equippedWeapon.stat;
                        Console.WriteLine($"{equippedWeapon.name}을(를) 해제했습니다.");
                    }
                    equippedWeapon = item;
                    attack += item.stat;
                    Console.WriteLine($"{item.name}을(를) 장착했습니다! (공격력 +{item.stat})");
                    break;

                case ItemType.Armor:
                    if (equippedArmor != null)
                    {
                        defense -= equippedArmor.stat;
                        Console.WriteLine($"{equippedArmor.name}을(를) 해제했습니다.");
                    }
                    equippedArmor = item;
                    defense += item.stat;
                    Console.WriteLine($"{item.name}을(를) 장착했습니다! (방어력 +{item.stat})");
                    break;

                case ItemType.Potion:
                    Console.WriteLine("포션은 장착할 수 없습니다.");
                    break;
            }
        }
            

        public void AddInventory(string id)
        {
            Item item = ItemDatabase.GetItemById(id);
            if (item != null)
            {
                inventory.Add(item);
                Console.WriteLine($"{item.name}을(를) 인벤토리에 추가했습니다!");
            }
            else
            {
                Console.WriteLine($"ID '{id}'에 해당하는 아이템이 없습니다.");
            }
        }
    }
}