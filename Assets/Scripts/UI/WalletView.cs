using TMPro;
using UnityEngine;

public class WalletView : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private TMP_Text _text;

    private void OnEnable()
    {
        _wallet.MoneyAdded += OnMoneyChanged;
        _wallet.MoneyReduced += OnMoneyChanged;
    }

    private void OnDisable()
    {
        _wallet.MoneyAdded -= OnMoneyChanged;
        _wallet.MoneyReduced -= OnMoneyChanged;
    }

    private void OnMoneyChanged(int money)
    {
        _text.text = _wallet.CurrentMoney.ToString();
    }
}
