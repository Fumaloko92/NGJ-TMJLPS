using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Rigidbody))]
public class BaloonSimple : MonoBehaviour {
    [SerializeField]
    private string horizontalAxisName;
    [SerializeField]
    private string verticalAxisName;
    [SerializeField]
    private float force;
    private Rigidbody _body;
	// Use this for initialization
	void Awake () {
        _body = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        float h = Input.GetAxis(horizontalAxisName);
        float v = Input.GetAxis(verticalAxisName);
        Vector3 position = new Vector3(h, v);
        _body.AddForce(position , ForceMode.Impulse);
	}
}
