using TextRPG_Team25.Core;
using TextRPG_Team25.UI;

namespace TextRPG_Team25.ItemSystem
{

    public class Shop()
    {
        private List<Item> shopItems = Item.items.Where(i => !i.isPurchase).ToList();
        public Utils utils = new Utils();

        public void ShowShop(Player buyer)
        {
            while (true)
            {
                Console.Clear();
                Utils.ColoredText("[ 상점 ]\n\n", ConsoleColor.DarkCyan);
                utils.PrintItems(Item.items, false, false, true, true);
                Console.WriteLine();
                Utils.MenuOption("1", "아이템 구매하기");
                Utils.MenuOption("2", "아이템 판매하기");
                Console.WriteLine();
                Utils.MenuOption("0", "상점 나가기");
                Console.Write("\n>> ");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        BuyItem(buyer);
                        break;
                    case "2":
                        SellItem(buyer);
                        break;
                    case "0":
                        Console.WriteLine("상점을 떠납니다.");
                        Console.ReadKey();
                        return;
                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        Console.ReadKey();
                        break;
                }

                Console.ReadKey();
            }
            
        }


        public void BuyItem(Player buyer)
        {
            Console.Clear();
            Utils.ColoredText("[ 구매하기 ]\n\n", ConsoleColor.DarkCyan);
            utils.PrintItems(shopItems, true, false, true, false);
            Console.WriteLine();
            Console.Write("구매할 아이템 번호를 입력하세요.\n>> ");

            string input = Console.ReadLine();
            if (!int.TryParse(input, out int number) || number < 1 || number > shopItems.Count)
            {
                Console.WriteLine("잘못된 입력입니다.");
                Console.ReadKey();
                return;
            }

            Item itemToBuy = shopItems[number - 1];

            if (buyer.gold < itemToBuy.price)
            {
                Console.WriteLine("골드가 부족합니다.");
                Console.ReadKey();
                return;
            }

            buyer.gold -= itemToBuy.price;
            buyer.AddInventory(itemToBuy.id);
            itemToBuy.isPurchase = true;

            Console.WriteLine($"{itemToBuy.name}을(를) 구매했습니다!");
            Console.ReadKey();
        }


        public void SellItem(Player buyer)
        {
            Console.Clear();
            Utils.ColoredText("[ 판매하기 ]\n\n", ConsoleColor.DarkCyan);

            if (buyer.inventory.Count == 0)
            {
                Console.WriteLine("판매할 아이템이 없습니다.");
                Console.ReadKey();
                return;
            }

            Utils.ColoredText("[ 내 인벤토리 ]\n", ConsoleColor.Yellow);
            utils.PrintItems(buyer.inventory, true, false, false, true);
            Console.WriteLine();
            Console.Write("판매할 아이템 번호를 입력하세요.\n>> ");

            string input = Console.ReadLine();
            if (!int.TryParse(input, out int number) || number < 1 || number > buyer.inventory.Count)
            {
                Console.WriteLine("잘못된 입력입니다.");
                Console.ReadKey();
                return;
            }

            Item itemToSell = buyer.inventory[number - 1];
            int sellPrice = (int)(itemToSell.price * 0.85f);

            buyer.gold += sellPrice;
            buyer.inventory.Remove(itemToSell);

            Console.WriteLine($"{itemToSell.name}을(를) 판매했습니다! (+{sellPrice} G)");
            Console.ReadKey();
        }
    }
}