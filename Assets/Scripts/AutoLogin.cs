using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Tự động đăng nhập khi mở game
/// Attach vào GameObject trong scene đầu tiên
/// </summary>
public class AutoLogin : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private bool autoLoginOnStart = true;
    [SerializeField] private string gameSceneName = "HuanSandbox"; // Tên scene game chính
    
    [Header("UI (Optional)")]
    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private TMPro.TextMeshProUGUI statusText;

    private async void Start()
    {
        if (!autoLoginOnStart) return;

        ShowLoading(true, "Đang kết nối...");

        // Kiểm tra đã login trước đó chưa
        if (NakamaManager.Instance.IsAuthenticated)
        {
            ShowLoading(true, "Chào mừng trở lại!");
            await System.Threading.Tasks.Task.Delay(500);
            LoadGameScene();
            return;
        }

        // Auto login bằng Device ID
        ShowLoading(true, "Đang vào game...");
        bool success = await NakamaManager.Instance.AuthenticateDevice();

        if (success)
        {
            ShowLoading(true, "Đăng nhập thành công!");
            await System.Threading.Tasks.Task.Delay(1000);
            LoadGameScene();
        }
        else
        {
            ShowLoading(false, "");
            Debug.LogError("[AutoLogin] Failed to authenticate!");
            // Giữ ở màn login để user thử lại
        }
    }

    private void LoadGameScene()
    {
        if (!string.IsNullOrEmpty(gameSceneName))
        {
            SceneManager.LoadScene(gameSceneName);
        }
        else
        {
            ShowLoading(false, "");
            Debug.Log("[AutoLogin] No game scene specified. Staying on login screen.");
        }
    }

    private void ShowLoading(bool show, string message = "")
    {
        if (loadingPanel != null)
            loadingPanel.SetActive(show);

        if (statusText != null)
            statusText.text = message;
    }
}
