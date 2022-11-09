using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartParticles : MonoBehaviour
{
    [SerializeField] private ParticleSystem _sparks;
    [SerializeField] private ParticleSystem _smoke;
    [SerializeField] private Transform _head;
    [SerializeField] private Press _press;

    private void OnEnable()
    {
        _press.PartDetected += OnPartDetected;
    }

    private void OnDisable()
    {
        _press.PartDetected -= OnPartDetected;
    }

    private void OnPartDetected(Part part)
    {
        part.Destroyed += StartParticles;
    }

    private void StartParticles(Part part)
    {
        PlayParticle(_sparks);
        PlayParticle(_smoke);
        part.Destroyed -= StartParticles;
    }

    private void PlayParticle(ParticleSystem particle)
    {
        particle.gameObject.transform.position = _head.position;
        particle.Play();
    }
}
