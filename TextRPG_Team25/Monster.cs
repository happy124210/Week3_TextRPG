using System.Runtime.CompilerServices;

namespace TextRPG_Team25
{
    internal class Monster
    {
        public string name;
        public int level;
        public int hp;
        public int attack;
        public bool isDead;

        public static List<Monster> monsters { get; } = new List<Monster>
        {
             new Monster
            {
                name = "미니언",
                level = 2,
                hp = 15,
                attack = 5,
                isDead = false
            },
            new Monster
            {
                name = "공허충",
                level = 3,
                hp = 10,
                attack = 9,
                isDead = false
            },
            new Monster
            {
                name = "대포미니언",
                level = 5,
                hp = 25,
                attack = 8,
                isDead = false
            }
        };


        public static Monster GenerateRandomMonster()
        {
            var random = new Random();
            int index = random.Next(monsters.Count);
            Monster original = monsters[index];

            // 새 객체 생성
            return new Monster
            {
                name = original.name,
                level = original.level,
                hp = original.hp,
                attack = original.attack,
                isDead = false
            };
        }


        public void TakeDamage(int damage)
        {
            hp -= damage;
        }
    }
}