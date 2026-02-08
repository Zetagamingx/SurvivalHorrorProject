using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using Unity.VisualScripting;


public class TriggerExplosionController : MonoBehaviour
{
    public ExplosiveWallController ExplosiveWallController;
    public GameObject unbrokenWall;
    public GameObject brokenWall;
    public GameObject explosionPieces;
    public GameObject postExplosionLight;
    public GameObject explosionLight;
    public GameObject holeInTheWallCollider;
    public GameObject particles;
    private Animator animatorExplosion;
    private Animator animatorLightForExplosion;
    public float delayForExplosive;
    [SerializeField] public bool hasToExplode;

    public void Start()
    {
        animatorExplosion = explosionPieces.GetComponent<Animator>();
        animatorLightForExplosion = explosionLight.GetComponent<Animator>();

        if (hasToExplode) StartCoroutine(ExplodeTheWall());
    }
    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && ExplosiveWallController.hasExplosiveCan)
        {

            StartCoroutine(ExplodeTheWall());

        }
    }

    public IEnumerator ExplodeTheWall()
    {
        
        animatorExplosion.SetTrigger("explode");
        
        yield return new WaitForSecondsRealtime(delayForExplosive);

        animatorLightForExplosion.SetTrigger("explode");
        brokenWall.SetActive(true);
        holeInTheWallCollider.SetActive(false);
        unbrokenWall.SetActive(false);
        particles.SetActive(true);

        yield return new WaitForSecondsRealtime(0.1f);
        postExplosionLight.SetActive(true);
    }
}

