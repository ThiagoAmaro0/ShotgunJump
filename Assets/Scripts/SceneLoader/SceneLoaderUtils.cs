
using System;
using UnityEngine.SceneManagement;
using static SceneLoader;

public static class SceneLoaderUtils
{
    public static bool Load(this Scenes scene)
    {
        if (scene == Scenes.PreviuousScene)
        {
            return instance.PreviuousScene();

        }
        else if (scene == Scenes.NextScene)
        {
            return instance.NextScene();
        }
        else
        {
            instance.Load(scene);
        }
        return true;
    }
    public static int GetIndex(this Scenes scene)
    {
        return (int)scene;
    }

    public static Scene GetScene(Scenes scene)
    {
        return SceneManager.GetSceneByBuildIndex(scene.GetIndex());
    }

}