using Agava.YandexGames;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private LeaderboardSaver _leaderboardSaver;
    [SerializeField] private Press _press;
    [SerializeField] private LeaderboardRecord _template;
    [SerializeField] private Transform _container;

    private readonly List<LeaderboardRecord> _records = new List<LeaderboardRecord>();
    private LeaderboardRecord _player;

    private void OnEnable()
    {
        _press.PartDetected += OnPartDetected;
    }

    private void OnDisable()
    {
        _press.PartDetected -= OnPartDetected;
    }

    public void Init(LeaderboardGetEntriesResponse leaderboardGetEntriesResponse)
    {
        foreach (LeaderboardEntryResponse entity in leaderboardGetEntriesResponse.entries)
        {
            LeaderboardRecord record = Instantiate(_template, _container);
            record.UpdateData(entity);
            _records.Add(record);

            if (leaderboardGetEntriesResponse.userRank == entity.rank)
                _player = record;
        }
    }

    private void OnPartDetected(Part part)
    {
        part.Destroyed += UpdateData;
    }

    private void UpdateData(Part part)
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        part.Destroyed -= UpdateData;
        return;
#endif

        int rankDifference = 1;

        Agava.YandexGames.Leaderboard.SetScore(LeaderboardConstants.Name, _player.LeaderboardData.score += Convert.ToInt32(part.Money));
        _player.UpdateData();

        LeaderboardRecord behindPlayer = _records.FirstOrDefault(record => record.LeaderboardData.rank == _player.LeaderboardData.rank - rankDifference);

        if (behindPlayer != null)
            behindPlayer.UpdateData();
    }
}
