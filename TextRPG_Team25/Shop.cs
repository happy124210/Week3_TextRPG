using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Team25.UI;
using TextRPG_Team25.Core;
using System.Xml.Linq;


namespace TextRPG_Team25
{

    internal class Shop()
    {
        public Utils utils = new Utils();
        public Item item = new Item();
        public List<Item> shopItems { get; } = new List<Item>
        {
            new Item("판매용 무기", 3, ItemType.Weapon, false, 300),
            new Item("판매용 방어구", 3, ItemType.Armor, false, 300),
        };

        public void ShowShop()
        {
            Console.Clear();
            utils.PrintItems(shopItems, true, false, true, true);
            Console.WriteLine();
            Utils.MenuOption("1", "아이템 구매하기");
            Console.WriteLine();
            Utils.MenuOption("2", "아이템 판매하기");
            Console.WriteLine();
            Utils.MenuOption("", "이외의 행동을 하고싶으신 경우 아무키나 눌러주세요");
        }

        public Item AddShopItem(int index)
        {
            Item addItem = shopItems[index];
            return new Item
            {
                name = addItem.name,
                effect = addItem.effect,
                type = addItem.type,
                isEquip = addItem.isEquip,
                price = addItem.price
            };
        }
    }
}
