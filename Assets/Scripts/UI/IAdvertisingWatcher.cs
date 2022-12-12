using System;

namespace Assets.Scripts.UI
{
    public interface IAdvertisingWatcher
    {
        public event Action AdvertisingWatched;

        public int AmountWatched { get; }
        public void SetAmountWatched(int advertisingAmount);
    }
}
