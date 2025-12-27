using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using System.Diagnostics;


public class DeckManager : MonoBehaviour
{
    public static DeckManager Instance;
    [Header("Card Database")]
    public CardDataInfo cardDatabase;
    public RectTransform deckContent;
    public GameObject cardPrefab;

    [Header("User's card")]
    public RectTransform userDeckContent;
    public GameObject userDeckPrefab;


    private Dictionary<string, int> userDeck = new();
    private Dictionary<string, Card> deck = new();

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        StartCoroutine(Init()); 
    }

    public static void CardInteract(string idCard) {
        if (Instance.deck[idCard].data.countInDeck == 0) {
            UnityEngine.Debug.Log("You have reach maximum");
        } else {
            Instance.deck[idCard].data.countInDeck--;
            // Lấy CardData từ deck và truyền vào
            Instance.AddUserCard(Instance.deck[idCard].data);
            UnityEngine.Debug.Log("+1");
        }
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
    public void AddUserCard(CardData data) {
        if (userDeck.ContainsKey(data.id))
        {
            userDeck[data.id]++;
            return;
        }
        userDeck[data.id] = 1;
        GameObject obj = Instantiate(userDeckPrefab, userDeckContent);
        Card cardUI = obj.GetComponent<Card>();
        cardUI.Setup(data);
        Transform imgTf = obj.transform.Find("Background");
        if (imgTf != null)
        {
            var image = imgTf.GetComponent<UnityEngine.UI.Image>();
            image.sprite = LoadSprite(data.id);
        }
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
