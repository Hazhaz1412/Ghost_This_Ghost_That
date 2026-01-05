public struct TCardData
{
    public TCardTypeEnum CardType { get; private set; }
    public string CardID { get; private set; }
    public string CardName { get; private set; }
    public string CardDescription { get; private set; }
    public int Mana { get; private set; }
    public int Attack { get; private set; }
    public int Hp { get; private set; }

    public TCardData(
        TCardTypeEnum cardType,
        string cardID,
        string cardName,
        string cardDescription,
        int mana,
        int attack,
        int hp
    )
    {
        CardType = cardType;
        CardID = cardID;
        CardName = cardName;
        CardDescription = cardDescription;
        Mana = mana;
        Attack = attack;
        Hp = hp;
    }
}
