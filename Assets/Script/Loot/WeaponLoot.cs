public class WeaponLoot : Loot
{
    public ItemData.Weapon weapon;
    public int level;
    public override void OnPickUpLoot(Player player)
    {
        base.OnPickUpLoot(player);
        OnPickUpLoot(player, weapon, level);
    }
}
