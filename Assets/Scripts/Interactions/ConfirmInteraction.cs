using UnityEngine;
using UnityEngine.Events;

namespace Game.Interactions
{
    public abstract class ConfirmInteraction : MonoBehaviour
    {
        private event UnityAction OnConfirmRequest;

        private void OnDestroy()
        {
            OnConfirmRequest = null;
        }

        protected void DispatchConfirmRequest()
        {
            OnConfirmRequest?.Invoke();
        }

        public void AddConfirmRequestListener(UnityAction listener)
        {
            OnConfirmRequest += listener;
        }
    }
}
