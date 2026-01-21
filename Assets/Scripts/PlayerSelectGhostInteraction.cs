using System.Collections.Generic;
using Game.Enums;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Game
{
    [RequireComponent(typeof(Player))]
    public class PlayerSelectGhostInteraction : MonoBehaviour
    {
        private Player _playerData;

        private InputAction _clickAction;

        private Ghost _selectedGhost;

        private void Awake()
        {
            _playerData = GetComponent<Player>();
            _clickAction = InputSystem.actions.FindAction("click");
        }

        private void Update()
        {
            switch (_playerData.State)
            {
                case Player.PlayerState.Idle:
                case Player.PlayerState.SelectGhost:
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

                if (target.gameObject.layer != LayerMask.NameToLayer(nameof(LayerMaskEnum.Ghost)) || !target.CompareTag(tag))
                {
                    return;
                }

                Ghost targetedGhost = target.GetComponent<Ghost>();

                _selectedGhost = _selectedGhost == targetedGhost ? null : targetedGhost;
                _playerData.State = _selectedGhost == null ? Player.PlayerState.Idle : Player.PlayerState.SelectGhost;
            }

            if (_playerData.SelectedGhost == _selectedGhost)
            {
                return;
            }

            if (_playerData.SelectedGhost != null)
            {
                if (_playerData.SelectedGhost.TryGetComponent(out GhostLayout ghostLayout))
                {
                    ghostLayout.ResetHighlight();
                }
            }

            if (_selectedGhost != null)
            {
                if (_selectedGhost.TryGetComponent(out GhostLayout ghostLayout))
                {
                    ghostLayout.HighlightSelected();
                }
            }

            _playerData.SelectedGhost = _selectedGhost;
        }


        public void ResetState()
        {
            _selectedGhost = null;
            _playerData.State = Player.PlayerState.Idle;
        }
    }
}
