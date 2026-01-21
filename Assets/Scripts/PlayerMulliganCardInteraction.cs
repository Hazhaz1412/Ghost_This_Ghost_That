using System.Collections.Generic;
using Game.Enums;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Game
{
    [RequireComponent(typeof(Player))]
    public class PlayerMulliganCardInteraction : MonoBehaviour
    {
        public event UnityAction<Card> OnMulliganCardRequest;

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
                case Player.PlayerState.Idle:
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

                if (target.gameObject.layer != LayerMask.NameToLayer(nameof(LayerMaskEnum.MulliganCard)) || !target.CompareTag(tag))
                {
                    return;
                }

                OnMulliganCardRequest?.Invoke(target.GetComponent<Card>());
            }

        }
    }
}
