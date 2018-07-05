using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPVP : Photon.MonoBehaviour {
	public int mode;
	private Rigidbody rb;
	public float speed;
	public float tiltx;
	public float tilty;
	public GameObject shotSpawn;
	public GameObject ammo;
	//public GameObject Ast;
	public float fireRate;
	float offsetTime=0;
	bool isSinch=false;
	//public GameObject TouchPad;
	private float nextFire;

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
		oldPos = Vector3.zero;
		newPos = Vector3.zero;
		oldRot = Quaternion.Euler(Vector3.zero);
		newRot = Quaternion.Euler(Vector3.zero);
		rb = GetComponent<Rigidbody>();
	}
	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
		pos=transform.position;
		rot= transform.rotation;
		stream.Serialize(ref pos);
		stream.Serialize(ref rot);
		if(stream.isReading){
			oldPos = transform.position;
			newPos = pos;
			oldRot = transform.rotation;
			newRot = rot;
			offsetTime = 0;
			isSinch = true;
			//transform.position=pos;
			//transform.rotation=rot;
		}
	}
	private void Update()
	{
		Vector3 move = Vector3.zero;
		if (photonView.isMine) {
			move = new Vector3 (Input.GetAxis ("Horizontal") * speed * 1000 * Time.deltaTime, 0, Input.GetAxis ("Vertical") * speed * 1000 * Time.deltaTime);
			rb.rotation = Quaternion.Euler (rb.velocity.z * -tilty, 180, rb.velocity.x * tiltx);
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
			if (isSinch)
			{

				if (Vector3.Distance(oldPos, newPos) > 50f)
				{
					transform.position = oldPos = newPos;
					transform.rotation = oldRot = newRot;
				}
				else
				{
					offsetTime += Time.deltaTime * 50f;
					transform.position = Vector3.Lerp(oldPos, newPos, offsetTime);
					transform.rotation = Quaternion.Lerp(oldRot, newRot, offsetTime);
				}
			}

		}

	}
}
