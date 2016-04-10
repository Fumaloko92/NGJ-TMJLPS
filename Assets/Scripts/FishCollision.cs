using UnityEngine;
using System.Collections;

public class FishCollision : MonoBehaviour
{
    private float oldSpeed;
    private float speed;
    private Rigidbody rb;
    private PlayerHealth ph;
 
    void Start()
    {
        ph = GetComponent<PlayerHealth>();
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        oldSpeed = speed;
        speed = rb.velocity.magnitude;
    }

    void OnCollisionEnter(Collision col)
    {
        var otherCollision = col.gameObject.GetComponent<FishCollision>();
        if (col.gameObject.tag == "Player")
        {
            var otherRigidBody = col.rigidbody;

            if(speed > 5)
            {
                col.gameObject.GetComponent<PlayerHealth>().dealDamage(20);
            }
        }
    }
}
