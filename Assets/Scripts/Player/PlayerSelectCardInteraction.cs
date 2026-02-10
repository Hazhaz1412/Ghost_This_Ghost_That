using System.Collections.Generic;
using Game.Card;
using Game.Enums;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Game.Player
{
    [RequireComponent(typeof(GamePlayer))]
    public class PlayerSelectCardInteraction : MonoBehaviour
    {
        private InputAction _clickAction;
        private InputAction _pointAction;

        private GamePlayer _playerData;

        private GameCard _selectedCard;

        private void Awake()
        {
            _clickAction = InputSystem.actions.FindAction("UI/Click") ?? InputSystem.actions.FindAction("Click");
            _pointAction = InputSystem.actions.FindAction("UI/Point") ?? InputSystem.actions.FindAction("Point");
            _playerData = GetComponent<GamePlayer>();
        }

        private void Update()
        {
            switch (_playerData.State)
            {
                case GamePlayer.PlayerState.Idle:
                case GamePlayer.PlayerState.SelectCard:
                    break;
                default:
                    return;
            }

            switch (_playerData.GameState)
            {
                case GamePlayer.PlayerGameState.PlayerIdle:
                case GamePlayer.PlayerGameState.PlayerPlay:
                case GamePlayer.PlayerGameState.PlayerAttack:
                    break;
                default:
                    return;
            }

            if (EventSystem.current == null || _clickAction == null || _pointAction == null)
            {
                return;
            }

            if (_clickAction.WasPressedThisFrame())
            {
                PointerEventData pointerEventData = new(EventSystem.current)
                {
                    position = _pointAction.ReadValue<Vector2>()
                };

                List<RaycastResult> raycastResults = new();

                EventSystem.current.RaycastAll(pointerEventData, raycastResults);

                if (raycastResults.Count <= 0)
                {
                    return;
                }

                BaseRaycaster target = raycastResults[0].module;

                if (target.gameObject.layer != LayerMask.NameToLayer(nameof(LayerMaskEnum.Card)) || !target.CompareTag(tag))
                {
                    return;
                }

                GameCard targetedCard = target.GetComponent<GameCard>();

                _selectedCard = _selectedCard == targetedCard ? null : targetedCard;
                _playerData.State = _selectedCard == null ? GamePlayer.PlayerState.Idle : GamePlayer.PlayerState.SelectCard;
            }

            if (_playerData.SelectedCard == _selectedCard)
            {
                return;
            }

            if (_playerData.SelectedCard != null)
            {
                if (_playerData.SelectedCard.TryGetComponent(out CardLayout cardLayout))
                {
                    cardLayout.SetFocus(false);
                }
            }

            if (_selectedCard != null)
            {
                if (_selectedCard.TryGetComponent(out CardLayout cardLayout))
                {
                    cardLayout.SetFocus(true);
                }
            }

            _playerData.SelectedCard = _selectedCard;
        }

        public void ResetState()
        {
            _selectedCard = null;
            _playerData.State = GamePlayer.PlayerState.Idle;
        }
    }
}
