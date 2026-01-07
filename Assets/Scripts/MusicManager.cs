using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    public AudioSource musicSource;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        PlayMusicForScene(SceneManager.GetActiveScene().name);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayMusicForScene(scene.name);
    }

    void PlayMusicForScene(string sceneName)
    {
        AudioClip clip = GetMusicClip(sceneName);

        if (clip == null || musicSource.clip == clip)
            return;

        musicSource.clip = clip;
        musicSource.Play();
    }

    AudioClip GetMusicClip(string sceneName)
    {
        switch (sceneName)
        {
            case "HuanSandbox":
                return Resources.Load<AudioClip>("Audios/BackgroundMusic/LobbyMusic/671993__infinita08__creeping-ghoul-loop");
            case "QueueScene":
                return Resources.Load<AudioClip>("Audios/BackgroundMusic/InGame/595853__szegvari__old-ritual-fantasy-background-soundscape-haunted-atmo-music-synth-drum-orchestra-master");
            default:
                return null;
        }
    }
}
