using UnityEngine;
using UnityEngine.Events;

namespace Game.Interactions
{
    public abstract class MulliganCardInteraction : MonoBehaviour
    {
        private event UnityAction OnMulliganCardRequest;

        private void OnDestroy()
        {
            OnMulliganCardRequest = null;
        }

        protected void DispatchMulliganCardRequest()
        {
            OnMulliganCardRequest?.Invoke();
        }

        public void AddMulliganCardRequestListener(UnityAction listener)
        {
            OnMulliganCardRequest += listener;
        }
    }
}
