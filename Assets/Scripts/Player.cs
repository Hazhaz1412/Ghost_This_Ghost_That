using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(PlayerSelectCardInteraction), typeof(PlayerSummonGhostInteraction))]
    public class Player : MonoBehaviour
    {
        [HideInInspector]
        public Card SelectedCard;
    }
}
