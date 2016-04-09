using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Rigidbody))]
public class blowFishManager : MonoBehaviour
{
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
    private GameObject mouthRef;
    [SerializeField]
    private float powerMultiplier;
    [SerializeField]
    private float rotationMultiplierSmall;
    [SerializeField]
    private float rotationMultiplierBig;
    [SerializeField]
    private GameObject waterSurface;
    [SerializeField]
    private GameObject centerRef;
    private float _waterSurfaceHeight;
    private Rigidbody _body;
    private GameObject _airParticle;
    private float _storedAir;

    private SkinnedMeshRenderer _skinnedMeshRenderer;
    private float _increaseDecreaseStep=0.01f;
    private float _inflateRatio;
    // Use this for initialization
    void Awake()
    {
        _body = GetComponent<Rigidbody>();
        _skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        _airParticle = (GameObject)Instantiate(airParticle, transform.position, Quaternion.Euler(270, 90, 0));
        _airParticle.SetActive(false);
        _airParticle.transform.parent = this.transform;
        _storedAir = 0;
        _waterSurfaceHeight = waterSurface.transform.position.y;
    }

    void changeScaleByRatio()
    {
        _skinnedMeshRenderer.SetBlendShapeWeight(0,Mathf.Lerp(0, 200, _inflateRatio));
        transform.localScale = Vector3.Lerp(new Vector3(0.5f, 0.5f, 0.5f), new Vector3(1.8f, 1.8f, 1.8f), _inflateRatio);
    }

    // Update is called once per frame
    void Update()
    {




        if (Input.GetAxis(breathAirKeyName) == 1)
        {
            if (_storedAir < 1f)
                _storedAir += Time.deltaTime / secondsForBreathing;
            if(_inflateRatio<1f)
            _inflateRatio += 0.1f;
            changeScaleByRatio();

        }
        else
        {
            if(_inflateRatio>0)
            _inflateRatio -= 0.1f;
            changeScaleByRatio();
        }
        if (Input.GetAxis(breathAirKeyName) != 1 && _storedAir > 0)
        {
            if (_storedAir > 0)
                _storedAir -= Time.deltaTime / secondsForBreathing;
           

        }

        float h = Input.GetAxis(horizontalAxisName);
        float t = h;
        transform.Rotate(new Vector3(t * Time.deltaTime * Mathf.Lerp(rotationMultiplierSmall, rotationMultiplierBig, _storedAir), 0, 0));


    }
    void FixedUpdate()
    {
        _body.AddForce(new Vector3(0, 2 * _waterSurfaceHeight / transform.position.y));

        if (Input.GetAxis(breathAirKeyName) != 1 && _storedAir > 0)
        {
            Vector3 position = centerRef.transform.position - mouthRef.transform.position;
            _body.AddForce(position * powerMultiplier, ForceMode.Force);
        }

    }
  
}
