using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Upgrader))]
public abstract class UpgradeView : MonoBehaviour
{
    [SerializeField] private TMP_Text _price;
    [SerializeField] private TMP_Text _level;
    [SerializeField] private GameRestarter _restarter;

    private Button _button;
    private Upgrader _upgrader;

    private void Awake()
    {
        _upgrader = GetComponent<Upgrader>();
        _button = GetComponent<Button>();

        OnChanged(_upgrader.Level, _upgrader.Price);
        CheckAvalible();
    }

    private void OnEnable()
    {
        _upgrader.Changed += OnChanged;
        _upgrader.Wallet.MoneyChanged += OnMoneyChanged;
        _button.onClick.AddListener(_upgrader.Execute);
        _button.onClick.AddListener(CheckAvalible);
    }

    private void OnDisable()
    {
        _upgrader.Changed -= OnChanged;
        _upgrader.Wallet.MoneyChanged -= OnMoneyChanged;
        _button.onClick.RemoveListener(_upgrader.Execute);
        _button.onClick.RemoveListener(CheckAvalible);
    }

    private void OnChanged(int level, long price)
    {
        _price.text = NumberCuter.Execute(price);
        _level.text = $"LV {level}";
    }

    private void OnMoneyChanged(long money)
    {
        CheckAvalible();
    }

    private void CheckAvalible()
    {
        _button.interactable = IsUpgradeAvalible();
    }

    protected virtual bool IsUpgradeAvalible()
    {
        return _upgrader.Wallet.CurrentMoney >= _upgrader.Price;
    }
}
