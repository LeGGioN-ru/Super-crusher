using Lean.Localization;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinPressView : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _buttonText;
    [SerializeField] private Slider _progress;
    [SerializeField] private TMP_Text _textProgress;
    [SerializeField] private string _equipButtonLabel;
    [SerializeField] private string _showAdvertisingButtonLabel;
    [SerializeField] private LeanLocalizedTextMeshProUGUI _textTranslate;

    private SkinPress _skinPress;

    public void Init(SkinPress skinPress)
    {
        _image.sprite = skinPress.PreviewImage;
        _skinPress = skinPress;

        UpdateUI();
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
        if (_skinPress.IsAvalible)
            _skinPress.SetModel();
        else
            _skinPress.ShowAdvertising();

        UpdateUI();
    }

    private void UpdateUI()
    {
        if (_skinPress.IsAvalible)
        {
            _progress.value = 1;
            _textProgress.enabled = false;
            _textTranslate.TranslationName = _equipButtonLabel;
        }
        else
        {
            _progress.value = _skinPress.AdvertisingWatched / Convert.ToSingle(_skinPress.NeedWatchAdvertising);
            _textProgress.text = $"{_skinPress.AdvertisingWatched}/{_skinPress.NeedWatchAdvertising}";
            _textTranslate.TranslationName = _showAdvertisingButtonLabel;
        }
    }
}
