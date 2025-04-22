using System;
using TextRPG_Team25;
namespace TextRPG_Team25.Core
{
	public class Monster
	{
		public string name;
		public int level;
		public int hp;
		public int attack;
		public bool isLive;

		public Player player;


        public Monster[] monsters = new Monster[3];
		public List<Monster> FieldMonster = new List<Monster>(); 
		
		public void SetMonster()
		{
			for(int i = 0; i < monsters.Length; i++)
			{
				monsters[i] = new Monster();
			}
			monsters[0].name = "미니언";
			monsters[0].level = 2;
			monsters[0].hp = 15;
			monsters[0].attack = 5;
            monsters[0].isLive = true;

            monsters[1].name = "공허충";
            monsters[1].level = 3;
            monsters[1].hp = 10;
            monsters[1].attack = 9;
            monsters[1].isLive = true;

            monsters[2].name = "대포미니언";
            monsters[2].level = 25;
            monsters[2].hp = 25;
            monsters[2].attack = 8;
            monsters[2].isLive = true;

        }

        /* battle 클래스에 해당 내용 추가
		public void RanMonster(int spawnNum)
		{
            Random rand = new Random();
			for(int i = 0; i < spawnNum; i++)
			{
                int num = rand.Next(0, 3);
				FieldMonster.Add(monsters[num]);
            }
        }
		public void SpawnMonster()
		{
			Random rand = new Random();
			int spawnNum = rand.Next(1, 4);
			RanMonster(spawnNum);
		}

		public void MonsterUI()
		{
			for(int i = 0; i < FieldMonster.Count; i++)
			{
                Console.WriteLine($"Lv.{FieldMonster[i].level}  {FieldMonster[i].name}  HP  {FieldMonster[i].hp}");
            }
            Console.WriteLine("");
            Console.WriteLine("[내정보]");
			Console.WriteLine($"Lv.{player.lv}  {player.name} ({player.job})");
			Console.WriteLine($"HP {player.maxHp}/{player.hp}\n");
			Console.WriteLine("0. 취소\n");
			Console.WriteLine("대상을 선택해주세요.\n");
			Console.WriteLine(">>");
		}
		*/

		public void Attack(Player player) // 몬스터 공격 메소드
		{
			if(!isLive)
			{
				return;
			}
			player.hp -= attack;
			if(player.hp <= 0)
			{
				//배틀종료
			}
		}
		public void TakeDamage(int dmg)
		{
            if (!isLive)
            {
                return;
            }
			hp -= dmg;
			if(hp <= 0)
			{
				hp = 0;
				isLive = true;
			}
        }
    }
}
