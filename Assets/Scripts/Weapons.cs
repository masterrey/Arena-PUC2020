using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public PhotonView pview;
    public GameObject pointoffire;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pview.IsMine)
        {
            if (Input.GetButtonDown("Jump"))
            {
               GameObject ob= (GameObject) 
                    PhotonNetwork.Instantiate("Bullet", pointoffire.transform.position,
                    pointoffire.transform.rotation, 0);
               
               
            }
        }
    }
}
