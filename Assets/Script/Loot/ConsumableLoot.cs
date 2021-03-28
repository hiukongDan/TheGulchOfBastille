public class ConsumableLoot : Loot
{
    public ItemData.Consumable consumable;
    public int amount;
    public override void OnPickUpLoot(Player player){
        base.OnPickUpLoot(player);
        OnPickUpLoot(player, consumable, amount);
    }

}
