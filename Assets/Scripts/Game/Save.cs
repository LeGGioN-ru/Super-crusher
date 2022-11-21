using System;

[Serializable]
public class Save
{
    public int Power;
    public int Energy;
    public long ItemIncome;
    public int ItemDurability;
    public int AmountRepeatsItems;
    public long Money;
    public int[] SkinsAdvertisingWatched;
    public bool IsEducatuionDone;

    public Save(int durability, long itemMoney, int power, int energy, long money, int[] skinsAdvertisingWatched, int amountRepeats, bool isEducatuionDone)
    {
        Power = power;
        Energy = energy;
        ItemIncome = itemMoney;
        ItemDurability = durability;
        AmountRepeatsItems = amountRepeats;
        Money = money;
        SkinsAdvertisingWatched = skinsAdvertisingWatched;
        IsEducatuionDone = isEducatuionDone;
    }
}

