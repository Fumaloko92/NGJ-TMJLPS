using UnityEngine;
using System.Collections;

public class FishRotator : MonoBehaviour
{
    private Vector2 DesiredDirection;
    private Vector2 CurrentDirection;

    private Transform tf;

    void Start()
    {
        tf = GetComponent<Transform>();
    }

    public void UpdateRotation( Vector2 newDesiredDirection )
    {
        DesiredDirection = newDesiredDirection;
    }

    public void Update()
    {
        CurrentDirection = DesiredDirection;
    }
}
