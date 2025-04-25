using TextRPG_Team25.UI;

namespace TextRPG_Team25.Core
{
    public class Player
    {
        public string name = "";
        public string job = "검투사";

        public int level = 1;
        public int attack;
        public int defense;
        public int maxHp;
        public int hp; // 현재 체력
        public int gold = 1500;

        internal List<Item> inventory = new List<Item>();
        Shop shop = new Shop();
        public void ShowStatus()
        {
            UpdateStatsBasedOnJob();

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

                Utils.MenuOption("1", "직업 선택");
                Utils.MenuOption("0", "나가기");
                Console.Write(">> ");

                string input = Console.ReadLine();
                if (input == "0") break;

                else if (input == "1")
                {
                    JobMenu jobMenu = new JobMenu(this);
                    jobMenu.ShowJobSelectionMenu();
                    UpdateStatsBasedOnJob();
                }

                // 잘못된 입력 처리
                Console.WriteLine("잘못된 입력입니다");
                Console.WriteLine("계속하려면 아무 키나 누르세요...");
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

            // 변경된 능력치에 맞게 현재 체력을 갱신
            hp = maxHp;
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

        public void ShowShopItem()
        {
            shop.ShowShop();
            string input = Console.ReadLine();
            bool num = int.TryParse(input, out int number);
            if (num)
            {
                switch (number)
                {
                    case 1:
                        Console.WriteLine("구매하실 아이템을 선택해 주세요");
                        string shopNum = Console.ReadLine();
                        int buyNum = int.Parse(shopNum);
                        BuyShopItem(buyNum);
                        break;
                    case 2:
                        Console.WriteLine("판매하실 아이템을 선택해 주세요");
                        break;
                }
            }
            else
            {
                Console.ReadKey();
            }
        }
        public void BuyShopItem(int buyNum)
        {
            int shopNum = buyNum - 1;
            if (buyNum > shop.shopItems.Count)
            {
                Console.WriteLine("상점에 판매하지 않는 물품입니다.");
                Console.ReadKey();
            }
            else
            {
                if (shop.shopItems[shopNum].price <= gold)
                {
                    gold -= shop.shopItems[shopNum].price;
                    inventory.Add(shop.AddShopItem(shopNum));
                }
                else
                {
                    Console.WriteLine("골드가 부족합니다.");
                }
            }
        }

    }
}
