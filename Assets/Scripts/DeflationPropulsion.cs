using UnityEngine;
using System.Collections;

[RequireComponent(typeof(InflateFish))]
public class DeflationPropulsion : MonoBehaviour
{
    public float ForceAcclerationFactor = 1.0f;

    InflateFish inflator;
    Rigidbody rb;
    Transform tf;

    void Start()
    {
        inflator = GetComponent<InflateFish>();
        rb = GetComponent<Rigidbody>();
        tf = GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        if(inflator.InflationVelocity < 0)
        {
            rb.AddForce(tf.forward * inflator.InflationVelocity * ForceAcclerationFactor, ForceMode.Acceleration);
        }
    }
}
