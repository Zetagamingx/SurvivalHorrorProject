using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;

public class TitleFadeOut : MonoBehaviour
{
    private Animator animator;
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
        animator = GetComponent<Animator>();     
        
        image.enabled = false;
    }
    private void OnEnable()
    {
        Debug.Log("Subscribed to OnBeginGame");
        StartButtonController.OnBeginGame += BeginToFade;
    }

    private void OnDisable()
    {
        StartButtonController.OnBeginGame -= BeginToFade;
    }

    private void BeginToFade()
    {
        StartCoroutine(FadeToGame());
    }

    private IEnumerator FadeToGame()
    {
        image.enabled = true;

        animator.Play("TitleFadeOut");

        yield return null;

        yield return new WaitForSeconds(2f);

        SceneManagerController.Instance.LoadScene(GameScene.IntroCinematic);
                
    }
}
