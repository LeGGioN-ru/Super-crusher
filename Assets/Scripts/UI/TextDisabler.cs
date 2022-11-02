using UnityEngine;

public class TextDisabler : MonoBehaviour
{
    [SerializeField] private MoneyAddText _moneyAddText;

    private void Execute()
    {
        transform.localPosition = new Vector3(0, 0, 0);
        _moneyAddText.gameObject.SetActive(false);
    }
}
