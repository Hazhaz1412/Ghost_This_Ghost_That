using System.Collections.Generic;
using Game.Enums;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Game.Player
{
    [RequireComponent(typeof(GamePlayer))]
    public class PlayerSummonGhostInteraction : SummonGhostInteraction
    {
        private InputAction _clickAction;

        private GamePlayer _playerData;

        private void Awake()
        {
            _clickAction = InputSystem.actions.FindAction("click");
            _playerData = GetComponent<GamePlayer>();
        }

        private void Update()
        {
            switch (_playerData.State)
            {
                case GamePlayer.PlayerState.SelectCard:
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

                if (target.gameObject.layer != LayerMask.NameToLayer(nameof(LayerMaskEnum.GhostZone)) || !target.CompareTag(tag))
                {
                    return;
                }

                DispatchSummonGhostRequest(_playerData.SelectedCard);
            }

        }
    }
}
