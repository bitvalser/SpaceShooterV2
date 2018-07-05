using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyAmmo : MonoBehaviour {

    private void OnTriggerExit(Collider other)
    {
		if (other.tag != "Boundary" && other.tag != "Player") {
			Destroy (other.gameObject);
		}
    }
}
