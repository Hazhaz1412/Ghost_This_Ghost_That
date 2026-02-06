using Game.Player;
using TMPro;
using UnityEngine;

namespace Game
{
    public class PlayerGameStateUI : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text[] _lsTxtRound;

        [SerializeField]
        private TMP_Text _txtPlayTurn;

        [SerializeField]
        private TMP_Text _txtAttackTurn;

        [SerializeField]
        private TMP_Text _txtOpponentTurn;

        [SerializeField]
        private GameObject _playerTurnDisplay;

        [SerializeField]
        private GamePlayer _player;

        private Color _disableColor = Color.grey;
        private Color _enableColor = Color.white;

        private void OnDestroy()
        {
            GameManager.OnRoundChange -= OnRoundChange;
        }

        private void Start()
        {
            GameManager.OnRoundChange += OnRoundChange;
        }

        private void Update()
        {
            switch (_player.GameState)
            {
                case GamePlayer.PlayerGameState.PlayerPlay:
                    SetAllColorDisable();
                    _txtPlayTurn.color = _enableColor;
                    break;
                case GamePlayer.PlayerGameState.PlayerAttack:
                    SetAllColorDisable();
                    _txtAttackTurn.color = _enableColor;
                    break;
                case GamePlayer.PlayerGameState.PlayerIdle:
                    SetAllColorDisable();
                    _txtOpponentTurn.color = _enableColor;
                    break;
                case GamePlayer.PlayerGameState.PlayerMulligan:
                case GamePlayer.PlayerGameState.PlayerFinishedMulligan:
                default:
                    break;
            }
        }

        private void SetAllColorDisable()
        {
            _txtPlayTurn.color = _disableColor;
            _txtAttackTurn.color = _disableColor;
            _txtOpponentTurn.color = _disableColor;
        }

        private void OnRoundChange(int round)
        {
            if (!_playerTurnDisplay.activeInHierarchy)
            {
                _playerTurnDisplay.SetActive(true);
            }

            foreach (TMP_Text txtRound in _lsTxtRound)
            {
                txtRound.SetText(round.ToString());
            }
        }
    }
}
