using Game.Interactions;
using UnityEngine;

namespace Game.PlayerBot
{
    [RequireComponent(typeof(GamePlayerBot))]
    public class PlayerBotConfirmInteraction : ConfirmInteraction
    {
        GamePlayerBot _playerBotData;

        private void Awake()
        {
            _playerBotData = GetComponent<GamePlayerBot>();
        }

        private void Update()
        {
            if (_playerBotData.IsActionFinished)
            {
                _playerBotData.IsActionFinished = false;
                DispatchConfirmRequest();
            }
        }
    }
}
