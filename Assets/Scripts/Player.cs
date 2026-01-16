using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(PlayerSelectCardInteraction), typeof(PlayerSummonGhostInteraction), typeof(PlayerSelectGhostInteraction))]
    [RequireComponent(typeof(PlayerBattleGhostInteraction))]
    public class Player : MonoBehaviour
    {
        public enum PlayerState
        {
            Idle,
            SelectCard,
            SelectGhost,
        };

        [HideInInspector]
        public PlayerState State;

        [HideInInspector]
        public Card SelectedCard;

        [HideInInspector]
        public Ghost SelectedGhost;
    }
}
