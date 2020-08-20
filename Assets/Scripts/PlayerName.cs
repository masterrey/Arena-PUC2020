using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerName : MonoBehaviour
{
    public Text nametext;
    public PhotonView pview;
    // Start is called before the first frame update
    void Start()
    {
        nametext.text = pview.Owner.NickName;

        GameObject content = GameObject.FindGameObjectWithTag("PlayerList");
        transform.parent = content.transform;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
