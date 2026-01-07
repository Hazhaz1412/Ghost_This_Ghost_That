using UnityEngine;

public class SettingButton : MonoBehaviour
{
    [SerializeField]
    private SettingActive settingActive;
    [SerializeField] 
    private SettingUI settingUI;

    public void OnClick()
    {
        if(settingActive.settingState == SettingActive.SettingState.Active)
        {
            settingActive.SetState(SettingActive.SettingState.DeActive);
        }
        else
        {
            settingActive.SetState(SettingActive.SettingState.Active);
        } 
    }

    public void OnBackClick()
    {
        settingUI.OnBack();
        settingActive.SetState(SettingActive.SettingState.DeActive);
    }

    public void OnApplyClick()
    {
        settingUI.OnApply();
        settingActive.SetState(SettingActive.SettingState.DeActive);
    }
}
