using UnityEngine;

public class SettingActive : MonoBehaviour
{
    public enum SettingState
    {
        Active,
        DeActive,
    }
    [SerializeField] 
    public SettingState settingState = SettingState.DeActive;
    
    [SerializeField] 
    private CanvasGroup canvasGroup;
    public void SetState(SettingState newState)
    {
        settingState = newState;
        OpenPanel();
    }
    void Start()
    {
        OpenPanel();
    }
    void OpenPanel()
    {
        canvasGroup.gameObject.SetActive(settingState == SettingState.Active);
    }
}
