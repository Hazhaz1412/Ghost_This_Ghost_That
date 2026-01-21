using UnityEngine;

namespace Game.Structs
{
    public class BattleDefender
    {
        private enum DefenderType
        {
            Ghost,
            Player,
        }

        private readonly DefenderType _type;
        private readonly Ghost _ghost;
        private readonly PlayerAttackTarget _player;

        public BattleDefender(Ghost ghost)
        {
            _type = DefenderType.Ghost;
            _ghost = ghost;
            _player = null;
        }

        public BattleDefender(GameObject go)
        {
            if (go.TryGetComponent(out Ghost ghost))
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
        }

        public void TakeDamage(BattleDefender attacker)
        {
            int damage = 0;

            switch (attacker._type)
            {
                case DefenderType.Ghost:
                    {
                        damage = attacker._ghost.GhostAttack;
                    }
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
            }
        }

        public void DealDamage(BattleDefender target)
        {
            switch (target._type)
            {
                case DefenderType.Ghost:
                    break;
                default:
                    return;
            }

            switch (_type)
            {
                case DefenderType.Ghost:
                    {
                        target._ghost.GhostHealth -= _ghost.GhostAttack;
                    }
                    break;
            }
        }
    }
}
