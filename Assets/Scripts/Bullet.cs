using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody rdb;
    public PhotonView pview;

    // Start is called before the first frame update
    void Start()
    {
        rdb.AddForce(transform.forward * 100, ForceMode.Impulse);
        Invoke("SelfDestroy", 3);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SelfDestroy()
    {
        PhotonNetwork.Destroy(gameObject);
    }
}
