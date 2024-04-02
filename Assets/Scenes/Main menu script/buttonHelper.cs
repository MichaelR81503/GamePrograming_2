using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonHelper : MonoBehaviour
{
    public void GoToScene(string sceneName)
    {
        Debug.Log("Pressed the button");
        SceneManager.LoadScene(sceneName);
    }
}
