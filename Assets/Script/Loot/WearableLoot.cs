public class WearableLoot : Loot
{
    public ItemData.Wearable wearable;
    public override void OnPickUpLoot(Player player)
    {
        base.OnPickUpLoot(player);
        OnPickUpLoot(player, wearable);
    }
}
