﻿using TextRPG_Team25.Core;

namespace TextRPG_Team25.BattleSystem
{
    // 검투사 첫 번째 스킬
    public class DecisiveStrike : Skill
    {
        public DecisiveStrike() : base("결정타", 12, 3, "공격력의 두 배로 하나의 적을 공격합니다.") { }

        public override int ActivateEffect(Player user, Monster target)
        {
            int damage = (int)(user.attack * 1.7f);
            target.TakeDamage(damage);
            return damage;
        }
    }

    // 검투사 두 번째 스킬
    public class Courage : Skill
    {
        private int buffAmount = 5;
        private int duration = 2;

        public Courage() : base("용기", 8, 4, "2턴 동안 방어력이 5 증가합니다.") { }

        public override int ActivateEffect(Player user, Monster target)
        {
            user.AddTemporaryDefense(buffAmount, duration);
            return 0; // 방어력 버프만, 직접 데미지는 없음
        }
    }

    // 화염술사 첫 번째 스킬
    public class Disintegrate : Skill
    {
        private int burnDuration = 3;

        public Disintegrate() : base("붕괴", 15, 3, $"대상에게 공격력 + 3의 피해를 주고 🔥화상을 입힙니다.") { }

        public override int ActivateEffect(Player user, Monster target)
        {
            int damage = user.attack + 3;
            target.TakeDamage(damage);
            target.ApplyStatusEffect(StatusEffect.Burn, burnDuration);
            return damage;
        }
    }

    // 화염술사 두 번째 스킬
    public class MoltenShield : Skill
    {
        private int buffAmount = 5;
        private int duration = 2;
        private int burnDuration = 3;

        public MoltenShield() : base("용암 방패", 10, 4, "2턴 동안 방어력이 5 증가하고, 대상에게 🔥화상을 입힙니다.") { }

        public override int ActivateEffect(Player user, Monster target)
        {
            user.AddTemporaryDefense(buffAmount, duration);
            target.ApplyStatusEffect(StatusEffect.Burn, burnDuration);
            return 0; // 직접 데미지 없음, 버프와 상태이상만 부여
        }
    }

    // 얼음술사 첫 번째 스킬
    public class IceShard : Skill
    {
        public IceShard() : base("얼음 파편", 12, 3, "대상에게 공격력 + 3의 피해를 주고, 1턴 동안 ❄️빙결 상태로 만듭니다.") { }

        public override int ActivateEffect(Player user, Monster target)
        {
            int damage = user.attack + 3;
            target.TakeDamage(damage);
            target.ApplyStatusEffect(StatusEffect.Freeze, 1);
            return damage;
        }
    }
    // 얼음술사 두 번째 스킬
    public class FrostArrow : Skill
    {
        public FrostArrow() : base("서리 화살", 18, 5, "대상에게 공격력 + 5의 피해를 주고, 2턴 동안 ❄️빙결 상태로 만듭니다.") { }

        public override int ActivateEffect(Player user, Monster target)
        {
            int damage = user.attack + 5;
            target.TakeDamage(damage);
            target.ApplyStatusEffect(StatusEffect.Freeze, 2);
            return damage;
        }
    }
}
