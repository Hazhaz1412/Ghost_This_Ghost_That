using Game.Enums;
using Game.Structs;
using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        private enum GameState
        {
            GameStart,
            Mulligan,
            WaitForMulligan,
            DrawPhase,
            Player1Play,
            Player2Play,
            Player1Attack,
            Player2Attack,
        }

        private const int MulliganAmount = 4;

        private const int StartingHandAmount = 4;

        private const int StartingHp = 20;

        private const int StartingMana = 1;

        [SerializeField]
        private Card _cardPrefab;

        [SerializeField]
        private Ghost _ghostPrefab;

        [SerializeField]
        private GameObject _spellZoneHl;

        [SerializeField]
        private Transform _spellZone;

        [SerializeField]
        private Transform _mulliganZone;

        [SerializeField]
        private Player _player1;

        [SerializeField]
        private Player _player2;

        private GameState _state;

        private bool _player1FinishMulligan;

        private void Awake()
        {
            _player1.Hp = StartingHp;
            _player1.Mana = StartingMana;
            _player1.GetComponent<PlayerSummonGhostInteraction>().OnGhostSummonRequest += OnPlayer1GhostSummon;
            _player1.GetComponent<PlayerBattleGhostInteraction>().OnGhostBattleRequest += OnPlayer1GhostBattle;
            _player1.GetComponent<PlayerMulliganCardInteraction>().OnMulliganCardRequest += OnPlayer1MulliganCard;
            _player1.GetComponent<PlayerConfirmInteraction>().OnConfirmRequest += OnPlayer1Confirm;

            _player2.Hp = StartingHp;
            _player2.Mana = StartingMana;
        }

        private void Update()
        {
            HandlePlayer1SelectCard();
            HandlePlayer1SelectGhost();

            switch (_state)
            {
                case GameState.GameStart:
                    {
                        _state = GameState.Mulligan;
                    }
                    break;
                case GameState.Mulligan:
                    {
                        ShuffleDeck(_player1);
                        for (int i = 0; i < MulliganAmount; i++)
                        {
                            AddCardToMulligan(_player1);
                        }
                        _state = GameState.WaitForMulligan;
                    }
                    break;
                case GameState.DrawPhase:
                    {
                        Draw(_player1);
                        _state = GameState.Player1Play;
                    }
                    break;
                case GameState.WaitForMulligan:
                    if (_player1FinishMulligan)
                    {
                        _state = GameState.Player1Play;
                    }
                    break;
                case GameState.Player1Play:
                case GameState.Player1Attack:
                case GameState.Player2Play:
                case GameState.Player2Attack:
                default:
                    break;
            }
        }

        private void HandlePlayer1SelectCard()
        {
            Card card = _player1.SelectedCard;

            if (card == null)
            {
                _player1.InteractGhostZone.SetActive(false);
                _spellZoneHl.SetActive(false);
                return;
            }

            if (_state == GameState.Player1Play)
            {
                _player1.InteractGhostZone.SetActive(card.CardType == CardTypeEnum.Ghost);
                _spellZoneHl.SetActive(card.CardType != CardTypeEnum.Ghost);
            }
        }

        private void HandlePlayer1SelectGhost()
        {
            Ghost ghost = _player1.SelectedGhost;

            if (ghost == null)
            {
                _player2.AttackTarget.gameObject.SetActive(false);

                foreach (Ghost g in _player2.SummonGhostZone.GetComponentsInChildren<Ghost>())
                {
                    if (g.TryGetComponent(out GhostLayout ghostLayout))
                    {
                        ghostLayout.ResetHighlight();
                    }
                }
                return;
            }

            if (_state != GameState.Player1Attack)
            {
                return;
            }

            _player2.AttackTarget.gameObject.SetActive(true);

            foreach (Ghost g in _player2.SummonGhostZone.GetComponentsInChildren<Ghost>())
            {
                if (g.TryGetComponent(out GhostLayout ghostLayout))
                {
                    ghostLayout.HighlightEnemy();
                }
            }
        }

        private void OnPlayer1GhostSummon(Card card)
        {
            if (_state != GameState.Player1Play)
            {
                return;
            }

            if (card == null)
            {
                return;
            }

            if (card.CardType != CardTypeEnum.Ghost)
            {
                return;
            }

            Ghost ghost = Instantiate(_ghostPrefab, _player1.SummonGhostZone);
            ghost.CardId = card.CardId;
            ghost.tag = _player1.tag;

            Destroy(card.gameObject);

            _player1.GetComponent<PlayerSelectCardInteraction>().ResetState();
        }

        private void OnPlayer1GhostBattle(BattleDefender attacker, BattleDefender defender)
        {
            if (_state != GameState.Player1Attack)
            {
                return;
            }

            attacker.DealDamage(defender);
            defender.TakeDamage(attacker);

            _player1.GetComponent<PlayerSelectGhostInteraction>().ResetState();
        }

        private void OnPlayer1MulliganCard(Card card)
        {
            if (card == null)
            {
                return;
            }

            card.Mulligan = !card.Mulligan;
        }

        private void OnPlayer1Confirm()
        {
            switch (_state)
            {
                case GameState.WaitForMulligan:
                    {
                        foreach (Card c in _mulliganZone.GetComponentsInChildren<Card>())
                        {
                            if (!c.Mulligan)
                            {
                                Card card = Instantiate(_cardPrefab, _player1.Hand);
                                card.CardId = c.CardId;
                                card.tag = _player1.tag;
                                c.gameObject.SetActive(false);
                                Destroy(c.gameObject);
                            }
                        }

                        for (int i = _player1.Hand.childCount; i < StartingHandAmount; i++)
                        {
                            Draw(_player1);
                        }

                        foreach (Card c in _mulliganZone.GetComponentsInChildren<Card>())
                        {
                            _player1.Deck.Add(c.CardId);
                            Destroy(c.gameObject);
                        }

                        ShuffleDeck(_player1);
                        _player1FinishMulligan = true;
                    }
                    break;
                case GameState.Player1Play:
                    {
                        _state = GameState.Player1Attack;
                    }
                    break;
                case GameState.Player1Attack:
                    {
                        _state = GameState.DrawPhase;
                    }
                    break;
                default:
                    break;
            }
        }

        private void AddCardToMulligan(Player player)
        {
            Card card = Instantiate(_cardPrefab, _mulliganZone);
            card.CardId = player.Deck[0];
            card.tag = player.tag;
            card.gameObject.layer = LayerMask.NameToLayer(nameof(LayerMaskEnum.MulliganCard));
            player.Deck.RemoveAt(0);
        }

        private void Draw(Player player)
        {
            Card card = Instantiate(_cardPrefab, player.Hand);
            card.CardId = player.Deck[0];
            card.tag = player.tag;
            player.Deck.RemoveAt(0);
        }

        private void ShuffleDeck(Player player)
        {
            for (int i = 0; i < player.Deck.Count; i++)
            {
                int swapIdx = Random.Range(0, player.Deck.Count - 1);
                (player.Deck[i], player.Deck[swapIdx]) =
                    (player.Deck[swapIdx], player.Deck[i]);
            }
        }
    }
}
