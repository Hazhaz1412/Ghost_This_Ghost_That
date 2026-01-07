using UnityEngine;
using System.IO;
public class DeckSlot : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    string path = Path.Combine(Application.persistentDataPath, "deck.json");
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
