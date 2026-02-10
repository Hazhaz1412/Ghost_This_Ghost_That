using System.Collections.Generic;
using Game.Card;
using Game.Enums;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Game.Player
{
    [RequireComponent(typeof(GamePlayer))]
    public class PlayerMulliganCardInteraction : MonoBehaviour
    {
        public event UnityAction<GameCard> OnMulliganCardRequest;

        private GamePlayer _playerData;

        private InputAction _clickAction;
        private InputAction _pointAction;

        private void Awake()
        {
            _playerData = GetComponent<GamePlayer>();
            _clickAction = InputSystem.actions.FindAction("UI/Click") ?? InputSystem.actions.FindAction("Click");
            _pointAction = InputSystem.actions.FindAction("UI/Point") ?? InputSystem.actions.FindAction("Point");
        }

        private void Update()
        {
            switch (_playerData.State)
            {
                case GamePlayer.PlayerState.Idle:
                    break;
                default:
                    return;
            }

            switch (_playerData.GameState)
            {
                case GamePlayer.PlayerGameState.PlayerMulligan:
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

                OnMulliganCardRequest?.Invoke(target.GetComponent<GameCard>());
            }

        }
    }
}
