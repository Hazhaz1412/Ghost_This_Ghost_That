using UnityEngine;
using UnityEngine.UI;

public class DeckActive : MonoBehaviour
{
    public enum DeckState
    {
        Active,
        DeActive,
    }
    [SerializeField] 
    public DeckState deckState = DeckState.DeActive;
    
    [SerializeField] 
    private CanvasGroup canvasGroup;
    public void SetState(DeckState newState)
    {
        deckState = newState;
        OpenPanel();
    }
    void Start()
    {
        OpenPanel();
    }
    void OpenPanel()
    {
        canvasGroup.gameObject.SetActive(deckState == DeckState.Active);
    }
}
