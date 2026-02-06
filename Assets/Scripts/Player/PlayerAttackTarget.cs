using UnityEngine;

namespace Game.Player
{
    public class PlayerAttackTarget : MonoBehaviour
    {
        [SerializeField]
        private GamePlayer _player;

        public void ReducePlayerHealth(int damage)
        {
            _player.Hp = Mathf.Max(0, _player.Hp - damage);
        }
    }
}
