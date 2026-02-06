using System.Collections;
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

        private Coroutine _summonRoutine;

        private const float TimeBetweenSummon = 0.5f;

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

            if (_summonRoutine != null)
            {
                return;
            }

            _summonRoutine = StartCoroutine(Summon());
        }

        private IEnumerator Summon()
        {
            foreach (GameCard card in _playerBotData.PlayerData.Hand.GetComponentsInChildren<GameCard>())
            {
                DispatchSummonGhostRequest(card);
                yield return new WaitForSeconds(TimeBetweenSummon);
            }

            FinishSummoning = true;

            _summonRoutine = null;
        }
    }
}
