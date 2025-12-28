using UnityEngine;

public class HubManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public void Quit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; 
        #else
            Application.Quit();
        #endif
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
