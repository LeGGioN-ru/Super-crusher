using Agava.YandexGames;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class GameSaver : MonoBehaviour
{
    [SerializeField] private GameRestarter _restarted;
    [SerializeField] private PowerUp _powerUp;
    [SerializeField] private EnergyUp _energyUp;
    [SerializeField] private ItemUp _itemUp;
    [SerializeField] private ItemSpawner _spawner;

    private void OnEnable()
    {
        _restarted.Restarted += OnGameRestarted;
    }

    private void OnDisable()
    {
        _restarted.Restarted -= OnGameRestarted;
    }

    private void OnGameRestarted()
    {
        var save = JsonConvert.SerializeObject(_itemUp, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        Debug.Log(save);
    }
}