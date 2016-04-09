using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class BaloonSimple : MonoBehaviour {
    [SerializeField]
    private string horizontalAxisName;
    [SerializeField]
    private string verticalAxisName;
    [SerializeField]
    private string breathAirKeyName;
    [SerializeField]
    private string blowAirKeyName;
    [SerializeField]
    private float force;
    [SerializeField]
    private GameObject airParticle;
    [SerializeField]
    private float secondsForBreathing;
    [SerializeField]
    private Transform mouthTransformRef;
    [SerializeField]
    private float powerMultiplier;
    [SerializeField]
    private float rotationMultiplierSmall;
    [SerializeField]
    private float rotationMultiplierBig;
    [SerializeField]
    private GameObject waterSurface;
    [SerializeField]
    private Transform centerTransformRef;
    private float _waterSurfaceHeight;
    private Rigidbody _body;
    private GameObject _airParticle;
    private Animator _animController;
    private float _storedAir;

	// Use this for initialization
	void Awake () {
        _body = GetComponent<Rigidbody>();

        _airParticle = (GameObject)Instantiate(airParticle, transform.position, Quaternion.Euler(270,90,0));
        _airParticle.SetActive(false);
        _airParticle.transform.parent = this.transform;
        _animController = GetComponent<Animator>();
        _storedAir = 0;
        _waterSurfaceHeight = waterSurface.transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetAxis(breathAirKeyName)==1)
        {
            if(_storedAir<1f)
                _storedAir += Time.deltaTime/ secondsForBreathing;
            if(_animController.GetFloat("ScaleFactor")<1f)
                _animController.SetFloat("ScaleFactor", _animController.GetFloat("ScaleFactor") + Time.deltaTime/ secondsForBreathing);
        }
        if (Input.GetAxis(breathAirKeyName) !=1&&_storedAir > 0)
        {
            if (_storedAir >0)
                _storedAir -= Time.deltaTime / secondsForBreathing;
            if (_animController.GetFloat("ScaleFactor") >0)
                _animController.SetFloat("ScaleFactor", _animController.GetFloat("ScaleFactor") - Time.deltaTime / secondsForBreathing);
        }

        float h = Input.GetAxis(horizontalAxisName);
        float t = h;
       transform.Rotate(new Vector3(t * Time.deltaTime * Mathf.Lerp(rotationMultiplierSmall,rotationMultiplierBig,_storedAir), 0, 0));


    }
    void FixedUpdate()
    {
        _body.AddForce(new Vector3(0, 2 * _waterSurfaceHeight / transform.position.y));
       
        if (Input.GetAxis(breathAirKeyName) != 1 && _storedAir > 0)
        {
            Vector3 position = centerTransformRef.position - mouthTransformRef.position;
            _body.AddForce(position * powerMultiplier, ForceMode.Force);
        }
       
    }

}
