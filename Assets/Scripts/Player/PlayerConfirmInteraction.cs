using Game.Interactions;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Player
{
    [RequireComponent(typeof(GamePlayer))]
    public class PlayerConfirmInteraction : ConfirmInteraction
    {
        [SerializeField]
        private Button _confirmButton;

        private void Awake()
        {
            _confirmButton.onClick.AddListener(OnConfirmButtonClicked);
        }

        private void OnConfirmButtonClicked()
        {
            DispatchConfirmRequest();
        }
    }
}
