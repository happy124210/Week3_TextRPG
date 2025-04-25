using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team25
{
    internal class Shop
    {
        public static List<Item> shopItems { get; } = new List<Item>
        {
            new Item("판매용 무기", 3, ItemType.Weapon, false, 300),
            new Item("판매용 방어구", 3, ItemType.Weapon, false, 300),
        };


    }
}
