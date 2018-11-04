using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RouteManager : MonoBehaviour {

    public void goToScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void goToMainScreen()
    {
        goToScene("MainScene");
    }

    public void goToSetParametersScreen()
    {
        goToScene("SetParameters");
    }
}
