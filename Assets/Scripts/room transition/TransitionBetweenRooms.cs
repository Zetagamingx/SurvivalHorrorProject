using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class TransitionBetweenRooms : MonoBehaviour, IInteract
{
    public GameObject doorOut;
    
    public GameObject roomA;
    public GameObject roomB;
    public string InteractionPrompt => null;

    public void Interact()
    {
        roomB.SetActive(true);

        var player = PersistantPlayer.Instance;
        var rb = player.GetComponent<Rigidbody>();

        //player.GetComponent<PlayerDetection>().ClearInteractable();
        player.GetComponent<CapsuleCollider>().enabled = false;
        rb.MovePosition(doorOut.transform.position);
        player.GetComponent<CapsuleCollider>().enabled = true;

        Debug.Log($"Transition triggered by: {gameObject.name}");
        Debug.Log($"doorOut assigned: {doorOut.name}");
                
        StartCoroutine(DisablePreviousRoomNextFrame());

        LoadingScreenController.Instance.isLoading = true;
        LoadingScreenController.Instance.RoomTransition();
    }

    private IEnumerator DisablePreviousRoomNextFrame()
    {
        yield return new WaitForSecondsRealtime(3f);
        roomA.SetActive(false);
    }
}
