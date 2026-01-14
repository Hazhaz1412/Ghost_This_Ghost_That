using Game.Enums;
using UnityEngine;

namespace Game.ScriptableObjects
{
    [CreateAssetMenu(fileName = "CardSO", menuName = "Scriptable Objects/CardSO")]
    public class CardSO : ScriptableObject
    {
        [field: SerializeField]
        public CardTypeEnum CardType { get; private set; }

        [field: SerializeField]
        public int CardCost { get; private set; }

        [field: SerializeField]
        public int GhostAttack { get; private set; }

        [field: SerializeField]
        public int GhostHealth { get; private set; }
    }
}
