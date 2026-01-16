using System.Collections.Generic;
using Game.Enums;
using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private Card _cardPrefab;

        [SerializeField]
        private Ghost _ghostPrefab;

        [SerializeField]
        private GameObject _spellZoneHl;

        [SerializeField]
        private Transform _spellZone;

        [SerializeField]
        private GameObject _player1GhostZoneHl;

        [SerializeField]
        private Transform _player1GhostZone;

        [SerializeField]
        private Transform _player2GhostZone;

        [SerializeField]
        private Transform _player1Hand;

        [SerializeField]
        private Player _player1;

        private HashSet<Ghost> _hasAttacked;

        private List<CardIndexEnum> _player1Deck;

        private void Awake()
        {
            _hasAttacked = new();

            _player1Deck = new List<CardIndexEnum>
            {
                CardIndexEnum.AM_BINH,
                CardIndexEnum.BA_NEN_NHAN,
                CardIndexEnum.GO_CHEN_HON_VE,
                CardIndexEnum.HIEN_XAC_TRA_THU,
                CardIndexEnum.KINH_HIEN_HON,
                CardIndexEnum.LINH_MIEU,
                CardIndexEnum.LUOI_DAO_PHUC_HAN,
                CardIndexEnum.LUOI_GUOM_TRU_TA,
                CardIndexEnum.VONG_NHI,
                CardIndexEnum.VONG_HO_MENH,
                CardIndexEnum.TOI_YEM_BUA,
                CardIndexEnum.THIEN_LINH_CAI,
                CardIndexEnum.THAN_TRUNG,
                CardIndexEnum.QUY_MOT_DO,
                CardIndexEnum.QUY_DA_XOA,
                CardIndexEnum.QUY_CAU,
                CardIndexEnum.QUAN_TAI_HIEN_TE,
                CardIndexEnum.ONG_KE,
                CardIndexEnum.MA_VU_DAI,
                CardIndexEnum.MA_TROI,
            };

            _player1.GetComponent<PlayerSelectCardInteraction>().OnCardSelectRequest += OnPlayer1CardSelect;
            _player1.GetComponent<PlayerSummonGhostInteraction>().OnGhostSummonRequest += OnPlayer1GhostSummon;
            _player1.GetComponent<PlayerSelectGhostInteraction>().OnGhostSelectRequest += OnPlayer1GhostSelect;
            _player1.GetComponent<PlayerBattleGhostInteraction>().OnGhostBattleRequest += OnPlayer1GhostBattle;
        }

        private void Start()
        {
            ShuffleDeck(_player1Deck);
            for (int i = 0; i < 5; i++)
            {
                Draw(_player1.tag, _player1Deck, _player1Hand);
            }
        }

        private void OnPlayer1CardSelect(Card card)
        {
            if (card != null)
            {
                _player1GhostZoneHl.SetActive(card.CardType == CardTypeEnum.Ghost);
                _spellZoneHl.SetActive(card.CardType != CardTypeEnum.Ghost);
            }
            else
            {
                _player1GhostZoneHl.SetActive(false);
                _spellZoneHl.SetActive(false);
            }
        }

        private void OnPlayer1GhostSummon(Card card)
        {
            if (card != null && card.CardType == CardTypeEnum.Ghost)
            {
                Ghost ghost = Instantiate(_ghostPrefab, _player1GhostZone);
                ghost.CardId = card.CardId;
                ghost.tag = _player1.tag;

                Destroy(card.gameObject);

                _player1.GetComponent<PlayerSelectCardInteraction>().ResetState();
            }
        }

        private void OnPlayer1GhostSelect(Ghost ghost)
        {
            if (ghost != null && !_hasAttacked.Contains(ghost))
            {
                foreach (Ghost g in _player2GhostZone.GetComponentsInChildren<Ghost>())
                {
                    if (g.TryGetComponent(out GhostLayout ghostLayout))
                    {
                        ghostLayout.HighlightEnemy();
                    }
                }
            }
            else
            {
                foreach (Ghost g in _player2GhostZone.GetComponentsInChildren<Ghost>())
                {
                    if (g.TryGetComponent(out GhostLayout ghostLayout))
                    {
                        ghostLayout.ResetHighlight();
                    }
                }
            }
        }

        private void OnPlayer1GhostBattle(Ghost attacker, Ghost defender)
        {
            attacker.GhostHealth -= defender.GhostAttack;
            defender.GhostHealth -= attacker.GhostAttack;

            _hasAttacked.Add(attacker);
            _player1.GetComponent<PlayerSelectGhostInteraction>().ResetState();
        }

        private void Draw(string playerTag, List<CardIndexEnum> deck, Transform hand)
        {
            Card card = Instantiate(_cardPrefab, hand);
            card.CardId = deck[0];
            card.tag = playerTag;
            deck.RemoveAt(0);
        }

        private void ShuffleDeck(List<CardIndexEnum> deck)
        {
            for (int i = 0; i < deck.Count; i++)
            {
                int swapIdx = Random.Range(0, deck.Count - 1);
                (deck[i], deck[swapIdx]) = (deck[swapIdx], deck[i]);
            }
        }
    }
}
