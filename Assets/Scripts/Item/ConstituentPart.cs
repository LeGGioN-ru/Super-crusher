using UnityEngine;

public class ConstituentPart : MonoBehaviour
{
    private readonly float _speed = 0.05f;
    private Vector3 _randomMoveDirection;
    private Vector3 _randomForceDirection;
    private readonly float _pushPower = 5;

    private void Start()
    {
        int minForceDirection = -1;
        int minMoveDirection = 0;
        int maxDirection = 1;
        int downDirection = -1;

        _randomMoveDirection = new Vector3(Random.Range(minMoveDirection, maxDirection + 1), downDirection, Random.Range(minMoveDirection, maxDirection + 1));
        _randomForceDirection = new Vector3(Random.Range(minForceDirection, maxDirection + 1), 0, Random.Range(minForceDirection, maxDirection + 1));
    }

    public void Push()
    {
        if (TryGetComponent(out Rigidbody rigidbody))
            rigidbody.AddForce(_randomForceDirection * _pushPower);
    }

    public void Move()
    {
        transform.position += _speed * Time.deltaTime * _randomMoveDirection;
    }
}
