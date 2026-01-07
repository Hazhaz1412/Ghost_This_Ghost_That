using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioMixer audioMixer;

    public float masterVolume = 1f;
    public float musicVolume = 1f;
    public float sfxVolume = 1f;

    public float tempMasterVolume = 1f;
    public float tempMusicVolume = 1f;
    public float tempSfxVolume = 1f;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadSettings();
        ApplyAllVolumes();
    }

    public void SetTempMaster(float value)
    {
        tempMasterVolume = value;
        SetMixer("MasterVolume", value);
    }

    public void SetTempMusic(float value)
    {
        tempMusicVolume = value;
        SetMixer("MusicVolume", value);
    }

    public void SetTempSFX(float value)
    {
        tempSfxVolume = value;
        SetMixer("SFXVolume", value);
    }

    public void ApplySettings()
    {
        masterVolume = tempMasterVolume;
        musicVolume = tempMusicVolume;
        sfxVolume = tempSfxVolume;

        ApplyAllVolumes();
        SaveSettings();
    }

    public void RevertSettings()
    {
        tempMasterVolume = masterVolume;
        tempMusicVolume = musicVolume;
        tempSfxVolume = sfxVolume;

        ApplyAllVolumes();
    }

    void ApplyAllVolumes()
    {
        SetMixer("MasterVolume", masterVolume);
        SetMixer("MusicVolume", musicVolume);
        SetMixer("SFXVolume", sfxVolume);
    }

    void SetMixer(string param, float value)
    {
        audioMixer.SetFloat(
            param,
            Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20
        );
    }

    void SaveSettings()
    {
        PlayerPrefs.SetFloat("MasterVolume", masterVolume);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
        PlayerPrefs.Save();
    }

    void LoadSettings()
    {
        masterVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);

        tempMasterVolume = masterVolume;
        tempMusicVolume = musicVolume;
        tempSfxVolume = sfxVolume;
    }
}
