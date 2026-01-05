using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TCard : MonoBehaviour
{
    [SerializeField]
    private string _resourceDir;

    [SerializeField]
    private GameObject _ghostPresentation;

    [SerializeField]
    private Image _imgGhost;

    [SerializeField]
    private TMP_Text _txtName;

    [SerializeField]
    private TMP_Text _txtDescription;

    [SerializeField]
    private TMP_Text _txtMana;

    [SerializeField]
    private TMP_Text _txtHp;

    [SerializeField]
    private TMP_Text _txtAttack;

    [SerializeField]
    private GameObject _spellPresentation;

    [SerializeField]
    private Image _imgSpell;

    public TCardData CardData;

    private void Start()
    {
        if (CardData.CardType == TCardTypeEnum.GHOST)
        {
            _imgGhost.sprite = Resources.Load<Sprite>(Path.Join(_resourceDir, CardData.CardID));
            _imgGhost.SetNativeSize();
            _txtName.SetText(CardData.CardName);
            _txtDescription.SetText(CardData.CardDescription);
            _txtMana.SetText(CardData.Mana.ToString());
            _txtHp.SetText(CardData.Hp.ToString());
            _txtAttack.SetText(CardData.Attack.ToString());
        }
        else
        {
            _imgSpell.sprite = Resources.Load<Sprite>(Path.Join(_resourceDir, CardData.CardID));
        }

        _ghostPresentation.SetActive(CardData.CardType == TCardTypeEnum.GHOST);
        _spellPresentation.SetActive(CardData.CardType != TCardTypeEnum.GHOST);
    }
}
