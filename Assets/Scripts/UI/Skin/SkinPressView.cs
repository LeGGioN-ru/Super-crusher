using Assets.Scripts.UI;
using Lean.Localization;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinPressView : MonoBehaviour, ISkinStorager
{
    [SerializeField] private Image _image;
    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _buttonText;
    [SerializeField] private Slider _progress;
    [SerializeField] private TMP_Text _textProgress;
    [SerializeField] private string _equipButtonLabel;
    [SerializeField] private string _showAdvertisingButtonLabel;
    [SerializeField] private LeanLocalizedTextMeshProUGUI _textTranslate;

    public SkinPress SkinPress => _skinPress;

    private SkinPress _skinPress;

    public IAdvertisingWatcher AdvertisingWatcher => _skinPress;

    public void Init(SkinPress skinPress)
    {
        _image.sprite = skinPress.PreviewImage;
        _skinPress = skinPress;
        UpdateUI();

        _skinPress.AdvertisingWatched += UpdateUI;
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _skinPress.AdvertisingWatched -= UpdateUI;
        _button.onClick.RemoveListener(OnButtonClick);
    }

    public void UpdateUI()
    {
        if (_skinPress.IsAvalible)
        {
            _progress.value = 1;
            _textProgress.enabled = false;
            _textTranslate.TranslationName = _equipButtonLabel;
        }
        else
        {
            _progress.value = _skinPress.AmountWatched / Convert.ToSingle(_skinPress.NeedWatchAdvertising);
            _textProgress.text = $"{_skinPress.AmountWatched}/{_skinPress.NeedWatchAdvertising}";
            _textTranslate.TranslationName = _showAdvertisingButtonLabel;
        }
    }

    private void OnButtonClick()
    {
        if (_skinPress.IsAvalible)
            _skinPress.SetModel();
        else
            _skinPress.ShowAdvertising();

        UpdateUI();
    }
}
