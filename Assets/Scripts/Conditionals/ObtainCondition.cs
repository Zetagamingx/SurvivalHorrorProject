using UnityEngine;

public class ObtainCondition : MonoBehaviour , IPickupCondition
{
    [SerializeField] private string blockedMessage = "I can't take this yet.";

    private bool conditionFulfilled;

    public void Unlock()
    {
        conditionFulfilled = true;
    }

    public bool CanPickup()
    {
        return conditionFulfilled;
    }

    public string GetBlockedMessage()
    {
        return blockedMessage;
    }
}
