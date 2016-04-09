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
    [SerializeField]
    private GameObject airParticle;
    private Rigidbody _body;
    private GameObject _airParticle;
	// Use this for initialization
	void Awake () {
        _body = GetComponent<Rigidbody>();

        _airParticle = (GameObject)Instantiate(airParticle, transform.position, Quaternion.Euler(270,90,0));
        _airParticle.SetActive(false);
        _airParticle.transform.parent = this.transform;
	}
	
	// Update is called once per frame
	void Update () {
        float h = Input.GetAxis(horizontalAxisName);
        float v = Input.GetAxis(verticalAxisName);
        Vector3 position = new Vector3(0, v,h);
        if (h == 0 && v == 0)
            _airParticle.SetActive(false);
        else
        {
            Vector3 newPos = transform.position;
            _airParticle.transform.position = newPos;
            float angle = 0;
            angle = Vector2.Angle(-Vector2.up, new Vector2(position.z, position.y));
            if (h > 0)
                angle *= -1;
            Debug.Log(angle);
            _airParticle.transform.rotation = Quaternion.Euler(angle+270, 0, 0);
            _airParticle.SetActive(true);
        }
        
        _body.AddForce(position , ForceMode.Impulse);
       // transform.localScale -=transform.localScale *Time.deltaTime * 0.1f;
	}
}
