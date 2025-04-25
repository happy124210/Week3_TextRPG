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
        public int itemPrice;
        //public bool hasItem  인벤토리 관련 리스트 작성 시 사용 현재 보류
        Player player;

        public Item() { }
        public Item(string newName, int newEffect, ItemType newType, bool newIsEquip)
        {
            name = newName;
            effect = newEffect;
            type = newType;
            isEquip = newIsEquip;
        }

        public Item(string newName, int newEffect, ItemType newType, bool newIsEquip, int newPrice)
        {
            name = newName;
            effect = newEffect;
            type = newType;
            isEquip = newIsEquip;
            itemPrice = newPrice;
        }

        public static List<Item> items { get; } = new List<Item>
        {
            new Item("무기", 5, ItemType.Weapon, false),
            new Item("방어구", 5, ItemType.Armor, false),
            new Item("포션", 5, ItemType.Potion, false),
        };

        /*public void EquipmentItem()
        {
            switch (type)
            {
                case ItemType.Weapon:   //플레이어 공격력 증가                  
                    if (!isEquip)
                    {
                        player.attack += effect;
                        isEquip = true;
                    }
                    else
                    {
                        player.attack -= effect;
                        isEquip = false;
                    }
                    break;
                case ItemType.Armor:  //플레이어 방어력 증가
                    if (!isEquip)
                    {
                        player.defense += effect;
                        isEquip = true;
                    }
                    else
                    {
                        player.defense -= effect;
                        isEquip = false;
                    }
                    break;
                case ItemType.Potion:   //플레이어 체력 증가
                    player.hp += effect;
                    break;
            }
        }*/
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
