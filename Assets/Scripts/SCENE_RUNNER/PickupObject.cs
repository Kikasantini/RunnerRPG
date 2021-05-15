using UnityEngine;

public class PickupObject : MonoBehaviour
{

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Item picked up --------------");
    }
}
