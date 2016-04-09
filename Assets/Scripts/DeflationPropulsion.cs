using UnityEngine;
using System.Collections;

[RequireComponent(typeof(InflateFish))]
[RequireComponent(typeof(PlayerController))]
public class DeflationPropulsion : MonoBehaviour
{
    public float ForceAcclerationFactor = 1.0f;

    public Transform modelTransform;

    InflateFish inflator;
    Rigidbody rb;
    Transform tf;
    Quaternion lookForward;
    Quaternion currentLook;
    Vector2 actualDirection;
    PlayerController controller;

    void Start()
    {
        controller = GetComponent<PlayerController>();
        inflator = GetComponent<InflateFish>();
        rb = GetComponent<Rigidbody>();
        tf = GetComponent<Transform>();
        lookForward = Quaternion.LookRotation(Vector3.left, Vector3.up);
    }

    void Update()
    {
        var x = Input.GetAxis("Horizontal" + controller.PlayerId);
        var y = Input.GetAxis("Vertical" + controller.PlayerId);

        if(x == 0 && y == 0)
        {
            currentLook = Quaternion.Slerp(currentLook, lookForward, 6f * Time.deltaTime);
        }
        else
        {
            Quaternion q = Quaternion.LookRotation(new Vector3(0, y, -x), Vector3.up);
            currentLook = Quaternion.Slerp(currentLook, q, 6f * Time.deltaTime);
        }
        actualDirection = new Vector2(x, y).normalized;
        modelTransform.rotation = currentLook;
    }

    void FixedUpdate()
    {
        if(inflator.InflationVelocity < 0)
        {
            rb.AddForce(new Vector3(0, actualDirection.y, -actualDirection.x) * inflator.InflationVelocity * ForceAcclerationFactor, ForceMode.Acceleration);
        }
    }
}
