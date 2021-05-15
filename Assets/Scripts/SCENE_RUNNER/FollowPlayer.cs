using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    Vector3 targetPos;
    
    void Update() // Update is called once per frame
    {
        targetPos = player.position + offset;
        //transform.position = player.position + offset;
        targetPos.x = 0;
        transform.position = targetPos;

        
    }
}
