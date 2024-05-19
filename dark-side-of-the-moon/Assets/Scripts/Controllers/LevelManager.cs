using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void ChangeScene(int sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
