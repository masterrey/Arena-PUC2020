using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay : MonoBehaviour
{
    public GameObject[] respawns;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("StartGame", 3);
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartGame()
    {
        int indexrespawn = Random.Range(0, respawns.Length);
        if (respawns[indexrespawn].GetComponent<RespawnValidator>().thing == null)
        {
            PhotonNetwork.Instantiate("TankFree", respawns[indexrespawn].transform.position, respawns[indexrespawn].transform.rotation, 0);
        }
        else
        {
            Invoke("StartGame", .1f);
        }

    }
}
