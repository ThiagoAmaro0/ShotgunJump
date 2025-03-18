using System;
using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private CoinSO _coin;
    [SerializeField] private Transform _coinsContent;
    [SerializeField] private float _finishDelay = 2;

    void OnEnable()
    {
        _coin.onMax += FinishLevel;
        _coin.Setup(_coinsContent.childCount);
    }

    void OnDisable()
    {
        _coin.onMax -= FinishLevel;
    }

    private void FinishLevel()
    {
        StartCoroutine(NextLevel());
    }

    private IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(_finishDelay);
        if (!SceneLoader.Scenes.NextScene.Load())
        {
            SceneLoader.Scenes.Menu.Load();
        }
    }
}