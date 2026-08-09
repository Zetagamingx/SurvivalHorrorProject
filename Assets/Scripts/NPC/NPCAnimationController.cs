using UnityEngine;

public class NPCAnimationController : MonoBehaviour
{
    private Animator animator;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Talk()
    {
        animator.SetBool("isTalking", true);
    }

    public void StopTalk()
    {
        animator.SetBool("isTalking", false);
    }
}
