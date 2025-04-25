using TextRPG_Team25.BattleSystem;
using TextRPG_Team25.Core;

namespace TextRPG_Team25.BattleSystem
{
    internal class DecisiveStrike : Skill
    {
        public DecisiveStrike() 
            : base("결정타", 10, 3, "공격력의 두 배로 하나의 적을 공격합니다.") { }

        public override void ActivateEffect(Player user, Monster target)
        {
            int damage = user.attack * 2;
            target.TakeDamage(damage);
        }
    }

    internal class Courage : Skill
    {
        private int buffAmount = 5;
        private int duration = 2;

        public Courage() : base("용기", 5, 4, "2턴 동안 방어력이 5 증가합니다.") { }

        public override void ActivateEffect(Player user, Monster target)
        {
            user.AddTemporaryDefense(buffAmount, duration);

            Console.WriteLine($"방어력이 {duration}턴 동안 +{buffAmount} 증가합니다.");
        }
    }


    internal class Disintegrate : Skill
    {
        public Disintegrate() : base("붕괴", 12, 3, "적 하나에게 강한 화염 피해를 주고 화상을 입힙니다.") { }

        public override void ActivateEffect(Player user, Monster target)
        {
            int damage = user.attack + 5;
            target.TakeDamage(damage);
            target.burnTurn = 3;

            Console.WriteLine($"[즉시 피해: {damage}], 🔥 화상 5");
        }
    }


    internal class MoltenShield : Skill
    {
        private int buffAmount = 5;
        private int duration = 2;

        public MoltenShield() : base("용암 방패", 8, 4, "2턴 동안 방어력이 5 증가하고, 대상에게 화상을 입힙니다.") { }
        public override void ActivateEffect(Player user, Monster target)
        {
            // 자기 자신에게 방어력 증가
            user.AddTemporaryDefense(buffAmount, duration);
            Console.WriteLine($"방어력이 {duration}턴 동안 +{buffAmount} 증가합니다.");

            // 대상 몬스터에게 화상 부여
            target.ApplyStatusEffect(StatusEffect.Burn, burnDuration);
            Console.WriteLine($"{target.name}이(가) 뜨거운 방패의 여열에 화상을 입었습니다! 🔥 (3턴)");
        }
    }

}