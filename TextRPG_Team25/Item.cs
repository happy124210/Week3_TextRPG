namespace TextRPG_Team25
{
    enum ItemType { Potion, Armor, Weapon }

    internal class Item
    {
        public string id;         // 아이템 고유 ID
        public string name;
        public int stat;
        public ItemType type;
        public string description;
        public int price;

        public Item(string id, string name, int stat, ItemType type, string description, int price)
        {
            this.id = id;
            this.name = name;
            this.stat = stat;
            this.type = type;
            this.description = description;
            this.price = price;
        }

        // 복제용 생성자
        public Item(Item other)
        {
            this.id = other.id;
            this.name = other.name;
            this.stat = other.stat;
            this.type = other.type;
            this.description = other.description;
            this.price = other.price;
        }
    }
}