using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public void SceneChanging(string nextSceneName)
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
