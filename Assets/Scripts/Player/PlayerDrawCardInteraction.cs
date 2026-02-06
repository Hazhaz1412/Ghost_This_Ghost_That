using Game.Card;
using Game.Enums;
using Game.Interfaces;
using UnityEngine;

namespace Game.Player
{
    [RequireComponent(typeof(GamePlayer))]
    public class PlayerDrawCardInteraction : MonoBehaviour, IDrawer
    {
        private GamePlayer _playerData;

        private void Awake()
        {
            _playerData = GetComponent<GamePlayer>();
        }

        public bool Draw()
        {
            if (_playerData.Peek() == CardIndexEnum.INVALID)
            {
                return false;
            }

            GameCard card = Instantiate(_playerData.CardPrefab, _playerData.Hand);
            card.CardId = _playerData.Peek();
            card.tag = _playerData.tag;
            _playerData.Pop();

            return true;
        }
    }
}
