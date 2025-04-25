using System.Numerics;

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
        // 상태 보기 메서드: 7개 속성만 출력

        internal List<Item> inventory = new List<Item>();
        
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
    }
}
