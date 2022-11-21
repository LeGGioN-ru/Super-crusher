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

    public void UpdateData(LeaderboardEntryResponse leaderboardEntryResponse)
    {
        SetData(leaderboardEntryResponse);

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

    private void SetData(LeaderboardEntryResponse leaderboardEntryResponse)
    {
        _place.text = leaderboardEntryResponse.rank.ToString();
        _score.text = NumberCuter.Execute(leaderboardEntryResponse.score);
        
        if (leaderboardEntryResponse.player.publicName == string.Empty || leaderboardEntryResponse.player.publicName == null)
            _name.text = "Anonym";
        else
            _name.text = leaderboardEntryResponse.player.publicName;
    }

    private void SetBackground(LeaderboardEntryResponse leaderboardEntryResponse)
    {
        Sprite background = DefineBackground(leaderboardEntryResponse);

        foreach (Image panel in _backgroundPanels)
            panel.sprite = background;
    }
}
