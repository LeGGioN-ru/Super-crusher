using Agava.YandexGames;
using UnityEngine;

public class GameInitializator : MonoBehaviour
{
    private void Awake()
    {
        YandexGamesSdk.Initialize();
    }
}
