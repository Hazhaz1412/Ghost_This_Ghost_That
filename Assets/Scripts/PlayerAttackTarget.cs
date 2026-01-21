using UnityEngine;

namespace Game
{
    public class PlayerAttackTarget : MonoBehaviour
    {
        [SerializeField]
        private Player _player;

        public void ReducePlayerHealth(int damage)
        {
            _player.Hp -= damage;
        }
    }
}
