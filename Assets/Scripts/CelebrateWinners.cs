using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class CelebrateWinners : MonoBehaviour {
    [SerializeField]
    private GameObject winnerText;

    public AudioSource WinSound;
    public AudioSource WinSound2;

    private bool won;
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
        if(deads>=_nPlayers-1)
        {
            if(Input.GetButton("StartButton"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            if(!won)
            {
                int winner = -1;
                for (int i = 0; i < _players.Length; i++)
                {
                    if(!_players[i].GetComponent<PlayerHealth>().isDead())
                    {
                        winner = i;
                        break;
                    }
                }

                won = true;

                if(winner == 0)
                {
                    if (WinSound != null)
                    {
                        WinSound.Play();
                    }
                } else if ( winner == 1)
                {
                    if (WinSound2 != null)
                    {
                        WinSound2.Play();
                    }
                }
                
            }


            winnerText.SetActive(true);
        }
    }
           
}
