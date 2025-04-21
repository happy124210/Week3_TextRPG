using System.Numerics;
using System.Threading;

namespace BattleMonsterAttack;
  public class MonsterAttack
  {
        int battleHp = new Player.PlayerHP //전투에서쓰는 HP따로 선언

    	for(int battleEnermy = 0; battleEnermy <= EnermyList.Count; battleEnermy ++)  //랜덤으로 생성된 적 개체수만큼 실행
		{
		
		int randomDmg = new Random().Next(1, 11) //적 공격력의 오차값 10퍼센트
		
		if (randomDmg == 1) {if (randomDmg == 1) { double i = Monster.attack; i = i* 90 / 100; double enermyDmg = Math.Round(i);}     //10퍼센트 확률로 90퍼센트의 데미지 반올림
        else if (randomDmg == 10) {if (randomDmg == 1) { double i = Monster.attack; i = i* 90 / 100; double enermyDmg = Math.Round(i);} //10퍼센트의 확률로 110퍼센트의 데미지 반올림

        (EnermyList[i].EnermyDead == true) { randomDmg = 0 }

        Console.WriteLine($"Lv.{Monster.level} {Monster.name} 의 공격!");
        Console.WriteLine($"Lv.{Player.level} {Player.name}을(를) 맞췄습니다. [데미지 : {enermyDmg}]");
        Console.WriteLine();
        Console.WriteLine($"Lv.{Player.level} {Player.name}")
        Console.WriteLine($"HP {battleHP} -> {battleHP - enermyDmg}") // 플레이어가 받은피해와 플레이어 체력 출력

        battleHP -= enermyDmg //위의 랜덤공격력으로 플레이어 HP감소

        if (battleHP <= 0) { battleEnermy = EnermyList.Count + 1 } //플레이어의 전투HP가 0이되면 for문에서 빠져나오기
		}
}