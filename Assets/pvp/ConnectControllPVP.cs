using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectControllPVP : Photon.MonoBehaviour {
	private bool isGame;
	void Start()
	{
		isGame = false;
		PhotonNetwork.ConnectUsingSettings("v1");

	}
	void OnJoinedLobby() {
		PhotonNetwork.JoinOrCreateRoom ("VA", new RoomOptions (), TypedLobby.Default);

	}
	void OnJoinedRoom(){
        PhotonNetwork.playerName = GameObject.FindWithTag("Nick").GetComponent<TextLine>().getNick();
        Destroy(GameObject.FindWithTag("Nick"));
        if (PhotonNetwork.countOfPlayersInRooms< 1) {
			Vector3 spawn = new Vector3 (0, 100, -20);
			PhotonNetwork.Instantiate ("fighterPVP1", spawn, Quaternion.Euler (0, 180, 0), 0);
            Debug.Log("fighterPVP1");
		}
		else
        {
			Vector3 spawn = new Vector3 (0, 100, 870);
			PhotonNetwork.Instantiate ("fighterPVP2", spawn, Quaternion.Euler (0, 0, 0), 0);
            Debug.Log("fighterPVP2");
            GameObject.FindWithTag("MainCamera").transform.rotation = Quaternion.Euler(new Vector3(90, 180, 0));
        }
	}
}
