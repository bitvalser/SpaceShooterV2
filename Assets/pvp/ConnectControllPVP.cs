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
		if (PhotonNetwork.countOfPlayersInRooms < 1) {
			Vector3 spawn = new Vector3 (266, 130, -2805);
			PhotonNetwork.Instantiate ("fighterPVP1", spawn, Quaternion.Euler (0, 180, 0), 0);
		}
		if (PhotonNetwork.countOfPlayersInRooms > 1) {
			Vector3 spawn = new Vector3 (193, 130, -409);
			PhotonNetwork.Instantiate ("fighterPVP2", spawn, Quaternion.Euler (0, 180, 0), 0);
		}
	}
}
