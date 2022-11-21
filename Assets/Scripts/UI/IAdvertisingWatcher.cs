namespace Assets.Scripts.UI
{
    public interface IAdvertisingWatcher
    {
        public int AmountWatched { get; }
        public void SetAmountWatched(int advertisingAmount);
    }
}
