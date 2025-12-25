using UnityEngine;

/*public class CardData
{
    public string id;
    public string cardName;
    public Sprite cardSprite; 
    public float hp;
    public float dmg;
    public float cost;
    public string description;
    public int countInDeck;
}*/

public class CardDataInfo : MonoBehaviour
{


    public CardData[] cardDatas = new CardData[30];
    public string[] cardIds = new string[30]
    {
        "c001", "c002", "c003", "c004", "c005", "c006", "c007", "c008", "c009", "c010",
        "c011", "c012", "c013", "c014", "c015", "c016", "c017", "c018", "c019", "c020", 
        "s001", "s002", "s003", "s004", "s005", "s006", "s007", "s008", "s009", "s010",
    };

    void Start()
    {
        for (int i = 0; i < 30; i++)
        {
            cardDatas[i] = new CardData();
            cardDatas[i].id = cardIds[i];
            cardDatas[i].cardName = "Card " + (i + 1);
            cardDatas[i].hp = 1 + i;
            cardDatas[i].dmg = 2 + i;
            cardDatas[i].cost = 3 + i;
            cardDatas[i].description = "";
            cardDatas[i].countInDeck = 3;
        }
        cardDatas[0].cardName = "Âm Binh";
        cardDatas[0].hp = 2;
        cardDatas[0].dmg = 2;
        cardDatas[0].cost = 3;
        cardDatas[0].description = "Binh hồn xuất chiếu";
        cardDatas[0].cardType = CardType.Fighter;
        cardDatas[0].skill = "- Khi tôi chết, Hồi sinh tôi khi bắt đầu vòng sau và ban tôi +1/ +1 với mỗi lần tôi đã chết. \n- Tôi không thể thủ.";

        cardDatas[1].cardName = "Ma Càng Sùng";
        cardDatas[1].hp = 5;
        cardDatas[1].dmg = 2;
        cardDatas[1].cost = 5;
        cardDatas[1].description = "Gào xé âm ti";
        cardDatas[1].cardType = CardType.Support;
        cardDatas[1].skill = "- Khi một đồng minh chết, hút 1 máu từ người chơi đối thủ cho bạn";

        cardDatas[2].cardName = "Ma Cơ";
        cardDatas[2].hp = 3;
        cardDatas[2].dmg = 2;
        cardDatas[2].cost = 3;
        cardDatas[2].description = "Rủ rỉ gọi mời";
        cardDatas[2].cardType = CardType.Support;
        cardDatas[2].skill = "- Khi tôi được triệu hồi, tạo trong tay một bản sao của một đồng minh đã chết trận này";
    

        cardDatas[3].cardName = "Ma Da";
        cardDatas[3].hp = 1;
        cardDatas[3].dmg = 3;
        cardDatas[3].cost = 2;
        cardDatas[3].description = "Kéo chân dưới nước";
        cardDatas[3].cardType = CardType.Fighter;
        cardDatas[3].skill = "- Khi tôi được triệu hồi, tạo trong tay một bản sao của một đồng minh đã chết trận này";
        


        cardDatas[4].cardName = "Ma Đói";
        cardDatas[4].hp = 2;
        cardDatas[4].dmg = 3;
        cardDatas[4].cost = 0;
        cardDatas[4].description = "Há miệng chờ ăn";
        cardDatas[4].cardType = CardType.Fighter;
        cardDatas[4].skill = "- Khi triệu hồi tôi, giết một đồng minh";
        


        cardDatas[5].cardName = "Ma Vú Dài";
        cardDatas[5].hp = 3;
        cardDatas[5].dmg = 2;
        cardDatas[5].cost = 2;
        cardDatas[5].description = "Vỗ về từ âm giới";
        cardDatas[5].cardType = CardType.Support;
        cardDatas[5].skill = "- Khi một đồng minh được triệu hồi, ban cho họ +1/ +1";
                

        cardDatas[6].cardName = "Ma Gà";
        cardDatas[6].hp = 1;
        cardDatas[6].dmg = 2;
        cardDatas[6].cost = 1;
        cardDatas[6].description = "Gáy gọi tai ương";
        cardDatas[6].cardType = CardType.Support;
        cardDatas[6].skill = "- Khi triệu hồi tôi, giảm mana một đồng minh trên tay đi 1.";
                

        cardDatas[7].cardName = "Ma Lai";
        cardDatas[7].hp = 5;
        cardDatas[7].dmg = 5;
        cardDatas[7].cost = 3;
        cardDatas[7].description = "Huyết mạch ly thân";
        cardDatas[7].cardType = CardType.Fighter;
        cardDatas[7].skill = "- Tôi sẽ chết sau khi tấn công hoặc kết thúc lượt. \n- hồi cho người chơi lượng sát thương gây ra";

        cardDatas[8].cardName = "Ma Lon";
        cardDatas[8].hp = 1;
        cardDatas[8].dmg = 1;
        cardDatas[8].cost = 1;
        cardDatas[8].description = "Rút ngãi đổi mạng";
        cardDatas[8].cardType = CardType.Fighter;
        cardDatas[8].skill = "- Lần đầu một đồng minh khác chết, ban tôi +2/ +2";

        
        cardDatas[9].cardName = "Ma Mặt Mâm";
        cardDatas[9].hp = 1;
        cardDatas[9].dmg = 3;
        cardDatas[9].cost = 2;
        cardDatas[9].description = "Vỡ mâm đoạt mạng";
        cardDatas[9].cardType = CardType.Support;
        cardDatas[9].skill = "- Khi tôi chết, rút 1 lá và hồi 2 máu cho bạn";


        cardDatas[10].cardName = "Quỷ Một Dò";
        cardDatas[10].hp = 2;
        cardDatas[10].dmg = 3;
        cardDatas[10].cost = 2;
        cardDatas[10].description = "Kinh Hãi";
        cardDatas[10].cardType = CardType.Fighter;
        cardDatas[10].skill = "- Chỉ kẻ địch với 3 ST trở lên có thể thủ";


        cardDatas[11].cardName = "Ma Trành";
        cardDatas[11].hp = 6;
        cardDatas[11].dmg = 8;
        cardDatas[11].cost = 8;
        cardDatas[11].description = "Mắt Trành rối trí";
        cardDatas[11].cardType = CardType.Fighter;
        cardDatas[11].skill = "- Khi triệu hồi tôi, giết 2 kẻ địch yếu nhất nếu một đồng minh đã chết trong vòng này.";


        cardDatas[12].cardName = "Ma Trơi";
        cardDatas[12].hp = 5;
        cardDatas[12].dmg = 5;
        cardDatas[12].cost = 10;
        cardDatas[12].description = "Lửa lạnh dẫn lối";
        cardDatas[12].cardType = CardType.Fighter;
        cardDatas[12].skill = "- Tôi tốn ít hơn 1 mana với mỗi đồng minh đã chết trận này";

        cardDatas[13].cardName = "Linh Miêu";
        cardDatas[13].hp = 4;
        cardDatas[13].dmg = 4;
        cardDatas[13].cost = 7;
        cardDatas[13].description = "Đổi xác hồi hồn";
        cardDatas[13].cardType = CardType.Support;
        cardDatas[13].skill = "- Khi triệu hồi tôi, hồi sinh đồng minh mạnh nhất đã chết trận này";

        cardDatas[14].cardName = "Ông Kẹ";
        cardDatas[14].hp = 1;
        cardDatas[14].dmg = 4;
        cardDatas[14].cost = 4;
        cardDatas[14].description = "Ác mộng gọi tên";
        cardDatas[14].cardType = CardType.Support;
        cardDatas[14].skill = "- Khi triệu hồi tôi, giết một đồng minh để rút 3 lá";

        cardDatas[15].cardName = "Quỷ Cẩu";
        cardDatas[15].hp = 2;
        cardDatas[15].dmg = 3;
        cardDatas[15].cost = 2;
        cardDatas[15].description = "Vẫy đuôi gọn hồn";
        cardDatas[15].cardType = CardType.Support;
        cardDatas[15].skill = "- Ban cho đồng minh tấn công phía bên phải của tôi +2/+0";

        cardDatas[16].cardName = "Quỷ Dạ Xoa";
        cardDatas[16].hp = 6;
        cardDatas[16].dmg = 10;
        cardDatas[16].cost = 9;
        cardDatas[16].description = "Giáng chùy âm phủ";
        cardDatas[16].cardType = CardType.Fighter;
        cardDatas[16].skill = "- Khi triệu hồi tôi, Gây sát thương lên người chơi đối thủ một lượng bằng một nửa máu của họ, làm tròn lên.\n- Khi tôi chết, Đưa tôi về tay";

        cardDatas[17].cardName = "Thần Trùng";
        cardDatas[17].hp = 3;
        cardDatas[17].dmg = 5;
        cardDatas[17].cost = 6;
        cardDatas[17].description = "Bắt hồn truyền kiếp";
        cardDatas[17].cardType = CardType.Support;
        cardDatas[17].skill = "- Khi triệu hồi tôi, giết một đồng minh để giết một kẻ địch";

        cardDatas[18].cardName = "Thiên Linh Cái";
        cardDatas[18].hp = 3;
        cardDatas[18].dmg = 3;
        cardDatas[18].cost = 4;
        cardDatas[18].description = "Luyện Hồn giữ pháp";
        cardDatas[18].cardType = CardType.Support;
        cardDatas[18].skill = "- Khi triệu hồi tôi, giết một đồng minh, rồi hồi sinh họ";

        cardDatas[19].cardName = "Vong Nhi";
        cardDatas[19].hp = 3;
        cardDatas[19].dmg = 0;
        cardDatas[19].cost = 3;
        cardDatas[19].description = "Khóc gào rút máu";
        cardDatas[19].cardType = CardType.Support;
        cardDatas[19].skill = "- Khi một đồng minh chết, gây 1 ST vào máu người chơi đối thủ";
 

        cardDatas[20].cardName = "Hiến Xác Trả Thù";
        cardDatas[20].hp = 0;
        cardDatas[20].dmg = 0;
        cardDatas[20].cost = 7;
        cardDatas[20].description = "Có thể dùng cùng với những bài phép nhanh khác";
        cardDatas[20].cardType = CardType.Spell;
        cardDatas[20].spellType = SpellType.Fast;
        cardDatas[20].skill = "Giết một đồng minh để gây ST của nó lên bất kỳ thứ gì.";

        cardDatas[21].cardName = "Vòng Hộ Mệnh";
        cardDatas[21].hp = 0;
        cardDatas[21].dmg = 0;
        cardDatas[21].cost = 6;
        cardDatas[21].description = "Có thể dùng cùng với những bài phép nhanh khác";
        cardDatas[21].cardType = CardType.Spell;
        cardDatas[21].spellType = SpellType.Fast;
        cardDatas[21].skill = "Giết một đồng minh hoặc một kẻ địch.";

        cardDatas[22].cardName = "Lưỡi Dao Phục Hận";
        cardDatas[22].hp = 0;
        cardDatas[22].dmg = 0;
        cardDatas[22].cost = 3;
        cardDatas[22].description = "Có thể dùng cùng với những bài phép nhanh khác";
        cardDatas[22].cardType = CardType.Spell;
        cardDatas[22].spellType = SpellType.Fast;
        cardDatas[22].skill = "Nếu một đồng minh đã chết vòng này, gây 4 ST lên một đơn vị.";

        cardDatas[23].cardName = "Quan Tài Hiến Tế";
        cardDatas[23].hp = 0;
        cardDatas[23].dmg = 0;
        cardDatas[23].cost = 2;
        cardDatas[23].description = "Có thể dùng cùng với những bài phép nhanh khác";
        cardDatas[23].cardType = CardType.Spell;
        cardDatas[23].spellType = SpellType.Fast;
        cardDatas[23].skill = "Giết một đồng minh để rút 2 lá.";

        cardDatas[24].cardName = "Gõ Chén Hồn Về";
        cardDatas[24].hp = 0;
        cardDatas[24].dmg = 0;
        cardDatas[24].cost = 3;
        cardDatas[24].description = "Có thể dùng cùng với những bài phép nhanh khác";
        cardDatas[24].cardType = CardType.Spell;
        cardDatas[24].spellType = SpellType.Fast;
        cardDatas[24].skill = "Hồi sinh một đồng minh ngẫu nhiên đã chết vòng này.";

        cardDatas[25].cardName = "Kính Hiện Hồn";
        cardDatas[25].hp = 0;
        cardDatas[25].dmg = 0;
        cardDatas[25].cost = 4;
        cardDatas[25].description = "";
        cardDatas[25].cardType = CardType.Spell;
        cardDatas[25].spellType = SpellType.Burst;
        cardDatas[25].skill = "Chọn một đồng minh, tạo một bản sao của họ trong tay.";

        cardDatas[26].cardName = "Lưỡi Gươm Trừ Tà";
        cardDatas[26].hp = 0;
        cardDatas[26].dmg = 0;
        cardDatas[26].cost = 4;
        cardDatas[26].description = "Chỉ có thể dùng 1 lá phép chậm trong 1 lượt. Không thể dùng chung với bài phép nhanh";
        cardDatas[26].cardType = CardType.Spell;
        cardDatas[26].spellType = SpellType.Slow;
        cardDatas[26].skill = "Giết tất cả đơn vị";

        cardDatas[27].cardName = "Tỏi Yểm Bùa";
        cardDatas[27].hp = 0;
        cardDatas[27].dmg = 0;
        cardDatas[27].cost = 4;
        cardDatas[27].description = "Tác dụng ngay lập tức";
        cardDatas[27].cardType = CardType.Spell;
        cardDatas[27].spellType = SpellType.Burst;
        cardDatas[27].skill = "Ngăn một lá phép bất kỳ (trừ lá phép tốc độ tức thì)";

        cardDatas[28].cardName = "3 Nén Nhang";
        cardDatas[28].hp = 0;
        cardDatas[28].dmg = 0;
        cardDatas[28].cost = 4;
        cardDatas[28].description = "Có thể dùng cùng với những  bài phép nhanh khác";
        cardDatas[28].cardType = CardType.Spell;
        cardDatas[28].spellType = SpellType.Fast;
        cardDatas[28].skill = "Gây 3 ST lên kẻ địch được triệu hồi trong vòng này";

        cardDatas[29].cardName = "Mâm Cúng Vong";
        cardDatas[29].hp = 0;
        cardDatas[29].dmg = 0;
        cardDatas[29].cost = 3;
        cardDatas[29].description = "Tác dụng ngay lập tức";
        cardDatas[29].cardType = CardType.Spell;
        cardDatas[29].spellType = SpellType.Burst;
        cardDatas[29].skill = "Ban 1 đồng minh +2/ +2";

    }

    void Update()
    {
    }
}
