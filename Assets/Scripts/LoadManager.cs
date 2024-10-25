using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadManager : MonoBehaviour
{
    [SerializeField] Slider loadingSlider;

    void Start()
    {
        StartCoroutine(CooldownLoadScene());
    }


    void Update()
    {
        
    }

    private IEnumerator CooldownLoadScene()
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(1);
        while (!loadOperation.isDone)
        {
            float sceneProgress = Mathf.Clamp01(loadOperation.progress / 0.9f);
            loadingSlider.value = sceneProgress;
            yield return null;

        }
    }
}
