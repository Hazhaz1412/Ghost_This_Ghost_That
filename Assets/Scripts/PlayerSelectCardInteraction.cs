using System.Collections.Generic;
using Game.Enums;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Game
{
    [RequireComponent(typeof(Player))]
    public class PlayerSelectCardInteraction : MonoBehaviour
    {
        private InputAction _clickAction;

        private Player _playerData;

        private Card _selectedCard;

        private void Awake()
        {
            _clickAction = InputSystem.actions.FindAction("Click");
            _playerData = GetComponent<Player>();
        }

        private void Update()
        {
            switch (_playerData.State)
            {
                case Player.PlayerState.Idle:
                case Player.PlayerState.SelectCard:
                    break;
                default:
                    return;
            }

            if (_clickAction.WasPressedThisFrame())
            {
                PointerEventData pointerEventData = new(EventSystem.current)
                {
                    position = Mouse.current.position.ReadValue()
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

                Card targetedCard = target.GetComponent<Card>();

                _selectedCard = _selectedCard == targetedCard ? null : targetedCard;
                _playerData.State = _selectedCard == null ? Player.PlayerState.Idle : Player.PlayerState.SelectCard;
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
            _playerData.State = Player.PlayerState.Idle;
        }
    }
}
