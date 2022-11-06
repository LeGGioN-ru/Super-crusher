using Agava.YandexGames;
using Lean.Localization;
using System.Collections;
using UnityEngine;

public class GameInitializator : MonoBehaviour
{
    private void Start()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
            StartCoroutine(Execute());
#endif
    }

    private IEnumerator Execute()
    {
        yield return StartCoroutine(YandexGamesSdk.Initialize());

        LeanLocalization.SetCurrentLanguageAll(YandexGamesSdk.Environment.i18n.lang);
    }
}
