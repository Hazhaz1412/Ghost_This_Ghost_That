using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Fader))]
public class IntroTransition : MonoBehaviour
{
    [SerializeField]
    private float _waitDuration;

    private Fader _fader;
    private float _waitTimer;
    private bool _isWaiting;
    private bool _transitionFinished;

    private void Awake()
    {
        _fader = GetComponent<Fader>();
        _waitTimer = _waitDuration;
        _isWaiting = false;
        _transitionFinished = false;
    }

    private void Start()
    {
        _fader.OnFadeInFinish += OnFadeInFinished;
        _fader.OnFadeOutFinish += OnFadeOutFinished;
        _fader.FadeIn();

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
                _fader.FadeOut();
            }
        }
    }

    private void OnFadeInFinished()
    {
        _isWaiting = true;
    }

    private void OnFadeOutFinished()
    {
        _transitionFinished = true;
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
