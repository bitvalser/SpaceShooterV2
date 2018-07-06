using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPVP : Photon.MonoBehaviour {
	public int mode;
	//bool isFiring;
	private Rigidbody rb;
	public float speed;
	//Animator m_Animator;
	public float tiltx;
	public float tilty;
	public GameObject shotSpawn;
	public GameObject ammo;

	//public GameObject Ast;
	public float fireRate;
	float offsetTime=0;
	bool isSinch=false;
//	//public GameObject TouchPad;
    private float nextFire;
    private int dir;

    public GameObject Nick;

    private void Start()
	{

        if (photonView.isMine)
        {
            Nick.GetComponent<TextMesh>().text = PhotonNetwork.playerName;
        }
        else
        {
            for (int i = 0; i < PhotonNetwork.playerList.Length; i++)
            {
                if (PhotonNetwork.playerList[i] != PhotonNetwork.player)
                {
                    Nick.GetComponent<TextMesh>().text = PhotonNetwork.playerList[i].NickName;
                }
            }
        }
        rb = GetComponent<Rigidbody>();
        dir = 1 - 2 * mode;

	}

	private void Update()
	{
		Vector3 move = Vector3.zero;
        if (mode == 0) Nick.transform.eulerAngles = new Vector3(90, 0, 0);
        else Nick.transform.eulerAngles = new Vector3(90, 180, 0);
        if (photonView.isMine) {
            
			move = new Vector3 (dir * Input.GetAxis ("Horizontal") * speed * 1000 * Time.deltaTime, 0, dir * Input.GetAxis ("Vertical") * speed * 1000 * Time.deltaTime);
			rb.rotation = Quaternion.Euler (rb.velocity.z * -tilty, 180 - mode * 180, rb.velocity.x * tiltx);
			if ((transform.position.x < -1481)) {
				transform.position = new Vector3 (-1481, transform.position.y, transform.position.z);
			}
			if ((transform.position.x > 1718)) {
				transform.position = new Vector3 (1718, transform.position.y, transform.position.z);
			}
			if ((transform.position.z > 2250)) {
				transform.position = new Vector3 (transform.position.x, transform.position.y, 2250);
			}
			if ((transform.position.z < -3031)) {
				transform.position = new Vector3 (transform.position.x, transform.position.y, -3031);
			}
			rb.velocity = move;
			if (Time.time > nextFire && Input.GetKey(KeyCode.Space)) {
				nextFire = Time.time + fireRate;
				if (mode == 0) {
					PhotonNetwork.Instantiate ("ammoPVP1", new Vector3 (shotSpawn.transform.position.x, shotSpawn.transform.position.y, shotSpawn.transform.position.z), Quaternion.Euler (0, 0, 0), 0);
				} 
				else {
					PhotonNetwork.Instantiate ("ammoPVP2", new Vector3 (shotSpawn.transform.position.x, shotSpawn.transform.position.y, shotSpawn.transform.position.z), Quaternion.Euler (0, 0, 0), 0);
				}
			}
		} else {

		}

	}
}
