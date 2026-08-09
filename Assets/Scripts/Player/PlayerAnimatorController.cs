
using UnityEngine;


public class PlayerAnimatorController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    
    public void SetWalkingState(bool isWalking)
    {
        animator.SetBool("isWalking", isWalking);
    }
}
