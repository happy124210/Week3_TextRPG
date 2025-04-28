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
                Utils.ColoredText("아직 사용할 수 없습니다.", ConsoleColor.Red);
                Console.Write("남은 쿨타임: ");
                Utils.ColoredText($"{currentCooldown}", ConsoleColor.Cyan);
                Console.WriteLine("턴\n");
                return -1;
            }

            if (user.mana < manaCost)
            {
                Utils.ColoredText("아직 사용할 수 없습니다.", ConsoleColor.Red);
                Console.Write("필요 마나: ");
                Utils.ColoredText($"{manaCost}\n\n", ConsoleColor.Cyan);
                return -1;
            }

            user.mana -= manaCost;
            currentCooldown = cooldown;

            return ActivateEffect(user, target);
        }

        // 스킬 쿨타임 줄이기
        public void ReduceCooldown()
        {
            if (currentCooldown > 0)
                currentCooldown -= 1;
        }
    }
}