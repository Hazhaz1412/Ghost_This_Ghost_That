using System.Collections.Generic;
using Game.Enums;
using UnityEngine;

namespace Game
{
    // [RequireComponent(typeof(PlayerSelectCardInteraction), typeof(PlayerSummonGhostInteraction), typeof(PlayerSelectGhostInteraction))]
    // [RequireComponent(typeof(PlayerBattleGhostInteraction), typeof(PlayerMulliganCardInteraction))]
    public class Player : MonoBehaviour
    {
        public enum PlayerState
        {
            Idle,
            SelectCard,
            SelectGhost,
        };

        [field: SerializeField]
        public GameObject InteractGhostZone { get; private set; }

        [field: SerializeField]
        public Transform SummonGhostZone { get; private set; }

        [field: SerializeField]
        public Transform Hand { get; private set; }

        [field: SerializeField]
        public PlayerAttackTarget AttackTarget { get; private set; }

        [field: SerializeField]
        public List<CardIndexEnum> Deck { get; private set; }

        [HideInInspector]
        public PlayerState State;

        [HideInInspector]
        public int Hp;

        [HideInInspector]
        public int Mana;

        [HideInInspector]
        public Card SelectedCard;

        [HideInInspector]
        public Ghost SelectedGhost;

        [HideInInspector]
        public bool FinishMulligan;
    }
}
