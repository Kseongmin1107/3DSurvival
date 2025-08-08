using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float jumpForce = 10f; 
    public ForceMode forceMode = ForceMode.Impulse; 

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();

            if (playerRigidbody != null)
            {
                playerRigidbody.AddForce(transform.up * jumpForce, forceMode);
            }
        }
    }
}