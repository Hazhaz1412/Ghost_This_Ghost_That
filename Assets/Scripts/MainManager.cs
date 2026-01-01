using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public enum MainMenuPanel
    {
        Active,
        DeActive,
    }
    [SerializeField] public MainMenuPanel panel = MainMenuPanel.DeActive;
    [SerializeField] 
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        panel = MainMenuPanel.DeActive;
    }

    public void SetState(MainMenuPanel newState)
    {
        panel = newState;
        OpenPanel();
    }
    void Start()
    {
        OpenPanel();
    }
    void OpenPanel()
    {
        canvasGroup.gameObject.SetActive(panel == MainMenuPanel.Active);
    }
    public void OnClick()
    {  
        if (NakamaManager.Instance != null && NakamaManager.Instance.IsAuthenticated)
        { 
            SceneManager.LoadScene(nameof(SceneEnum.HuanSandbox));
            return;
        }
 
        if(panel == MainMenuPanel.Active)
        {
            SetState(MainMenuPanel.DeActive);
        }
        else
        {
            SetState(MainMenuPanel.Active);
        } 
    }
}
