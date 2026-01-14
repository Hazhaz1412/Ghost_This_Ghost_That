using System.Collections.Generic;
using Game.Enums;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Game
{
    [RequireComponent(typeof(Player))]
    public class PlayerSelectCardInteraction : MonoBehaviour
    {
        public event UnityAction<Card> OnCardSelectRequest;

        [SerializeField]
        private Vector2 _focusOffset;

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
            SynchronizeStateData();

            if (!_clickAction.WasPressedThisFrame())
            {
                return;
            }

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

            if (target.gameObject.layer != LayerMask.NameToLayer(nameof(LayerMaskEnum.Card)))
            {
                return;
            }

            Card selectedCard = target.GetComponent<Card>();

            if (_selectedCard == selectedCard)
            {
                _selectedCard = null;
            }
            else
            {
                _selectedCard = selectedCard;
            }
        }

        private void SynchronizeStateData()
        {
            if (_playerData.SelectedCard != _selectedCard)
            {
                if (_playerData.SelectedCard != null)
                {
                    if (_playerData.SelectedCard.TryGetComponent(out Canvas cardCanvas))
                    {
                        cardCanvas.overrideSorting = false;
                    }

                    if (_playerData.SelectedCard.TryGetComponent(out CardLayout cardLayout))
                    {
                        cardLayout.LayoutObject.anchoredPosition = Vector2.zero;
                    }
                }

                if (_selectedCard != null)
                {
                    if (_selectedCard.TryGetComponent(out Canvas cardCanvas))
                    {
                        cardCanvas.overrideSorting = true;
                    }

                    if (_selectedCard.TryGetComponent(out CardLayout cardLayout))
                    {
                        cardLayout.LayoutObject.anchoredPosition = cardLayout.LayoutObject.anchoredPosition + _focusOffset;
                    }
                }

                _playerData.SelectedCard = _selectedCard;
                OnCardSelectRequest?.Invoke(_selectedCard);
            }
        }

        public void ResetState()
        {
            _selectedCard = null;
            OnCardSelectRequest?.Invoke(null);
        }
    }
}
