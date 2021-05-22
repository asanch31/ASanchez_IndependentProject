using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUIScreen : MonoBehaviour
{
    // Start is called before the first frame update
    public void Retry()
    {
        SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    public void MainMenuReturn()
    {
        SceneManager.LoadScene(0);
    }
}
