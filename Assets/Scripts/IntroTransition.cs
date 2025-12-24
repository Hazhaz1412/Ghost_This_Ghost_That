using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Fader))]
public class IntroTransition : MonoBehaviour
{
    [SerializeField]
    private float _waitDuration;

    private Fader _overlayFader;
    private float _waitTimer;
    private bool _isWaiting;
    private bool _transitionFinished;

    private void Awake()
    {
        _overlayFader = GetComponent<Fader>();
        _waitTimer = _waitDuration;
        _isWaiting = false;
        _transitionFinished = false;
    }

    private void Start()
    {
        _overlayFader.OnFadeInFinish += OnFadeInFinished;
        _overlayFader.OnFadeOutFinish += OnFadeOutFinished;
        _overlayFader.FadeOut();

        StartCoroutine(LoadMainMenu());
    }

    private void Update()
    {
        if (_isWaiting)
        {
            _waitTimer -= Time.deltaTime;
            if (_waitTimer <= 0)
            {
                _isWaiting = false;
                _overlayFader.FadeIn();
            }
        }
    }

    private void OnFadeInFinished()
    {
        _transitionFinished = true;
    }

    private void OnFadeOutFinished()
    {
        _isWaiting = true;
    }

    private IEnumerator LoadMainMenu()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(nameof(SceneEnum.MainMenu));
        op.allowSceneActivation = false;

        while (!op.isDone)
        {
            if (_transitionFinished)
            {
                op.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
