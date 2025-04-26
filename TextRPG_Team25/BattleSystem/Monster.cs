namespace TextRPG_Team25.BattleSystem
{
    public class Monster
    {
        

        public string name;
        public int level;
        public int hp;
        public int attack;
        public bool isDead;

        public Dictionary<StatusEffect, int> statusEffects = new Dictionary<StatusEffect, int>();


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

            if (hp <= 0)
            {
                hp = 0;
                isDead = true;
                statusEffects.Clear();
            }
        }

        // 상태이상 부여 
        public bool HasStatus(StatusEffect effect)
        {
            return statusEffects.ContainsKey(effect);
        }


        public int GetStatusTurns(StatusEffect effect)
        {
            return statusEffects.ContainsKey(effect) ? statusEffects[effect] : 0;
        }


        public void OnTurnEnd()
        {
            if (isDead) return;

            List<StatusEffect> expired = new List<StatusEffect>();

            foreach (var effect in statusEffects.Keys.ToList())
            {
                statusEffects[effect]--;

                switch (effect)
                {
                    case StatusEffect.Burn:
                        int burnDamage = 5;
                        TakeDamage(burnDamage);
                        Console.WriteLine($"{name}이(가) 화상으로 {burnDamage}의 피해를 입었습니다! 🔥");
                        break;

                    case StatusEffect.Freeze:
                        Console.WriteLine($"{name}이(가) 얼어붙어 움직임이 둔해졌습니다. ❄️");
                        break;
                }

                if (statusEffects[effect] <= 0)
                {
                    expired.Add(effect);
                }
            }

            foreach (var e in expired)
            {
                statusEffects.Remove(e);
                Console.WriteLine($"{name}의 {e} 상태이상이 해제되었습니다.");
            }
        }

        public void ApplyStatusEffect(StatusEffect effect, int turns)
        {
            statusEffects[effect] = turns;
        }
    }
}