using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CardSpriteDatabase", menuName = "Cards/Card Sprite Database")]
public class CardSpriteDatabase : ScriptableObject
{
    [System.Serializable]
    public class CardSpriteEntry
    {
        public string id;
        public Sprite sprite;
    }

    public List<CardSpriteEntry> entries = new List<CardSpriteEntry>();

    public Sprite GetSprite(string id)
    {
        var entry = entries.Find(e => e.id == id);
        return entry != null ? entry.sprite : null;
    }
}
