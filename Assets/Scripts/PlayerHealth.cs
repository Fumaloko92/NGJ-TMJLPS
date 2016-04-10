using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
    [SerializeField]
    private float startingHealth;

    [SerializeField]
    private Material damagingMaterial;

    private float _blinkTimer = 0.2f;
    private float _timer;
    private float _timer1;

    private Material _startingMat;
    private SkinnedMeshRenderer _rendererRef;
    private bool _blink;
    private float _health;

    void Awake()
    {
        _rendererRef = GetComponentInChildren<SkinnedMeshRenderer>();
        _startingMat = _rendererRef.material;
        _health = startingHealth;
        _timer = 0;
        _timer1 = 0;
    }

    public void dealDamage(float dmg)
    {
        _health -= dmg;
        blinkDamagingMaterial();
    }

    private void blinkDamagingMaterial()
    {
        _blink = true;
    }

    private bool isLowHealth()
    {
        return _health <= startingHealth * 20 / 100&&_health>=0;
    }

    public bool isDead()
    {
        return _health <= 0;
    }

    void Update()
    {
        if (isDead())
        {
            _rendererRef.material = _startingMat;
            return;
        }


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
