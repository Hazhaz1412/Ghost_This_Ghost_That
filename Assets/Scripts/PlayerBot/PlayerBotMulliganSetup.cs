using Game.Enums;
using Game.Interfaces;
using UnityEngine;

namespace Game.PlayerBot
{
    [RequireComponent(typeof(GamePlayerBot))]
    public class PlayerBotMulliganSetup : MonoBehaviour, IMulliganSetup
    {
        private GamePlayerBot _playerBotData;

        private void Awake()
        {
            _playerBotData = GetComponent<GamePlayerBot>();
        }

        public void AddCardToMulliganZone()
        {
            _playerBotData.MulliganZone.Add(_playerBotData.PlayerData.Peek());
            _playerBotData.PlayerData.Pop();
        }

        public void FinishMulligan()
        {
            _playerBotData.PlayerData.ShuffleDeck();

            foreach (CardIndexEnum card in _playerBotData.MulliganZone)
            {
                _playerBotData.PlayerData.PrependCard(card);
            }
        }
    }
}
