using System.Collections.Generic;
using Game.Enums;
using Game.Ghost;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Game.Player
{
    [RequireComponent(typeof(GamePlayer))]
    public class PlayerSelectGhostInteraction : MonoBehaviour
    {
        private GamePlayer _playerData;

        private InputAction _clickAction;

        private GameGhost _selectedGhost;

        private void Awake()
        {
            _playerData = GetComponent<GamePlayer>();
            _clickAction = InputSystem.actions.FindAction("click");
        }

        private void Update()
        {
            switch (_playerData.State)
            {
                case GamePlayer.PlayerState.Idle:
                case GamePlayer.PlayerState.SelectGhost:
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

                GameGhost targetedGhost = target.GetComponent<GameGhost>();

                _selectedGhost = _selectedGhost == targetedGhost ? null : targetedGhost;
                _playerData.State = _selectedGhost == null ? GamePlayer.PlayerState.Idle : GamePlayer.PlayerState.SelectGhost;
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
            _playerData.State = GamePlayer.PlayerState.Idle;
        }
    }
}
