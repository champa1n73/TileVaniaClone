using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] private float levelLoadDelay = 1f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            StartCoroutine(LoadNextScene());
        }
       
    }

    private IEnumerator LoadNextScene()
    {
        yield return new WaitForSecondsRealtime(levelLoadDelay);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneindex = currentSceneIndex + 1;

        if(nextSceneindex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneindex = 0;
        }
        SceneManager.LoadScene(nextSceneindex);
    } 
}
