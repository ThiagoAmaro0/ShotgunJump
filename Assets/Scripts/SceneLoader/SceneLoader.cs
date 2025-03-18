using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-1)]
public class SceneLoader : MonoBehaviour
{
    private Scenes _currentScene;
    public static SceneLoader instance;
    private int _scenesCount;

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        _scenesCount = Enum.GetNames(typeof(Scenes)).Length - 3;
    }
    public async void Load(Scenes scene)
    {
        if ((int)_currentScene > 0)
        {
            await SceneManager.UnloadSceneAsync(SceneLoaderUtils.GetIndex(_currentScene));
        }
        await SceneManager.LoadSceneAsync(SceneLoaderUtils.GetIndex(scene), LoadSceneMode.Additive);
        SceneManager.SetActiveScene(SceneLoaderUtils.GetScene(scene));
        _currentScene = scene;
    }

    public bool PreviuousScene()
    {
        if ((int)_currentScene > 1)
        {
            Scenes scene = (Scenes)(SceneLoaderUtils.GetIndex(_currentScene) - 1);
            Load(scene);
            return true;
        }
        return false;
    }

    public bool NextScene()
    {
        if ((int)_currentScene < _scenesCount)
        {
            Scenes scene = (Scenes)(SceneLoaderUtils.GetIndex(_currentScene) + 1);
            Load(scene);
            return true;
        }
        return false;
    }

    public enum Scenes
    {
        PreviuousScene = -2,
        NextScene = -1,
        BootLoader = 0,
        Menu = 1,
        Credits = 2,
        Level1 = 3,
        Level2 = 4,
        Level3 = 5,
        FinalLevel = 6
    }
}
