using System.ComponentModel.Design;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace TextRPG_Team25
{
    internal class Battle(MonsterAttack monsterAttack)
    {
        public double BattleHP { get; set; } 
        public void Battlehp(double battlehp)
        {
            battlehp = player.hp;
        }
        public void MonsterAttackWindow()
        {
            Console.WriteLine($"Lv.{player.level} {player.name}을(를) 맞췄습니다.");
            Console.WriteLine();
            Console.WriteLine($"Lv.{player.level} {player.name}");
            Console.Write($"HP {BattleHP} -> "); // 플레이어가 받은피해와 플레이어 체력 출력
        }
        public void MonsterAttack()
        {
            
            double monsterDmg;

            for (int battleMonster = 0; battleMonster <= FieldMonster.Count; battleMonster++)  //랜덤으로 생성된 적 개체수만큼 실행
            {
                int randomDmg = new Random().Next(1, 101);

                if (monster.hp <= 0) { randomDmg = 0; } //필드 몬스터의 체력이 0이면 데미지 0으로 설정

                    else
                    {
                        if (randomDmg <= 10) 
                        { 
                        double i = FieldMonster[battleMonster].attack; i = i * 90 / 100; monsterDmg = Math.Round(i);  //10퍼센트 확률로 90퍼센트의 데미지 반올림
                        Console.WriteLine($"Lv.{FieldMonster[battleMonster].level} {FieldMonster[battleMonster].name} 의 공격!");
                        MonsterAttackWindow();
                        Console.WriteLine(BattleHP -monsterDmg);
                        Console.WriteLine($"받은 피해 : {monsterDmg}");// 플레이어가 받은피해와 플레이어 체력 출력;
                        BattleHP -= monsterDmg; //위의 랜덤공격력으로 플레이어 HP감소
                       }
                    else if (randomDmg >= 90) 
                        { 
                        double i = FieldMonster[battleMonster].attack; i = i * 90 / 100; monsterDmg = Math.Round(i);  //10퍼센트의 확률로 110퍼센트의 데미지 반올림
                        Console.WriteLine($"Lv.{FieldMonster[battleMonster].level} {FieldMonster[battleMonster].name} 의 공격!");
                        MonsterAttackWindow();
                        Console.WriteLine(BattleHP - monsterDmg);
                        Console.WriteLine($"받은 피해 : {monsterDmg}");// 플레이어가 받은피해와 플레이어 체력 출력
                        BattleHP -= monsterDmg; //위의 랜덤공격력으로 플레이어 HP감소
                    }
                    else
                        {
                        double i = FieldMonster[battleMonster].attack = monsterDmg;
                        Console.WriteLine($"Lv.{FieldMonster[battleMonster].level} {FieldMonster[battleMonster].name} 의 공격!");
                        MonsterAttackWindow();
                        Console.WriteLine(BattleHP - monsterDmg);
                        Console.WriteLine($"받은 피해 : {monsterDmg}");// 플레이어가 받은피해와 플레이어 체력 출력
                        BattleHP -= monsterDmg; //위의 랜덤공격력으로 플레이어 HP감소
                        }
                    }
            if (BattleHP <= 0) { battleMonster = FieldMonster[battleMonster].Count + 1; } //플레이어의 전투HP가 0이되면 for문에서 빠져나가기
            }
        }

    }
}