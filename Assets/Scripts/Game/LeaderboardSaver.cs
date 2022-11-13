using Agava.YandexGames;
using System;
using UnityEngine;

public class LeaderboardSaver : MonoBehaviour
{
    [SerializeField] private Press _press;

    private int _scoreAdd;
    private LeaderboardEntryResponse _player;

    public event Action<LeaderboardEntryResponse> Saved;

    public long Score => _scoreAdd;

    private void Awake()
    {
        //Agava.YandexGames.Leaderboard.GetPlayerEntry(LeaderboardConstants.Name, SetScore);
    }

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
        part.Destroyed += Execute;
    }

    private void Execute(Part part)
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        part.Destroyed -= Execute;
        return;
#endif
        _scoreAdd = Convert.ToInt32(part.Money);
    }

    
}
