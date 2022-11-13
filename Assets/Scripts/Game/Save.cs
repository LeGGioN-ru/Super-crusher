using System;

[Serializable]
public class Save
{
    public int Power;
    public int Energy;
    public long ItemMoney;
    public int ItemDurability;
    public int CurrentItemIndex;
    public int AmountRepeatItems;
    public long Money;
    public int[] SkinsAdvertisingWatched;

    public Save(int durability, long itemMoney, int power, int energy, long money, int[] skinsAdvertisingWatched, int amountRepeats)
    {
        Power = power;
        Energy = energy;
        ItemMoney = itemMoney;
        ItemDurability = durability;
        AmountRepeatItems = amountRepeats;
        Money = money;
        SkinsAdvertisingWatched = skinsAdvertisingWatched;
    }
}

