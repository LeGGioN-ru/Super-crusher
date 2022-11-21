using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Press))]
public class PressMoverBack : MonoBehaviour
{
    [SerializeField] private Transform _endPoint;
    [SerializeField] private float _speed;

    private Press _press;

    public event Action Backed;

    private void Start()
    {
        _press = GetComponent<Press>();
    }

    public void Execute()
    {
        StartCoroutine(Retrieve());
    }

    private IEnumerator Retrieve()
    {
        while (_press.transform.position != _endPoint.transform.position)
        {
            _press.transform.position = Vector3.MoveTowards(_press.transform.position, _endPoint.position, _speed * Time.deltaTime);
            yield return null;
        }

        Backed?.Invoke();
    }
}
