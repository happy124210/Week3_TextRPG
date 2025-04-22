using TextRPG_Team25.UI;
using TextRPG_Team25.Core;

namespace TextRPG_Team25
{
    internal class Battle(Player player)
    {
        Utils utils = new Utils();

        private void PlayerAttack(Monster selected)
        {
            int baseAttack = player.attack;
            int currentHp = selected.hp;
            int offset = (int)Math.Ceiling(baseAttack * 0.1f);
            int damage = new Random().Next(baseAttack - offset, baseAttack + offset + 1);

            selected.hp -= damage;

            Console.WriteLine($"{player.name}의 공격!");
            Console.WriteLine($"{selected.name}을 맞췄습니다. [데미지 : {damage}]");
            Console.Write($" Lv.");
            Utils.ColoredText($"{selected.level} ", ConsoleColor.Magenta);
            Console.WriteLine($"{selected.name}");

            if (selected.hp <= 0) // 몬스터 안 죽었을 때
            {

                // 메세지 출력
                         
                Console.WriteLine($"HP "); 
                Utils.ColoredText($"{currentHp} ", ConsoleColor.Green);
                Console.WriteLine($"-> Dead");

                // 텍스트 회색으로 처리 코드 추가
                
            }

            else
            {
                Console.WriteLine($"HP {currentHp}-> HP {selected.HP}");
            }

            
        }
    }
}
