using Agava.YandexGames;
using Lean.Localization;
using Newtonsoft.Json;
using System;
using System.Collections;
using UnityEngine;

public class GameInitializator : MonoBehaviour
{
    [SerializeField] private Leaderboard _leaderboard;
    [SerializeField] private ItemSpawner _spawner;
    [SerializeField] private PowerUp _powerUp;
    [SerializeField] private EnergyUp _energyUp;
    [SerializeField] private ItemUp _itemUp;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private SkinGenerator _skinGenerator;
    [SerializeField] private Press _press;
    [SerializeField] private PressEnergy _pressEnergy;

    private void Awake()
    {
        YandexGamesSdk.CallbackLogging = true;
    }

    private IEnumerator Start()
    {

#if !UNITY_WEBGL || UNITY_EDITOR
        yield break;
#endif

        yield return YandexGamesSdk.Initialize();

        LeanLocalization.SetCurrentLanguageAll(YandexGamesSdk.Environment.i18n.lang);

        if (PlayerAccount.IsAuthorized == false)
            PlayerAccount.Authorize();

        if (PlayerAccount.HasPersonalProfileDataPermission == false)
            PlayerAccount.RequestPersonalProfileDataPermission();

        PlayerAccount.GetPlayerData(LoadSave);

        Agava.YandexGames.Leaderboard.GetPlayerEntry(LeaderboardConstants.Name, TryCreatePlayerLeaderboardEntity);
        Agava.YandexGames.Leaderboard.GetEntries(LeaderboardConstants.Name, _leaderboard.Init);
    }

    private void TryCreatePlayerLeaderboardEntity(LeaderboardEntryResponse leaderboardEntryResponse)
    {
        if (leaderboardEntryResponse == null)
            Agava.YandexGames.Leaderboard.SetScore(LeaderboardConstants.Name, Convert.ToInt32(0));
    }

    private void LoadSave(string jsonSave)
    {
        if (jsonSave == null || jsonSave == "{}")
            return;

        Save save = JsonConvert.DeserializeObject<Save>(jsonSave);

        _press.SetPower(save.Power);
        _pressEnergy.SetEnergy(save.Energy);

        _wallet.SetCurrentMoney(save.Money);

        _spawner.SetSave(new PartStats(save.ItemMoney, save.ItemDurability), save.AmountRepeatItems);
        _skinGenerator.SetAdvertisingWatched(save.SkinsAdvertisingWatched);
    }
}
