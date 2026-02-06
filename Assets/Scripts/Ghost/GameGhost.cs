using Game.Enums;
using Game.ScriptableObjects;
using UnityEngine;

namespace Game.Ghost
{
    public class GameGhost : MonoBehaviour
    {
        [SerializeField]
        private ResourcesSO _resourcesData;

        public CardIndexEnum CardId;

        [HideInInspector]
        public int GhostAttack;

        [HideInInspector]
        public int GhostHealth;

        public bool IsAlive { get; private set; }

        private void Awake()
        {
            IsAlive = true;
        }

        private void Start()
        {
            CardSO cardData = _resourcesData.LoadCardData<CardSO>(CardId);
            GhostAttack = cardData.GhostAttack;
            GhostHealth = cardData.GhostHealth;
        }

        private void Update()
        {
            if (!IsAlive)
            {
                return;
            }

            if (GhostHealth <= 0)
            {
                IsAlive = false;
            }
        }
    }
}
