using Assets.Scripts.UI;
using System;
using UnityEngine;

[Serializable]
public class SkinPress : IAdvertisingWatcher
{
    [SerializeField] private Sprite _previewImage;
    [SerializeField] private Mesh _model;
    [SerializeField] private int _needWatchAdvertising;

    private MeshFilter _pressModel;
    private int _advertisingWatched;
    private VideoAdvertisingShower _videoAdvertisingShower;

    public event Action AdvertisingWatched;

    public Sprite PreviewImage => _previewImage;
    public int AmountWatched => _advertisingWatched;
    public int NeedWatchAdvertising => _needWatchAdvertising;
    public bool IsAvalible => NeedWatchAdvertising <= AmountWatched;

    public void Init(MeshFilter pressModel)
    {
        _videoAdvertisingShower = new VideoAdvertisingShower();
        _pressModel = pressModel;
    }

    public void SetAmountWatched(int advertisingWatched)
    {
        _advertisingWatched = advertisingWatched;
    }

    public void ShowAdvertising()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        return;
#endif
        _videoAdvertisingShower.Execute(IncrementAdvertisingWathced);
    }

    private void IncrementAdvertisingWathced()
    {
        _advertisingWatched++;
        AdvertisingWatched?.Invoke();
    }

    public void SetModel()
    {
        _pressModel.mesh = _model;
    }
}
