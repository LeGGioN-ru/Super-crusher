using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Upgrader))]
public abstract class UpgradeView : MonoBehaviour
{
    [SerializeField] private TMP_Text _price;
    [SerializeField] private TMP_Text _level;

    private Button _button;
    private Upgrader _upgrader;

    private void Awake()
    {
        _upgrader = GetComponent<Upgrader>();
        _button = GetComponent<Button>();

        OnUpgraded(_upgrader.Level, _upgrader.Price);
        CheckAvalible();
    }

    private void OnEnable()
    {
        _upgrader.Upgraded += OnUpgraded;
        _upgrader.Wallet.MoneyChanged += OnMoneyChanged;
        _button.onClick.AddListener(_upgrader.Execute);
        _button.onClick.AddListener(CheckAvalible);
    }

    private void OnDisable()
    {
        _upgrader.Upgraded -= OnUpgraded;
        _upgrader.Wallet.MoneyChanged -= OnMoneyChanged;
        _button.onClick.RemoveListener(_upgrader.Execute);
        _button.onClick.RemoveListener(CheckAvalible);
    }

    private void OnUpgraded(int level, int price)
    {
        _price.text = price.ToString();
        _level.text = $"LV {level}";
    }

    private void OnMoneyChanged(int money)
    {
        CheckAvalible();
    }

    private void CheckAvalible()
    {
        if (IsUpgradeAvalible())
        {
            _button.interactable = true;
            return;
        }

        _button.interactable = false;
    }

    protected virtual bool IsUpgradeAvalible()
    {
        return _upgrader.Wallet.CurrentMoney >= _upgrader.Price;
    }
}
