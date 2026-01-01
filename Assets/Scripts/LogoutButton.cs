using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LogoutButton : MonoBehaviour
{
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnLogoutClicked);
    }

    private void OnLogoutClicked()
    {
        if (NakamaManager.Instance != null)
        {
            NakamaManager.Instance.Logout();
        }
        else
        {
            Debug.Log("NakamaManager not found!");
        }
    }

    private void OnDestroy()
    {
        if (button != null)
        {
            button.onClick.RemoveListener(OnLogoutClicked);
        }
    }
}
