using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class celebrateWinner : MonoBehaviour {
    [SerializeField]
    private GameObject winnerText;

    private GameObject[] _players;
    private int _nPlayers;
    void Awake()
    {
        _players = GameObject.FindGameObjectsWithTag("Player");
        _nPlayers = _players.Length;
    }

    void Update()
    {
        int deads = 0;
        foreach (GameObject p in _players)
            if (p.GetComponent<PlayerHealth>().isDead())
            {
               //Disable inputs
                deads++;
            }
        if(deads==_nPlayers-1)
        {
        
            winnerText.SetActive(true);
        }
    }
           
}
