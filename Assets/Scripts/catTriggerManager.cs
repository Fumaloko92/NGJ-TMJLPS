using UnityEngine;
using System.Collections;

public class catTriggerManager : MonoBehaviour {
    public int playersInside = 0;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
            playersInside++;
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
            playersInside--;
    }
}
