using UnityEngine;

public class SkinGenerator : MonoBehaviour
{
    [SerializeField] private SkinPress[] _skins;
    [SerializeField] private SkinPressView _template;
    [SerializeField] private Transform _container;
    [SerializeField] private MeshFilter _pressModel;

    private void Start()
    {
        foreach (SkinPress skin in _skins)
        {
            skin.Init(_pressModel);
            Instantiate(_template, _container).Init(skin);
        }
    }
}
