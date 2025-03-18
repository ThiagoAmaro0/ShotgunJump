using System;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSceneButton : MonoBehaviour
{
    [SerializeField] private SceneLoader.Scenes _scene;
    private Button _button;

    void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(ChangeScene);
    }

    private void ChangeScene()
    {
        _scene.Load();
    }
}
