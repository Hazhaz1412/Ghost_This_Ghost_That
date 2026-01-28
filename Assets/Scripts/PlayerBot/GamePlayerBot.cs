using System.Collections.Generic;
using Game.Enums;
using Game.Player;
using UnityEngine;

namespace Game.PlayerBot
{
    [RequireComponent(typeof(GamePlayer))]
    public class GamePlayerBot : MonoBehaviour
    {
        public GamePlayer PlayerData { get; private set; }

        [HideInInspector]
        public List<CardIndexEnum> MulliganZone;

        [HideInInspector]
        public bool IsActionFinished;

        private void Awake()
        {
            PlayerData = GetComponent<GamePlayer>();
        }
    }
}
