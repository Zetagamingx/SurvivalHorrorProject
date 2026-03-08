using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class IntroCinematicController : MonoBehaviour
{
    [SerializeField] private PlayableDirector playableDirector;
       
    private AsyncOperation loadOperation;

    private void Start()
    {
        StartCoroutine(PreloadScene());
        playableDirector.Play();
    }

    IEnumerator PreloadScene()
    {
        loadOperation = SceneManagerController.Instance.LoadSceneAsync(GameScene.IntroScene);
        loadOperation.allowSceneActivation = false;

        while (loadOperation.progress < 0.9f)
        {
            yield return null;
        }

        Debug.Log("Intro scene loaded and ready");
    }

    public bool IsSceneReady()
    {
        return loadOperation != null && loadOperation.progress >= 0.9f;
    }

    public void ActivateScene()
    {
        StartCoroutine(ActivateWhenReady());
    }

    IEnumerator ActivateWhenReady()
    {
        while (!IsSceneReady())
            yield return null;

        loadOperation.allowSceneActivation = true;
    }
}
