using System.Collections.Generic;
using UnityEngine;

public class SkinGenerator : MonoBehaviour
{
    [SerializeField] private SkinPress[] _skins;
    [SerializeField] private SkinPressView _template;
    [SerializeField] private Transform _container;
    [SerializeField] private MeshFilter _pressModel;

    private List<SkinPressView> _pressViews = new List<SkinPressView>();

    public IReadOnlyList<SkinPressView> PressViews => _pressViews;

    private void Start()
    {
        foreach (SkinPress skin in _skins)
        {
            skin.Init(_pressModel);
            SkinPressView skinPressView = Instantiate(_template, _container);
            skinPressView.Init(skin);
            _pressViews.Add(skinPressView);
        }
    }

    public void SetAdvertisingWatched(int[] advertisingWatched)
    {
        for (int i = 0; i < advertisingWatched.Length; i++)
        {
            _pressViews[i].SkinPress.SetAdvertisingWatched(advertisingWatched[i]);
            _pressViews[i].UpdateUI();
        }
    }
}
