using UnityEngine;
using System.Collections;

public class startingEffectManager : MonoBehaviour {
    [SerializeField]
    private GameObject[] textForEverySecond;

    private float _timer;
    private int _index;
    void Awake()
    {
        _index = 0;
        _timer = 0;
    }
    void Update()
    {
        _timer += Time.deltaTime;
        if(_timer>1&&_index<textForEverySecond.Length)
        {
            if (_index > 0)
                textForEverySecond[_index - 1].SetActive(false);
            textForEverySecond[_index].SetActive(true);
            _index++;
            _timer = 0;
        }
    }
}
