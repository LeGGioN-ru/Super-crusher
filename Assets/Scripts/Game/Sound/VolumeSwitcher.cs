using Agava.WebUtility;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
public class VolumeSwitcher : MonoBehaviour
{
    [SerializeField] private Sprite _offVolume;
    [SerializeField] private Sprite _onVolume;

    public const int OffVolumeValue = 0;
    public const int OnVolumeValue = 1;

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
        if (inBackground)
            AudioListener.volume = OffVolumeValue;
        else
            AudioListener.volume =_isEnable ? OffVolumeValue : OnVolumeValue;
    }

    private void OnButtonClick()
    {
        _isEnable = !_isEnable;

        if (_isEnable)
        {
            AudioListener.volume = OffVolumeValue;
            _image.sprite = _offVolume;
        }
        else
        {
            AudioListener.volume = OnVolumeValue;
            _image.sprite = _onVolume;
        }
    }
}
