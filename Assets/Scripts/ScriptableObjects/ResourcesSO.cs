using System.IO;
using Game.Enums;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "ResourcesSO", menuName = "Scriptable Objects/ResourcesSO")]
    public class ResourcesSO : ScriptableObject
    {
        [SerializeField]
        private string _cardDataDirectory;

        [SerializeField]
        private string _ghostSpriteDirectory;

        [SerializeField]
        private string _ghostTitleDirectory;

        [SerializeField]
        private string _ghostDescriptionDirectory;

        [SerializeField]
        private string _ghostAnimationDirectory;

        [SerializeField]
        private string _spellSpriteDirectory;

        [SerializeField]
        private string _numberSpriteDirectory;

        public T LoadCardData<T>(CardIndexEnum cardId) where T : Object
        {
            return Resources.Load<T>(Path.Join(_cardDataDirectory, cardId.ToString()));
        }

        public T LoadGhostSprite<T>(CardIndexEnum cardId) where T : Object
        {
            return Resources.Load<T>(Path.Join(_ghostSpriteDirectory, cardId.ToString()));
        }

        public T LoadGhostTitle<T>(CardIndexEnum cardId) where T : Object
        {
            return Resources.Load<T>(Path.Join(_ghostTitleDirectory, cardId.ToString()));
        }

        public T LoadGhostDescription<T>(CardIndexEnum cardId) where T : Object
        {
            return Resources.Load<T>(Path.Join(_ghostDescriptionDirectory, cardId.ToString()));
        }

        public T LoadGhostAnimation<T>(CardIndexEnum cardId) where T : Object
        {
            return Resources.Load<T>(Path.Join(_ghostAnimationDirectory, cardId.ToString()));
        }

        public T LoadSpellSprite<T>(CardIndexEnum cardId) where T : Object
        {
            return Resources.Load<T>(Path.Join(_spellSpriteDirectory, cardId.ToString()));
        }

        public T LoadNumberSprite<T>(int n) where T : Object
        {
            return Resources.Load<T>(Path.Join(_numberSpriteDirectory, n.ToString()));
        }
    }
}
