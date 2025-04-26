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

        // 스킬 사용 가능 여부 체크
        public bool TryActivate(Player user, Monster target)
        {
            if (currentCooldown > 0)
            {
                Console.WriteLine("아직 사용할 수 없습니다.");
                return false;
            }

            if (user.mana < manaCost)
            {
                Utils.ColoredText("마나가 부족합니다.", ConsoleColor.DarkRed);
                return false;
            }

            user.mana -= manaCost;
            currentCooldown = cooldown;

            ActivateEffect(user, target);
            return true;
        }


        public void ReduceCooldown()
        {
            if (currentCooldown > 0)
                currentCooldown -= 1;
        }


        public abstract void ActivateEffect(Player user, Monster target);
    }
}