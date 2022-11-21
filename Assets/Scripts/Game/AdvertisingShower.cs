using Agava.YandexGames;
using UnityEngine;

public class AdvertisingShower : MonoBehaviour
{
    [SerializeField] private ItemSpawner _spawner;
    [SerializeField] private int _itemsForAdvertising;

    private int _currentItemsSpawned;

    private void OnEnable()
    {
        _spawner.Spawned += Execute;
    }

    private void OnDisable()
    {
        _spawner.Spawned -= Execute;
    }

    private void Execute(Item item)
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        return;
#endif
        _currentItemsSpawned++;

        if (_currentItemsSpawned >= _itemsForAdvertising)
            InterstitialAd.Show(ResetSpawnedItems);
        
    }

    private void ResetSpawnedItems()
    {
        _currentItemsSpawned = 0;
    }
}
