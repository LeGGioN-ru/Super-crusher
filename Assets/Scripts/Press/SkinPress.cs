using System;
using UnityEngine;


[Serializable]
public class SkinPress
{
    [SerializeField] private Sprite _previewImage;
    [SerializeField] private Mesh _model;
    [SerializeField] private int _needWatchAdvertising;

    private MeshFilter _pressModel;
    private int _advertisingWatched;

    public Sprite PreviewImage => _previewImage;
    public int AdvertisingWatched => _advertisingWatched;
    public int NeedWatchAdvertising => _needWatchAdvertising;
    public bool IsAvalible => NeedWatchAdvertising <= AdvertisingWatched;

    public void Init(MeshFilter pressModel)
    {
        _pressModel = pressModel;
    }

    public void ShowAdvertising()
    {
        _advertisingWatched++;
    }

    public void SetModel()
    {
        _pressModel.mesh = _model;
    }
}
