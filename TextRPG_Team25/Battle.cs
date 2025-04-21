namespace TextRPG_Team25
{
    internal class Battle(Player player)
    {
        private void PlayerAttack(Monster selected)
        {
            int baseAttack = player.attack;
            int offset = (int)Math.Ceiling(baseAttack * 0.1f);
            int damage = new Random().Next(baseAttack - offset, baseAttack + offset + 1);

            selected.hp -= damage;

            if (selected.hp <= 0)
            {
                // 텍스트 회색으로 처리
            }
        }
    }
}
