using Agava.YandexGames;
using System;
using UnityEngine;

[Serializable]
public class VideoAdvertisingShower
{
    public void Execute(Action reward)
    {
        VideoAd.Show(DisableVolume, reward, TryEnableVolume);
    }

    private void DisableVolume()
    {
        AudioListener.pause = true;
    }

    private void TryEnableVolume()
    {
        AudioListener.pause = false;
    }
}
