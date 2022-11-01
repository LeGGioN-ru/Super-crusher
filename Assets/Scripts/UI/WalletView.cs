using TMPro;
using UnityEngine;

public class WalletView : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private TMP_Text _text;

    private void OnEnable()
    {
        _wallet.MoneyChanged += OnMoneyAdded;
    }

    private void OnDisable()
    {
        _wallet.MoneyChanged -= OnMoneyAdded;
    }

    private void OnMoneyAdded(int money)
    {
        _text.text = money.ToString();
    }
}
