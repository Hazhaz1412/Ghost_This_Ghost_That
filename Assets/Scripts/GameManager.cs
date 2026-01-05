using UnityEngine;

public class GameManager : MonoBehaviour
{
    private enum GameState
    {
        MulliganPhase,
        DrawPhase,
    }

    private TCardData[] _p1Deck;
    private TCardData[] _p2Deck;

    private void Awake()
    {
        _p1Deck = new TCardData[] {
            CardCollection.AM_BINH,
            CardCollection.MA_MAT_MAM,
            CardCollection.MA_LON,
            CardCollection.MA_LAI,
            CardCollection.MA_GA,
            CardCollection.MA_VU_DAI,
            CardCollection.MA_DOI,
            CardCollection.MA_DA,
            CardCollection.MA_CO,
            CardCollection.MA_CANG_SUNG,
        };
        _p2Deck = new TCardData[] {
            CardCollection.AM_BINH,
            CardCollection.MA_MAT_MAM,
            CardCollection.MA_LON,
            CardCollection.MA_LAI,
            CardCollection.MA_GA,
            CardCollection.MA_VU_DAI,
            CardCollection.MA_DOI,
            CardCollection.MA_DA,
            CardCollection.MA_CO,
            CardCollection.MA_CANG_SUNG,
        };
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

        }
    }
}
