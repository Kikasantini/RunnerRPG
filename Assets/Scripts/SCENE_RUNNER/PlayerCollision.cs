using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public PlayerMovement movement;

    public Inventory inventory;

    public IntVariable coins;

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
            Debug.Log("Pegou moedinha, adicionou em gold");
            Destroy(collisionInfo.gameObject);
            coins.Value++;
        }

    }
}
