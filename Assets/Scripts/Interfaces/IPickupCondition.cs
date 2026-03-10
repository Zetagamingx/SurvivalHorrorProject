using UnityEngine;

public interface IPickupCondition
{
    bool CanPickup();
    string GetBlockedMessage();
}
