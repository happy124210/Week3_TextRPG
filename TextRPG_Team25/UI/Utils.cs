namespace TextRPG_Team25.UI
{
    internal class Utils
    {
        // 글자 색 바꾸기 ex) Utils.ColoredText("메인 메뉴\n", ConsoleColor.DarkRed);
        public static void ColoredText(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }

        // 입력 메뉴 스타일 ex) Utils.MenuOption("0", "게임 종료");
        public static void MenuOption(string number, string label, ConsoleColor color = ConsoleColor.Yellow)
        {
            Console.ForegroundColor = color;
            Console.Write($"[{number}] ");
            Console.ResetColor();
            Console.WriteLine(label);
        }

        // 한 글자씩 출력
        public static void TypeEffect(string text, ConsoleColor color = ConsoleColor.White, int delay = 50)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
            Console.WriteLine();
            Console.ReadKey();
            Console.ResetColor();
        }

        //아이템 리스트 출력 함수 ex) utils.PrintItems(shopItems, showIndex, showequipped, showPrice, showSellPrice);
        public void PrintItems(
                List<Item> items,
                bool showIndex = false, // index표시
                bool showEquip = false, // (E) 표시
                bool showPrice = false, // 아이템 가격 표시
                bool showSellPrice = false) // 판매 가격 표시

        {
            int displayIndex = 1;

            foreach (var item in items)
            {
                string prefix = showIndex ? $"[{displayIndex++}]" : "";
                string priceLabel = showPrice ? $" 구매가격 | {item.price}원  |" : "";
                string sellPriceLabel = showSellPrice ? $" 판매가격 | {(int)(item.price * 0.85f)}원 " : "";
                string statLabel = item.type == ItemType.Weapon ? "공격력" : "방어력";
                string equipped = showEquip && item.isEquip ? "(E) " : "";

                Console.WriteLine($"{prefix} {equipped}{item.name}{priceLabel}{sellPriceLabel}| ({statLabel} +{item.effect})");
            }
        }
    }
}
