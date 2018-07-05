using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByPVP : MonoBehaviour {

	public GameObject explosion;

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			return;
		}
		Instantiate(explosion, transform.position, transform.rotation);
		Destroy(other.gameObject);

		if(other.tag == "Enemy")
		{
			Instantiate(explosion, other.transform.position, other.transform.rotation);
		}
		Destroy(gameObject);
	}
}
