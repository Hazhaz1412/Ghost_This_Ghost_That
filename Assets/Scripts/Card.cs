using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public enum CardType
{
    Fighter,
    Support,
    Spell, 
}

public enum SpellType
{
    Fast,
    Slow,
    Burst,
}


[Serializable]
public class CardData
{
    public string id;
    public CardType cardType;
    public SpellType spellType;
    public string cardName;
    public Sprite cardSprite; 
    public float hp;
    public float dmg;
    public float cost;
    public string description;
    public string skill;
    public int countInDeck;
}

public class Card : MonoBehaviour
{
    public CardData data;

    [SerializeField] private Image cardImage;
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private Button cardButton;

    //public event Action<Card> OnCardClicked;

    void Awake()
    {
        // if (cardButton != null)
        //     cardButton.onClick.AddListener(OnClick);
    }
    public void Setup(CardData cardData)
    {
        data = cardData;
        UpdateUI();
    } 

    public void OnClick()
    {
        //OnCardClicked?.Invoke(this);
        DeckManager.CardInteract(data.id);
        UnityEngine.Debug.Log("Clicked !" + data.id);
    }

    public void SetCount(int count)
    {
        data.countInDeck = count;
        UpdateCountDisplay();
    }

    private void UpdateUI()
    {
        if (data == null) return;

        if (cardImage != null && data.cardSprite != null)
            cardImage.sprite = data.cardSprite;

        UpdateCountDisplay();
    }

    private void UpdateCountDisplay()
    {
        if (countText == null) return;

        bool hasCard = data.countInDeck > 0;
        countText.gameObject.SetActive(hasCard);
        if (hasCard)
            countText.text = $"x{data.countInDeck}";
    }

    private void OnDestroy()
    {
        if (cardButton != null)
            cardButton.onClick.RemoveListener(OnClick);
    }
}