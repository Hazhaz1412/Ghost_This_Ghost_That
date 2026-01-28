using Game.Structs;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Interactions
{
    public abstract class BattleInteraction : MonoBehaviour
    {
        private event UnityAction<Battler, Battler> OnBattleRequest;

        private void OnDestroy()
        {
            OnBattleRequest = null;
        }

        protected void DispatchBattleRequest(Battler attacker, Battler defender)
        {
            OnBattleRequest?.Invoke(attacker, defender);
        }

        public void AddBattleRequestListener(UnityAction<Battler, Battler> listener)
        {
            OnBattleRequest += listener;
        }
    }
}
