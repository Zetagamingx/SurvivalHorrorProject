using System;
using UnityEngine;

public class TitleSelectionViewModel : MonoBehaviour
{
    [SerializeField] private SimpleStateMachine stateMachine;

    private TitleSelectionModel titleSelectionModel;
       
    private void Awake()
    {
        titleSelectionModel = GetComponentInParent<TitleSelectionModel>();
    }

    private void OnEnable()
    {
        titleSelectionModel.OnSectionChanged += OnSectionChanged;
        OnSectionChanged();
    }

    private void OnDisable()
    {
        titleSelectionModel.OnSectionChanged -= OnSectionChanged;
    }

    private void OnSectionChanged()
    {
        stateMachine.SetState(titleSelectionModel.CurrentSection);
    }
}
