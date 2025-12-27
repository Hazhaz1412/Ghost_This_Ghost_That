using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;


public class DeckManager : MonoBehaviour
{
    [Header("References")]
    public CardDataInfo cardDatabase;
    public RectTransform deckContent;
    public GameObject cardPrefab;

    private Dictionary<string, Card> deck = new();

    void Start()
    {
        StartCoroutine(Init()); 
    }
    IEnumerator Init()
    {
        yield return null;  

        foreach (var data in cardDatabase.cardDatas)
        {
            if (data.countInDeck > 0)
                AddCard(data);
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(deckContent);
    }
    // void LoadInitialDeck()
    // {
    //     foreach (var cardData in cardDatabase.cardDatas)
    //     {
    //         if (cardData.countInDeck > 0)
    //         {
    //             AddCard(cardData);
    //         }
    //     }
    // }
    private Sprite LoadSprite(string id)
    {
        string folder = "";

        if (id.StartsWith("c"))
            folder = "CardsGhost";
        else
            folder = "UtilityCard";
        return Resources.Load<Sprite>($"{folder}/{id}");
    }
    public void AddCard(CardData data)
    {
        if (deck.ContainsKey(data.id))
        {
            deck[data.id].SetCount(deck[data.id].data.countInDeck + 1);
            return;
        }
        GameObject obj = Instantiate(cardPrefab, deckContent);
        Card cardUI = obj.GetComponent<Card>();
        cardUI.Setup(data);
        Transform imgTf = obj.transform.Find("CardImage");
        if (imgTf != null)
        {
            var image = imgTf.GetComponent<UnityEngine.UI.Image>();
            image.sprite = LoadSprite(data.id);

        }

        deck.Add(data.id, cardUI);
    }
}
