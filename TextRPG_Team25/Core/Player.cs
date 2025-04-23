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

        public void ShowInventory()
        {
            Console.Clear();
            for(int i = 0; i < inventory.Count; i++)
            {
                var item = inventory[i];
                switch (item.type)
                {
                    case ItemType.atKEquip:break;
                    case ItemType.defEquip: break;
                    case ItemType.portion: break;
                }
                Console.WriteLine($"{item.name}");
            }
            //인벤토리 보기
        }

        public void EquipManage()
        {
            //장비 장착 관리
        }

        public void AddInventory()
        {
            //아이템 추가 배틀에서 관리 시 삭제
        }
    }
}
