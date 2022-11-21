using Agava.WebUtility;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
public class VolumeSwitcher : MonoBehaviour
{
    [SerializeField] private Sprite _offVolume;
    [SerializeField] private Sprite _onVolume;

    private readonly int _offVolumeValue = 0;
    private readonly int _onVolumeValue = 1;
    private Image _image;
    private Button _button;
    private bool _isEnable;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        WebApplication.InBackgroundChangeEvent += OnBackgroundChange;
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        WebApplication.InBackgroundChangeEvent -= OnBackgroundChange;
        _button.onClick.RemoveListener(OnButtonClick);
    }

    private void OnBackgroundChange(bool inBackground)
    {
        AudioListener.pause = inBackground;

        if (inBackground)
            AudioListener.volume = _offVolumeValue;
        else
            AudioListener.volume = _isEnable ? _offVolumeValue : _onVolumeValue;
    }

    private void OnButtonClick()
    {
        _isEnable = !_isEnable;

        if (_isEnable)
        {
            AudioListener.volume = _offVolumeValue;
            _image.sprite = _offVolume;
        }
        else
        {
            AudioListener.volume = _onVolumeValue;
            _image.sprite = _onVolume;
        }
    }
}
