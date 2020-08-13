using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankID : MonoBehaviour
{
    public TextMesh name;
    public PhotonView pview;
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
    }
}
