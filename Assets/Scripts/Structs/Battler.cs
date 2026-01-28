using Game.Ghost;
using Game.Player;
using UnityEngine;

namespace Game.Structs
{
    public class Battler
    {
        private enum DefenderType
        {
            Ghost,
            Player,
        }

        public string Tag { get; private set; }

        private readonly DefenderType _type;
        private readonly GameGhost _ghost;
        private readonly PlayerAttackTarget _player;

        public Battler(GameGhost ghost)
        {
            _type = DefenderType.Ghost;
            _ghost = ghost;
            _player = null;
            Tag = _ghost.tag;
        }

        public Battler(PlayerAttackTarget player)
        {
            _type = DefenderType.Player;
            _ghost = null;
            _player = player;
            Tag = _player.tag;
        }

        public Battler(GameObject go)
        {
            if (go.TryGetComponent(out GameGhost ghost))
            {
                _type = DefenderType.Ghost;
                _ghost = ghost;
                _player = null;
            }
            else if (go.TryGetComponent(out PlayerAttackTarget player))
            {
                _type = DefenderType.Player;
                _ghost = null;
                _player = player;
            }

            if (_ghost != null)
            {
                Tag = _ghost.tag;
            }
            else if (_player != null)
            {
                Tag = _player.tag;
            }
        }

        private void TakeDamage(Battler attacker)
        {
            int damage = 0;

            switch (attacker._type)
            {
                case DefenderType.Ghost:
                    {
                        damage = attacker._ghost.GhostAttack;
                    }
                    break;
                case DefenderType.Player:
                default:
                    break;
            }

            switch (_type)
            {
                case DefenderType.Ghost:
                    {
                        _ghost.GhostHealth -= damage;
                    }
                    break;
                case DefenderType.Player:
                    {
                        _player.ReducePlayerHealth(damage);
                    }
                    break;
                default:
                    break;
            }
        }

        public void Battle(Battler defender)
        {
            TakeDamage(defender);
            defender.TakeDamage(this);
        }
    }
}
