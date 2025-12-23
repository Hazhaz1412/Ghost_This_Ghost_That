using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BlackFadeIntro : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 2f;
    public float TimeAnimation = 1.5f;

    private float timer = 0f;
    private bool isTrigger = false;

    void Update() {
        timer += Time.deltaTime;
        if (timer >= TimeAnimation && !isTrigger) {
            isTrigger = true;
            LoadNextScene();
        }

        if (Input.GetMouseButtonDown(0) && !isTrigger) {
            isTrigger = true;
            LoadNextScene();
        }
    }

    public void LoadNextScene() {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int sceneIndex) {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneIndex);
    }
}
