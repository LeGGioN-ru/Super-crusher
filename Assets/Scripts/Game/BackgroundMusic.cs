using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BackgroundMusic : MonoBehaviour
{
    [SerializeField] private PressMoverForward _pressMoverForward;
    [SerializeField] private GameRestarter _gameRestarter;
    [SerializeField] private float _durationVolumeChange;
    [SerializeField] private float _maxVolume;
    [SerializeField] private float _minVolume;

    private AudioSource _sound;

    private void Awake()
    {
        _sound = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _gameRestarter.Restarting += OnGameStartRestarting;
        _pressMoverForward.Moved += OnMoved;
    }

    private void OnDisable()
    {
        _gameRestarter.Restarting -= OnGameStartRestarting;
        _pressMoverForward.Moved -= OnMoved;
    }

    private void OnGameStartRestarting()
    {
        _sound.DOFade(_maxVolume, _durationVolumeChange);
    }

    private void OnMoved()
    {
        _sound.DOFade(_minVolume, _durationVolumeChange);
    }
}
