using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Press _press;

    private Camera _camera;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    private void OnEnable()
    {
        _press.PartDetected += OnPartDetected;
    }

    private void OnDisable()
    {

    }

    private void OnPartDetected(Part part)
    {
       
    }
}
