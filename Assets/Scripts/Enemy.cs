using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Photon.MonoBehaviour {

    public float speed;
    public GameObject shotSpawn;
    public float fireRate;
    public GameObject ammo;
    private float nextFire;
    public GameObject explosion;
    private Rigidbody rb;
    public float tilt;
    public float manevrWait;
    private int dir;
    private int newdir;

    float offsetTime = 0;
    bool isSinch = false;
    /*
    private Vector3 pos;
    private Quaternion rot;

    private Vector3 oldPos;
    private Vector3 newPos;

    private Quaternion oldRot;
    private Quaternion newRot;
    */
    // Use this for initialization
    void Start () {
        dir = 0;
        rb = GetComponent<Rigidbody>();
        StartCoroutine(RandomManevr());
        //rb.velocity = transform.forward * speed * 1000 * Time.deltaTime;      
    }

    /*
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        pos = transform.position;
        rot = transform.rotation;
        stream.Serialize(ref pos);
        stream.Serialize(ref rot);
        if (stream.isReading)
        {
            oldPos = transform.position;
            newPos = pos;
            oldRot = transform.rotation;
            newRot = rot;
            offsetTime = 0;
            isSinch = true;
        }
    }*/

        // Update is called once per frame
        void Update () {
        
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(ammo, new Vector3(shotSpawn.transform.position.x, shotSpawn.transform.position.y, shotSpawn.transform.position.z), Quaternion.Euler(0, 0, 0));
        }
        if (photonView.isMine)
        {
            if ((transform.position.x < -200))
            {
                dir = -1;
            }
            if ((transform.position.x > 200))
            {
                dir = 1;
            }
            rb.velocity = new Vector3(dir * 2 * speed * 1000 * Time.deltaTime, 0, speed * 1000 * Time.deltaTime);
            rb.rotation = Quaternion.Euler(0.0f, 180, rb.velocity.x * -tilt);
        }
        else
        {/*
            if (isSinch)
            {

                if (Vector3.Distance(oldPos, newPos) > 50f)
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
            }*/
        }
    }

    IEnumerator RandomManevr()
    {
        while (true)
        {
            yield return new WaitForSeconds(manevrWait);
            dir = Mathf.CeilToInt(Random.Range(-1, 2));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Boundary") || (other.tag == "Enemy") || (other.tag == "FireEnemy"))
        {
            return;
        }
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(other.gameObject);
        if(other.tag == "Fire")
        {
            GameObject.FindWithTag("GameController").GetComponent<GameController>().addScore(100);
        }
        if (other.tag == "Player")
        {
            Instantiate(explosion, other.transform.position, other.transform.rotation);
        }
        Destroy(gameObject);
    }
}
