using TextRPG_Team25.UI;
using TextRPG_Team25.Core;

namespace TextRPG_Team25
{
    internal class Battle(Player player)
    {
        Utils utils = new Utils();
        Monster monster = new Monster();

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

        private void SpawnEnemy() // 몬스터 소환
        {
            Random rand = new Random();
            int spawnNum = rand.Next(1, 4);
            RanMonsterSpawn(spawnNum);
        }

        private void RanMonsterSpawn(int spawnNum) // 랜덤 몬스터 소환
        {
            Random rand = new Random();
            for (int i = 0; i < spawnNum; i++)
            {
                int num = rand.Next(0, 3);
                monster.FieldMonster.Add(monster[num]);
            }
        }

        public void StartBattle() // 배틀 시작
        {
            SpawnEnemy();
            for(int i = 0; i < monster.FieldMonster.Count; i++)
            {
                Console.WriteLine($"Lv.{monster.FieldMonster[i].level}  {monster.FieldMonster[i].name}  HP  {monster.FieldMonster[i].hp}");
            }

            Console.WriteLine("");
            Console.WriteLine("[내정보]");
            Console.WriteLine($"Lv.{player.level}  {player.name} ({player.job})");
            Console.WriteLine($"HP {player.maxHp}/{player.hp}\n");
            Console.WriteLine("0. 취소\n");
            Console.WriteLine("대상을 선택해주세요.\n");
            Console.WriteLine(">>");
        }

        public void SelectTarget() // 타겟 몬스터 지정
        {
            string input = Console.ReadLine();
            int num = int.Parse(input);
            if(num > monster.FieldMonster.count || num < 0 || !monster.FieldMonster[num].isLive)
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
        }

    }
}
