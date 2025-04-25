using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Team25.UI;
using TextRPG_Team25.Core;


namespace TextRPG_Team25
{
    enum ItemType { Potion, Armor, Weapon }; // 포션, 방어 장비, 공격 장비
    internal class Item
    {
        public string name;
        public int effect;
        public ItemType type;
        public bool isEquip;
        public int price;
        //public bool hasItem  인벤토리 관련 리스트 작성 시 사용 현재 보류
        Player player;

        public Item() { }
       
        public Item(string newName, int newEffect, ItemType newType, bool newIsEquip, int newPrice)
        {
            name = newName;
            effect = newEffect;
            type = newType;
            isEquip = newIsEquip;
            price = newPrice;
        }

        public static List<Item> items { get; } = new List<Item>
        {
            new Item("무기", 5, ItemType.Weapon, false, 0),
            new Item("방어구", 5, ItemType.Armor, false, 0),
            new Item("포션", 5, ItemType.Potion, false, 0),
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

        public static Item AddItem(int index)
        {
            Item addItem = items[index];
            return new Item
            {
                name = addItem.name,
                effect = addItem.effect,
                type = addItem.type,
                isEquip = addItem.isEquip
            };
        }
    }
}
