using Game.Card;
using Game.Interfaces;
using UnityEngine;

namespace Game.Player
{
    [RequireComponent(typeof(GamePlayer))]
    public class PlayerMulliganSetup : MonoBehaviour, IMulliganSetup
    {
        private GamePlayer _playerData;

        private void Awake()
        {
            _playerData = GetComponent<GamePlayer>();
        }

        public void AddCardToMulliganZone()
        {
            GameCard card = Instantiate(_playerData.CardPrefab, _playerData.MulliganZone);
            card.CardId = _playerData.Peek();
            card.tag = tag;
            _playerData.Pop();
        }

        public void FinishMulligan()
        {
            foreach (GameCard card in _playerData.MulliganZone.GetComponentsInChildren<GameCard>())
            {
                if (card.Mulligan)
                {
                    _playerData.AppendCard(card.CardId);
                    card.gameObject.SetActive(false);
                    Destroy(card.gameObject);
                }
            }

            _playerData.ShuffleDeck();

            foreach (GameCard card in _playerData.MulliganZone.GetComponentsInChildren<GameCard>())
            {
                _playerData.PrependCard(card.CardId);
                card.gameObject.SetActive(false);
                Destroy(card.gameObject);
            }
        }
    }
}
