using UnityEngine;

public class HospitalSceneInitialPlayerPosition : MonoBehaviour
{
    
    [SerializeField] private Vector3 initialPlayerPosition;
    private static bool hasEnteredThisSceneBefore = false;

    private void Start()
    {
        var player = PersistantPlayer.Instance;
        initialPlayerPosition = transform.position;
        if (!hasEnteredThisSceneBefore)
        {
            player.GetComponent<PlayerDetection>().ClearInteractable();
            player.GetComponent<CapsuleCollider>().enabled = false;
            player.GetComponent<Rigidbody>().MovePosition(initialPlayerPosition);
            player.GetComponent<CapsuleCollider>().enabled |= true;
            hasEnteredThisSceneBefore = true;
            LoadingScreenController.Instance.isLoading = true;
            LoadingScreenController.Instance.RoomTransition();
        }

        Destroy(this);
    }

}
