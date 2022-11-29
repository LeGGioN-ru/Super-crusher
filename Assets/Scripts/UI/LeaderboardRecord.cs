using Agava.YandexGames;
using Lean.Localization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardRecord : MonoBehaviour
{
    [SerializeField] private Image[] _backgroundPanels;
    [SerializeField] private TMP_Text _place;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _score;
    [SerializeField] private Sprite _firstPlacePanel;
    [SerializeField] private Sprite _secondPlacePanel;
    [SerializeField] private Sprite _thirdPlacePanel;
    [SerializeField] private Sprite _defaultPlacePanel;

    private string _anonym = "Anonym";

    public void UpdateData(LeaderboardEntryResponse leaderboardEntryResponse, int place=0)
    {
        SetData(leaderboardEntryResponse, place);

        SetBackground(leaderboardEntryResponse);
    }

    private Sprite DefineBackground(LeaderboardEntryResponse leaderboardEntryResponse)
    {
        return leaderboardEntryResponse.rank switch
        {
            1 => _firstPlacePanel,
            2 => _secondPlacePanel,
            3 => _thirdPlacePanel,
            _ => _defaultPlacePanel,
        };
    }

    private void SetData(LeaderboardEntryResponse leaderboardEntryResponse, int place)
    {
        _place.text = place == 0 ? leaderboardEntryResponse.rank.ToString() : place.ToString();
        _score.text = NumberCuter.Execute(leaderboardEntryResponse.score);

        if (leaderboardEntryResponse.player.publicName == string.Empty || leaderboardEntryResponse.player.publicName == null)
            return;

        _name.text = leaderboardEntryResponse.player.publicName;
    }

    private void SetBackground(LeaderboardEntryResponse leaderboardEntryResponse)
    {
        Sprite background = DefineBackground(leaderboardEntryResponse);

        foreach (Image panel in _backgroundPanels)
            panel.sprite = background;
    }
}
