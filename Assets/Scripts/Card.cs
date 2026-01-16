using Game.Enums;
using Game.ScriptableObjects;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(CardLayout))]
    public class Card : MonoBehaviour
    {
        [SerializeField]
        private ResourcesSO _resourcesData;

        public CardIndexEnum CardId;

        public CardTypeEnum CardType { get; private set; }

        [HideInInspector]
        public int CardMana;

        [HideInInspector]
        public int GhostAttack { get; private set; }

        [HideInInspector]
        public int GhostHealth { get; private set; }

        private void Start()
        {
            CardSO cardData = _resourcesData.LoadCardData<CardSO>(CardId);
            CardType = cardData.CardType;
            CardMana = cardData.CardCost;
            GhostAttack = cardData.GhostAttack;
            GhostHealth = cardData.GhostHealth;
        }
    }
}
