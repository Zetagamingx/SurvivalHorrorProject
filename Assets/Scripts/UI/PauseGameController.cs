using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseGameController : MonoBehaviour
{
    [SerializeField] private GameObject bgImage;
    [SerializeField] private GameObject sectionContainer;
    [SerializeField] private PauseSelectionModel pauseSelectionModel;

    [SerializeField] private string mainSectionName = "MainSection";

    private bool canPauseGame = true;
    private bool IsOnMainSection =>
        pauseSelectionModel.CurrentSection == mainSectionName;

    public static PauseGameController Instance;

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void PauseGame()
    {
        if (canPauseGame)
        {
            Time.timeScale = 0f;
            canPauseGame = false;
            bgImage.SetActive(true);
            sectionContainer.SetActive(true);
        }

        else
        {
            UnPauseGame();
        }
            
    }

    private void UnPauseGame()
    {
        if (!canPauseGame && IsOnMainSection)
        {
            Time.timeScale = 1.0f;
            canPauseGame = true;
            bgImage.SetActive(false);
            sectionContainer.SetActive(false);
        }    
    }
}
