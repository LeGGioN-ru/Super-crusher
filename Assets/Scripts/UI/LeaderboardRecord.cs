using Agava.YandexGames;
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

    public void UpdateData(LeaderboardEntryResponse leaderboardEntryResponse, int place = 0)
    {
        if (place == 0)
            place = leaderboardEntryResponse.rank;

        SetData(leaderboardEntryResponse, place);

        SetBackground(place);
    }

    private Sprite DefineBackground(int place)
    {
        return place switch
        {
            1 => _firstPlacePanel,
            2 => _secondPlacePanel,
            3 => _thirdPlacePanel,
            _ => _defaultPlacePanel,
        };
    }

    private void SetData(LeaderboardEntryResponse leaderboardEntryResponse, int place)
    {
        _place.text = place.ToString();
        _score.text = NumberCuter.Execute(leaderboardEntryResponse.score);

        if (leaderboardEntryResponse.player.publicName == string.Empty || leaderboardEntryResponse.player.publicName == null)
            return;

        _name.text = leaderboardEntryResponse.player.publicName;
    }

    private void SetBackground(int place)
    {
        Sprite background = DefineBackground(place);

        foreach (Image panel in _backgroundPanels)
            panel.sprite = background;
    }
}
