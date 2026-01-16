using System.Collections.Generic;
using Game.Enums;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Game
{
    [RequireComponent(typeof(Player))]
    public class PlayerBattleGhostInteraction : MonoBehaviour
    {
        public event UnityAction<Ghost, Ghost> OnGhostBattleRequest;

        private Player _playerData;

        private InputAction _clickAction;

        private void Awake()
        {
            _playerData = GetComponent<Player>();
            _clickAction = InputSystem.actions.FindAction("click");
        }

        private void Update()
        {
            switch (_playerData.State)
            {
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
                bool isAllyGhost = target.CompareTag(tag);

                if (target.gameObject.layer != LayerMask.NameToLayer(nameof(LayerMaskEnum.Ghost)) || isAllyGhost)
                {
                    return;
                }

                Ghost targetedGhost = target.GetComponent<Ghost>();

                OnGhostBattleRequest?.Invoke(_playerData.SelectedGhost, targetedGhost);
            }
        }
    }
}
