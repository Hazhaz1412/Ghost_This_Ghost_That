using UnityEngine;

public class DeckButton : MonoBehaviour
{
    [SerializeField]
    private DeckActive deckActive;

    void Start()
    {
    }

    public void OnClick()
    {
        if(deckActive.deckState == DeckActive.DeckState.Active)
        {
            deckActive.SetState(DeckActive.DeckState.DeActive);
        }
        else
        {
            deckActive.SetState(DeckActive.DeckState.Active);
        } 
    }
}
