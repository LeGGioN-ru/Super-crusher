using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class ShopPanelSwitcher : MonoBehaviour
{
    [SerializeField] private Image _panel;
    [SerializeField] private Button _openButton;
    [SerializeField] private Button _closeButton;
    [SerializeField] private PressMoverForward _mover;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _openButton.onClick.AddListener(OnOpenButtonClick);
        _closeButton.onClick.AddListener(OnCloseButtonClick);
    }

    private void OnDisable()
    {
        _openButton.onClick.RemoveListener(OnOpenButtonClick);
        _closeButton.onClick.RemoveListener(OnCloseButtonClick);
    }

    private void OnCloseButtonClick()
    {
        _mover.enabled = true;
        _animator.Play(ShopAnimationController.State.Hide);
    }

    private void OnOpenButtonClick()
    {
        _mover.enabled = false;
        _animator.Play(ShopAnimationController.State.Show);
    }
}
