using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PressEnergyView : MonoBehaviour
{
    [SerializeField] private PressEnergy _pressEnergy;
    [SerializeField] private Slider _energyBar;
    [SerializeField] private TMP_Text _text;

    private void OnEnable()
    {
        _pressEnergy.Changed += Display;
        _pressEnergy.EnergySetted += Display;
    }

    private void OnDisable()
    {
        _pressEnergy.Changed -= Display;
        _pressEnergy.EnergySetted -= Display;
    }

    private void Display(float currentEnergy)
    {
        currentEnergy = Convert.ToInt32(Math.Clamp(currentEnergy, 0, float.MaxValue));

        _text.text = $"{currentEnergy}/{_pressEnergy.Energy}";
        _energyBar.value = currentEnergy / _pressEnergy.Energy;
    }
}
