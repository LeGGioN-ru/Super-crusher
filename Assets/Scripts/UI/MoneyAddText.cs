using UnityEngine;

public class MoneyAddText : MonoBehaviour
{
    [SerializeField] private TextMesh _text;
    [SerializeField] private Animator _animator;

    public Animator Animator => _animator;

    public void SetText(string text)
    {
        _text.text = text;
    }
}
