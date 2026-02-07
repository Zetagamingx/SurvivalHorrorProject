using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class FogController : MonoBehaviour
{
    public Volume fogVolume; // assign in inspector

    public void EnableFog()
    {
        fogVolume.enabled = true;
    }

    public void DisableFog()
    {
        fogVolume.enabled = false;
    }
}