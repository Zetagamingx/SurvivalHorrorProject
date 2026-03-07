using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Playables;
using Unity.VisualScripting;


public class LampAnimationController : MonoBehaviour
{
    [SerializeField] private Animator bulbAnimator;
    [SerializeField] private Animator lightAnimator;
    [SerializeField] private PlayableDirector playableDirector;
    [SerializeField] private Light lampLight;
    [SerializeField] private Renderer bulbRender;

    private Material bulbMaterial;

    private static readonly int BulbEmission = Animator.StringToHash("BulbEmission");
    private static readonly int BulbEmission2 = Animator.StringToHash("BulbEmission2");

    private static readonly int LightFlicker = Animator.StringToHash("LightFlicker");
    private static readonly int LightFlicker2 = Animator.StringToHash("LightFlicker2");

    private Coroutine flickerCoroutine;
    void Start()
    {
        bulbMaterial = bulbRender.material;

        flickerCoroutine = StartCoroutine(FlickerAnimation());

        playableDirector.stopped += OnTimelineFinished;
    }

    private void OnDestroy()
    {
        playableDirector.stopped -= OnTimelineFinished;
    }

    private void OnTimelineFinished(PlayableDirector director)
    {        
        TurnLampOff();        
    }

    IEnumerator FlickerAnimation()
    {
        while (gameObject.activeInHierarchy)
        {
            int flickerAnim = Random.Range(0, 2);

            if (flickerAnim == 0)
            {
                bulbAnimator.Play(BulbEmission);
                lightAnimator.Play(LightFlicker);
            }

            else
            {
                bulbAnimator.Play(BulbEmission2);
                lightAnimator.Play(LightFlicker2);
            }

            yield return null;

            yield return new WaitForSeconds(bulbAnimator.GetCurrentAnimatorStateInfo(0).length);
                       
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.5f, 1.5f));
        }
    }

    private void TurnLampOff()
    {
        if (flickerCoroutine != null)
        {
            StopCoroutine(flickerCoroutine);
        }

        bulbAnimator.enabled = false;
        lightAnimator.enabled = false;

        lampLight.intensity = 0f;
        bulbMaterial.SetColor("_EmissionColor", Color.black);
    }
}
