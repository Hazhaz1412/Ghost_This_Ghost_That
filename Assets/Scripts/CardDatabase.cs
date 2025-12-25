using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CardDatabase", menuName = "Cards/Card Database")]
public class CardDatabase : ScriptableObject
{
    public List<CardData> allCards = new List<CardData>();
    
    public CardData GetCardById(string id)
    {
        return allCards.Find(card => card.id == id);
    }
    
    public CardData GetCardByName(string cardName)
    {
        return allCards.Find(card => card.cardName == cardName);
    }
}
