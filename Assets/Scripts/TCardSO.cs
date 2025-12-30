using UnityEngine;

[CreateAssetMenu(fileName = "CardSO", menuName = "Scriptable Objects/CardSO")]
public class TCardSO : ScriptableObject
{
    [field: SerializeField]
    public Sprite GhostSprite { get; private set; }

    [field: SerializeField]
    public Sprite NameSprite { get; private set; }

    [field: SerializeField]
    public Sprite DescriptionSprite { get; private set; }

    [field: SerializeField]
    public int Mana { get; private set; }

    [field: SerializeField]
    public int Hp { get; private set; }

    [field: SerializeField]
    public int Attack { get; private set; }
}
