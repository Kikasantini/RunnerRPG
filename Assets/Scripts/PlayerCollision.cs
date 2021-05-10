using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public PlayerMovement movement;
    
    void OnCollisionEnter(Collision collisionInfo)
    {
        //Debug.Log(collisionInfo.collider.name);

        if(collisionInfo.collider.tag == "Obstacle") // Colidiu com um objeto com tag "Obstacle"
        {
            movement.enabled = false;
            FindObjectOfType<GameManager>().EndGame();
        }
    }

    private void OnTriggerEnter(Collider collisionInfo)
    {
        if (collisionInfo.tag == "Collectable") // Colidiu com um objeto com tag "Collectable"
        {
            Debug.Log("aaaaaaa moedinha");
        }
        else
        {
            Debug.Log("não sei pq entrou aqui");
        }
    }
}
