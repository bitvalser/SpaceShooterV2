using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGscroller : MonoBehaviour {

    public float scrollsSpeed;
    public float tileSize;

    private Vector3 startPos;

    // Use this for initialization
    void Start () {
        startPos = transform.position;
        //transform.localScale = new Vector3(Screen.currentResolution.height, transform.localScale.y, Screen.currentResolution.width - 400);
        
	}
	
	// Update is called once per frame
	void Update () {
        float newPos = Mathf.Repeat(Time.time * scrollsSpeed, tileSize);
        transform.position = startPos + Vector3.forward* newPos;
	}
}
