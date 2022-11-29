using Agava.YandexGames;
using Lean.Localization;
using Newtonsoft.Json;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameInitializator : MonoBehaviour
{
    [SerializeField] private Leaderboard _leaderboard;
    [SerializeField] private ItemSpawner _spawner;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private SkinGenerator _skinGenerator;
    [SerializeField] private Press _press;
    [SerializeField] private PressEnergy _pressEnergy;
    [SerializeField] private Education _education;
    [SerializeField] private Image _loadScreen;

    private IEnumerator Start()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        yield break;
#endif

        yield return YandexGamesSdk.Initialize();

        LeanLocalization.SetCurrentLanguageAll(YandexGamesSdk.Environment.i18n.lang);

        if (PlayerAccount.IsAuthorized == false)
            PlayerAccount.Authorize();

        PlayerAccount.GetPlayerData(LoadSave);

        Agava.YandexGames.Leaderboard.GetPlayerEntry(LeaderboardConstants.Name, TryCreatePlayerLeaderboardEntity);
        Agava.YandexGames.Leaderboard.GetEntries(LeaderboardConstants.Name, _leaderboard.Init);
    }

    private void TryCreatePlayerLeaderboardEntity(LeaderboardEntryResponse leaderboardEntryResponse)
    {
        if (leaderboardEntryResponse == null)
            Agava.YandexGames.Leaderboard.SetScore(LeaderboardConstants.Name, 0);
    }

    private void LoadSave(string jsonSave)
    {
        string emptyJson = "{}";

        if (jsonSave == emptyJson)
            return;

        Save save = JsonConvert.DeserializeObject<Save>(jsonSave);

        _press.SetPower(save.Power);
        _pressEnergy.SetEnergy(save.Energy);
        _wallet.SetCurrentMoney(save.Money);
        _spawner.SetSave(new PartStats(save.ItemIncome, save.ItemDurability), save.AmountRepeatsItems);
        _skinGenerator.SetAdvertisingWatched(save.SkinsAdvertisingWatched);

        if (save.IsEducatuionDone)
        {
            _education.CountEducation();
            _education.gameObject.SetActive(false);
        }

        _loadScreen.gameObject.SetActive(false);
    }
}
