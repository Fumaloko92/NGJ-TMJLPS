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
            float Scale = MinInflation + (MaxInflation - MinInflation) * inflation;

            ObjectToScale.localScale = new Vector3(Scale, Scale, Scale);
        }
    }   
}
