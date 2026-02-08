using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using TMPro;

[RequireComponent(typeof(PlayableDirector))]
public class TimelineSubtitleBinder : MonoBehaviour
{
    [Header("Track Settings")]
    [Tooltip("Exact name of the subtitle track in Timeline")]
    [SerializeField] private string subtitleTrackName = "Subtitle Track";

    [Header("Subtitle Target")]
    [SerializeField] private TMP_Text subtitleText;

    private PlayableDirector director;

    private void Awake()
    {
        director = GetComponent<PlayableDirector>();

        if (subtitleText == null)
        {
            Debug.LogWarning("Subtitle TMP_Text not assigned. Trying to find one in scene...");
            subtitleText = Object.FindFirstObjectByType<TMP_Text>();
        }
    }

    public void Rebind()
    {
        if (director.playableAsset == null)
        {
            Debug.LogError("PlayableDirector has no Timeline assigned.");
            return;
        }

        foreach (var output in director.playableAsset.outputs)
        {
            if (output.outputTargetType == typeof(TMP_Text))
            {
                director.SetGenericBinding(output.sourceObject, subtitleText);
                Debug.Log("Text Track rebound successfully.");
            }
        }
    }
}