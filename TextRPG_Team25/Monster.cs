namespace TextRPG_Team25
{
    internal class Monster
    {
        public string name;
        public int level;
        public int hp;
        public int attack;
        public bool isLive;

        public List<Monster> monsters = new List<Monster>();
        public List<Monster> fieldMonsters = new List<Monster>();
    }
}
