namespace TextRPG_Team25
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
        public Item(string newId, string newName, int newEffect, ItemType newType, bool newIsEquip, bool newIsPurchase, int newPrice)
        {
            id = newId;
            name = newName;
            effect = newEffect;
            type = newType;
            isEquip = newIsEquip;
            isPurchase = newIsPurchase;
            price = newPrice;
            
        }


        public Item(Item original)
        {
            this.id = original.id;
            this.name = original.name;
            this.effect = original.effect;
            this.type = original.type;
            this.isEquip = original.isEquip;
            this.isEquip = original.isPurchase;
            this.price = original.price;
        }


        public static List<Item> items { get; } = new List<Item>
        {
            new Item("1", "무기", 5, ItemType.Weapon, false, false, 0),
            new Item("2", "방어구", 5, ItemType.Armor, false, false,0),
            new Item("3", "포션", 5, ItemType.Potion, false, false, 0),
        };

        
        public void ShowItem()
        {
            string itemName = "";
            switch (type)
            {
                case ItemType.Weapon:
                    itemName = "공격력";
                    break;
                case ItemType.Armor:
                    itemName = "방어력";
                    break;
                case ItemType.Potion:
                    itemName = "포션";
                    break;
            }

            if (isEquip)
            {
                Console.WriteLine($"[장착중] {name}   {itemName} : +{effect}");
            }

            else
            {
                Console.WriteLine($"{name}   {itemName} : +{effect}");
            }


        }

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
