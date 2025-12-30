using UnityEngine;
using UnityEngine.UI;

public class TCard : MonoBehaviour
{
    [SerializeField]
    private Image _imgGhost;

    [SerializeField]
    private Image _imgName;

    [SerializeField]
    private Image _imgDescription;

    [SerializeField]
    private Image _imgMana;

    [SerializeField]
    private Image _imgHp;

    [SerializeField]
    private Image _imgAttack;

    [SerializeField]
    private TCardSO _cardData;

    [SerializeField]
    private NumberSpriteSO _numberSprite;

    private void Start()
    {
        _imgGhost.sprite = _cardData.GhostSprite;
        _imgName.sprite = _cardData.NameSprite;
        _imgDescription.sprite = _cardData.DescriptionSprite;
        _imgMana.sprite = _numberSprite.GetNumberSprite(_cardData.Mana);
        _imgAttack.sprite = _numberSprite.GetNumberSprite(_cardData.Attack);
        _imgHp.sprite = _numberSprite.GetNumberSprite(_cardData.Hp);

        _imgGhost.SetNativeSize();
        _imgName.SetNativeSize();
        _imgDescription.SetNativeSize();
        _imgMana.SetNativeSize();
        _imgAttack.SetNativeSize();
        _imgHp.SetNativeSize();
    }
}
