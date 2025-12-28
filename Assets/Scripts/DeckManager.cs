using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using TMPro; 

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
     
    private Dictionary<string, GameObject> userDeckUI = new();
    
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
        if (!Instance.deck.ContainsKey(idCard)) return;

        if (Instance.deck[idCard].data.countInDeck == 0) {
            UnityEngine.Debug.Log("You have reached maximum");
        } else {
            Instance.deck[idCard].data.countInDeck--; 
            Instance.AddUserCard(Instance.deck[idCard].data);
            UnityEngine.Debug.Log("+1 Card: " + idCard);
        }
    }

    IEnumerator Init()
    {
        yield return null;  
        foreach (var data in cardDatabase.cardDatas)
        {
            if (data.countInDeck > 0) AddCard(data);
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(deckContent);
    }

    private Sprite LoadSprite(string id)
    {
        string folder = id.StartsWith("c") ? "CardsGhost" : "UtilityCard";
        return Resources.Load<Sprite>($"{folder}/{id}");
    }
    
    private Sprite LoadVariantSprite(string id)
    {
        string folder = id.StartsWith("c") ? "CardsGhost/VariantGhost" : "UtilityCard";
        return Resources.Load<Sprite>($"{folder}/{id}");
    }
 
    public void AddUserCard(CardData data) { 
        if (userDeck.ContainsKey(data.id)) {
            userDeck[data.id]++; 
        } else {
            userDeck[data.id] = 1;
        } 
        if (userDeckUI.ContainsKey(data.id))
        { 
            QuantityChange(userDeckUI[data.id], userDeck[data.id]);
        }
        else
        {
            GameObject obj = Instantiate(userDeckPrefab, userDeckContent);
            
            userDeckUI.Add(data.id, obj);

            Transform imgTf = obj.transform.Find("Panel/Background");
            if (imgTf != null)
            {
                var image = imgTf.GetComponent<UnityEngine.UI.Image>();
                if (image != null) image.sprite = LoadVariantSprite(data.id);
            }

            // Set số lượng ban đầu
            QuantityChange(obj, userDeck[data.id]);
        }
    }

    private void QuantityChange(GameObject cardObj, int quantity)
    {
        Transform qtyTf = cardObj.transform.Find("Panel/Quantity");
        
        if (qtyTf != null)
        {
            var qtyText = qtyTf.GetComponent<TextMeshProUGUI>();
            
            if (qtyText != null)
            {
                qtyText.text = quantity.ToString();
            }
            
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