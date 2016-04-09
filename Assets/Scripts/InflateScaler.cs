using UnityEngine;
using System.Collections;

public class InflateScaler : MonoBehaviour
{
    public Transform ObjectToScale;

    public float MinInflation = 0.1f;
    public float MaxInflation = 1;

    public void SetInflation(float inflation)
    {
        if (ObjectToScale != null && MaxInflation > 0)
        {
            float Scale = MinInflation + Mathf.Clamp(1.0f - MinInflation, 0.0f, 1.0f) * (inflation / MaxInflation);

            ObjectToScale.localScale = new Vector3(Scale, Scale, Scale);
        }
    }   
}
