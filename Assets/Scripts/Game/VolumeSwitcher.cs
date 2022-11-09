using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
public class VolumeSwitcher : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Sprite _offVolume;
    [SerializeField] private Sprite _onVolume;

    private Image _image;
    private Button _button;
    private bool _isEnable;
    private string _volume = "Volume";

    private void Awake()
    {
        _image = GetComponent<Image>();
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        _isEnable = !_isEnable;

        if (_isEnable)
        {
            _audioMixer.SetFloat(_volume, -80);
            _image.sprite = _offVolume;
        }
        else
        {
            _audioMixer.SetFloat(_volume, 0);
            _image.sprite = _onVolume;
        }
    }
}
