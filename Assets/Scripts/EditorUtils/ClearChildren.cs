using UnityEngine;

namespace Game.EditorUtils
{
    public class ClearChildren : MonoBehaviour
    {
#if UNITY_EDITOR
        private void Awake()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }
#endif
    }
}
