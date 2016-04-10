using UnityEngine;
using System.Collections;

public class startingEffectManager : MonoBehaviour {
    [SerializeField]
    private GameObject[] textForEverySecond;
    
    private GameObject[] _players;
    private float _timer;
    private int _index;
    void Awake()
    {
        _index = 0;
        _timer = 0;
        _players = GameObject.FindGameObjectsWithTag("Player");
        
    }
    void Update()
    {
            if (_timer<=1)
        _timer += Time.deltaTime;
        if (_timer > 1 && _index < textForEverySecond.Length)
        {
            if (_index > 0)
                textForEverySecond[_index - 1].SetActive(false);
            textForEverySecond[_index].SetActive(true);
            _index++;
            _timer = 0;

        }
        else
            if(_timer>1&&_index>=textForEverySecond.Length)
            textForEverySecond[_index - 1].SetActive(false);
        
        if (_index >= textForEverySecond.Length)
        {
            foreach (GameObject p in _players)
                p.GetComponent<InflateFish>().setInflation(true);
            
        }
        else
            foreach (GameObject p in _players)
                p.GetComponent<InflateFish>().setInflation(false);
    }
    
}
