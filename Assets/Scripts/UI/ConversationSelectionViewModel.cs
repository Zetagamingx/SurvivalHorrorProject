using System;
using UnityEngine;

public class ConversationSelectionViewModel : MonoBehaviour
{
    [SerializeField] private SimpleStateMachine stateMachine;

    private ConversationSelectionModel titleSelectionModel;
       
    private void Awake()
    {
        titleSelectionModel = GetComponentInParent<ConversationSelectionModel>();
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
