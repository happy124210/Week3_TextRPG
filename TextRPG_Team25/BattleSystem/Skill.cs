using TextRPG_Team25.Core;
using TextRPG_Team25.UI;

namespace TextRPG_Team25.BattleSystem
{
    public abstract class Skill
    {
        public string name;
        public int manaCost;
        public int cooldown;
        public string description;
        public int currentCooldown = 0;

        public Skill(string name, int manaCost, int cooldown, string description)
        {
            this.name = name;
            this.manaCost = manaCost;
            this.cooldown = cooldown;
            this.description = description;
        }

        public abstract int ActivateEffect(Player user, Monster target);

        // 스킬 사용 가능 여부 체크
        public int TryActivate(Player user, Monster target)
        {
            if (currentCooldown > 0)
            {
                Console.WriteLine("아직 사용할 수 없습니다.");
                return 0;
            }

            if (user.mana < manaCost)
            {
                Utils.ColoredText("마나가 부족합니다.", ConsoleColor.DarkRed);
                return 0;
            }

            user.mana -= manaCost;
            currentCooldown = cooldown;

            return ActivateEffect(user, target);
        }


        public void ReduceCooldown()
        {
            if (currentCooldown > 0)
                currentCooldown -= 1;
        }
    }
}