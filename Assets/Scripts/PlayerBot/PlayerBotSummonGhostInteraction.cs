using Game.Card;
using Game.Player;
using UnityEngine;

namespace Game.PlayerBot
{
    [RequireComponent(typeof(GamePlayerBot))]
    public class PlayerBotSummonGhostInteraction : SummonGhostInteraction
    {
        private GamePlayerBot _playerBotData;

        public bool FinishSummoning { get; private set; }

        private void Awake()
        {
            _playerBotData = GetComponent<GamePlayerBot>();
        }

        private void Update()
        {
            if (_playerBotData.PlayerData.GameState != GamePlayer.PlayerGameState.PlayerPlay)
            {
                FinishSummoning = false;
                return;
            }

            if (FinishSummoning)
            {
                return;
            }

            foreach (GameCard card in _playerBotData.PlayerData.Hand.GetComponentsInChildren<GameCard>())
            {
                DispatchSummonGhostRequest(card);
            }

            FinishSummoning = true;
        }
    }
}
