using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    [SerializeField] private SettingActive settingActive;

    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    void OnEnable()
    {
        if (AudioManager.Instance != null)
        {
            masterSlider.value = AudioManager.Instance.tempMasterVolume;
            musicSlider.value = AudioManager.Instance.tempMusicVolume;
            sfxSlider.value = AudioManager.Instance.tempSfxVolume;
        }
    }

    public void OnMasterChanged(float value)
    {
        AudioManager.Instance.SetTempMaster(value);
    }

    public void OnMusicChanged(float value)
    {
        AudioManager.Instance.SetTempMusic(value);
    }

    public void OnSFXChanged(float value)
    {
        AudioManager.Instance.SetTempSFX(value);
    }

    public void OnApply()
    {
        AudioManager.Instance.ApplySettings();
        settingActive.SetState(SettingActive.SettingState.DeActive);
    }

    public void OnBack()
    {
        AudioManager.Instance.RevertSettings();
        settingActive.SetState(SettingActive.SettingState.DeActive);
    }

    public void OnReset()
    {
        masterSlider.value = 1f;
        musicSlider.value = 1f;
        sfxSlider.value = 1f;

        AudioManager.Instance.SetTempMaster(1f);
        AudioManager.Instance.SetTempMusic(1f);
        AudioManager.Instance.SetTempSFX(1f);
    }
}
