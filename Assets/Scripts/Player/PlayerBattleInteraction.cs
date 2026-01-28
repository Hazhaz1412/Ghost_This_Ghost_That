using System.Collections.Generic;
using Game.Enums;
using Game.Interactions;
using Game.Structs;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Game.Player
{
    [RequireComponent(typeof(GamePlayer))]
    public class PlayerBattleInteraction : BattleInteraction
    {
        private GamePlayer _playerData;

        private InputAction _clickAction;

        private void Awake()
        {
            _playerData = GetComponent<GamePlayer>();
            _clickAction = InputSystem.actions.FindAction("click");
        }

        private void Update()
        {
            switch (_playerData.State)
            {
                case GamePlayer.PlayerState.SelectGhost:
                    break;
                default:
                    return;
            }

            switch (_playerData.GameState)
            {
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
                bool isAlly = target.CompareTag(tag);

                if ((target.gameObject.layer != LayerMask.NameToLayer(nameof(LayerMaskEnum.Ghost)) &&
                        target.gameObject.layer != LayerMask.NameToLayer(nameof(LayerMaskEnum.PlayerTarget))) || isAlly)
                {
                    return;
                }

                Battler attacker = new(_playerData.SelectedGhost);
                Battler defender = new(target.gameObject);

                DispatchBattleRequest(attacker, defender);
            }
        }
    }
}
