using Game.Card;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public abstract class SummonGhostInteraction : MonoBehaviour
    {
        private event UnityAction<GameCard> OnGhostSummonRequest;

        private void OnDestroy()
        {
            OnGhostSummonRequest = null;
        }

        protected void DispatchSummonGhostRequest(GameCard card)
        {
            OnGhostSummonRequest?.Invoke(card);
        }

        public void AddSummonGhostRequestListener(UnityAction<GameCard> listener)
        {
            OnGhostSummonRequest += listener;
        }
    }
}
