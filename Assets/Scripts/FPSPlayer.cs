using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSPlayer : MonoBehaviour
{
    PhotonView pview;
    float power;
    float turn;
    float turnAxis;
    Rigidbody rdb;
    Animator anim;
    bool run = false;
    Vector3 localVelocity;
    public GameObject aim;
    public int live = 100;
    // Start is called before the first frame update
    void Start()
    {
        rdb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        pview = GetComponent<PhotonView>();
        if (pview.IsMine)
        {
            Camera.main.GetComponent<NetCamera>().SetPlayer(gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (pview.IsMine)
        {
            
            power = Input.GetAxis("Vertical");
            turn = Input.GetAxis("Horizontal");
            turnAxis = Input.GetAxis("Mouse X");
            run = Input.GetButton("Fire3");
            if (Input.GetButtonDown("Fire1"))
            {
                pview.RPC("Shoot", RpcTarget.AllBuffered, null);
            }
        }
        else
        {
            gameObject.tag = "RemotePlayer";
        }

    }

    private void FixedUpdate()
    {

        if (pview.IsMine)
        {
            transform.Rotate(transform.up * turnAxis);
           
            Vector3 localmovement = transform.TransformDirection(new Vector3(turn , 0, power ));

            if (run)
            {
                localmovement = localmovement * 4;
            }
            else
            {
                localmovement = localmovement * 2;
            }

            rdb.velocity = new Vector3(localmovement.x, rdb.velocity.y, localmovement.z);

            localVelocity = transform.InverseTransformDirection(rdb.velocity) / 2;
        }
        else
        {
            localVelocity =Vector3.Lerp(localVelocity,transform.InverseTransformDirection(rdb.velocity) / 2,Time.fixedDeltaTime);
        }

        
        
        anim.SetFloat("VelX", localVelocity.x);
        anim.SetFloat("VelZ", localVelocity.z);

    }
    [PunRPC]
    void Shoot()
    {
        if(Physics.Raycast(aim.transform.position,aim.transform.forward,out RaycastHit hit, 1000))
        {
            if (hit.collider.CompareTag("RemotePlayer"))
            {
                PhotonView remotepview = hit.collider.GetComponent<PhotonView>();
                if (remotepview)
                {
                    remotepview.RPC("Shooted", RpcTarget.AllBuffered,null);
                }
            }
        }
        anim.SetTrigger("Shoot");
    }
    [PunRPC]
    public void Shooted()
    {
        live-=10;
        anim.SetTrigger("Damage");
        if (live < 1)
        {
            anim.SetBool("Died", true);
            this.enabled = false;
        }
    }
}
