using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
    private GameObject[] _players;
	// Use this for initialization
	void Awake () {
        _players = GameObject.FindGameObjectsWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 midPosition=_players[0].transform.position;
        for (int i = 1; i < _players.Length; i++)
            midPosition = Vector3.Lerp(midPosition, _players[i].transform.position, 0.5f);
        midPosition.x = transform.position.x;
        midPosition.z = transform.position.z;
        transform.Translate((midPosition - transform.position) * Time.deltaTime);
	}
}
