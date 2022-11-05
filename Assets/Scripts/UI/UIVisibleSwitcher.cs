using UnityEngine;

public class UIVisibleSwitcher : MonoBehaviour
{
    [SerializeField] private PressMoverForward _moverForward;
    [SerializeField] private CanvasGroup _upgrades;
    [SerializeField] private CanvasGroup _shop;
    [SerializeField] private GameRestarter _gameRestarter;

    private void OnEnable()
    {
        _moverForward.Moved += OnPressMoved;
        _gameRestarter.Restarted += OnRestarted;
    }

    private void OnDisable()
    {
        _moverForward.Moved -= OnPressMoved;
        _gameRestarter.Restarted -= OnRestarted;
    }

    private void OnPressMoved()
    {
        DisableCanvasGroup(_upgrades);
        DisableCanvasGroup(_shop);
    }

    private void OnRestarted()
    {
        EnableCanvasGroup(_upgrades);
        EnableCanvasGroup(_shop);
    }

    private void EnableCanvasGroup(CanvasGroup canvasGroup)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1;
    }

    private void DisableCanvasGroup(CanvasGroup canvasGroup)
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0;
    }
}
