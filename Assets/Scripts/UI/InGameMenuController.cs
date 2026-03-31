using System;
using UnityEngine;
using UnityEngine.UI;

public class InGameMenuController : MonoBehaviour
{
    [SerializeField] private GameObject bgImage;
    [SerializeField] private GameObject sectionContainer;
    [SerializeField] private PauseSelectionModel pauseSelectionModel;

    [SerializeField] private string mainSectionName = "MainSection";

    public bool isMenuOpen = false;
    private bool IsOnMainSection =>
        pauseSelectionModel.CurrentSection == mainSectionName;

    public static InGameMenuController Instance;

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void OpenMenu()
    {
        if (!isMenuOpen)
        {
            Time.timeScale = 0f;
            isMenuOpen = true;

            bgImage.SetActive(true);
            sectionContainer.SetActive(true);
        }

    }

    public void CloseMenu()
    {
        if (isMenuOpen)
        {
            Time.timeScale = 1f;
            isMenuOpen = false;

            bgImage.SetActive(false);
            sectionContainer.SetActive(false);
        }
    }

    private void OnEnable()
    {
        pauseSelectionModel.OnPauseSectionChanged += HandleSectionChanged;
    }

    private void OnDisable()
    {
        pauseSelectionModel.OnPauseSectionChanged -= HandleSectionChanged;
    }

    private void HandleSectionChanged()
    {
        if (pauseSelectionModel.CurrentSection == mainSectionName && isMenuOpen)
        {
            CloseMenu();
        }
    }
}
