using TMPro;
using UnityEngine;

namespace Game.Player
{
    public class PlayerAvatar : MonoBehaviour
    {
        [SerializeField]
        private GamePlayer _player;

        [SerializeField]
        private TMP_Text _txtPlayerHp;

        [SerializeField]
        private TMP_Text _txtPlayerMana;

        private int _uiPlayerHp;
        private int _uiPlayerMana;

        private void Update()
        {
            if (_uiPlayerHp != _player.Hp)
            {
                _uiPlayerHp = _player.Hp;
                _txtPlayerHp.SetText(_uiPlayerHp.ToString());
            }
            if (_uiPlayerMana != _player.Mana)
            {
                _uiPlayerMana = _player.Mana;
                _txtPlayerMana.SetText(_uiPlayerMana.ToString());
            }
        }
    }
}
