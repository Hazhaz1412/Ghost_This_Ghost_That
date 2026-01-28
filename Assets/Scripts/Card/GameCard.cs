using Game.Enums;
using Game.ScriptableObjects;
using UnityEngine;

namespace Game.Card
{
    public class GameCard : MonoBehaviour
    {
        [SerializeField]
        private ResourcesSO _resourcesData;

        [HideInInspector]
        public bool Mulligan;

        public CardIndexEnum CardId;

        public CardTypeEnum CardType { get; private set; }

        [HideInInspector]
        public int CardMana;

        public int GhostAttack { get; private set; }

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
