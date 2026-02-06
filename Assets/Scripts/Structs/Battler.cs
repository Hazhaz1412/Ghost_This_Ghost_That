using DG.Tweening;
using Game.Ghost;
using Game.Player;
using UnityEngine;

namespace Game.Structs
{
    public class Battler
    {
        private enum BattlerType
        {
            Ghost,
            Player,
        }

        public string Tag { get; private set; }

        private readonly BattlerType _type;
        private readonly GameGhost _ghost;
        private readonly PlayerAttackTarget _player;

        private const float AttackAnimationStepDuration = 1;

        public Battler(GameGhost ghost)
        {
            _type = BattlerType.Ghost;
            _ghost = ghost;
            _player = null;
            Tag = _ghost.tag;
        }

        public Battler(PlayerAttackTarget player)
        {
            _type = BattlerType.Player;
            _ghost = null;
            _player = player;
            Tag = _player.tag;
        }

        public Battler(GameObject go)
        {
            if (go.TryGetComponent(out GameGhost ghost))
            {
                _type = BattlerType.Ghost;
                _ghost = ghost;
                _player = null;
            }
            else if (go.TryGetComponent(out PlayerAttackTarget player))
            {
                _type = BattlerType.Player;
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
                case BattlerType.Ghost:
                    {
                        damage = attacker._ghost.GhostAttack;
                    }
                    break;
                case BattlerType.Player:
                default:
                    break;
            }

            switch (_type)
            {
                case BattlerType.Ghost:
                    {
                        _ghost.GhostHealth = Mathf.Max(0, _ghost.GhostHealth - damage);
                    }
                    break;
                case BattlerType.Player:
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
            RectTransform attackerRt = null;
            RectTransform targetRt = null;

            switch (_type)
            {
                case BattlerType.Ghost:
                    {
                        attackerRt = _ghost.GetComponent<RectTransform>();
                    }
                    break;
                case BattlerType.Player:
                default:
                    break;
            }

            switch (defender._type)
            {
                case BattlerType.Ghost:
                    {
                        targetRt = defender._ghost.GetComponent<RectTransform>();
                    }
                    break;
                case BattlerType.Player:
                    {
                        targetRt = defender._player.GetComponent<RectTransform>();
                    }
                    break;
                default:
                    break;
            }

            Vector2 startPos = attackerRt.position;
            Vector2 direction = targetRt.position - attackerRt.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Canvas cv = attackerRt.GetComponent<Canvas>();
            CanvasGroup cvGroup = attackerRt.GetComponent<CanvasGroup>();

            DOTween.Sequence()
                .AppendCallback(() =>
                {
                    if (cv != null)
                    {
                        cv.overrideSorting = true;
                    }
                    if (cvGroup != null)
                    {
                        cvGroup.blocksRaycasts = false;
                    }
                })
                .Append(attackerRt.DOMove(targetRt.position, AttackAnimationStepDuration))
                .OnComplete(() =>
                {
                    TakeDamage(defender);
                    defender.TakeDamage(this);
                    attackerRt.position = startPos;

                    if (cv != null)
                    {
                        cv.overrideSorting = false;
                    }
                    if (cvGroup != null)
                    {
                        cvGroup.blocksRaycasts = true;
                    }
                });
        }
    }
}
