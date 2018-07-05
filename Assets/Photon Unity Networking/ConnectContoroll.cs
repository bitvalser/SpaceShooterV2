using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectContoroll : Photon.MonoBehaviour {

	// Use this for initialization
	void Start () {
		PhotonNetwork.ConnectUsingSettings ("v1");
	}
	void OnJoinedLobby() {
		PhotonNetwork.JoinOrCreateRoom ("VA", new RoomOptions (), TypedLobby.Default);
	}
	void OnJoinedRoom(){
        GameObject.FindWithTag("GameController").GetComponent<GameController>().addPlayer();
        Vector3 spawn=new Vector3(0,20,-686);
        PhotonNetwork.Instantiate("fighter01",spawn,Quaternion.Euler(0,180,0),0);
    }
}
