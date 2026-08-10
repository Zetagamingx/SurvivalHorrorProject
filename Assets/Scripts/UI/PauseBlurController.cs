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
        Instance = this;
    }

    public void ActivateBluer()
    {
        if (dof == null) return;

        dof.active = !dof.active;
    }

}
