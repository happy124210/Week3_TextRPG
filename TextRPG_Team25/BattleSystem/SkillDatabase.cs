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
}