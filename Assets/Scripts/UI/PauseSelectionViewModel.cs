using UnityEngine;
using System;
public class PauseSelectionViewModel: MonoBehaviour
{
    [SerializeField] private SimpleStateMachine stateMachine;

    private PauseSelectionModel pauseSelectionModel;

    private void Awake()
    {
        pauseSelectionModel = GetComponentInParent<PauseSelectionModel>();
    }

    private void OnEnable()
    {
        pauseSelectionModel.OnSectionChanged += OnSectionChanged;
        OnSectionChanged();
    }

    private void OnDisable()
    {
        pauseSelectionModel.OnSectionChanged -= OnSectionChanged;
    }

    private void OnSectionChanged()
    {
        stateMachine.SetState(pauseSelectionModel.CurrentSection);
    }
}
