using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankID : MonoBehaviour
{
    public TextMesh name;
    public PhotonView pview;
    public int lives = 100;
    // Start is called before the first frame update
    void Start()
    {
        name.text = pview.Owner.NickName;
    }

    // Update is called once per frame
    void Update()
    {
        // name.transform.LookAt(Camera.main.transform);
        name.transform.forward = transform.position - Camera.main.transform.position;

        if (pview.IsMine) {
            if (lives < 0)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }

    public void DamageTaken(Vector3 pos)
    {
        pview.RPC("DamageCall", RpcTarget.Others, pos);
    }

    [PunRPC]
    void DamageCall(Vector3 pos)
    {
        GetComponent<Rigidbody>().AddExplosionForce(100, pos, 20);
        lives--;
        name.text = pview.Owner.NickName+" "+lives.ToString();
    }
}
