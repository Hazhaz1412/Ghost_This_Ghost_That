using Game.Card;
using Game.Enums;
using Game.Ghost;
using Game.Interactions;
using Game.Interfaces;
using Game.Player;
using Game.Structs;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        public static event UnityAction<int> OnRoundChange;

        private enum GameState
        {
            GameStart,
            Mulligan,
            WaitForMulligan,
            PlayerTurnStart,
            AttackPlayerPlay,
            DefendPlayerPlay,
            PlayerAttack,
        }

        private int _round;
        private void SetRound(int value)
        {
            OnRoundChange?.Invoke(value);
            _round = value;
        }

        private const int MulliganAmount = 4;

        private const int StartingHandAmount = 4;

        private const int StartingHp = 20;

        private const int StartingMana = 1;

        [SerializeField]
        private GameCard _cardPrefab;

        [SerializeField]
        private GameGhost _ghostPrefab;

        [SerializeField]
        private GameObject _spellZoneHl;

        [SerializeField]
        private Transform _spellZone;

        [SerializeField]
        private Transform _mulliganZone;

        [SerializeField]
        private GamePlayer _player1;

        [SerializeField]
        private GamePlayer _player2;

        [SerializeField] // NOTE: temporary
        private GameState _state;

        private void Awake()
        {
            SetRound(0);

            _player1.IsAttackTurn = Random.Range(0, 2) == 0;
            _player2.IsAttackTurn = !_player1.IsAttackTurn;

            _player1.Hp = StartingHp;
            _player1.Mana = StartingMana;
            _player1.GetComponent<SummonGhostInteraction>().AddSummonGhostRequestListener((card) => OnPlayerGhostSummon(_player1, card));
            _player1.GetComponent<BattleInteraction>().AddBattleRequestListener((attacker, defender) => OnPlayerBattle(attacker, defender, _player1));
            _player1.GetComponent<ConfirmInteraction>().AddConfirmRequestListener(() => OnPlayerConfirm(_player1));
            _player1.GetComponent<PlayerMulliganCardInteraction>().OnMulliganCardRequest += OnPlayer1MulliganCard;

            _player2.Hp = StartingHp;
            _player2.Mana = StartingMana;
            _player2.GetComponent<SummonGhostInteraction>().AddSummonGhostRequestListener((card) => OnPlayerGhostSummon(_player2, card));
            _player2.GetComponent<BattleInteraction>().AddBattleRequestListener((attacker, defender) => OnPlayerBattle(attacker, defender, _player2));
            _player2.GetComponent<ConfirmInteraction>().AddConfirmRequestListener(() => OnPlayerConfirm(_player2));
        }

        private void Update()
        {
            HandlePlayerGameState();
            HandlePlayerSelectGhost(_player1);

            HandlePlayer1SelectCard();

            switch (_state)
            {
                case GameState.GameStart:
                    {
                        _state = GameState.Mulligan;
                        _player1.ShuffleDeck();
                        _player2.ShuffleDeck();
                    }
                    break;
                case GameState.Mulligan:
                    {
                        for (int i = 0; i < MulliganAmount; i++)
                        {
                            _player1.GetComponent<IMulliganSetup>().AddCardToMulliganZone();
                            _player2.GetComponent<IMulliganSetup>().AddCardToMulliganZone();
                        }
                        _state = GameState.WaitForMulligan;
                    }
                    break;
                case GameState.WaitForMulligan:
                    {
                        if (_player1.GameState == GamePlayer.PlayerGameState.PlayerFinishedMulligan &&
                                _player2.GameState == GamePlayer.PlayerGameState.PlayerFinishedMulligan)
                        {
                            SetRound(_round + 1);

                            _state = GameState.AttackPlayerPlay;

                            for (int i = 0; i < StartingHandAmount; i++)
                            {
                                if (_player1.TryGetComponent(out IDrawer drawer1))
                                {
                                    drawer1.Draw();
                                }
                                if (_player2.TryGetComponent(out IDrawer drawer2))
                                {
                                    drawer2.Draw();
                                }
                            }
                        }
                    }
                    break;
                case GameState.PlayerTurnStart:
                    {
                        SetRound(_round + 1);

                        _player1.Mana++;
                        _player2.Mana++;

                        if (_player1.TryGetComponent(out IDrawer drawer))
                        {
                            drawer.Draw();
                        }
                        if (_player2.TryGetComponent(out IDrawer drawer2))
                        {
                            drawer2.Draw();
                        }

                        _state = GameState.AttackPlayerPlay;
                    }
                    break;
                case GameState.AttackPlayerPlay:
                case GameState.PlayerAttack:
                default:
                    break;
            }
        }

        private void HandlePlayer1SelectCard()
        {
            GameCard card = _player1.SelectedCard;

            if (card == null || _player1.Mana < card.CardMana)
            {
                _player1.InteractGhostZone.SetActive(false);
                _spellZoneHl.SetActive(false);
                return;
            }

            if (_player1.GameState == GamePlayer.PlayerGameState.PlayerPlay)
            {
                _player1.InteractGhostZone.SetActive(card.CardType == CardTypeEnum.Ghost);
                _spellZoneHl.SetActive(card.CardType != CardTypeEnum.Ghost);
            }
        }

        private void HandlePlayerSelectGhost(GamePlayer player)
        {
            GameGhost ghost = player.SelectedGhost;

            if (ghost == null || player.GameState != GamePlayer.PlayerGameState.PlayerAttack)
            {
                player.Enemy.AttackTarget.gameObject.SetActive(false);

                foreach (GameGhost g in player.Enemy.SummonGhostZone.GetComponentsInChildren<GameGhost>())
                {
                    if (g.TryGetComponent(out GhostLayout ghostLayout))
                    {
                        ghostLayout.ResetHighlight();
                    }
                }
                return;
            }

            if (player.GameState == GamePlayer.PlayerGameState.PlayerAttack)
            {
                player.Enemy.AttackTarget.gameObject.SetActive(true);

                foreach (GameGhost g in player.Enemy.SummonGhostZone.GetComponentsInChildren<GameGhost>())
                {
                    if (g.TryGetComponent(out GhostLayout ghostLayout))
                    {
                        ghostLayout.HighlightEnemy();
                    }
                }
            }
        }

        private void OnPlayerGhostSummon(GamePlayer player, GameCard card)
        {
            if (player.GameState != GamePlayer.PlayerGameState.PlayerPlay)
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

            if (player.Mana < card.CardMana)
            {
                return;
            }

            GameGhost ghost = Instantiate(_ghostPrefab, player.SummonGhostZone);
            ghost.CardId = card.CardId;
            ghost.tag = player.tag;

            Destroy(card.gameObject);

            if (player.TryGetComponent(out PlayerSelectCardInteraction selectCardInteraction))
            {
                selectCardInteraction.ResetState();
            }

            player.Mana -= card.CardMana;
        }

        private void OnPlayerBattle(Battler attacker, Battler defender, GamePlayer player)
        {
            if (player.GameState != GamePlayer.PlayerGameState.PlayerAttack)
            {
                return;
            }

            if (!player.CompareTag(attacker.Tag))
            {
                return;
            }

            attacker.Battle(defender);

            if (player.TryGetComponent(out PlayerSelectGhostInteraction selectGhostInteraction))
            {
                selectGhostInteraction.ResetState();
            }
        }

        private void OnPlayer1MulliganCard(GameCard card)
        {
            if (card == null)
            {
                return;
            }

            card.Mulligan = !card.Mulligan;
        }

        private void OnPlayerConfirm(GamePlayer player)
        {
            switch (_state)
            {
                case GameState.WaitForMulligan:
                    {
                        if (player.GameState != GamePlayer.PlayerGameState.PlayerFinishedMulligan)
                        {
                            player.GetComponent<IMulliganSetup>().FinishMulligan();
                            player.GameState = GamePlayer.PlayerGameState.PlayerFinishedMulligan;
                        }
                    }
                    break;
                case GameState.AttackPlayerPlay:
                    {
                        if (player.GameState == GamePlayer.PlayerGameState.PlayerPlay)
                        {
                            _state = GameState.DefendPlayerPlay;
                        }
                    }
                    break;
                case GameState.DefendPlayerPlay:
                    {
                        if (player.GameState == GamePlayer.PlayerGameState.PlayerPlay)
                        {
                            _state = GameState.PlayerAttack;
                        }
                    }
                    break;
                case GameState.PlayerAttack:
                    {
                        if (player.GameState == GamePlayer.PlayerGameState.PlayerAttack)
                        {
                            _state = GameState.PlayerTurnStart;
                        }
                    }
                    break;
                case GameState.GameStart:
                case GameState.Mulligan:
                case GameState.PlayerTurnStart:
                default:
                    break;
            }
        }

        private void HandlePlayerGameState()
        {
            switch (_state)
            {
                case GameState.PlayerTurnStart:
                    {
                        (_player1.IsAttackTurn, _player2.IsAttackTurn) =
                            (_player2.IsAttackTurn, _player1.IsAttackTurn);
                    }
                    break;
                case GameState.Mulligan:
                    {
                        _player1.GameState = GamePlayer.PlayerGameState.PlayerMulligan;
                        _player2.GameState = GamePlayer.PlayerGameState.PlayerMulligan;
                    }
                    break;
                case GameState.AttackPlayerPlay:
                    {
                        if (_player1.IsAttackTurn)
                        {
                            _player1.GameState = GamePlayer.PlayerGameState.PlayerPlay;
                            _player2.GameState = GamePlayer.PlayerGameState.PlayerIdle;
                        }
                        else if (_player2.IsAttackTurn)
                        {
                            _player1.GameState = GamePlayer.PlayerGameState.PlayerIdle;
                            _player2.GameState = GamePlayer.PlayerGameState.PlayerPlay;
                        }
                    }
                    break;
                case GameState.DefendPlayerPlay:
                    {
                        if (_player1.IsAttackTurn)
                        {
                            _player1.GameState = GamePlayer.PlayerGameState.PlayerIdle;
                            _player2.GameState = GamePlayer.PlayerGameState.PlayerPlay;
                        }
                        else if (_player2.IsAttackTurn)
                        {
                            _player1.GameState = GamePlayer.PlayerGameState.PlayerPlay;
                            _player2.GameState = GamePlayer.PlayerGameState.PlayerIdle;
                        }
                    }
                    break;
                case GameState.PlayerAttack:
                    {
                        if (_player1.IsAttackTurn)
                        {
                            _player1.GameState = GamePlayer.PlayerGameState.PlayerAttack;
                            _player2.GameState = GamePlayer.PlayerGameState.PlayerIdle;
                        }
                        else if (_player2.IsAttackTurn)
                        {
                            _player1.GameState = GamePlayer.PlayerGameState.PlayerIdle;
                            _player2.GameState = GamePlayer.PlayerGameState.PlayerAttack;
                        }
                    }
                    break;
                case GameState.GameStart:
                case GameState.WaitForMulligan:
                default:
                    break;
            }
        }
    }
}
