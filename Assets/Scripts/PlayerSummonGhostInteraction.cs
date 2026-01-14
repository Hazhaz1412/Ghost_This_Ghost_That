using System.Collections.Generic;
using Game.Enums;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Game
{
    [RequireComponent(typeof(Player))]
    public class PlayerSummonGhostInteraction : MonoBehaviour
    {
        public event UnityAction<Card> OnGhostSummonRequest;

        private InputAction _clickAction;

        private Player _stateData;

        private void Awake()
        {
            _clickAction = InputSystem.actions.FindAction("click");
            _stateData = GetComponent<Player>();
        }

        private void Update()
        {
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

            if (target.gameObject.layer != LayerMask.NameToLayer(nameof(LayerMaskEnum.GhostZone)))
            {
                return;
            }

            OnGhostSummonRequest?.Invoke(_stateData.SelectedCard);
        }
    }
}
