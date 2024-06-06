using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // method for changing scenes
    public void SceneChange(string name)
    {
        // load the scene with name.
        SceneManager.LoadScene(name);
        // reseting time scale after scene to avoid bugs with replaying
        Time.timeScale = 1;
    }
}
