using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private Item _item;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Press _press;

    private void Start()
    {
        Execute();
    }

    public void Execute()
    {
        Instantiate(_item, _spawnPoint).Init(_press);
    }
}
