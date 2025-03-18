using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraNoise : MonoBehaviour
{
    [SerializeField] private CinemachineBasicMultiChannelPerlin _noise;

    public static CameraNoise instance;

    void Awake()
    {
        if (instance)
        {
            Destroy(this);
            return;
        }

        instance = this;
    }
    private void OnDestroy()
    {
        if (instance)
        {
            if (instance.gameObject == gameObject)
            {
                instance = null;
            }
        }
    }

    public void Shake(float duration)
    {
        StartCoroutine(ShakeCoroutine(duration));
    }

    private IEnumerator ShakeCoroutine(float duration)
    {
        _noise.AmplitudeGain = 1;
        yield return new WaitForSeconds(duration);
        _noise.AmplitudeGain = 0;
    }
}
