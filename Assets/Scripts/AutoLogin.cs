using UnityEngine;
using UnityEngine.SceneManagement;
 
public class AutoLogin : MonoBehaviour
{ 
    [SerializeField] private bool autoLoginOnStart = true;
    [SerializeField] private string gameSceneName = "HuanSandbox";  
     
    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private TMPro.TextMeshProUGUI statusText;

    private async void Start()
    {
        if (!autoLoginOnStart) return;

        ShowLoading(true, "Đang kết nối..."); 
        if (NakamaManager.Instance.IsAuthenticated)
        {
            ShowLoading(true, "Chào mừng trở lại!");
            await System.Threading.Tasks.Task.Delay(500);
            LoadGameScene();
            return;
        } 
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
