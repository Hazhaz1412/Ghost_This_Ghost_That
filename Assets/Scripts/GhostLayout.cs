using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    [RequireComponent(typeof(Ghost))]
    public class GhostLayout : MonoBehaviour
    {
        [SerializeField]
        private ResourcesSO _resourcesData;

        [SerializeField]
        private GameObject _selectedHighlight;

        [SerializeField]
        private GameObject _allyHighlight;

        [SerializeField]
        private GameObject _enemyHighlight;

        [SerializeField]
        private Animator _ghostAnim;

        [SerializeField]
        private TMP_Text _txtAttackNumber;

        [SerializeField]
        private TMP_Text _txtHealthNumber;

        private Ghost _ghostData;

        private int _uiAttackNumber;
        private int _uiHealthNumber;

        private void Awake()
        {
            _ghostData = GetComponent<Ghost>();
        }

        private void Start()
        {
            Image ghostImg = _ghostAnim.GetComponent<Image>();
            ghostImg.sprite = _resourcesData.LoadGhostSprite<Sprite>(_ghostData.CardId);
            ghostImg.SetNativeSize();

            _ghostAnim.runtimeAnimatorController = _resourcesData.LoadGhostAnimation<RuntimeAnimatorController>(_ghostData.CardId);

            _txtAttackNumber.SetText(_uiAttackNumber.ToString());
            _txtHealthNumber.SetText(_uiHealthNumber.ToString());
        }

        private void Update()
        {
            if (_uiAttackNumber != _ghostData.GhostAttack)
            {
                _uiAttackNumber = _ghostData.GhostAttack;
                _txtAttackNumber.SetText(_uiAttackNumber.ToString());
            }

            if (_uiHealthNumber != _ghostData.GhostHealth)
            {
                _uiHealthNumber = _ghostData.GhostHealth;
                _txtHealthNumber.SetText(_uiHealthNumber.ToString());
            }
        }

        public void HighlightSelected()
        {
            ResetHighlight();
            _selectedHighlight.SetActive(true);
        }

        public void HighlightEnemy()
        {
            ResetHighlight();
            _enemyHighlight.SetActive(true);
        }

        public void HighlightAlly()
        {
            ResetHighlight();
            _allyHighlight.SetActive(true);
        }

        public void ResetHighlight()
        {
            _selectedHighlight.SetActive(false);
            _allyHighlight.SetActive(false);
            _enemyHighlight.SetActive(false);
        }
    }
}
