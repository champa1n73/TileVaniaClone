using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePersist : MonoBehaviour
{
    public static ScenePersist instance { get; private set; }
    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Hello");
            Destroy(gameObject);
            return;
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
    }

    public void ResetScenePersist()
    {
        Destroy(gameObject);
    }
}
