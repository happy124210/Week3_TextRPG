namespace TextRPG_Team25.Quest
{
    public class QuestManager
    {
        public List<Quest> questList = new List<Quest>();

        public void InitQuests()
        {
            Quest quest1 = new Quest();
            quest1.title = "마을을 위협하는 미니언 처치";
            quest1.description = "마을 근처에 미니언들이 너무 많아졌다고 생각하지 않나??\n마을 주민들의 안전을 위해서라도 저것들 수를 좀 줄여야 한다고!\n자네가 좀 처치해주게!";
            quest1.goalCount = 5;
            quest1.rewardItemId = "doranSword";
            quest1.rewardGold = 5;

            Quest quest2 = new Quest();
            quest2.title = "장비를 장착해보자";
            quest2.description = "강해지려면 장비가 필수지! 한 번 장착해보게!";
            quest2.goalCount = 1;
            quest2.rewardItemId = "doranShield";
            quest2.rewardGold = 3;

            Quest quest3 = new Quest();
            quest3.title = "더욱 더 강해지기";
            quest3.description = "자네라면 더 강해질 수 있을 것 같군!\n한 단계 더 성장해보게!";
            quest3.goalCount = 10;
            quest3.rewardItemId = "healthPotion";
            quest3.rewardGold = 20;

            questList.Add(quest1);
            questList.Add(quest2); 
            questList.Add(quest3);
        }
    }
}
