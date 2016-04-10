using UnityEngine;
using System.Collections;

public class FishCollision : MonoBehaviour
{
    private float oldSpeed;
    private float speed;
    private Rigidbody rb;
    private PlayerHealth ph;

    public AudioSource source;
    public AudioManager manager;
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();


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
                if(source != null)
                {
                    source.clip = manager.getRandomCatSound();
                    source.Play();
                }
                col.gameObject.GetComponent<PlayerHealth>().dealDamage(20);
            }
        }
    }
}
