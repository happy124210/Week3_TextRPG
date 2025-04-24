namespace TextRPG_Team25
{
    internal static class ItemDatabase
    {
        private static Dictionary<string, Item> items = new Dictionary<string, Item>
        {
            {
                "doranSword",
                new Item(
                    "doranSword",
                    "도란의 검",
                    10,
                    ItemType.Weapon,
                    "공격력과 체력을 증가시키며, 생명력을 흡수합니다.",
                    450
                )
            },
            {
                "doranShield",
                new Item(
                    "doranShield",
                    "도란의 방패",
                    110,
                    ItemType.Armor,
                    "피해를 입은 후 체력을 회복하며, 미니언에게 추가 피해를 줍니다.",
                    450
                )
            },
            {
                "healthPotion",
                new Item(
                    "healthPotion",
                    "체력 물약",
                    120,
                    ItemType.Potion,
                    "15초 동안 총 120의 체력을 회복합니다.",
                    50
                )
            },
            {
                "longSword",
                new Item(
                    "longSword",
                    "롱소드",
                    10,
                    ItemType.Weapon,
                    "공격력을 증가시키는 기본 아이템입니다.",
                    350
                )
            },
            {
                "chainVest",
                new Item(
                    "chainVest",
                    "사슬 조끼",
                    40,
                    ItemType.Armor,
                    "방어력을 크게 증가시키는 아이템입니다.",
                    800
                )
            },
            {
                "refillablePotion",
                new Item(
                    "refillablePotion",
                    "재사용 물약",
                    100,
                    ItemType.Potion,
                    "충전 가능한 물약으로, 귀환 시 충전됩니다.",
                    150
                )
            },
            {
                "bfSword",
                new Item(
                    "bfSword",
                    "B.F. 대검",
                    40,
                    ItemType.Weapon,
                    "공격력을 크게 증가시키는 고급 아이템입니다.",
                    1300
                )
            },
            {
                "thornmail",
                new Item(
                    "thornmail",
                    "가시 갑옷",
                    75,
                    ItemType.Armor,
                    "공격자를 반격하며, 그들에게 상처를 입힙니다.",
                    2450
                )
            },
            {
                "elixirOfWrath",
                new Item(
                    "elixirOfWrath",
                    "분노의 영약",
                    30,
                    ItemType.Potion,
                    "공격력과 흡혈 효과를 3분간 부여합니다.",
                    500
                )
            },
            {
                "infinityEdge",
                new Item(
                    "infinityEdge",
                    "무한의 대검",
                    65,
                    ItemType.Weapon,
                    "치명타 피해량을 증가시키는 전설의 무기입니다.",
                    3400
                )
            }
        };

        public static Item GetItemById(string id) // id로 검색 가능
        {
            if (items.ContainsKey(id))
            {
                return new Item(items[id]); // 복사본 반환
            }

            else
            {
                Console.WriteLine($"존재하지 않는 아이템 ID: {id}");
                return null;
            }
        }

        public static void ShowAllItems()
        {
            foreach (Item item in items.Values)
            {
                int index = 1;
                Console.WriteLine($"[{index}] {item.name} (ID: {item.id})");
            }
        }
    }
}
