using System;
using System.Collections.Generic;

[Serializable]
public class Save
{
    public PowerUp PowerUp;
    public EnergyUp EnergyUp;
    public ItemUp ItemUp;
    public IReadOnlyCollection<Item> Items;

    public Save(PowerUp powerUp, EnergyUp energyUp, ItemUp itemUp, IReadOnlyCollection<Item> items)
    {
        PowerUp = powerUp;
        EnergyUp = energyUp;
        ItemUp = itemUp;
        Items = items;
    }
}

