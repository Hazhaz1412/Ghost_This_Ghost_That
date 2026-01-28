using Game.Player;
using UnityEngine;

namespace Game.PlayerBot
{
    [RequireComponent(typeof(GamePlayerBot), typeof(PlayerBotSummonGhostInteraction))]
    public class PlayerBotPlayTurn : MonoBehaviour
    {
        private GamePlayerBot _playerBotData;
        private PlayerBotSummonGhostInteraction _summonGhostInteraction;

        private void Awake()
        {
            _playerBotData = GetComponent<GamePlayerBot>();
            _summonGhostInteraction = GetComponent<PlayerBotSummonGhostInteraction>();
        }

        private void Update()
        {
            if (_playerBotData.PlayerData.GameState != GamePlayer.PlayerGameState.PlayerPlay)
            {
                return;
            }

            if (!_summonGhostInteraction.FinishSummoning)
            {
                return;
            }

            _playerBotData.IsActionFinished = true;
        }
    }
}
