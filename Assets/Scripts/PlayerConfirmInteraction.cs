using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game
{
    [RequireComponent(typeof(Player))]
    public class PlayerConfirmInteraction : MonoBehaviour
    {
        public event UnityAction OnConfirmRequest;

        [SerializeField]
        private Button _confirmButton;

        private void Awake()
        {
            _confirmButton.onClick.AddListener(OnConfirmButtonClicked);
        }

        private void OnConfirmButtonClicked()
        {
            OnConfirmRequest?.Invoke();
        }
    }
}
