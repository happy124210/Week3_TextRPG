namespace TextRPG_Team25.ItemSystem
{
    public enum ItemType 
    { 
        Potion, 
        Armor, 
        Weapon 
    };


    public class Item
    {
        public string id;
        public string name;
        public int effect;
        public ItemType type;
        public bool isEquip;
        public bool isPurchase;
        public int price;

        public Item() { }
        public Item(string newId, string newName, int newEffect, ItemType newType, int newPrice)
        {
            id = newId;
            name = newName;
            effect = newEffect;
            type = newType;
            price = newPrice;
            
        }


        public Item(Item original)
        {
            id = original.id;
            name = original.name;
            effect = original.effect;
            type = original.type;
            price = original.price;
        }


        public static List<Item> items = new List<Item>
        {
            new Item { id = "doranSword", name = "도란의 검", effect = 8, type = ItemType.Weapon, price = 450 },
            new Item { id = "doranShield", name = "도란의 방패", effect = 8, type = ItemType.Armor, price = 450 },
            new Item { id = "longSword", name = "롱소드", effect = 10, type = ItemType.Weapon, price = 350 },
            new Item { id = "chainVest", name = "사슬 조끼", effect = 15, type = ItemType.Armor, price = 800 },
            new Item {id = "healthPotion", name = "체력 물약", effect = 30, type = ItemType.Potion, price = 50},
            new Item {id = "bigHealthPotion", name = "대형 체력 물약", effect = 75, type = ItemType.Potion, price = 150},
            new Item {id = "bfSword", name = "B.F. 대검", effect = 40, type = ItemType.Weapon, price = 1300},
            new Item {id = "thornmail", name = "가시 갑옷", effect = 30, type = ItemType.Armor, price = 2700},
            new Item {id = "elixirOfWrath", name = "분노의 영약", effect = 50, type = ItemType.Potion, price = 500},
            new Item {id = "infinityEdge", name = "무한의 대검", effect = 60, type = ItemType.Weapon, price = 3400},
        };


        public static Item GetItem(string id)
        {
            Item foundItem = items.FirstOrDefault(item => item.id == id);

            if (foundItem == null)
            {
                Console.WriteLine($"[오류] 아이디 '{id}'에 해당하는 아이템을 찾을 수 없습니다.");
                return null;
            }

            return new Item(foundItem); // 복사본 반환
        }
    }
}
