using UnityEngine;
using UnityEngine.Events;

public class Fader : MonoBehaviour
{
    private enum FaderState
    {
        Inactive,
        FadeIn,
        FadeOut,
    }

    [SerializeField]
    private CanvasGroup _fadeObject;

    [SerializeField]
    private float _fadeInDuration;

    [SerializeField]
    private float _fadeOutDuration;

    public event UnityAction OnFadeInFinish;
    public event UnityAction OnFadeOutFinish;

    private FaderState _state;
    private float _timer;

    public void FadeIn()
    {
        _state = FaderState.FadeIn;
        _fadeObject.alpha = 0;
        _timer = _fadeInDuration;
    }

    public void FadeOut()
    {
        _state = FaderState.FadeOut;
        _fadeObject.alpha = 1;
        _timer = _fadeOutDuration;
    }

    private void Update()
    {
        _timer -= Time.deltaTime;

        float t;
        float duration = 0;
        float targetAlpha = 0;
        float timerInverse;
        UnityAction ev = null;

        switch (_state)
        {
            case FaderState.FadeIn:
                {
                    duration = _fadeInDuration;
                    targetAlpha = 1;
                    ev = OnFadeInFinish;
                }
                break;
            case FaderState.FadeOut:
                {
                    duration = _fadeOutDuration;
                    targetAlpha = 0;
                    ev = OnFadeOutFinish;
                }
                break;
            default:
                break;
        }

        timerInverse = duration - _timer;
        t = timerInverse / duration;

        switch (_state)
        {
            case FaderState.FadeIn:
            case FaderState.FadeOut:
                {
                    _fadeObject.alpha = Mathf.Lerp(_fadeObject.alpha, targetAlpha, t);

                    if (_timer <= 0)
                    {
                        ev?.Invoke();
                        _state = FaderState.Inactive;
                    }
                }
                break;
            case FaderState.Inactive:
            default:
                break;
        }
    }
}
