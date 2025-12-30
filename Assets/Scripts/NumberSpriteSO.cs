using UnityEngine;

[CreateAssetMenu(fileName = "NumberSpriteSO", menuName = "Scriptable Objects/NumberSpriteSO")]
public class NumberSpriteSO : ScriptableObject
{
    [SerializeField]
    private Sprite[] _numberSprites;

    public Sprite GetNumberSprite(int n)
    {
        int idx = n;
        if (idx < 0 || idx >= _numberSprites.Length)
        {
            return null;
        }
        return _numberSprites[idx];
    }
}
