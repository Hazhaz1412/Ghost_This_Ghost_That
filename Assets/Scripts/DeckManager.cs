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
    public TMP_Text cardCount;
 
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

    public static void CardInteract(string idCard) 
    { 
        
        int crn = CardCount();
        if(!valid(crn)) return;
        if (!Instance.deck.ContainsKey(idCard)) 
        {
            
            CardCount();
            return;
        }
        CardData data = Instance.deck[idCard].data; 
        
        if (data.countInDeck > 0) 
        { 

            data.countInDeck--;  
            Instance.AddUserCard(data); 
            Instance.QuantityChange(Instance.deck[idCard].gameObject, data.countInDeck, true);
            
            CardCount();
        }
    }

    public static void UserCardInteract(string idCard) 
    {
        
        if (!Instance.userDeck.ContainsKey(idCard)) return; 
        Instance.userDeck[idCard]--;

        if (Instance.deck.ContainsKey(idCard))
        {
            CardData data = Instance.deck[idCard].data;
            data.countInDeck++;
            CardCount();
            Instance.QuantityChange(Instance.deck[idCard].gameObject, data.countInDeck, true);
        }

        if (Instance.userDeck[idCard] <= 0)
        {
            GameObject objToRemove = Instance.userDeckUI[idCard];
            Instance.userDeck.Remove(idCard);
            Instance.userDeckUI.Remove(idCard);
            Destroy(objToRemove);
        }
        else
        {
            GameObject objToUpdate = Instance.userDeckUI[idCard];
            Instance.QuantityChange(objToUpdate, Instance.userDeck[idCard], false);
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
 
    public void AddUserCard(CardData data) 
    {   
        int crn = CardCount();
        if(!valid(crn)) return;
        if (userDeck.ContainsKey(data.id)) 
        {
            userDeck[data.id]++; 
            
            CardCount();
        } 
        else 
        {
            userDeck[data.id] = 1;
            
            CardCount();
        }  

        if (userDeckUI.ContainsKey(data.id))
        {  
            QuantityChange(userDeckUI[data.id], userDeck[data.id], false);
            
            CardCount();
        }
        else
        {
            
            GameObject obj = Instantiate(userDeckPrefab, userDeckContent);
            userDeckUI.Add(data.id, obj); 

            Button btn = obj.GetComponent<Button>();
            if (btn == null) btn = obj.AddComponent<Button>(); 
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() => UserCardInteract(data.id));

            Transform imgTf = obj.transform.Find("Panel/Background");
            if (imgTf != null)
            {
                var image = imgTf.GetComponent<UnityEngine.UI.Image>();
                if (image != null) image.sprite = LoadVariantSprite(data.id);
            }
 
            Transform cardNameTf = obj.transform.Find("Panel/CardName"); 
            if (cardNameTf != null)
            {
                var cardText = cardNameTf.GetComponent<TextMeshProUGUI>(); 
                if (cardText != null) cardText.text = data.cardName; 
            } 
            
            QuantityChange(obj, userDeck[data.id], false);
            
            CardCount();
        }
    } 
    public static bool valid(int count)
    {
        if (count >= 40) return false;
        return true;
    }
    public static int CardCount() //TODO: After make the deck-load, make this run at init and +1 -1 the deck.
    {
        int total = 0;
        foreach (var count in Instance.userDeck.Values)
        {
            total += count;
        }
        Instance.cardCount.text = total.ToString();
        if (total >= 40)
        {
            Instance.cardCount.color = UnityEngine.Color.red;
        }
        else
        {
            Instance.cardCount.color = UnityEngine.Color.white;
        }
        return total;
    }
    private void QuantityChange(GameObject cardObj, int quantity, bool databaseCard)
    { 
        
        int crn = CardCount();
        if(!valid(crn)) return;
        string path = databaseCard ? "Quantity" : "Panel/Quantity";
        Transform qtyTf = cardObj.transform.Find(path);
        
        if (qtyTf != null)
        {
            var qtyText = qtyTf.GetComponent<TextMeshProUGUI>();
            if (qtyText != null)
            {
                qtyText.text = quantity.ToString();
            }
            else 
            { 
                var textLegacy = qtyTf.GetComponent<Text>();
                if(textLegacy != null) textLegacy.text = quantity.ToString();
            }
        } 
    }

    public void AddCard(CardData data)
    { 
        
        int crn = CardCount();
        if(!valid(crn)) return;
        if (deck.ContainsKey(data.id))
        {
            QuantityChange(deck[data.id].gameObject, data.countInDeck, true);
            
            CardCount();
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
          
        QuantityChange(obj, data.countInDeck, true);
        deck.Add(data.id, cardUI);
        CardCount();
    }
}