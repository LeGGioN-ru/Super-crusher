using Agava.YandexGames;
using Newtonsoft.Json;
using UnityEngine;

public class GameSaver : MonoBehaviour
{
    [SerializeField] private GameRestarter _restarted;
    [SerializeField] private ItemSpawner _spawner;
    [SerializeField] private Press _press;
    [SerializeField] private PressEnergy _pressEnergy;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private SkinGenerator _skinGenerator;
    [SerializeField] private Education _education;
    [SerializeField] private Upgrader[] _upgrades;

    private void OnEnable()
    {
        _restarted.Restarted += Execute;

        foreach (var upgrade in _upgrades)
            upgrade.Executed += Execute;
    }

    private void OnDisable()
    {
        _restarted.Restarted -= Execute;

        foreach (var upgrade in _upgrades)
            upgrade.Executed -= Execute;
    }

    public void Execute()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        return;
#endif
        Save save = new Save(_spawner.PartStats.Durability, _spawner.PartStats.Money, _press.CurrentPower, _pressEnergy.Energy, _wallet.CurrentMoney, GetSkinsAdvertisingWatched(), _spawner.AmountRepeats, _education.IsDone);

        PlayerPrefs.SetString(nameof(Save), JsonConvert.SerializeObject(save));
        PlayerPrefs.Save();
    }

    private int[] GetSkinsAdvertisingWatched()
    {
        int[] skinsAdvertisingWatched = new int[_skinGenerator.PressViews.Count];

        for (int i = 0; i < skinsAdvertisingWatched.Length; i++)
            skinsAdvertisingWatched[i] = _skinGenerator.PressViews[i].AdvertisingWatcher.AmountWatched;

        return skinsAdvertisingWatched;
    }
}