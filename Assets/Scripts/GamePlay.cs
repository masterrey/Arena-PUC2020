using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay : MonoBehaviour
{
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
        PhotonNetwork.Instantiate("TankFree", transform.position, transform.rotation, 0);

    }
}
