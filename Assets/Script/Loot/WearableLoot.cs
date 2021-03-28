public class WearableLoot : Loot
{
    public ItemData.Wearable wearable;
    public override void OnPickUpLoot(Player player)
    {
        base.OnPickUpLoot(player);
        player.playerRuntimeData.playerStock.Pick(new ItemData.WearableRuntimeData(wearable));
        string info = "Pick up " + string.Join(" ", wearable.ToString().Split('_'));
        UIEventListener.Instance.OnInfomationChange(new UIEventListener.InfomationChangeData(info));
    }
}
