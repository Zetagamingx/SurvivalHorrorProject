using Meryuhi.Rendering;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class QualityPresetsController : MonoBehaviour
{
    public static QualityPresetsController instance;

    [SerializeField] private Volume volume;

    private VolumeProfile graphicPresets;
    private FullScreenFog fogSet;
    private Bloom bloom;

    void Awake()
    {
        graphicPresets = volume.profile;

        if (!graphicPresets.TryGet(out fogSet))
        {
            Debug.LogError("Fog not found in the Volume Profile.");
        }

        if (!graphicPresets.TryGet(out bloom))
        {
            Debug.LogError("Bloom not found in the volume Profile");
        }

        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    public void SetPreset(GraphicsPreset preset)
    {
        switch(preset)
        {
            case GraphicsPreset.Low:
                SetGraphicsToLow();
                break;

            case GraphicsPreset.Medium:
                SetGraphicsToMedium();
                break;

            case GraphicsPreset.High:
                SetGraphicsToHigh();
                break;
        }
    }
    
    public void SetGraphicsToLow()
    {
        if (fogSet != null)
        {
            fogSet.active = false;
        }

        if (bloom != null)
        {
            bloom.active = false;
        }

        Debug.Log($"SettingNotFound");
    }
    public void SetGraphicsToMedium()
    {
        if (fogSet != null)
        {
            fogSet.active = true;
        }

        if (bloom != null)
        {
            bloom.active = true;
        }

        Debug.Log($"SettingNotFound");
    }
    public void SetGraphicsToHigh()
    {
        if (fogSet != null)
        {
            fogSet.active = true;
        }

        if (bloom != null)
        {
            bloom.active = true;
        }

        Debug.Log($"SettingNotFound");
    }
}
