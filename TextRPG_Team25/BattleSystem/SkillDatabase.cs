using TextRPG_Team25.BattleSystem;
using TextRPG_Team25.Core;
using static TextRPG_Team25.Monster;

namespace TextRPG_Team25.BattleSystem
{
    public class DecisiveStrike : Skill
    {
        public DecisiveStrike() 
            : base("결정타", 10, 3, "공격력의 두 배로 하나의 적을 공격합니다.") { }

        public override void ActivateEffect(Player user, Monster target)
        {
            int damage = user.attack * 2;
            target.TakeDamage(damage);
        }
    }

    public class Courage : Skill
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


    public class Disintegrate : Skill
    {
        private int burnDuration = 3;

        public Disintegrate() : base("붕괴", 12, 3, "적 하나에게 강한 화염 피해를 주고 화상을 입힙니다.") { }

        public override void ActivateEffect(Player user, Monster target)
        {
            int damage = user.attack + 5;
            target.TakeDamage(damage);
            target.ApplyStatusEffect(StatusEffect.Burn, burnDuration);

            Console.WriteLine($"[대미지: {damage}], 🔥 화상 5");
        }
    }


    public class MoltenShield : Skill
    {
        private int buffAmount = 5;
        private int duration = 2;
        private int burnDuration = 3;

        public MoltenShield() : base("용암 방패", 8, 4, "2턴 동안 방어력이 5 증가하고, 대상에게 화상을 입힙니다.") { }
       
        public override void ActivateEffect(Player user, Monster target)
        {
            user.AddTemporaryDefense(buffAmount, duration);
            Console.WriteLine($"방어력이 {duration}턴 동안 +{buffAmount} 증가합니다.");

            target.ApplyStatusEffect(StatusEffect.Burn, burnDuration);
            Console.WriteLine($"{target.name} 🔥 화상 5");
        }
    }


    public class IceShard : Skill
    {
        public IceShard() : base("얼음 파편", 10, 3, "적 하나에게 피해를 주고, 1턴 동안 빙결 상태로 만듭니다.") { }

        public override void ActivateEffect(Player user, Monster target)
        {
            int damage = user.attack + 4;
            target.TakeDamage(damage);
            target.ApplyStatusEffect(StatusEffect.Freeze, 1);

            Console.WriteLine($"[대미지: {damage}], ❄️ 빙결 1");
        }
    }


    public class FrostArrow : Skill
    {
        public FrostArrow() : base("서리 화살", 15, 5, "강력한 피해와 함께 2턴 동안 적을 얼립니다.") { }

        public override void ActivateEffect(Player user, Monster target)
        {
            int damage = user.attack + 6;
            target.TakeDamage(damage);
            target.ApplyStatusEffect(StatusEffect.Freeze, 2);

            Console.WriteLine($"{user.name}이(가) {target.name}에게 서리 화살을 발사했습니다! 🧊");
            Console.WriteLine($"[대미지: {damage}], ❄️ 빙결 2");
        }
    }

}