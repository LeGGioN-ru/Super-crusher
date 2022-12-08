using Agava.YandexGames;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class Authorization : MonoBehaviour
{
    [SerializeField] private GameInitializator _initializator;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(Execute);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(Execute);
    }

    private void Execute()
    {
        PlayerAccount.Authorize(Disable);
    }

    private void Disable()
    {
        _initializator.LoadCloudData();
        gameObject.SetActive(false);
    }
}
