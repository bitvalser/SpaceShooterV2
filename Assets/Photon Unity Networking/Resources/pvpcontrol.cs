using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pvpcontrol : Photon.MonoBehaviour {
	private Rigidbody rb;
	public float speed;
	public float tiltx;
	public float tilty;
	public GameObject shotSpawn;
	public GameObject ammo;
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
	public int Mode;
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
			rb.rotation = Quaternion.Euler (rb.velocity.z * -tilty, 0, rb.velocity.x * tiltx);
			if ((transform.position.x < 66)) {
				transform.position = new Vector3 (66, transform.position.y, transform.position.z);
			}
			if ((transform.position.x > 288)) {
				transform.position = new Vector3 (288, transform.position.y, transform.position.z);
			}
			if ((transform.position.z < 1558)) {
				transform.position = new Vector3 (transform.position.x, transform.position.y, 1558);
			}
			if ((transform.position.z > 1994)) {
				transform.position = new Vector3 (transform.position.x, transform.position.y, 1994);
			}
			rb.velocity = move;
			if (Time.time > nextFire && Input.GetKeyDown (KeyCode.Space)) {
				nextFire = Time.time + fireRate;
				if (Mode == 0) {
					PhotonNetwork.Instantiate ("ammoEnemy", new Vector3 (shotSpawn.transform.position.x, shotSpawn.transform.position.y, shotSpawn.transform.position.z), Quaternion.Euler (0, 0, 0), 0);
				}
				if (Mode == 1) {
					PhotonNetwork.Instantiate ("ammoPVP", new Vector3 (shotSpawn.transform.position.x, shotSpawn.transform.position.y, shotSpawn.transform.position.z), Quaternion.Euler (0, 0, 0), 0);
				}
			}
		} else {
			if (isSinch) {

				if (Vector3.Distance(oldPos, newPos) > 9f)
				{
					transform.position = oldPos = newPos;
					transform.rotation = oldRot = newRot;
				}
				else
				{
					offsetTime += Time.deltaTime * 9f;
					transform.position = Vector3.Lerp(oldPos, newPos, offsetTime);
					transform.rotation = Quaternion.Lerp(oldRot, newRot, offsetTime);
				}
			}

		}

	}



}
