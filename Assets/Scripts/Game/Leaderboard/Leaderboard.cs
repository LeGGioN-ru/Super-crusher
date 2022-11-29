using Agava.YandexGames;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private Press _press;
    [SerializeField] private LeaderboardRecord _template;
    [SerializeField] private Transform _container;
    [SerializeField] private int _amountRecords;

    private List<LeaderboardEntryResponse> _entries = new List<LeaderboardEntryResponse>();
    private readonly List<LeaderboardRecord> _records = new List<LeaderboardRecord>();
    private LeaderboardEntryResponse _player;

    private void OnEnable()
    {
        _press.PartDetected += OnPartDetected;
    }

    private void OnDisable()
    {
        _press.PartDetected -= OnPartDetected;
    }

    private void OnPartDetected(Part part)
    {
        part.Destroyed += UpdateData;
    }

    public void Init(LeaderboardGetEntriesResponse leaderboardGetEntriesResponse)
    {
        for (int i = 0; i < _amountRecords; i++)
        {
            if (leaderboardGetEntriesResponse.entries.Length <= i)
                break;

            LeaderboardRecord record = Instantiate(_template, _container);
            LeaderboardEntryResponse entity = leaderboardGetEntriesResponse.entries[i];
            record.UpdateData(entity);
            _records.Add(record);
            _entries.Add(entity);

            if (leaderboardGetEntriesResponse.userRank == entity.rank)
                _player = entity;
        }
    }

    private void UpdateData(Part part)
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        return;
#endif
        _player.score += Convert.ToInt32(part.Money);

        Agava.YandexGames.Leaderboard.SetScore(LeaderboardConstants.Name, _player.score);

        UpdateViews();

        part.Destroyed -= UpdateData;
    }

    private void UpdateViews()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        return;
#endif
        _entries = _entries.OrderByDescending(entry => entry.score).ToList();

        for (int i = 0; i < _records.Count; i++)
            _records[i].UpdateData(_entries[i], i + 1);
    }
}
