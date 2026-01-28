using System.Collections.Generic;
using Game.Card;
using Game.Enums;
using Game.Ghost;
using Game.Interfaces;
using UnityEngine;

namespace Game.Player
{
    [RequireComponent(typeof(IDrawer), typeof(IMulliganSetup))]
    public class GamePlayer : MonoBehaviour
    {
        public enum PlayerState
        {
            Idle,
            SelectCard,
            SelectGhost,
        };

        public enum PlayerGameState
        {
            PlayerIdle,
            PlayerMulligan,
            PlayerFinishedMulligan,
            PlayerPlay,
            PlayerAttack,
        }

        [field: SerializeField]
        public GameCard CardPrefab { get; private set; }

        [field: SerializeField]
        public GameObject InteractGhostZone { get; private set; }

        [field: SerializeField]
        public Transform SummonGhostZone { get; private set; }

        [field: SerializeField]
        public Transform GhostZone { get; private set; }

        [field: SerializeField]
        public Transform MulliganZone { get; private set; }

        [field: SerializeField]
        public Transform Hand { get; private set; }

        [field: SerializeField]
        public PlayerAttackTarget AttackTarget { get; private set; }

        [field: SerializeField]
        public GamePlayer Enemy { get; private set; }

        [SerializeField]
        private List<CardIndexEnum> _deck;

        // NOTE: [HideInInspector]
        public PlayerState State;

        // NOTE: [HideInInspector]
        public PlayerGameState GameState;

        [HideInInspector]
        public int Hp;

        [HideInInspector]
        public int Mana;

        [HideInInspector]
        public GameCard SelectedCard;

        [HideInInspector]
        public GameGhost SelectedGhost;

        [HideInInspector]
        public bool IsAttackTurn;

        public CardIndexEnum Peek()
        {
            if (_deck.Count <= 0)
            {
                return CardIndexEnum.INVALID;
            }
            return _deck[0];
        }

        public void Pop()
        {
            if (_deck.Count <= 0)
            {
                return;
            }
            _deck.RemoveAt(0);
        }

        public void AppendCard(CardIndexEnum card)
        {
            _deck.Add(card);
        }

        public void PrependCard(CardIndexEnum card)
        {
            _deck.Insert(0, card);
        }

        public void ShuffleDeck()
        {
            for (int i = 0; i < _deck.Count; i++)
            {
                int swapIdx = Random.Range(0, _deck.Count - 1);
                (_deck[i], _deck[swapIdx]) = (_deck[swapIdx], _deck[i]);
            }
        }
    }
}
