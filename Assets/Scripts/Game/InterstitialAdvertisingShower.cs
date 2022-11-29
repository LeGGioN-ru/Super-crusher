using Agava.YandexGames;
using UnityEngine;

public class InterstitialAdvertisingShower : MonoBehaviour
{
    [SerializeField] private ItemSpawner _spawner;
    [SerializeField] private int _itemsForAdvertising;

    private int _currentItemsSpawned;
    private float _passedTime = 0;
    private int _delaySecondAdvertisingShow = 60;

    private void OnEnable()
    {
        _spawner.Spawned += Execute;
    }

    private void OnDisable()
    {
        _spawner.Spawned -= Execute;
    }

    private void Update()
    {
        _passedTime += Time.deltaTime;
    }

    private void Execute(Item item)
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        return;
#endif
        _currentItemsSpawned++;

        if (_currentItemsSpawned >= _itemsForAdvertising && _passedTime >= _delaySecondAdvertisingShow)
            InterstitialAd.Show(ResetSpawnedItems);

    }

    private void ResetSpawnedItems()
    {
        _currentItemsSpawned = 0;
    }
}
