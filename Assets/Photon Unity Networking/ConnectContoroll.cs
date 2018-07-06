using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectContoroll : Photon.MonoBehaviour {

    private bool isGame;
    // Use this for initialization
    void Start()
    {
        isGame = false;
        PhotonNetwork.ConnectUsingSettings("v1");
        
    }
	void OnJoinedLobby() {
		PhotonNetwork.JoinOrCreateRoom ("VA", new RoomOptions (), TypedLobby.Default);
        
    }
	void OnJoinedRoom(){
        Vector3 spawn=new Vector3(0,20,-686);
        PhotonNetwork.Instantiate("fighter01",spawn,Quaternion.Euler(0,180,0),0);
        PhotonNetwork.playerName = GameObject.FindWithTag("Nick").GetComponent<TextLine>().getNick();
        Destroy(GameObject.FindWithTag("Nick"));

    }

    
}
