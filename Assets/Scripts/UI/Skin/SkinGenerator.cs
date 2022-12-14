using Assets.Scripts.UI;
using System.Collections.Generic;
using UnityEngine;

public class SkinGenerator : MonoBehaviour
{
    [SerializeField] private SkinPress[] _skins;
    [SerializeField] private SkinPressView _template;
    [SerializeField] private Transform _container;
    [SerializeField] private MeshFilter _pressModel;
    [SerializeField] private GameSaver _saver;

    private readonly List<SkinPressView> _pressViews = new List<SkinPressView>();

    public IReadOnlyList<ISkinStorager> PressViews => _pressViews;

    private void Awake()
    {
        foreach (SkinPress skin in _skins)
        {
            skin.Init(_pressModel);
            SkinPressView skinPressView = Instantiate(_template, _container);
            skinPressView.Init(skin);
            _pressViews.Add(skinPressView);
        }
    }

    private void OnEnable()
    {
        foreach (var pressView in _pressViews)
            pressView.SkinPress.AdvertisingWatched += _saver.Execute;

    }

    private void OnDisable()
    {
        foreach (var pressView in _pressViews)
            pressView.SkinPress.AdvertisingWatched -= _saver.Execute;
    }

    public void SetAdvertisingWatched(int[] advertisingWatched)
    {
        for (int i = 0; i < advertisingWatched.Length; i++)
        {
            _pressViews[i].AdvertisingWatcher.SetAmountWatched(advertisingWatched[i]);
            _pressViews[i].UpdateUI();
        }
    }
}
