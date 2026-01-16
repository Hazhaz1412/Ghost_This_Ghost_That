using Game.Enums;
using Game.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    [RequireComponent(typeof(Card), typeof(Canvas))]
    public class CardLayout : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _layoutObject;

        [SerializeField]
        private Vector2 _focusAnchor;

        [SerializeField]
        private ResourcesSO _resourcesData;

        [SerializeField]
        private Image _ghostImage;

        [SerializeField]
        private Image _ghostTitleImage;

        [SerializeField]
        private Image _ghostDescriptionImage;

        [SerializeField]
        private Transform _ghostManaNumberContainer;

        [SerializeField]
        private Transform _ghostAttackNumberContainer;

        [SerializeField]
        private Transform _ghostHealthNumberContainer;

        [SerializeField]
        private Image _spellImage;

        private Card _cardData;
        private Canvas _cardCanvas;

        private int _uiCardMana;
        private int _uiGhostAttack;
        private int _uiGhostHealth;

        private void Awake()
        {
            _cardData = GetComponent<Card>();
            _cardCanvas = GetComponent<Canvas>();
        }

        private void Start()
        {
            CardSO cardData = _resourcesData.LoadCardData<CardSO>(_cardData.CardId);

            bool isGhost = cardData.CardType == CardTypeEnum.Ghost;

            if (isGhost)
            {
                _ghostImage.sprite = _resourcesData.LoadGhostSprite<Sprite>(_cardData.CardId);
                _ghostTitleImage.sprite = _resourcesData.LoadGhostTitle<Sprite>(_cardData.CardId); ;
                _ghostDescriptionImage.sprite = _resourcesData.LoadGhostDescription<Sprite>(_cardData.CardId);
                _ghostImage.SetNativeSize();
                _ghostTitleImage.SetNativeSize();
                _ghostDescriptionImage.SetNativeSize();
                UpdateNumber(_ghostManaNumberContainer, _uiCardMana);
                UpdateNumber(_ghostAttackNumberContainer, _uiGhostAttack);
                UpdateNumber(_ghostHealthNumberContainer, _uiGhostHealth);
            }
            else
            {
                _spellImage.sprite = _resourcesData.LoadSpellSprite<Sprite>(_cardData.CardId);
                _spellImage.SetNativeSize();
            }

            _ghostImage.transform.parent.gameObject.SetActive(isGhost);
            _spellImage.gameObject.SetActive(!isGhost);
        }

        private void Update()
        {
            if (_cardData.CardType == CardTypeEnum.Ghost)
            {
                if (_uiCardMana != _cardData.CardMana)
                {
                    _uiCardMana = _cardData.CardMana;
                    UpdateNumber(_ghostManaNumberContainer, _uiCardMana);
                }

                if (_uiGhostAttack != _cardData.GhostAttack)
                {
                    _uiGhostAttack = _cardData.GhostAttack;
                    UpdateNumber(_ghostAttackNumberContainer, _uiGhostAttack);
                }

                if (_uiGhostHealth != _cardData.GhostHealth)
                {
                    _uiGhostHealth = _cardData.GhostHealth;
                    UpdateNumber(_ghostHealthNumberContainer, _uiGhostHealth);
                }
            }
        }

        private void UpdateNumber(Transform parent, int n)
        {
            foreach (Transform child in parent)
            {
                Destroy(child.gameObject);
            }
            foreach (char digit in n.ToString())
            {
                GameObject digitObj = new("digit", typeof(Image));

                Image digitImg = digitObj.GetComponent<Image>();
                digitImg.sprite = _resourcesData.LoadNumberSprite<Sprite>(digit - '0');
                digitImg.transform.SetParent(parent, false);
                digitImg.SetNativeSize();

            }
        }

        public void SetFocus(bool on)
        {
            if (on)
            {
                _layoutObject.anchoredPosition = _focusAnchor;
                _cardCanvas.overrideSorting = true;
            }
            else
            {
                _layoutObject.anchoredPosition = Vector2.zero;
                _cardCanvas.overrideSorting = false;
            }
        }
    }
}
