using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEventFunction : MonoBehaviour
{
    public void LoadShootingScene()
    {
        Loader.Instance.Load(Loader.Scene.ShootingScene);
    }
    public void LoadMenuScene()
    {
        Loader.Instance.Load(Loader.Scene.ShootingScene);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
