public class ConsumableLoot : Loot
{
    public ItemData.Consumable consumable;
    public int amount;
    public override void OnPickUpLoot(Player player){
        base.OnPickUpLoot(player);
        player.playerRuntimeData.playerStock.Pick(new ItemData.ConsumableRuntimeData(consumable, amount));
        string info = "Pick up " + string.Join(" ", consumable.ToString().Split('_') + " Amount " + amount);
        UIEventListener.Instance.OnInfomationChange(new UIEventListener.InfomationChangeData(info));
    }
}
