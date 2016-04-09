using UnityEngine;
using System.Collections;

public class enableDisableText : MonoBehaviour
{
    [SerializeField]
    private GameObject text;
    [SerializeField]
    private float disabledTime;
    [SerializeField]
    private float enabledTime;

    private float _timer, _timer1;

    [SerializeField]
    private string startButton;


    void Awake()
    {
        _timer = _timer1 = 0;
    }

    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > enabledTime)
        {
            text.SetActive(false);
            _timer1 += Time.deltaTime;
            if (_timer1 > disabledTime)
            {
                text.SetActive(true);
                _timer = 0;
                _timer1 = 0;
            }
        }
        if (Input.GetAxis(startButton) == 1)
            Application.LoadLevel(1);


    }
}
