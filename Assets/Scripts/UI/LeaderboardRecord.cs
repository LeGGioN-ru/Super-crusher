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

    private LeaderboardEntryResponse _leaderboardData;

    public LeaderboardEntryResponse LeaderboardData => _leaderboardData;

    public void UpdateData(LeaderboardEntryResponse leaderboardEntryResponse)
    {
        _leaderboardData = leaderboardEntryResponse;

        SetData(leaderboardEntryResponse);
        SetBackground();
    }

    public void UpdateData()
    {
        SetData(_leaderboardData);

        SetBackground();
    }

    private Sprite DefineBackground()
    {
        return LeaderboardData.rank switch
        {
            1 => _firstPlacePanel,
            2 => _secondPlacePanel,
            3 => _thirdPlacePanel,
            _ => _defaultPlacePanel,
        };
    }

    private void SetData(LeaderboardEntryResponse leaderboardEntryResponse)
    {
        _place.text = leaderboardEntryResponse.rank.ToString();
        _score.text = NumberCuter.Execute(leaderboardEntryResponse.score);
        _name.text = leaderboardEntryResponse.player.publicName;
    }

    private void SetBackground()
    {
        Sprite background = DefineBackground();

        foreach (Image panel in _backgroundPanels)
            panel.sprite = background;
    }
}
