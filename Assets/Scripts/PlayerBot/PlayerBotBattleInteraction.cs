using Game.Ghost;
using Game.Interactions;
using Game.Player;
using UnityEngine;

namespace Game.PlayerBot
{
    [RequireComponent(typeof(GamePlayerBot))]
    public class PlayerBotBattleInteraction : BattleInteraction
    {
        public bool FinishBattle { get; private set; }

        private GamePlayerBot _playerBotData;

        private void Awake()
        {
            _playerBotData = GetComponent<GamePlayerBot>();
        }

        private void Update()
        {
            if (_playerBotData.PlayerData.GameState != GamePlayer.PlayerGameState.PlayerAttack)
            {
                FinishBattle = false;
                return;
            }

            if (FinishBattle)
            {
                return;
            }

            foreach (GameGhost ghost in _playerBotData.PlayerData.SummonGhostZone.GetComponentsInChildren<GameGhost>())
            {
                GameGhost enemyGhost = _playerBotData.PlayerData.Enemy.SummonGhostZone.GetComponentInChildren<GameGhost>(false);
                PlayerAttackTarget enemyAttackTarget = _playerBotData.PlayerData.Enemy.AttackTarget;

                if (enemyGhost)
                {
                    DispatchBattleRequest(new(ghost), new(enemyGhost));
                }
                else
                {
                    DispatchBattleRequest(new(ghost), new(enemyAttackTarget));
                }
            }

            FinishBattle = true;
        }
    }
}
