using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoneyAddTextPool : MonoBehaviour
{
    [SerializeField] private MoneyAddText _template;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private Transform[] _showPoints;
    [SerializeField] private Vector3 _bias;
    [SerializeField] private int _poolSize;

    private MoneyAddText[] _moneyAddTexts;

    private void Awake()
    {
        CreatePull();
    }

    private void OnEnable()
    {
        _wallet.MoneyAdded += OnMoneyAdded;
    }

    private void OnDisable()
    {
        _wallet.MoneyAdded -= OnMoneyAdded;
    }

    private void CreatePull()
    {
        _moneyAddTexts = new MoneyAddText[_poolSize];

        for (int i = 0; i < _moneyAddTexts.Length; i++)
        {
            _moneyAddTexts[i] = Instantiate(_template, _showPoints[Random.Range(0, _showPoints.Length)]);
            _moneyAddTexts[i].gameObject.SetActive(false);
        }

        RandomizePoolPositions();
    }

    private void RandomizePoolPositions()
    {
        for (int i = 0; i < _moneyAddTexts.Length; i++)
        {
            Vector3 currentBias = new Vector3(Random.Range(-_bias.x, _bias.x), Random.Range(-_bias.y, _bias.y));
            _moneyAddTexts[i].transform.position += currentBias;
        }
    }

    private void OnMoneyAdded(long money)
    {
        if (TryGetMoneyAddText(out MoneyAddText moneyAddText))
        {
            moneyAddText.SetText(money.ToString());
            moneyAddText.gameObject.SetActive(true);
            moneyAddText.Animator.Play(FloatingTextAnimatorController.State.Incarnate);
        }
    }

    private bool TryGetMoneyAddText(out MoneyAddText moneyAddText)
    {
        List<MoneyAddText> notActiveAddTexts = _moneyAddTexts.Where(text => text.gameObject.activeSelf == false).ToList();
        moneyAddText = notActiveAddTexts[Random.Range(0, notActiveAddTexts.Count())];
        return moneyAddText != null;
    }
}
