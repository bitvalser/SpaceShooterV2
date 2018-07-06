using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player : Photon.MonoBehaviour {

    private Rigidbody rb;
    public float speed;
    public float tiltx;
    public float tilty;
    public GameObject shotSpawn;
	public GameObject shotSpawn1;
	public GameObject shotSpawn2;
    public GameObject ammo;
    public float fireRate;
	float offsetTime=0;
	bool isSinch=false;
    //public GameObject TouchPad;
    private float nextFire;
    public GameObject Nick;

    private Vector3 pos;
    private Quaternion rot;

    private Vector3 oldPos;
    private Vector3 newPos;

    private Quaternion oldRot;
    private Quaternion newRot;

    //CharacterController controller;
    private void Start()
    {
        //controller = GetComponent<CharacterController> ();
        if (photonView.isMine)
        {
            Nick.GetComponent<TextMesh>().text = PhotonNetwork.playerName;
        }
        else
        {
            for(int i = 0; i < PhotonNetwork.playerList.Length; i++)
            {
                if(PhotonNetwork.playerList[i] != PhotonNetwork.player)
                {
                    Nick.GetComponent<TextMesh>().text = PhotonNetwork.playerList[i].NickName;
                }
            }
        }
        oldPos = Vector3.zero;
        newPos = Vector3.zero;
        oldRot = Quaternion.Euler(Vector3.zero);
        newRot = Quaternion.Euler(Vector3.zero);
        rb = GetComponent<Rigidbody>();
        photonView.RPC("PlayerConnect", PhotonTargets.All, PhotonNetwork.countOfPlayers);
    }

    [PunRPC]
    void PlayerConnect(int P)
    {
        GameObject.FindWithTag("GameController").GetComponent<GameController>().setPlayers(P);
    }

    private void Update()
    {
		Vector3 move = Vector3.zero;
        Nick.transform.eulerAngles = new Vector3(90, 0, 0);
        if (photonView.isMine) {
			move = new Vector3 (Input.GetAxis ("Horizontal") * speed * 1000 * Time.deltaTime, 0, Input.GetAxis ("Vertical") * speed * 1000 * Time.deltaTime);
			rb.rotation = Quaternion.Euler (rb.velocity.z * -tilty, 180, rb.velocity.x * tiltx);            
            if ((transform.position.x < -200)) {
				transform.position = new Vector3 (-200, transform.position.y, transform.position.z);
			}
			if ((transform.position.x > 200)) {
				transform.position = new Vector3 (200, transform.position.y, transform.position.z);
			}
			if ((transform.position.z < -781)) {
				transform.position = new Vector3 (transform.position.x, transform.position.y, -781);
			}
			rb.velocity = move;
			if (Time.time > nextFire && Input.GetKey(KeyCode.Space)) {
				nextFire = Time.time + fireRate;
				PhotonNetwork.Instantiate ("ammo", new Vector3 (shotSpawn.transform.position.x, 36, shotSpawn.transform.position.z), Quaternion.Euler (0, 0, 0),0);
				if (GameObject.FindWithTag ("GameController").GetComponent<GameController> ().multishot ()) {
					PhotonNetwork.Instantiate ("ammo", new Vector3 (shotSpawn1.transform.position.x, 36, shotSpawn.transform.position.z), Quaternion.Euler (0, 0, 0),0);
					PhotonNetwork.Instantiate ("ammo", new Vector3 (shotSpawn2.transform.position.x, 36, shotSpawn.transform.position.z), Quaternion.Euler (0, 0, 0),0);

				}
			}
		} else {

		}

    }


//    private void FixedUpdate()
//    {
//        Vector3 move = Vector3.zero;
//
//        Vector2 direct = TouchPad.GetComponent<TouchPad>().getDirect();
//        move = new Vector3(direct.x * speed * 1000 * Time.deltaTime, 0, direct.y * speed * 1000 * Time.deltaTime);
//
//        move = new Vector3(Input.GetAxis("Horizontal") * speed * 1000 * Time.deltaTime, 0, Input.GetAxis("Vertical") * speed * 1000 * Time.deltaTime);
//        rb.rotation = Quaternion.Euler(rb.velocity.z * -tilty, 180, rb.velocity.x * tiltx);
//        if ((transform.position.x < -200))
//        {
//            transform.position = new Vector3(-200, transform.position.y, transform.position.z);
//        }
//        if ((transform.position.x > 200))
//        {
//            transform.position = new Vector3(200, transform.position.y, transform.position.z);
//        }
//        if ((transform.position.z < -781))
//        {
//            transform.position = new Vector3(transform.position.x, transform.position.y, -781);
//        }
//        rb.velocity = move;
//    }
}
