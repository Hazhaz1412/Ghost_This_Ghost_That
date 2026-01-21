using Game.Enums;
using Game.ScriptableObjects;
using UnityEngine;

namespace Game
{
    // [RequireComponent(typeof(GhostLayout))]
    public class Ghost : MonoBehaviour
    {
        [SerializeField]
        private ResourcesSO _resourcesData;

        public CardIndexEnum CardId;

        [HideInInspector]
        public int GhostAttack;

        [HideInInspector]
        public int GhostHealth;

        private void Start()
        {
            CardSO cardData = _resourcesData.LoadCardData<CardSO>(CardId);
            GhostAttack = cardData.GhostAttack;
            GhostHealth = cardData.GhostHealth;
        }

        private void Update()
        {
            if (GhostHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
