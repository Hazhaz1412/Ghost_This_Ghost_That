using System.Collections;
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

        private Coroutine _battleRoutine;

        private const float TimeBetweenAttack = 0.5f;

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

            if (_battleRoutine != null)
            {
                return;
            }

            _battleRoutine = StartCoroutine(Battle());
        }

        private IEnumerator Battle()
        {
            GameGhost[] enemies = _playerBotData.PlayerData.Enemy.SummonGhostZone.GetComponentsInChildren<GameGhost>();
            int currentAttack = 0;

            foreach (GameGhost ghost in _playerBotData.PlayerData.SummonGhostZone.GetComponentsInChildren<GameGhost>())
            {
                GameGhost enemyGhost = null;

                if (currentAttack < enemies.Length && enemies[currentAttack].IsAlive)
                {
                    enemyGhost = enemies[currentAttack];
                    currentAttack++;
                }

                PlayerAttackTarget enemyAttackTarget = _playerBotData.PlayerData.Enemy.AttackTarget;

                if (enemyGhost != null)
                {
                    DispatchBattleRequest(new(ghost), new(enemyGhost));
                }
                else
                {
                    DispatchBattleRequest(new(ghost), new(enemyAttackTarget));
                }

                yield return new WaitForSeconds(TimeBetweenAttack);
            }

            FinishBattle = true;

            _battleRoutine = null;
        }
    }
}
