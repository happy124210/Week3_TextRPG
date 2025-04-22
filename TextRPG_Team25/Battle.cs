using System.ComponentModel.Design;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace TextRPG_Team25
{
    internal class Battle(Player player)
    {
        public void MonsterAttack(Player player, List<Monster> FieldMonster)
        {
            Random rand = new Random();

            foreach (Monster monster in FieldMonster)
            {
                if (monster.isDead) continue; //몬스터의 is Dead의 bool값이 활성화 되있으면 해당 과정은 넘기기

                //기본공격 10% 랜덤 오차계산
                int offset = (int)Math.Ceiling(monster.attack * 0.1f);
                int damage = rand.Next(monster.attack - offset, monster.attack + offset + 1);

                //로그
                Console.WriteLine($"\n Lv.{monster.level} {monster.name} 의 공격!");
                Console.WriteLine($"{player.name} 을(를) 맞췄습니다. [데미지 : {damage}]");
                Console.WriteLine();
                Console.WriteLine($"Lv.{player.level} {player.name}");
                Console.WriteLine($"HP {player.hp} -> {Math.Max(player.hp - damage, 0)}"); //Math.Max로 더 큰 값 출력 player의 HP가 0아래로 떨어졌을때 0을 띄우기 위함
                Console.WriteLine();

                //체력 감소
                player.hp -= damage;
                if (player.hp == 0) { Console.WriteLine($"{player.name} 이(가) 쓰러졌습니다......"); break; } //플레이어 패배

            }
            Console.WriteLine("진행 하시려면 아무숫자나 입력해주세요");
            Console.WriteLine();
            Console.WriteLine("대상을 선택해 주세요.");
            Console.ReadLine();
        }
    }
}