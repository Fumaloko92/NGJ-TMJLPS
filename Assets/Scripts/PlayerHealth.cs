using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
    [SerializeField]
    private float startingHealth;

    [SerializeField]
    private Material damagingMaterial;

    private float _blinkTimer = 0.5f;
    private float _timer;
    private float _timer1;

    private Material _startingMat;
    private MeshRenderer _rendererRef;
    private bool _blink;
    private float _health;

    void Awake()
    {
        _rendererRef = GetComponent<MeshRenderer>();
        _startingMat = _rendererRef.material;
        _health = startingHealth;
        _timer = 0;
        _timer1 = 0;
    }

    public void dealDamage(float dmg)
    {
        _health -= dmg;
    }

    private void blinkDamagingMaterial()
    {
        _blink = true;
    }

    private bool isLowHealth()
    {
        return _health <= startingHealth * 5 / 100;
    }

    private bool isDead()
    {
        return _health <= 0;
    }

    void Update()
    {
        if (isDead())
            Debug.Log("I AM DEAD, I WANT MY BELLY UP");
        if (isLowHealth())
        {
            _rendererRef.material = _startingMat;
            _timer1 += Time.deltaTime;
            if(_timer1>_blinkTimer*2)
            {
                _blink = true;
                _timer1 = 0;
            }
        }
        if(_blink)
        {
            _rendererRef.material = damagingMaterial;
            _timer += Time.deltaTime;
            if(_timer > _blinkTimer)
            {
                _rendererRef.material = _startingMat;
                _blink = false;
                _timer = 0;
            }
        }
    }
    
}
