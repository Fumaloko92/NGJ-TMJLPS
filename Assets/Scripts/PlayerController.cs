using UnityEngine;
using System.Collections;

[RequireComponent(typeof(InflateFish))]
public class PlayerController : MonoBehaviour
{
    public int PlayerId;
    public Animator animator;
    private InflateFish inflation;
    private PlayerHealth health;

    void Start()
    {
        inflation = GetComponent<InflateFish>();
        health = GetComponent<PlayerHealth>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if(health.isDead())
        {
            inflation.SetInput(1);
            animator.SetBool("isdead", true);
        }
        else
        {
            inflation.SetInput(Input.GetAxis("Inflate" + PlayerId));
        }
    }
}
