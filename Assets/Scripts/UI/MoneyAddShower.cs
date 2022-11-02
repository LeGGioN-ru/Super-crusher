using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyAddShower : MonoBehaviour
{
    [SerializeField] private GameObject _template;

    private void Start()
    {
        var a = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));

        Instantiate(_template, a, Quaternion.identity);
    }
}
