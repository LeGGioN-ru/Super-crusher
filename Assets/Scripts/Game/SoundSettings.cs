using System;
using UnityEngine;


[Serializable]
public class SoundSettings
{
    [SerializeField] private AudioSource _sound;
    [SerializeField] private float _minPitch;
    [SerializeField] private float _maxPitch;

    public void PlaySound()
    {
        _sound.pitch = UnityEngine.Random.Range(_minPitch, _maxPitch);
        _sound.Play();
    }
}