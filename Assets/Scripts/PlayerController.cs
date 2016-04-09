using UnityEngine;
using System.Collections;

[RequireComponent(typeof(InflateFish))]
public class PlayerController : MonoBehaviour
{
    public int PlayerId;
    private InflateFish inflation;

    void Start()
    {
        inflation = GetComponent<InflateFish>();
    }

    void Update()
    {
        inflation.SetInput(Input.GetAxis("Inflate" + PlayerId));
    }
}
