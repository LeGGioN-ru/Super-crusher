using TMPro;
using UnityEngine;

public class UIVisibleSwitcher : MonoBehaviour
{
    [SerializeField] private PressMoverForward _moverForward;
    [SerializeField] private GameRestarter _gameRestarter;
    [SerializeField] private Animator _shopUpgrades;
    [SerializeField] private Animator _energyBar;
    [SerializeField] private TMP_Text _holdPlayText;

    private void OnEnable()
    {
        _moverForward.Moved += OnPressMoved;
        _gameRestarter.Restarting += OnStartRestarting;
        _gameRestarter.Restarted += OnRestarted;
    }

    private void OnDisable()
    {
        _moverForward.Moved -= OnPressMoved;
        _gameRestarter.Restarting -= OnStartRestarting;
        _gameRestarter.Restarted -= OnRestarted;
    }

    private void OnPressMoved()
    {
        _holdPlayText.enabled = false;
        _shopUpgrades.Play(ShopUpgradesAnimationController.State.Hide);
        _energyBar.Play(EnergyBarAnimationController.State.Show);
    }

    private void OnRestarted()
    {
        _holdPlayText.enabled = true;
        _shopUpgrades.Play(ShopUpgradesAnimationController.State.Show);
    }

    private void OnStartRestarting()
    {
        _energyBar.Play(EnergyBarAnimationController.State.Hide);
    }
}
