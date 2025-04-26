namespace TextRPG_Team25.BattleSystem
{
    public class Monster
    {
        public string name;
        public int level;
        public int hp;
        public int attack;
        public bool isDead = false;

        public Dictionary<StatusEffect, int> statusEffects = new Dictionary<StatusEffect, int>();

        public static List<(Monster monster, int probability)> monsterPool = new List<(Monster, int)>
        {
            (new Monster { name = "미니언", level = 2, hp = 50, attack = 5 }, 30),
            (new Monster { name = "공허충", level = 3, hp = 45, attack = 8 }, 25),
            (new Monster { name = "대포미니언", level = 5, hp = 80, attack = 10 }, 25),
            (new Monster { name = "바론 나셔", level = 10, hp = 500, attack = 50 }, 5),
            (new Monster { name = "드래곤", level = 7, hp = 300, attack = 35 }, 5),
            (new Monster { name = "협곡의 전령", level = 8, hp = 400, attack = 40 }, 3),
            (new Monster { name = "붉은 덩굴 정령", level = 5, hp = 200, attack = 20 }, 4),
            (new Monster { name = "푸른 골렘", level = 5, hp = 220, attack = 18 }, 3),
        };

        // 확률 기반 몬스터 생성
        public static Monster GenerateRandomMonster()
        {
            Random rand = new Random();
            int totalProbability = monsterPool.Sum(m => m.probability);
            int roll = rand.Next(1, totalProbability + 1); 

            int cumulative = 0;
            foreach (var (monster, probability) in monsterPool)
            {
                cumulative += probability;
                if (roll <= cumulative)
                {
                    return new Monster
                    {
                        name = monster.name,
                        level = monster.level,
                        hp = monster.hp,
                        attack = monster.attack,
                    };
                }
            }

            return monsterPool.Last().monster; // 예외
        }

        // 데미지 받기
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

        // 상태이상 턴 가져오기
        public int GetStatusTurns(StatusEffect effect)
        {
            return statusEffects.ContainsKey(effect) ? statusEffects[effect] : 0;
        }

        // 턴 끝난 후 처리
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

        // 상태이상 턴 적용
        public void ApplyStatusEffect(StatusEffect effect, int turns)
        {
            statusEffects[effect] = turns;
        }
    }
}