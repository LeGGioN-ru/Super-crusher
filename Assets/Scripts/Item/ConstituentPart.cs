using UnityEngine;

public class ConstituentPart : MonoBehaviour
{
    private readonly float _speed = 0.05f;
    private Vector3 _randomMoveDirection;

    private void Start()
    {
        int minMoveDirection = 0;
        int maxMoveDirection = 1;
        int downDirection = -1;

        _randomMoveDirection = new Vector3(Random.Range(minMoveDirection, maxMoveDirection + 1), downDirection, Random.Range(minMoveDirection, maxMoveDirection + 1));
    }

    public void Move()
    {
        transform.position += _speed * Time.deltaTime * _randomMoveDirection;
    }
}
