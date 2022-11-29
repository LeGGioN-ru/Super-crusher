using Agava.YandexGames;
using System;

[Serializable]
public class CloudSave
{
    private LeaderboardEntryResponse _player;
    private LeaderboardGetEntriesResponse _allPlayers;
    private string _saveJson;

    public LeaderboardEntryResponse Player => _player;
    public LeaderboardGetEntriesResponse AllPlayers => _allPlayers;
    public string SaveJson => _saveJson;

    public CloudSave(LeaderboardEntryResponse player, LeaderboardGetEntriesResponse allPlayers, string saveJson)
    {
        _player = player;
        _allPlayers = allPlayers;
        _saveJson = saveJson;
    }
}
