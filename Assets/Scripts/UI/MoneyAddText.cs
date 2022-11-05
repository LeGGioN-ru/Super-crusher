using TMPro;
using UnityEngine;

public class MoneyAddText : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Animator _animator;

    public Animator Animator => _animator;

    public void SetText(long money)
    {
        _text.text = NumberCuter.Execute(money);
    }
}
