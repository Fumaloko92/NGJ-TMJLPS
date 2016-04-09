using UnityEngine;
using System.Collections;

public class UrchineManager : MonoBehaviour {
    [SerializeField]
    private float scaleChangePercentage;
    [SerializeField]
    private float damage;
    private float _index;
    private float _multiplier;
    private Vector3 _offset;
    private Vector3 _initialScale;
    private Vector3 _initalPosition;
    void Awake()
    {
        _multiplier = 1;
        _index = 0;
        _offset = scaleChangePercentage * transform.localScale / 100;
        _initalPosition = transform.position;
        _initialScale = transform.localScale;
    }
	void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<PlayerHealth>().dealDamage(damage);
            //col.gameObject.GetComponent<InflateFish>().FullyDeflate();
        }
    }

    void Update()
    {
        if (_index > 1)
            _multiplier *= -1;
        else
            if (_index < 0)
            _multiplier *= -1;
        _index += _multiplier * 0.01f;

        Vector3 offset = transform.localScale;
        transform.localScale=Vector3.Lerp(_initialScale - _offset, _initialScale + _offset, _index);
        //transform.position = Vector3.Lerp(_initalPosition, _initalPosition + Vector3.up/8, _index);

    }
}
