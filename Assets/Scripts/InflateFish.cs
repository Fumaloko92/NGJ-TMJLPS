using UnityEngine;
using System.Collections;

public class InflateFish : MonoBehaviour
{
    public float InflationAcceleration = 100;
    public float InflationFriction = 5;
    public float Inflation;
    public float InflationVelocity;

    private InflateScaler Scaler;

    private float inputValue;

    // Use this for initialization
    void Start()
    {
        Scaler = GetComponent<InflateScaler>();
    }

    public void SetInput( float input)
    {
        this.inputValue = input;
    }


    // Update is called once per frame
    void Update()
    {
        float Inflate = -1.0f;

        const float threshold = 0.95f;

        if (inputValue >= threshold)
        {
            Inflate = 1.0f;
        }
        else
        {
            Inflate = -(1.0f - (inputValue / threshold)) * 0.5f;
        }

        Inflation += InflationVelocity * Time.deltaTime;
        InflationVelocity += Inflate * InflationAcceleration * Time.deltaTime;
        InflationVelocity -= InflationVelocity * InflationFriction * Time.deltaTime;

        //UE_LOG( LogFish, Display, TEXT("Val: %f - Inflate: %f - Inflation: %f - InflationVelocity: %f"), Val, Inflate, Inflation, InflationVelocity )

        if (Inflation >= 1.0f)
        {
            Inflation = 1.0f;
            InflationVelocity = Mathf.Min(0, InflationVelocity);
        }
        else if (Inflation <= 0)
        {
            Inflation = 0;
            InflationVelocity = Mathf.Max(0, InflationVelocity);
        }

        if(Scaler != null)
        {
            Scaler.SetInflation(Inflation);
        }

    }
}
