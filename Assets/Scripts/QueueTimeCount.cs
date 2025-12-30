using UnityEngine;
using TMPro;

public class QueueTimeCount : MonoBehaviour
{
    public TMP_Text timeText;

    private float timeElapsed; 
    private bool isSearching;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isSearching = true;
        timeElapsed = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSearching)
        {
            timeElapsed += Time.deltaTime;
            DisplayTime(timeElapsed);
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void StopTimer()
    {
        isSearching = false;
    }
}
