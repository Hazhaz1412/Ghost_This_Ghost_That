using Game.Player;
using UnityEngine;

namespace Game.PlayerBot
{
    [RequireComponent(typeof(GamePlayerBot), typeof(PlayerBotBattleInteraction))]
    public class PlayerBotAttackTurn : MonoBehaviour
    {
        private GamePlayerBot _playerBotData;
        private PlayerBotBattleInteraction _battleInteraction;

        private void Awake()
        {
            _playerBotData = GetComponent<GamePlayerBot>();
            _battleInteraction = GetComponent<PlayerBotBattleInteraction>();
        }

        private void Update()
        {
            if (_playerBotData.PlayerData.GameState != GamePlayer.PlayerGameState.PlayerAttack)
            {
                return;
            }

            if (!_battleInteraction.FinishBattle)
            {
                return;
            }

            _playerBotData.IsActionFinished = true;
        }
    }
}
