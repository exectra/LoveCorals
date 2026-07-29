using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnloadScene : MonoBehaviour
{

    public void unloadscene(string scene)
    {
        SceneManager.UnloadSceneAsync(scene);
    }
}
