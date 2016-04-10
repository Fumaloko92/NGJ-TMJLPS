using UnityEngine;
using System.Collections;

public class InflateFish : MonoBehaviour
{
    public float InflationAcceleration = 100;
    public float InflationFriction = 5;
    public float Inflation;
    public float InflationVelocity;

    public float InflationVelocityThreshold = 2;

    private InflateScaler Scaler;
    private Rigidbody Rigidbody;

    private float inputValue;

    private bool canInflate;

    // Use this for initialization
    void Start()
    {
        Scaler = GetComponent<InflateScaler>();
        Rigidbody = GetComponent<Rigidbody>();
        canInflate = true;
    }

    public void setInflation(bool val)
    {
        canInflate = val;
    }

    public void SetInput( float input)
    {
        this.inputValue = input;
    }

    public bool IsFullyReleased()
    {
        return inputValue <= 0.025f;
    }

    public void FullyDeflate()
    {
        Inflation = 0;
        InflationVelocity = 0;
    }
    
    public bool CanInflate()
    {
        return canInflate&&Rigidbody.velocity.magnitude < InflationVelocityThreshold;
    }

    // Update is called once per frame
    void Update()
    {
        float Inflate = -1.0f;

        const float threshold = 0.95f;

        if (inputValue >= threshold && CanInflate())
        {
            Inflate = 1.0f;
            if(InflationVelocity < 0)
            {
                InflationVelocity = 0;
            }
        }
        else
        {
            Inflate = -0.1f;
            if(InflationVelocity > 0)
            {
                InflationVelocity = 0;
            }
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
