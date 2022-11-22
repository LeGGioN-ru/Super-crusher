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

    private void OnEnable()
    {
        _restarted.Restarted += Execute;
    }

    private void OnDisable()
    {
        _restarted.Restarted -= Execute;
    }

    private void Execute()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        return;

#endif
        Save save = new Save(_spawner.PartStats.Durability, _spawner.PartStats.Money, _press.CurrentPower, _pressEnergy.Energy, _wallet.CurrentMoney, GetSkinsAdvertisingWatched(), _spawner.AmountRepeats, _education.IsDone);

        PlayerAccount.SetPlayerData(JsonConvert.SerializeObject(save));
    }

    private int[] GetSkinsAdvertisingWatched()
    {
        int[] skinsAdvertisingWatched = new int[_skinGenerator.PressViews.Count];

        for (int i = 0; i < skinsAdvertisingWatched.Length; i++)
            skinsAdvertisingWatched[i] = _skinGenerator.PressViews[i].AdvertisingWatcher.AmountWatched;

        return skinsAdvertisingWatched;
    }
}