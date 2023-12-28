using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public static class Loader
{
    public enum Scene
    {
        MainMenu,
        Map,
        Game, 
        Loading
    }

    private static Scene targetScene;

    public static void  LoadScene(Scene scene)
    {
        targetScene = scene;

        SceneManager.LoadScene(Scene.Loading.ToString());
    }

    public static void LoadingCallback()
    {
        SceneManager.LoadScene(targetScene.ToString());
    }
}
