using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody rdb;
    public PhotonView pview;
    public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        rdb.AddForce(transform.forward * 100, ForceMode.Impulse);
        Invoke("SelfDestroy", 10);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SelfDestroy()
    {
        PhotonNetwork.Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        PhotonNetwork.Destroy(gameObject);
    }
}
