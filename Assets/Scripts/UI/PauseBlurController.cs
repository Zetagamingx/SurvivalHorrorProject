using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PauseBlurController : MonoBehaviour
{
    [SerializeField] Volume globalVolume;

    private DepthOfField dof;

    public static PauseBlurController Instance;

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;


        if (!globalVolume.profile.TryGet(out dof))
        {
            Debug.LogError("DepthOfField not found in Volume Profile!");
        }
        
    }

    public void ActivateBluer()
    {
        if (dof == null) return;

        dof.active = !dof.active;
    }

}
