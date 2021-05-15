using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float forwardForce = 800f;
    public float sidewaysForce = 100f;
    //public Transform player;
    
    void FixedUpdate() // Update is called once per frame
    {
        rb.AddForce(0, 0, forwardForce * Time.deltaTime); // Add a forward force

        if (Input.GetKey("d"))
        {
            rb.AddForce(sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }
        if (Input.GetKey("a"))
        {
            rb.AddForce(-sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }


        //Debug.Log("Player position: " + player.position.x + " " + player.position.y + " " + player.position.z);
        
        if(rb.position.y < -1f)
        {
            FindObjectOfType<GameManager>().EndGame();
        }

    }
}
