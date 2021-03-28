public class WeaponLoot : Loot
{
    public ItemData.Weapon weapon;
    public int level;
    public override void OnPickUpLoot(Player player)
    {
        base.OnPickUpLoot(player);
        player.playerRuntimeData.playerStock.Pick(new ItemData.WeaponRuntimeData(weapon, level));
        string info = "Pick up " + string.Join(" ", weapon.ToString().Split('_')) + (level>0?" level "+level:"");
        UIEventListener.Instance.OnInfomationChange(new UIEventListener.InfomationChangeData(info));
    }
}
