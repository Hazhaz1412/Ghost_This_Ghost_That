using UnityEngine;

public class SettingButton : MonoBehaviour
{
    [SerializeField]
    private SettingActive settingActive;

    void Start()
    {
        
    }

    // Update is called once per frame
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
}
