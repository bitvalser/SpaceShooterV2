using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Photon.MonoBehaviour {

    private Rigidbody rb;
    public float speed;
    public float tiltx;
    public float tilty;
    public GameObject shotSpawn;
    public GameObject ammo;
    public float fireRate;
    //public GameObject TouchPad;
    private float nextFire;
	//CharacterController controller;
    private void Start()
    {
		//controller = GetComponent<CharacterController> ();
        rb = GetComponent<Rigidbody>();
    }
	void OnPhotonSerializeView(PhotonStream stream,PhotonMessageInfo info){
		Vector3 pos=transform.position;
		Quaternion rot= transform.rotation;
		stream.Serialize(ref pos);
		stream.Serialize(ref rot);
		if(stream.isReading){
			transform.position=pos;
			transform.rotation=rot;
		}
	}
    private void Update()
    {
		Vector3 move = Vector3.zero;
		if(photonView.isMine){
			move = new Vector3(Input.GetAxis("Horizontal") * speed * 1000 * Time.deltaTime, 0, Input.GetAxis("Vertical") * speed * 1000 * Time.deltaTime);
			rb.rotation = Quaternion.Euler(rb.velocity.z * -tilty, 180, rb.velocity.x * tiltx);
			if ((transform.position.x < -200))
			{
				transform.position = new Vector3(-200, transform.position.y, transform.position.z);
			}
			if ((transform.position.x > 200))
			{
				transform.position = new Vector3(200, transform.position.y, transform.position.z);
			}
			if ((transform.position.z < -781))
			{
				transform.position = new Vector3(transform.position.x, transform.position.y, -781);
			}
			rb.velocity = move;
			if(Time.time > nextFire && Input.GetKeyDown(KeyCode.Space))
			{
				nextFire = Time.time + fireRate;
				Instantiate(ammo, new Vector3(shotSpawn.transform.position.x, 36, shotSpawn.transform.position.z) , Quaternion.Euler(0, 0, 0));
			}
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
