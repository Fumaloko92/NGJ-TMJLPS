using UnityEngine;
using System.Collections;

[RequireComponent(typeof(InflateFish))]
[RequireComponent(typeof(PlayerController))]
public class DeflationPropulsion : MonoBehaviour
{
    public float ForceAcclerationFactor = 1.0f;
    public float ShootFactor = 10.0f;
    public Transform modelTransform;

    private AudioManager _audioManager;
    private AudioSource _audioSource;
    InflateFish inflator;
    Rigidbody rb;
    Transform tf;
    Quaternion lookForward;
    Quaternion currentLook;
    Vector2 actualDirection;
    PlayerController controller;
    public ParticleSystem blowParticles;
    public Animator animator;
    PlayerHealth health;

    void Start()
    {
        controller = GetComponent<PlayerController>();
        inflator = GetComponent<InflateFish>();
        rb = GetComponent<Rigidbody>();
        tf = GetComponent<Transform>();
        lookForward = Quaternion.LookRotation(Vector3.left, Vector3.up);
        health = GetComponent<PlayerHealth>();
        _audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        var x = Input.GetAxis("Horizontal" + controller.PlayerId);
        var y = Input.GetAxis("Vertical" + controller.PlayerId);

        if(health.isDead())
        {
            var deadLook = Quaternion.LookRotation(Vector3.left, Vector3.down);
            currentLook = Quaternion.Slerp(currentLook, deadLook, Time.deltaTime);
        }
        else if (!inflator.CanInflate())
        {
            Quaternion q = Quaternion.LookRotation(-rb.velocity.normalized, Vector3.up);
            currentLook = Quaternion.Slerp(currentLook, q, 6f * Time.deltaTime);
        }
        else if (x == 0 && y == 0)
        {
            currentLook = Quaternion.Slerp(currentLook, lookForward, 6f * Time.deltaTime);
        }
        else
        {
            Quaternion q = Quaternion.LookRotation(new Vector3(0, y, -x), Vector3.up);
            currentLook = Quaternion.Slerp(currentLook, q, 6f * Time.deltaTime);
        }

        var emitter = blowParticles.emission;

        if (!inflator.CanInflate())
        {
            if(!emitter.enabled)
            {
                _audioSource.clip = _audioManager.getRandomBubbleSound();
                _audioSource.Play();
            }

            animator.SetBool("isShot", true);
            emitter.enabled = true;            
        }
        else
        {
            animator.SetBool("isShot", false);
            emitter.enabled = false;
            _audioSource.Stop();
        }

        actualDirection = new Vector2(x, y);
        modelTransform.rotation = currentLook;
        
    }

    void FixedUpdate()
    {
        var normal = actualDirection.normalized;
        if(inflator.Inflation > 0 && inflator.IsFullyReleased() && actualDirection.magnitude > 0.25f)
        {
            rb.AddForce(new Vector3(0, actualDirection.y, -actualDirection.x).normalized * inflator.Inflation * -ShootFactor, ForceMode.Impulse);
            inflator.FullyDeflate();
        }
        else if(inflator.InflationVelocity < 0 && actualDirection.magnitude > 0.25f)
        {
            rb.AddForce(new Vector3(0, actualDirection.y, -actualDirection.x).normalized * -ForceAcclerationFactor, ForceMode.Acceleration);
        }
    }
}
