using UnityEngine;
using System.Collections;

public class catArmManager : MonoBehaviour {
    [SerializeField]
    private catTriggerManager catTriggerRef;
    [SerializeField]
    private float speed;
    private bool _detected;
    private bool _move;

    void Awake()
    {
        _detected = false;
        _move = false;
    }
    void Update()
    {
        if (catTriggerRef.playersInside > 0)
            _detected = true;
        else
            _detected = false;
        if (_detected && !_move)
            _move = true;
        if (_move)
        {
            transform.Rotate(new Vector3(speed * Time.deltaTime,0,0));
            if (transform.rotation.eulerAngles.x >= 180f)
            {
                transform.rotation = Quaternion.identity;
                _move = false;
            }
        }
    }
}
