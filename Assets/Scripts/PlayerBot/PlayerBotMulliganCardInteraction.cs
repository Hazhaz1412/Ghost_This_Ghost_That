using Game.Player;
using UnityEngine;

namespace Game.PlayerBot
{
    [RequireComponent(typeof(GamePlayerBot))]
    public class PlayerBotMulliganCardInteraction : MonoBehaviour
    {
        private GamePlayerBot _playerBotData;

        private void Awake()
        {
            _playerBotData = GetComponent<GamePlayerBot>();
        }

        private void Update()
        {
            if (_playerBotData.PlayerData.GameState == GamePlayer.PlayerGameState.PlayerMulligan && !_playerBotData.IsActionFinished)
            {
                _playerBotData.IsActionFinished = true;
            }
        }
    }
}
